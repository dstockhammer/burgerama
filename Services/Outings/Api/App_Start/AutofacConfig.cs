﻿using System;
using System.Diagnostics.Contracts;
using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Burgerama.Services.Outings.Data;
using Burgerama.Services.Outings.Domain.Contracts;

namespace Burgerama.Services.Outings.Api
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
            builder.RegisterType<OutingRepository>().As<IOutingRepository>();

            return builder.Build();
        }
    }
}