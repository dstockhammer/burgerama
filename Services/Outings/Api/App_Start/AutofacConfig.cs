using Autofac;
using Autofac.Integration.WebApi;
using Burgerama.Common.Logging;
using Burgerama.Services.Outings.Data.MongoDB;
using Burgerama.Services.Outings.Data.Rest;
using Burgerama.Services.Outings.Domain.Contracts;
using System;
using System.Diagnostics.Contracts;
using System.Reflection;
using System.Web.Http;

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
            builder.RegisterType<VenueRepository>().As<IVenueRepository>();

            // Logging
            builder.RegisterModule<LoggingModule>();

            return builder.Build();
        }
    }
}