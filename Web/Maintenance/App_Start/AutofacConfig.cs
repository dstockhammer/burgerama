using System.Reflection;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Burgerama.Common.Logging;
using Burgerama.Messaging.Commands;
using Burgerama.Messaging.MassTransit;
using Burgerama.Messaging.MassTransit.Commands;

namespace Burgerama.Web.Maintenance
{
    public sealed class AutofacConfig
    {
        public static void RegisterDependencies()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterModelBinders(Assembly.GetExecutingAssembly());
            builder.RegisterModelBinderProvider();

            // Logging
            builder.RegisterModule<LoggingModule>();

            // Messaging
            builder.RegisterModule<ServiceBusModule>();
            builder.RegisterType<CommandDispatcher>().As<ICommandDispatcher>();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
