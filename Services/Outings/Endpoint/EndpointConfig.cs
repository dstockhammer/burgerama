using Autofac;
using Burgerama.Services.Outings.Data;
using Burgerama.Services.Outings.Domain.Contracts;
using NServiceBus;

namespace Burgerama.Services.Outings.Endpoint
{
    public class EndpointConfig : IConfigureThisEndpoint, AsA_Client, IWantCustomInitialization
    {
        public void Init()
        {
            var container = InitContainer();

            Configure.With()
                //.DefineEndpointName("Burgerama.Service.Ratings")
                .AutofacBuilder(container)
                .UseTransport<Msmq>()
                .DefiningCommandsAs(t => t.Namespace != null && t.Namespace.StartsWith("Burgerama.Messaging.Commands"))
                .DefiningEventsAs(t => t.Namespace != null && t.Namespace.StartsWith("Burgerama.Messaging.Events"));
        }

        private static IContainer InitContainer()
        {
            var builder = new ContainerBuilder();

            // Repositories
            builder.RegisterType<OutingRepository>().As<IOutingRepository>();

            return builder.Build();
        }
    }
}
