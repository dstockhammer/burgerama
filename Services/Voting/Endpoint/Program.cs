using Autofac;
using Burgerama.Common.Logging;
using Burgerama.Messaging.Endpoint.Host;
using Burgerama.Messaging.Events;
using Burgerama.Messaging.MassTransit;
using Burgerama.Messaging.MassTransit.Events;
using Burgerama.Services.Voting.Domain;
using Burgerama.Services.Voting.Domain.Services;
using Burgerama.Shared.Candidates.Data.MongoDB;
using Burgerama.Shared.Candidates.Domain.Contracts;
using Burgerama.Shared.Candidates.Services;
using Burgerama.Shared.Candidates.Services.Contracts;

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

            // Candidates
            builder.RegisterType<ContextRepository>().As<IContextRepository>();
            builder.RegisterType<CandidateRepository>().As<ICandidateRepository>();
            builder.RegisterType<CandidateFactory>().As<ICandidateFactory>();
            builder.RegisterType<ContextService>().As<IContextService>();
            builder.RegisterType<CandidateService<Vote>>().As<ICandidateService>();

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
