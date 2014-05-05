using System;
using System.Threading;
using Autofac;
using Burgerama.Messaging.Events;
using Burgerama.Services.Ratings.Data;
using Burgerama.Services.Ratings.Domain.Contracts;
using Burgerama.Services.Ratings.Endpoint.Handlers;
using MassTransit;

namespace Burgerama.Services.Ratings.Endpoint
{
    public class Program
    {
        static void Main(string[] args)
        {
            var container = GetAutofacContainer();

            // todo: improve this. quite dodgy
            while (true)
            {
                Thread.Sleep(1000);
            }
        }

        private static IContainer GetAutofacContainer()
        {
            var builder = new ContainerBuilder();

            // Repositories
            builder.RegisterType<VenueRepository>().As<IVenueRepository>();

            // Messaging infrastructure
            builder.Register(GetServiceBus).As<IServiceBus>().SingleInstance();
            builder.RegisterType<EventDispatcher>().As<IEventDispatcher>();

            // Event handlers
            builder.RegisterType<OutingCreatedHandler>().AsSelf();
            builder.RegisterType<VenueCreatedHandler>().AsSelf();

            return builder.Build();
        }

        private static IServiceBus GetServiceBus(IComponentContext context)
        {
            return ServiceBusFactory.New(sbc =>
            {
                sbc.UseRabbitMq();
                sbc.ReceiveFrom("amqp:// paste url here");
                sbc.Subscribe(x => x.LoadFrom(context.Resolve<ILifetimeScope>()));
            });
        }
    }
}
