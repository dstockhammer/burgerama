using System;
using Autofac;
using Burgerama.Common.Logging;
using Burgerama.Messaging.Commands;
using Burgerama.Messaging.Commands.Outings;
using Burgerama.Messaging.MassTransit.Autofac;
using Burgerama.Messaging.MassTransit.Commands;
using Burgerama.Services.OutingScheduler.Data.Rest;
using Burgerama.Services.OutingScheduler.Domain.Contracts;
using Burgerama.Services.OutingScheduler.Services;
using MassTransit;
using Serilog;

namespace Burgerama.Services.OutingScheduler.App
{
    public sealed class Program
    {
        public static void Main(string[] args)
        {
            var container = GetAutofacContainer();

            var logger = container.Resolve<ILogger>();
            logger.Information("OutingScheduler run started.");

            // set date to tomorrow 7 pm
            // todo: get the date passed in as parameter or read it from somewhere
            var date = DateTime.Today.AddDays(1).AddHours(19);

            var scheduler = container.Resolve<ISchedulingService>();
            var outing = scheduler.ScheduleOuting(date);
            if (outing == null)
            {
                logger.Information("OutingScheduler run successful: No outing was scheduled.");
            }
            else
            {
                var command = new CreateOuting
                {
                    VenueId = outing.Venue.Id,
                    Date = outing.Date
                };

                var commandDispatcher = container.Resolve<ICommandDispatcher>();
                commandDispatcher.Send(command);
                logger.Information("OutingScheduler run successful: Scheduled outing {@Outing}.", new { command.VenueId, command.Date });
            }

            // disposing the bus is very important in order to unsubscribe and stop consuming.
            // todo: how to implement this in a way that the dev is not forced dispose manually?
            var bus = container.Resolve<IServiceBus>();
            bus.Dispose();
        }

        private static IContainer GetAutofacContainer()
        {
            var builder = new ContainerBuilder();

            // Services
            builder.RegisterType<SchedulingService>().As<ISchedulingService>();

            // Logging
            builder.RegisterModule<LoggingModule>();

            // Repositories
            builder.RegisterType<OutingRepository>().As<IOutingRepository>();
            builder.RegisterType<VenueRepository>().As<IVenueRepository>();

            // Messaging infrastructure
            builder.RegisterServiceBus();
            builder.RegisterType<CommandDispatcher>().As<ICommandDispatcher>();

            return builder.Build();
        }
    }
}
