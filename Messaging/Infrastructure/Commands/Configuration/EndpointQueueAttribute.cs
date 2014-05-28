using System;

namespace Burgerama.Messaging.Infrastructure.Commands.Configuration
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public sealed class EndpointQueueAttribute : Attribute
    {
        public string Name { get; set; }

        public EndpointQueueAttribute(string name = "")
        {
            Name = name;
        }
    }
}
