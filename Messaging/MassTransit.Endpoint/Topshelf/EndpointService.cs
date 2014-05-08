using MassTransit;

namespace Burgerama.Messaging.MassTransit.Endpoint.Topshelf
{
    public sealed class EndpointService : IEndpointService
    {
        private readonly IServiceBus _bus;

        public EndpointService(IServiceBus bus)
        {
            _bus = bus;
        }

        public void Start()
        {
            // nothing to do
        }

        public void Stop()
        {
            _bus.Dispose();
        }
    }
}
