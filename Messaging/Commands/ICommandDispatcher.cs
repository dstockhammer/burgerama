using System.Diagnostics.Contracts;

namespace Burgerama.Messaging.Commands
{
    [ContractClass(typeof(CommandDispatcherContract))]
    public interface ICommandDispatcher
    {
        void Send<T>(T message) where T : class, ICommand;
    }
}
