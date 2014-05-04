﻿using Autofac;
using Autofac.Integration.WebApi;
using Burgerama.Messaging.Events;
using Burgerama.Services.Ratings.Core.Contracts;
using Burgerama.Services.Ratings.Data;
using System;
using System.Diagnostics.Contracts;
using System.Reflection;
using System.Web.Http;
using Burgerama.Services.Ratings.Endpoint;

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
            builder.RegisterType<VenueRepository>().As<IVenueRepository>().InstancePerApiRequest();

            // Messaging
            builder.RegisterType<NServiceBusEventDispatcher>().As<IEventDispatcher>().InstancePerApiRequest();

            return builder.Build();
        }
    }
}