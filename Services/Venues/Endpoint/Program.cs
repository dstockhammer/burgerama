using Autofac;
using Burgerama.Common.Logging;
using Burgerama.Messaging.Commands;
using Burgerama.Messaging.Endpoint.Host;
using Burgerama.Messaging.MassTransit;
using Burgerama.Messaging.MassTransit.Commands;
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

            // Logging
            builder.RegisterModule<LoggingModule>();

            // Messaging infrastructure
            builder.RegisterType<CommandDispatcher>().As<ICommandDispatcher>();
            builder.RegisterModule<ServiceBusModule>();
            builder.RegisterModule<EndpointHostModule>();

            return builder.Build();
        }
    }
}
