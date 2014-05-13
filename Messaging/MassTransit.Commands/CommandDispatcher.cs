using Burgerama.Messaging.Commands;
using Burgerama.Messaging.Commands.Configuration;
using MassTransit;

namespace Burgerama.Messaging.MassTransit.Commands
{
    public sealed class CommandDispatcher : ICommandDispatcher
    {
        private readonly IServiceBus _bus;

        public CommandDispatcher(IServiceBus bus)
        {
            _bus = bus;
        }

        public void Send<T>(T message) where T : class, ICommand
        {
            _bus.GetEndpoint(message.GetEndpointUri()).Send(message);
        }
    }
}
