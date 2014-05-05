using System;
using Autofac;
using Burgerama.Messaging.Commands;
using Burgerama.Messaging.Commands.Outings;
using Burgerama.Messaging.NServiceBus.Commands;
using Burgerama.Services.OutingScheduler.Data.Rest;
using Burgerama.Services.OutingScheduler.Domain.Contracts;
using Burgerama.Services.OutingScheduler.Services;
using NServiceBus;
using NServiceBus.Installation.Environments;

namespace Burgerama.Services.OutingScheduler.App
{
    public sealed class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("OutingScheduler run started.");

            var container = GetAutofacContainer();
            ConfigureNServiceBus(container);

            var scheduler = container.Resolve<SchedulingService>();
            var outing = scheduler.ScheduleOuting();
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
        }

        private static IContainer GetAutofacContainer()
        {
            var builder = new ContainerBuilder();

            // Services
            builder.RegisterType<SchedulingService>();

            // Repositories
            builder.RegisterType<OutingRepository>().As<IOutingRepository>();
            builder.RegisterType<VenueRepository>().As<IVenueRepository>();

            // Messaging
            builder.RegisterType<CommandDispatcher>().As<ICommandDispatcher>();

            return builder.Build();
        }

        private static void ConfigureNServiceBus(ILifetimeScope container)
        {
            Configure.With()
                //.DefineEndpointName("Burgerama.Service.OutingScheduler")
                .AutofacBuilder(container)
                .DefiningEventsAs(t => t.Namespace != null && t.Namespace.StartsWith("Burgerama.Messaging.Events"))
                .DefiningCommandsAs(t => t.Namespace != null && t.Namespace.StartsWith("Burgerama.Messaging.Commands"))
                .InMemorySubscriptionStorage()
                .UseTransport<Msmq>()
                .UnicastBus()
                .CreateBus()
                .Start(() => Configure.Instance.ForInstallationOn<Windows>().Install());
        }
    }
}
