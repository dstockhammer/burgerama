using System;

namespace Burgerama.Messaging.Events.Rating
{
    [Serializable]
    public sealed class RatingAdded : IEvent
    {
        public Guid VenueId { get; set; }

        public Guid UserId { get; set; }

        public double Rating { get; set; }

        public string Text { get; set; }
    }
}
