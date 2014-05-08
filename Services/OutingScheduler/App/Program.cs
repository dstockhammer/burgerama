﻿using System;
using System.Configuration;
using Autofac;
using Burgerama.Common.Configuration;
using Burgerama.Messaging.Commands;
using Burgerama.Messaging.Commands.Outings;
using Burgerama.Messaging.MassTransit.Commands;
using Burgerama.Services.OutingScheduler.Data.Rest;
using Burgerama.Services.OutingScheduler.Domain.Contracts;
using Burgerama.Services.OutingScheduler.Services;
using MassTransit;

namespace Burgerama.Services.OutingScheduler.App
{
    public sealed class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("OutingScheduler run started.");

            var container = GetAutofacContainer();
            var scheduler = container.Resolve<SchedulingService>();
            var outing = scheduler.ScheduleOuting();
            if (outing == null)
            {
                Console.WriteLine("OutingScheduler run successful: No outing was scheduled.");
            }
            else
            {
                var commandDispatcher = container.Resolve<ICommandDispatcher>();

                // instead of the endpoint argument, add a mappin extension method like
                // string GetEndpointUrl(this CreateOuting) -- the question is where to 
                // put the extension method. probably in the masstransit assembly, because
                // it is rabbitmq specific. we don't want to add a dependency to all the
                // command assemblies though... :/

                commandDispatcher.Send(new CreateOuting
                {
                    VenueId = outing.Venue.Id,
                    Date = outing.Date
                });

                Console.WriteLine("OutingScheduler run successful: Outing '{0}' scheduled for '{1}' on '{2}'.", outing.Id, outing.Venue.Title, outing.Date);
            }

            // disposing the bus is very important in order to unsubscribe and stop consuming.
            // todo: how to implement this in a way that the dev is not forced dispose manually?
            var bus = container.Resolve<IServiceBus>();
            bus.Dispose();
        }

        private static IContainer GetAutofacContainer()
        {
            var builder = new ContainerBuilder();

            // Services
            builder.RegisterType<SchedulingService>();

            // Repositories
            builder.RegisterType<OutingRepository>().As<IOutingRepository>();
            builder.RegisterType<VenueRepository>().As<IVenueRepository>();

            // Messaging
            builder.Register(GetServiceBus).As<IServiceBus>().SingleInstance();
            builder.RegisterType<CommandDispatcher>().As<ICommandDispatcher>();

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

        private static string GetEndpointUrl(string service)
        {
            var config = (RabbitMqConfiguration)ConfigurationManager.GetSection("burgerama/rabbitMq");
            var uri = string.Format("{0}/{1}/", config.Server, config.VHost);
            var credentials = string.Format("{0}:{1}", config.UserName, config.Password);
            var queue = string.Format("burgerama.services.{0}.endpoint", service);

            return "rabbitmq://" + credentials + "@" + uri + queue;
        }
    }
}
