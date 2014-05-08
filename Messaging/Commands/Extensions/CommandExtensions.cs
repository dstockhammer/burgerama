using System.Linq;
using Burgerama.Messaging.Commands.Exceptions;

namespace Burgerama.Messaging.Commands.Extensions
{
    public static class CommandExtensions
    {
        public static string GetEndpointName(this ICommand command)
        {
            var attributes = command.GetType()
                .GetCustomAttributes(true)
                .Where(a => a is EndpointQueueAttribute)
                .Cast<EndpointQueueAttribute>()
                .ToList();

            if (attributes.Count() != 1)
                throw new ConfigurationException("The EndpointNameAttribute must be specified on every command.");

            return attributes.First().Name;
        }
    }
}
