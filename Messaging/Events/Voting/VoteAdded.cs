using System;

namespace Burgerama.Messaging.Events.Voting
{
    [Serializable]
    public sealed class VoteAdded : IEvent
    {
        public Guid VenueId { get; set; }

        public Guid UserId { get; set; }
    }
}
