using System;
using System.Linq;
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

        public void Send<T>(T message) where T : class, ICommand
        {
            // todo: this should throw a friendly exception if there isn't EXACTLY ONE EndpointNameAttribute
            var endpointName = (EndpointNameAttribute)message.GetType().GetCustomAttributes(true).Single(a => a is EndpointNameAttribute);
            _bus.GetEndpoint(new Uri(endpointName.Name)).Send(message);
        }
    }
}
