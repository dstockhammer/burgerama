using System;

namespace Burgerama.Messaging.Events.Voting
{
    [Serializable]
    public sealed class CandidateQueued : IEvent
    {
        public string ContextKey { get; set; }

        public Guid Reference { get; set; }
    }
}