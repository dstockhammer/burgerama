using System;

namespace Burgerama.Messaging.Events.Outings
{
    [Serializable]
    public sealed class OutingCreated : IEvent
    {
        public Guid OutingId { get; set; }

        public Guid VenueId { get; set; }

        public DateTime DateTime { get; set; }
    }
}
