using System;

namespace Burgerama.Messaging.Events.Voting
{
    [Serializable]
    public sealed class TriedToVoteForUnknownCandidate : IEvent
    {
        public string ContextKey { get; set; }

        public Guid Reference { get; set; }

        public string UserId { get; set; }
    }
}