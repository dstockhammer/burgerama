using System;

namespace Burgerama.Messaging.Events.Candidates
{
    public sealed class CandidateOpened : IEvent
    {
        public string ContextKey { get; set; }

        public Guid Reference { get; set; }

        public DateTime OpeningDate { get; set; }
    }

    public sealed class CandidateClosed : IEvent
    {
        public string ContextKey { get; set; }

        public Guid Reference { get; set; }

        public DateTime ClosingDate { get; set; }
    }

    public sealed class CandidateCreated : IEvent
    {
        public string ContextKey { get; set; }

        public Guid Reference { get; set; }
    }
}
