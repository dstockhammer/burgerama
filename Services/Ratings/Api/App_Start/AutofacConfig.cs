using Autofac;
using Autofac.Integration.WebApi;
using System;
using System.Diagnostics.Contracts;
using System.Reflection;
using System.Web.Http;
using Burgerama.Common.Logging;
using Burgerama.Messaging.Events;
using Burgerama.Messaging.MassTransit.Autofac;
using Burgerama.Messaging.MassTransit.Events;
using Burgerama.Services.Ratings.Data;
using Burgerama.Services.Ratings.Domain.Contracts;

namespace Burgerama.Services.Ratings.Api
{
    public static class AutofacConfig
    {
        public static void Register(HttpConfiguration config)
        {
            Contract.Requires<ArgumentNullException>(config != null);

            var container = BuildContainer();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();

            // Web API controllers
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // Repositories
            builder.RegisterType<VenueRepository>().As<IVenueRepository>();

            // Logging
            builder.RegisterModule<LoggingModule>();

            // Messaging
            builder.RegisterServiceBus();
            builder.RegisterType<EventDispatcher>().As<IEventDispatcher>();

            return builder.Build();
        }
    }
}
