using System;
using System.Configuration;
using System.Reflection;
using Autofac;
using Burgerama.Common.Configuration;
using MassTransit;

namespace Burgerama.Messaging.MassTransit.Endpoint.Autofac
{
    public static class ContainerBuilderExtensions
    {
        public static ContainerBuilder RegisterServiceBus(this ContainerBuilder builder)
        {
            builder.Register(GetServiceBus)
                .As<IServiceBus>()
                .SingleInstance();

            return builder;
        }

        public static ContainerBuilder RegisterConsumers(this ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.GetEntryAssembly())
                .Where(t => t.BaseType == typeof(Consumes<>.Context))
                .AsSelf();

            return builder;
        }

        private static IServiceBus GetServiceBus(IComponentContext context)
        {
            return ServiceBusFactory.New(sbc =>
            {
                var config = (RabbitMqConfiguration)ConfigurationManager.GetSection("burgerama/rabbitMq");
                var uri = string.Format("{0}/{1}/", config.Server, config.VHost);
                var credentials = string.Format("{0}:{1}", config.UserName, config.Password);
                var queue = Assembly.GetEntryAssembly().GetName().Name.ToLowerInvariant();

                sbc.UseRabbitMq(r => r.ConfigureHost(new Uri("rabbitmq://" + uri + queue), h =>
                {
                    h.SetUsername(config.UserName);
                    h.SetPassword(config.Password);
                }));
                sbc.ReceiveFrom("rabbitmq://" + credentials + "@" + uri + queue);
                sbc.Subscribe(x => x.LoadFrom(context.Resolve<ILifetimeScope>()));
            });
        }
    }
}
