using System.Linq;

namespace Burgerama.Messaging.Commands.Configuration
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
                throw new ConfigurationException("The EndpointQueueAttribute must be specified on every command.");

            return attributes.First().Name;
        }
    }
}
