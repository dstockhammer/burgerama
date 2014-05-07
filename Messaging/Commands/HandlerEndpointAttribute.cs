using System;

namespace Burgerama.Messaging.Commands
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public sealed class EndpointNameAttribute : Attribute
    {
        public string Name { get; set; }

        public EndpointNameAttribute(string name = "")
        {
            Name = name;
        }
    }
}
