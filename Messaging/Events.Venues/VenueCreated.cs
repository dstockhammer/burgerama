using System;

namespace Burgerama.Messaging.Events.Venues
{
    [Serializable]
    public sealed class VenueCreated : IEvent
    {
        public Guid VenueId { get; set; }

        public string Title { get; set; }
    }
}
