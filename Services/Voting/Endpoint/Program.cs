using Autofac;
using Burgerama.Common.Logging;
using Burgerama.Messaging.Endpoint.Host;
using Burgerama.Messaging.Events;
using Burgerama.Messaging.MassTransit;
using Burgerama.Messaging.MassTransit.Events;
using Burgerama.Services.Voting.Data.MongoDB;
using Burgerama.Services.Voting.Domain.Contracts;

namespace Burgerama.Services.Voting.Endpoint
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
            builder.RegisterType<ContextRepository>().As<IContextRepository>();

            // Logging
            builder.RegisterModule<LoggingModule>();

            // Messaging infrastructure
            builder.RegisterModule<ServiceBusModule>();
            builder.RegisterModule<EndpointHostModule>();
            builder.RegisterType<EventDispatcher>().As<IEventDispatcher>();
            builder.RegisterType<EndpointHostFactory>().AsSelf().SingleInstance();

            return builder.Build();
        }
    }
}
