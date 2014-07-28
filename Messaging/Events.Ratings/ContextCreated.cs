using System;

namespace Burgerama.Messaging.Events.Ratings
{
    [Serializable]
    public sealed class ContextCreated : IEvent
    {
        public string ContextKey { get; set; }
    }
}
