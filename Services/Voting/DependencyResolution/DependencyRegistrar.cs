using System;
using System.Web;
using System.Web.Caching;
using Burgerama.Services.Voting.Core.DI;
using Burgerama.Services.Voting.Core.Messaging;
using Burgerama.Services.Voting.Data;
using Burgerama.Services.Voting.DependencyResolution;
using Burgerama.Services.Voting.Domain.Contracts;
using Burgerama.Services.Voting.Endpoint;
using Microsoft.Practices.Unity;

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
            var container = new UnityContainer();

            // Repositories
            container.RegisterType<IVenueRepository, VenueRepository>();

            // Messaging
            container.RegisterType<IEventDispatcher, NServiceBusEventDispatcher>();

            // Set Unity as the Service Locator provider.
            ServiceLocator.SetServiceLocator(() => new UnityServiceLocator(container));
        }
    }
}
