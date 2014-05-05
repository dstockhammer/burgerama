using System;
using System.Diagnostics.Contracts;
using Autofac;
using NServiceBus;
using NServiceBus.Installation.Environments;

namespace Burgerama.Services.Venues.Api
{
    public static class NServiceBusConfig
    {
        public static void Register(ILifetimeScope container)
        {
            Contract.Requires<ArgumentNullException>(container != null);

            Configure.With()
                //.DefineEndpointName("Burgerama.Service.Venues")
                .AutofacBuilder(container)
                .DefiningEventsAs(t => t.Namespace != null && t.Namespace.StartsWith("Burgerama.Messaging.Events"))
                .InMemorySubscriptionStorage()
                .UseTransport<Msmq>()
                .UnicastBus()
                .CreateBus()
                .Start(() => Configure.Instance.ForInstallationOn<Windows>().Install());
        }
    }
}