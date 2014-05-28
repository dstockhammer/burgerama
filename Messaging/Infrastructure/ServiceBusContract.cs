using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Burgerama.Messaging.Infrastructure.Commands;
using Burgerama.Messaging.Infrastructure.Events;

namespace Burgerama.Messaging.Infrastructure
{
    [ContractClassFor(typeof(IServiceBus))]
    internal abstract class ServiceBusContract : IServiceBus
    {
        public void Publish<T>(T message) where T : class, IEvent
        {
            Contract.Requires<ArgumentNullException>(message != null);
        }

        public void Publish<T>(IEnumerable<T> messages) where T : class, IEvent
        {
            Contract.Requires<ArgumentNullException>(messages != null);
        }

        public void Send<T>(T message) where T : class, ICommand
        {
            Contract.Requires<ArgumentNullException>(message != null);
        }

        public void Dispose()
        {
        }
    }
}
