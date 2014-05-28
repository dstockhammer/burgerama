using System;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;
using Autofac;
using Burgerama.Common.Configuration;
using MassTransit;
using Serilog;
using Module = Autofac.Module;

namespace Burgerama.Messaging.MassTransit
{
    /// <summary>
    /// The MassTransit service bus module registers the service bus as <see cref="IServiceBus"/>. Additionally,
    /// every class in the executing assembly that implements <see cref="IConsumer"/> is registered as consumer.
    /// </summary>
    /// <remarks>
    /// The service bus is constructed and started the first time that <see cref="IServiceBus"/> is resolved.
    /// </remarks>
    public sealed class ServiceBusModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(GetServiceBus)
                .As<IServiceBus>()
                .SingleInstance();

            builder.RegisterAssemblyTypes(GetEntryAssembly())
                .Where(t => t.GetInterfaces().Contains(typeof(IConsumer)))
                .AsSelf();
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
                sbc.UseSerilog(context.Resolve<ILogger>());
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
