﻿using System.Reflection;
using Autofac;
using Serilog;
using Serilog.Extras.Topshelf;
using Topshelf;

namespace Burgerama.Messaging.Endpoint.Host
{
    public sealed class EndpointHostFactory
    {
        private readonly IComponentContext _container;

        public EndpointHostFactory(IComponentContext container)
        {
            _container = container;
        }

        public Topshelf.Host CreateNew()
        {
            return HostFactory.New(cfg =>
            {
                var name = Assembly.GetEntryAssembly().GetName().Name;

                cfg.SetServiceName(name);
                cfg.SetDisplayName(name);
                cfg.SetDescription(name);

                cfg.UseLinuxIfAvailable();
                cfg.UseSerilog(_container.Resolve<ILogger>());

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
