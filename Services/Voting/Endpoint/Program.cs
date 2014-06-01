using Autofac;
using Burgerama.Common.Logging;
using Burgerama.Messaging.Endpoint.Host.Topshelf;
using Burgerama.Messaging.MassTransit;
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
            builder.RegisterType<VenueRepository>().As<IVenueRepository>();

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
