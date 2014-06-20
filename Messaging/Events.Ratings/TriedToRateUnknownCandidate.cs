using System;

namespace Burgerama.Messaging.Events.Ratings
{
    [Serializable]
    public sealed class TriedToRateUnknownCandidate : IEvent
    {
        public string ContextKey { get; set; }

        public Guid Reference { get; set; }

        public string UserId { get; set; }
    }
}
