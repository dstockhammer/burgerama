using Autofac;
using Burgerama.Common.Logging;
using Burgerama.Messaging.Endpoint.Host;
using Burgerama.Messaging.Events;
using Burgerama.Messaging.MassTransit;
using Burgerama.Messaging.MassTransit.Events;
using Burgerama.Services.Ratings.Data;
using Burgerama.Services.Ratings.Domain.Contracts;
using Burgerama.Services.Ratings.Endpoint.Services;

namespace Burgerama.Services.Ratings.Endpoint
{
    public sealed class Program
    {
        static void Main(string[] args)
        {
            var container = GetAutofacContainer();

            var hostFactory = container.Resolve<EndpointHostFactory>();
            hostFactory.CreateNew().Run();
        }

        private static IContainer GetAutofacContainer()
        {
            var builder = new ContainerBuilder();

            // Repositories
            builder.RegisterType<CandidateRepository>().As<ICandidateRepository>();

            // Services
            builder.RegisterType<CandidateService>().AsSelf();

            // Logging
            builder.RegisterModule<LoggingModule>();

            // Messaging infrastructure
            builder.RegisterModule<ServiceBusModule>();
            builder.RegisterModule<EndpointHostModule>();
            builder.RegisterType<EventDispatcher>().As<IEventDispatcher>();

            return builder.Build();
        }
    }
}
