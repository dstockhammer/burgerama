using System;
using System.Configuration;
using Autofac;
using Burgerama.Common.Configuration;
using Burgerama.Services.Ratings.Data;
using Burgerama.Services.Ratings.Domain.Contracts;
using Burgerama.Services.Ratings.Endpoint.Handlers;
using MassTransit;
using Topshelf;

namespace Burgerama.Services.Ratings.Endpoint
{
    public sealed class Program
    {
        static void Main(string[] args)
        {
            var host = HostFactory.New(cfg =>
            {
                var container = GetAutofacContainer();

                cfg.SetServiceName("Burgerama.Services.Ratings.Endpoint");
                cfg.SetDisplayName("Burgerama.Services.Ratings.Endpoint");
                cfg.SetDescription("Burgerama.Services.Ratings.Endpoint");
                
                cfg.Service<EndpointService>(h =>
                {
                    h.ConstructUsing(s => container.Resolve<EndpointService>());
                    h.WhenStarted(s => s.Start());
                    h.WhenStopped(s => s.Stop());
                });
            });

            host.Run();
        }

        private static IContainer GetAutofacContainer()
        {
            var builder = new ContainerBuilder();

            // Repositories
            builder.RegisterType<VenueRepository>().As<IVenueRepository>();

            // Messaging infrastructure
            builder.Register(GetServiceBus).As<IServiceBus>().SingleInstance();
            builder.RegisterType<EndpointService>().AsSelf();

            // Event handlers
            builder.RegisterType<OutingCreatedHandler>().AsSelf();
            builder.RegisterType<VenueCreatedHandler>().AsSelf();

            return builder.Build();
        }

        private static IServiceBus GetServiceBus(IComponentContext context)
        {
            return ServiceBusFactory.New(sbc =>
            {
                var config = (RabbitMqConfiguration)ConfigurationManager.GetSection("burgerama/rabbitMq");
                var uri = string.Format("{0}/{1}/", config.Server, config.VHost);
                var credentials = string.Format("{0}:{1}", config.UserName, config.Password);
                var queue = typeof(Program).Assembly.GetName().Name.ToLowerInvariant();
                
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
