using System;
using System.Diagnostics.Contracts;
using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Burgerama.Messages.NServiceBus.Events;
using Burgerama.Messaging.Events;
using Burgerama.Services.Venues.Data;
using Burgerama.Services.Venues.Domain.Contracts;

namespace Burgerama.Services.Venues.Api
{
    public static class AutofacConfig
    {
        public static void Register(HttpConfiguration config)
        {
            Contract.Requires<ArgumentNullException>(config != null);

            var container = BuildContainer();
            NServiceBusConfig.Register(container);
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();

            // Web API controllers
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // Repositories
            builder.RegisterType<VenueRepository>().As<IVenueRepository>().InstancePerApiRequest();

            // Messaging
            builder.RegisterType<EventDispatcher>().As<IEventDispatcher>();

            return builder.Build();
        }
    }
}
