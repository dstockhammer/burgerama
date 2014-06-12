using System;

namespace Burgerama.Messaging.Events.Ratings
{
    [Serializable]
    public sealed class CandidateCreated : IEvent
    {
        public string ContextKey { get; set; }

        public Guid Reference { get; set; }
    }
}
