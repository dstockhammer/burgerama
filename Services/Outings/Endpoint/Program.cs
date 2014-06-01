﻿using Autofac;
using Burgerama.Common.Logging;
using Burgerama.Messaging.MassTransit;
using Burgerama.Services.Outings.Data.MongoDB;
using Burgerama.Services.Outings.Domain.Contracts;
using MassTransit;
using Microsoft.WindowsAzure.Jobs;

namespace Burgerama.Services.Outings.Endpoint
{
    public sealed class Program
    {
        static void Main(string[] args)
        {
            var container = GetAutofacContainer();
            var bus = container.Resolve<IServiceBus>();
            
            var host = new JobHost();
            host.RunAndBlock();
            bus.Dispose();
        }

        private static IContainer GetAutofacContainer()
        {
            var builder = new ContainerBuilder();

            // Repositories
            builder.RegisterType<OutingRepository>().As<IOutingRepository>();

            // Logging
            builder.RegisterModule<LoggingModule>();

            // Messaging infrastructure
            builder.RegisterModule<ServiceBusModule>();

            return builder.Build();
        }
    }
}
