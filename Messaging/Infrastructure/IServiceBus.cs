using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Burgerama.Messaging.Infrastructure.Commands;
using Burgerama.Messaging.Infrastructure.Events;

namespace Burgerama.Messaging.Infrastructure
{
    [ContractClass(typeof(ServiceBusContract))]
    public interface IServiceBus : IDisposable
    {
        void Publish<T>(T message) where T : class, IEvent;

        void Publish<T>(IEnumerable<T> messages) where T : class, IEvent;

        void Send<T>(T message) where T : class, ICommand;
    }
}
