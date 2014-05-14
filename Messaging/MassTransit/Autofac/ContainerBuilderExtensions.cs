using System;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;
using Autofac;
using Burgerama.Common.Configuration;
using MassTransit;

namespace Burgerama.Messaging.MassTransit.Autofac
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
            builder.RegisterAssemblyTypes(GetEntryAssembly())
                .Where(t => t.GetInterfaces().Contains(typeof(IConsumer)))
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
                var queue = GetEntryAssembly().GetName().Name.ToLowerInvariant();

                sbc.UseRabbitMq(r => r.ConfigureHost(new Uri("rabbitmq://" + uri + queue), h =>
                {
                    h.SetUsername(config.UserName);
                    h.SetPassword(config.Password);
                }));
                sbc.ReceiveFrom("rabbitmq://" + credentials + "@" + uri + queue);
                sbc.Subscribe(x => x.LoadFrom(context.Resolve<ILifetimeScope>()));
            });
        }

        private static Assembly GetEntryAssembly()
        {
            return Assembly.GetEntryAssembly() ?? GetWebEntryAssembly();
        }

        private static Assembly GetWebEntryAssembly()
        {
            if (HttpContext.Current == null || HttpContext.Current.ApplicationInstance == null)
                return null;

            var type = HttpContext.Current.ApplicationInstance.GetType();
            while (type != null && type.Namespace == "ASP")
            {
                type = type.BaseType;
            }

            return type == null ? null : type.Assembly;
        }
    }
}
