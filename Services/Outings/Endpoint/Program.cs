using Autofac;
using Burgerama.Messaging.MassTransit.Endpoint.Autofac;
using Burgerama.Messaging.MassTransit.Endpoint.Topshelf;
using Burgerama.Services.Outings.Data;
using Burgerama.Services.Outings.Domain.Contracts;
using Burgerama.Services.Outings.Endpoint.Handlers;

namespace Burgerama.Services.Outings.Endpoint
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
            builder.RegisterType<OutingRepository>().As<IOutingRepository>();

            // Messaging infrastructure
            builder.RegisterServiceBus();
            builder.RegisterType<EndpointService>().As<IEndpointService>();
            builder.RegisterType<EndpointHostFactory>().AsSelf().SingleInstance();
            
            // Command handlers
            builder.RegisterType<CreateOutingHandler>().AsSelf();

            return builder.Build();
        }
    }
}
