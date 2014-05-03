using System.Collections.Generic;
using NServiceBus;
using NServiceBus.Unicast;
using IEvent = Burgerama.Messaging.Events.IEvent;

namespace Burgerama.Services.Venues.Endpoint
{
    public sealed class NServiceBusEventDispatcher : IEventDispatcher
    {
        private readonly IBus _bus;

        public NServiceBusEventDispatcher()
        {
            _bus = new UnicastBus();
        }

        public void Publish<T>(T message)
            where T : class, IEvent
        {
            _bus.Publish(message);
        }

        public void Publish<T>(IEnumerable<T> messages)
            where T : class, IEvent
        {
            foreach (var message in messages)
            {
                _bus.Publish(message);
            }
        }
    }
}
