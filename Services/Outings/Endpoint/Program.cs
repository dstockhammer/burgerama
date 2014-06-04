using Autofac;
using Burgerama.Common.Logging;
using Burgerama.Messaging.Endpoint.Host;
using Burgerama.Messaging.MassTransit;
using Burgerama.Services.Outings.Data.MongoDB;
using Burgerama.Services.Outings.Domain.Contracts;

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

            // Logging
            builder.RegisterModule<LoggingModule>();

            // Messaging infrastructure
            builder.RegisterModule<ServiceBusModule>();
            builder.RegisterModule<EndpointHostModule>();

            return builder.Build();
        }
    }
}
