using System;

namespace Burgerama.Messaging.Events.Voting
{
    [Serializable]
    public sealed class CandidateCreated : IEvent
    {
        public string ContextKey { get; set; }

        public Guid Reference { get; set; }
    }
}
