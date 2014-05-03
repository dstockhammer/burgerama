using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Burgerama.Messaging.Events
{
    [ContractClass(typeof(EventDispatcherContract))]
    public interface IEventDispatcher
    {
        void Publish<T>(T message)
            where T : class, IEvent;

        void Publish<T>(IEnumerable<T> messages)
            where T : class, IEvent;
    }
}
