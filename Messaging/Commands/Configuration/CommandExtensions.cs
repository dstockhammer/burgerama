using System;
using System.Configuration;
using System.Diagnostics.Contracts;
using System.Linq;
using Burgerama.Common.Configuration;

namespace Burgerama.Messaging.Commands.Configuration
{
    public static class CommandExtensions
    {
        public static string GetEndpointName(this ICommand command)
        {
            Contract.Requires<ArgumentNullException>(command != null);

            var attributes = command.GetType()
                .GetCustomAttributes(true)
                .Where(a => a is EndpointQueueAttribute)
                .Cast<EndpointQueueAttribute>()
                .ToList();

            if (attributes.Count() != 1)
                throw new ConfigurationException("The EndpointQueueAttribute must be specified on every command.");

            return attributes.First().Name;
        }

        public static Uri GetEndpointUri(this ICommand command)
        {
            Contract.Requires<ArgumentNullException>(command != null);

            var config = (RabbitMqConfiguration)ConfigurationManager.GetSection("burgerama/rabbitMq");
            var uri = string.Format("{0}/{1}/", config.Server, config.VHost);
            var credentials = string.Format("{0}:{1}", config.UserName, config.Password);
            var queue = command.GetEndpointName();

            return new Uri("rabbitmq://" + uri + queue);
        }
    }
}
