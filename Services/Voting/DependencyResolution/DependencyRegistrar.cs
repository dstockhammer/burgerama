﻿using System.Web;
using Burgerama.Services.Voting.Core.Data;
using Burgerama.Services.Voting.Core.DI;
using Burgerama.Services.Voting.Core.Messaging;
using Burgerama.Services.Voting.Data.Repositories;
using Burgerama.Services.Voting.DependencyResolution;
using Burgerama.Services.Voting.Endpoint;
using Microsoft.Practices.Unity;

[assembly: PreApplicationStartMethod(typeof(DependencyRegistrar), "Initialize")]

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
            container.RegisterType<IEventDispatcher, EventDispatcher>();

            // Set Unity as the Service Locator provider.
            ServiceLocator.SetServiceLocator(() => new UnityServiceLocator(container));
        }
    }
}
