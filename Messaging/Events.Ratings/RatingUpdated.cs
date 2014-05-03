using System;

namespace Burgerama.Messaging.Events.Ratings
{
    [Serializable]
    public sealed class RatingUpdated : IEvent
    {
        public Guid VenueId { get; set; }

        public double NewRating { get; set; }
    }
}
