using Autofac;
using Autofac.Integration.WebApi;
using System;
using System.Diagnostics.Contracts;
using System.Reflection;
using System.Web.Http;
using Burgerama.Common.Logging;
using Burgerama.Messaging.Events;
using Burgerama.Messaging.MassTransit;
using Burgerama.Messaging.MassTransit.Events;
using Burgerama.Services.Ratings.Domain;
using Burgerama.Services.Ratings.Domain.Services;
using Burgerama.Shared.Candidates.Data.MongoDB;
using Burgerama.Shared.Candidates.Domain.Contracts;
using Burgerama.Shared.Candidates.Services;
using Burgerama.Shared.Candidates.Services.Contracts;

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

            // Candidates
            builder.RegisterType<ContextRepository>().As<IContextRepository>();
            builder.RegisterType<CandidateRepository>().As<ICandidateRepository>();
            builder.RegisterType<CandidateFactory>().As<ICandidateFactory>();
            builder.RegisterType<CandidateService<Rating>>().As<ICandidateService>();

            // Logging
            builder.RegisterModule<LoggingModule>();

            // Messaging
            builder.RegisterModule<ServiceBusModule>();
            builder.RegisterType<EventDispatcher>().As<IEventDispatcher>();

            return builder.Build();
        }
    }
}
