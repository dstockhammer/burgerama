using Autofac;
using Burgerama.Messaging.MassTransit.Endpoint.Autofac;
using Burgerama.Messaging.MassTransit.Endpoint.Topshelf;
using Burgerama.Services.Venues.Data;
using Burgerama.Services.Venues.Domain.Contracts;

namespace Burgerama.Services.Venues.Endpoint
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
