using System;

namespace Burgerama.Messaging.Events.Ratings
{
    [Serializable]
    public sealed class RatingUpdated : IEvent
    {
        public string ContextKey { get; set; }

        public Guid Reference { get; set; }

        public double NewTotal { get; set; }
    }
}
