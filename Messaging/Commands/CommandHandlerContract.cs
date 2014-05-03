using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Burgerama.Messaging.Commands
{
    [ContractClassFor(typeof(ICommandHandler))]
    public abstract class CommandHandlerContract : ICommandHandler
    {
        public void Send<T>(T message) where T : class, ICommand
        {
            Contract.Requires<ArgumentNullException>(message != null);
        }

        public void Send<T>(IEnumerable<T> messages) where T : class, ICommand
        {
            Contract.Requires<ArgumentNullException>(messages != null);
        }
    }
}
