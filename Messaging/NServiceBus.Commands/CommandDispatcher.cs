using Burgerama.Messaging.Commands;
using NServiceBus;
using ICommand = Burgerama.Messaging.Commands.ICommand;

namespace Burgerama.Messaging.NServiceBus.Commands
{
    public sealed class CommandDispatcher : ICommandDispatcher
    {
        private readonly IBus _bus;

        public CommandDispatcher(IBus bus)
        {
            _bus = bus;
        }

        public void Send<T>(T message) where T : class, ICommand
        {
            _bus.Send(message);
        }
    }
}
