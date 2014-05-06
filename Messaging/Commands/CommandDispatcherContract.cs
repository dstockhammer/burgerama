using System;
using System.Diagnostics.Contracts;

namespace Burgerama.Messaging.Commands
{
    [ContractClassFor(typeof(ICommandDispatcher))]
    public abstract class CommandDispatcherContract : ICommandDispatcher
    {
        public void Send<T>(string receiver, T message) where T : class, ICommand
        {
            Contract.Requires<ArgumentNullException>(receiver != null);
            Contract.Requires<ArgumentNullException>(message != null);
        }
    }
}
