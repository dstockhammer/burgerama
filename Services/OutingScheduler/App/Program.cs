using System;
using Autofac;
using Burgerama.Messaging.Commands;
using Burgerama.Messaging.Commands.Outings;
using Burgerama.Messaging.MassTransit.Autofac;
using Burgerama.Messaging.MassTransit.Commands;
using Burgerama.Services.OutingScheduler.Data.Rest;
using Burgerama.Services.OutingScheduler.Domain.Contracts;
using Burgerama.Services.OutingScheduler.Services;
using MassTransit;

namespace Burgerama.Services.OutingScheduler.App
{
    public sealed class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("OutingScheduler run started.");

            // set date to tomorrow 7 pm
            // todo: get the date passed in as parameter or read it from somewhere
            var date = DateTime.Today.AddDays(1).AddHours(19);

            var container = GetAutofacContainer();
            var scheduler = container.Resolve<ISchedulingService>();
            var outing = scheduler.ScheduleOuting(date);
            if (outing == null)
            {
                Console.WriteLine("OutingScheduler run successful: No outing was scheduled.");
            }
            else
            {
                var commandDispatcher = container.Resolve<ICommandDispatcher>();
                commandDispatcher.Send(new CreateOuting
                {
                    VenueId = outing.Venue.Id,
                    Date = outing.Date
                });

                Console.WriteLine("OutingScheduler run successful: Outing '{0}' scheduled for '{1}' on '{2}'.", outing.Id, outing.Venue.Title, outing.Date);
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

            // Messaging infrastructure
            builder.RegisterServiceBus();
            builder.RegisterType<CommandDispatcher>().As<ICommandDispatcher>();

            return builder.Build();
        }
    }
}
