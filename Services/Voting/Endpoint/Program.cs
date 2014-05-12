using Autofac;
using Burgerama.Messaging.MassTransit.Autofac;
using Burgerama.Messaging.MassTransit.Endpoint.Topshelf;
using Burgerama.Services.Voting.Data;
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

            // Messaging infrastructure
            builder.RegisterServiceBus();
            builder.RegisterConsumers();
            builder.RegisterType<EndpointService>().As<IEndpointService>();
            builder.RegisterType<EndpointHostFactory>().AsSelf().SingleInstance();

            return builder.Build();
        }
    }
}
