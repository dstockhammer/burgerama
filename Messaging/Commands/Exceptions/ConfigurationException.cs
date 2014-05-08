using System;

namespace Burgerama.Messaging.Commands.Exceptions
{
    public sealed class ConfigurationException : Exception
    {
        public ConfigurationException(string message) : base(message)
        {
        }
    }
}