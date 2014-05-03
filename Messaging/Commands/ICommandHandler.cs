using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Burgerama.Messaging.Commands
{
    [ContractClass(typeof(CommandHandlerContract))]
    public interface ICommandHandler
    {
        void Send<T>(T message)
            where T : class, ICommand;

        void Send<T>(IEnumerable<T> messages)
            where T : class, ICommand;
    }
}
