using NServiceBus;

namespace Burgerama.Services.Ratings.Endpoint
{
    public sealed class EndpointConfig : IConfigureThisEndpoint, AsA_Publisher, IWantCustomInitialization
    {
        public void Init()
        {
            Configure.With()
                .DefaultBuilder()
                .DefiningEventsAs(t => t.Namespace != null && t.Namespace.StartsWith("Burgerama.Messaging.Events"));
        }
    }
}
