using Autofac;
using Burgerama.Services.Ratings.Data;
using Burgerama.Services.Ratings.Domain.Contracts;
using NServiceBus;

namespace Burgerama.Services.Ratings.Endpoint
{
    public sealed class EndpointConfig : IConfigureThisEndpoint, AsA_Client, IWantCustomInitialization
    {
        public void Init()
        {
            var container = InitContainer();

            Configure.With()
                //.DefineEndpointName("Burgerama.Service.Ratings")
                .AutofacBuilder(container)
                .UseTransport<Msmq>()
                .DefiningEventsAs(t => t.Namespace != null && t.Namespace.StartsWith("Burgerama.Messaging.Events"));
        }

        private static IContainer InitContainer()
        {
            var builder = new ContainerBuilder();

            // Repositories
            builder.RegisterType<VenueRepository>().As<IVenueRepository>();

            return builder.Build();
        }
    }
}
