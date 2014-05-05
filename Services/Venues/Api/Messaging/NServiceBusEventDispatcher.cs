using System.Collections.Generic;
using Burgerama.Messaging.Events;
using NServiceBus;
using IEvent = Burgerama.Messaging.Events.IEvent;

namespace Burgerama.Services.Venues.Api.Messaging
{
    /// <summary>
    /// todo: move this into a Burgerama.Common package.
    /// </summary>
    public sealed class NServiceBusEventDispatcher : IEventDispatcher
    {
        private readonly IBus _bus;

        public NServiceBusEventDispatcher(IBus bus)
        {
            _bus = bus;
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
