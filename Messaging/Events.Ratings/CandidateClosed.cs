using System;

namespace Burgerama.Messaging.Events.Ratings
{
    [Serializable]
    public sealed class CandidateClosed : IEvent
    {
        public string ContextKey { get; set; }

        public Guid Reference { get; set; }

        public DateTime ClosingDate { get; set; }
    }
}
