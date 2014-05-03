using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Burgerama.Messaging.Events
{
    [ContractClassFor(typeof(IEventDispatcher))]
    public abstract class EventDispatcherContract : IEventDispatcher
    {
        public void Publish<T>(T message) where T : class, IEvent
        {
            Contract.Requires<ArgumentNullException>(message != null);
        }

        public void Publish<T>(IEnumerable<T> messages) where T : class, IEvent
        {
            Contract.Requires<ArgumentNullException>(messages != null);
        }
    }
}
