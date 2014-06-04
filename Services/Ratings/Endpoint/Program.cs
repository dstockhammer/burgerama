using Autofac;
using Burgerama.Common.Logging;
using Burgerama.Messaging.Endpoint.Host.Topshelf;
using Burgerama.Messaging.MassTransit;
using Burgerama.Services.Ratings.Data;
using Burgerama.Services.Ratings.Domain.Contracts;

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

            // Logging
            builder.RegisterModule<LoggingModule>();

            // Messaging infrastructure
            builder.RegisterModule<ServiceBusModule>();
            builder.RegisterType<EndpointService>().As<IEndpointService>();
            builder.RegisterType<EndpointHostFactory>().AsSelf().SingleInstance();

            return builder.Build();
        }
    }
}
