using System;

namespace Burgerama.Messaging.Events.Voting
{
    [Serializable]
    public sealed class ContextCreated : IEvent
    {
        public string ContextKey { get; set; }
    }
}
