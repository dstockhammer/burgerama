using Autofac;
using MassTransit;

namespace Burgerama.Services.Venues.Api
{
    public static class MassTransitConfig
    {
        public static ContainerBuilder RegisterMassTransit(this ContainerBuilder builder)
        {
            var bus = ServiceBusFactory.New(sbc =>
            {
                sbc.UseRabbitMq();
                sbc.ReceiveFrom("amqp:// paste url here");
                sbc.Subscribe(x => x.LoadFrom(c.Resolve<ILifetimeScope>()));
            });

            builder.RegisterInstance(bus).As<IServiceBus>();
            return builder;
        }
    }
}