using System.Collections.Generic;
using Burgerama.Messaging.Events;
using MassTransit;

namespace Burgerama.Services.Venues.Api.Messaging
{
    public sealed class EventDispatcher : IEventDispatcher
    {
        private readonly IServiceBus _bus;

        public EventDispatcher(IServiceBus bus)
        {
            _bus = bus;
        }

        public void Publish<T>(T message) where T : class, IEvent
        {
            _bus.Publish(message);
        }

        public void Publish<T>(IEnumerable<T> messages) where T : class, IEvent
        {
            foreach (var message in messages)
            {
                _bus.Publish(message);
            }
        }
    }
}