using MassTransit;

namespace Burgerama.Services.Venues.Endpoint
{
    public sealed class EndpointService
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
