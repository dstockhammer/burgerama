using System.Collections.Generic;
using Burgerama.Messaging.Events;
using Magnum.Reflection;
using MassTransit;

namespace Burgerama.Messaging.MassTransit.Events
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
                // reflection is needed because otherwise the events are typed as
                // IEvent and thus can't be subscribed
                _bus.FastInvoke(new[] { message.GetType() }, "Publish", message);
            }
        }
    }
}
