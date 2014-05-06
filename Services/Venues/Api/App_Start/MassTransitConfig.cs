using System;
using System.Configuration;
using Autofac;
using Burgerama.Common.Configuration;
using MassTransit;

namespace Burgerama.Services.Venues.Api
{
    public static class MassTransitConfig
    {
        public static ContainerBuilder RegisterMassTransit(this ContainerBuilder builder)
        {
            builder.RegisterInstance(ServiceBusFactory.New(sbc =>
            {
                var config = (RabbitMqConfiguration)ConfigurationManager.GetSection("burgerama/rabbitMq");
                var uri = string.Format("{0}/{1}/", config.Server, config.VHost);
                var credentials = string.Format("{0}:{1}", config.UserName, config.Password);
                var queue = typeof(Startup).Assembly.GetName().Name.ToLowerInvariant();

                sbc.UseRabbitMq(r => r.ConfigureHost(new Uri("rabbitmq://" + uri + queue), h =>
                {
                    h.SetUsername(config.UserName);
                    h.SetPassword(config.Password);
                }));
                sbc.ReceiveFrom("rabbitmq://" + credentials + "@" + uri + queue);
            })).As<IServiceBus>().SingleInstance();

            return builder;
        }
    }
}
