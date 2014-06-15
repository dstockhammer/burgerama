using System;
using Autofac;
using Burgerama.Common.Logging;
using Burgerama.Messaging.Commands;
using Burgerama.Messaging.Commands.Outings;
using Burgerama.Messaging.Commands.Ratings;
using Burgerama.Messaging.MassTransit;
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
        private const string VenueContextKey = "venues";

        public static void Main(string[] args)
        {
            var container = GetAutofacContainer();

            var logger = container.Resolve<ILogger>();
            logger.Information("OutingScheduler run started.");

            // set date to tomorrow 7 pm
            // todo: get the date passed in as parameter or read it from somewhere
            var date = DateTime.Today.AddDays(-1).AddHours(19);

            var scheduler = container.Resolve<ISchedulingService>();
            var outing = scheduler.ScheduleOuting(date);
            if (outing == null)
            {
                logger.Information("OutingScheduler run successful: No outing was scheduled.");
            }
            else
            {
                var commandDispatcher = container.Resolve<ICommandDispatcher>();

                commandDispatcher.Send(new CreateOuting
                {
                    VenueId = outing.Venue.Id,
                    Date = outing.Date
                });

                commandDispatcher.Send(new OpenCandidate
                {
                    ContextKey = VenueContextKey,
                    Reference = outing.Venue.Id,
                    OpeningDate = outing.Date
                });

                // todo: close voting
                //commandDispatcher.Send(new Burgerama.Messaging.Commands.Voting.CloseCandidate
                //{
                //    ContextKey = VenueContextKey,
                //    Reference = outing.Venue.Id,
                //    ClosingDate = DateTime.Now
                //});

                logger.Information("OutingScheduler run successful: Scheduled outing {@Outing}.", new { outing.Venue.Id, outing.Date });
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

            // Repositories
            builder.RegisterType<OutingRepository>().As<IOutingRepository>();
            builder.RegisterType<VenueRepository>().As<IVenueRepository>();

            // Logging
            builder.RegisterModule<LoggingModule>();

            // Messaging infrastructure
            builder.RegisterModule<ServiceBusModule>();
            builder.RegisterType<CommandDispatcher>().As<ICommandDispatcher>();

            return builder.Build();
        }
    }
}
