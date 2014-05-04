using System;

namespace Burgerama.Messaging.Events.Ratings
{
    [Serializable]
    public sealed class RatingAdded : IEvent
    {
        public Guid VenueId { get; set; }

        public string UserId { get; set; }

        public double Value { get; set; }

        public string Text { get; set; }
    }
}
