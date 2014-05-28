using System;

namespace Burgerama.Messaging.Infrastructure.Commands.Configuration
{
    public sealed class ConfigurationException : Exception
    {
        public ConfigurationException(string message) : base(message)
        {
        }
    }
}