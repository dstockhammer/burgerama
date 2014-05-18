﻿using Autofac;
using Burgerama.Common.Logging;
using Burgerama.Messaging.MassTransit.Autofac;
using Burgerama.Messaging.MassTransit.Endpoint.Topshelf;
using Burgerama.Services.Outings.Data;
using Burgerama.Services.Outings.Domain.Contracts;

namespace Burgerama.Services.Outings.Endpoint
{
    public sealed class Program
    {
        static void Main(string[] args)
        {
            var container = GetAutofacContainer();

            var hostFactory = container.Resolve<EndpointHostFactory>();
            hostFactory.CreateNew().Run();
        }

        private static IContainer GetAutofacContainer()
        {
            var builder = new ContainerBuilder();

            // Repositories
            builder.RegisterType<OutingRepository>().As<IOutingRepository>();

            // Logging
            builder.RegisterModule<LoggingModule>();

            // Messaging infrastructure
            builder.RegisterServiceBus();
            builder.RegisterConsumers();
            builder.RegisterType<EndpointService>().As<IEndpointService>();
            builder.RegisterType<EndpointHostFactory>().AsSelf().SingleInstance();

            return builder.Build();
        }
    }
}
