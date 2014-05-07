using System.Reflection;
using Autofac;
using Topshelf;

namespace Burgerama.Messaging.MassTransit.Endpoint.Topshelf
{
    public sealed class EndpointHostFactory
    {
        private readonly IComponentContext _container;

        public EndpointHostFactory(IComponentContext container)
        {
            _container = container;
        }

        public Host CreateNew()
        {
            return HostFactory.New(cfg =>
            {
                var name = Assembly.GetEntryAssembly().GetName().Name;

                cfg.SetServiceName(name);
                cfg.SetDisplayName(name);
                cfg.SetDescription(name);

                cfg.Service<IEndpointService>(h =>
                {
                    h.ConstructUsing(s => _container.Resolve<IEndpointService>());
                    h.WhenStarted(s => s.Start());
                    h.WhenStopped(s => s.Stop());
                });
            });
        }
    }
}
