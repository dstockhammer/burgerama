using System;
using Burgerama.Messaging.Commands;
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

        public void Send<T>(string receiver, T message) where T : class, ICommand
        {
            _bus.GetEndpoint(new Uri(receiver)).Send(message);
        }
    }
}
