using Autofac;
using Autofac.Extras.CommonServiceLocator;
using Burgerama.Common.Logging;
using Burgerama.Messaging.Events;
using Burgerama.Messaging.MassTransit.Autofac;
using Burgerama.Messaging.MassTransit.Events;
using Burgerama.Services.Voting.Data;
using Burgerama.Services.Voting.Domain.Contracts;
using Microsoft.Practices.ServiceLocation;

namespace Burgerama.Services.Voting.DependencyResolution
{
    public static class DependencyRegistrar
    {
        public static void Initialize()
        {
            RegisterDependencies();
        }

        private static void RegisterDependencies()
        {
            var builder = new ContainerBuilder();

            // Repositories
            builder.RegisterType<VenueRepository>().As<IVenueRepository>();

            // Logging
            builder.RegisterModule<LoggingModule>();

            // Messaging infrastructure
            builder.RegisterServiceBus();
            builder.RegisterType<EventDispatcher>().As<IEventDispatcher>();
            
            // Set Autofac as the Service Locator provider.
            var container = builder.Build();
            ServiceLocator.SetLocatorProvider(() => new AutofacServiceLocator(container));
        }
    }
}
