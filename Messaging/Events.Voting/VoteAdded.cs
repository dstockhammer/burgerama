using System;

namespace Burgerama.Messaging.Events.Voting
{
    [Serializable]
    public sealed class VoteAdded : IEvent
    {
        public string ContextKey { get; set; }

        public Guid CandidateReference { get; set; }

        public string UserId { get; set; }

        public int TotalOfVotes { get; set; }
    }
}
