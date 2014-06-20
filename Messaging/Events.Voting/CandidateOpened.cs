﻿using System;

namespace Burgerama.Messaging.Events.Voting
{
    [Serializable]
    public sealed class CandidateOpened : IEvent
    {
        public string ContextKey { get; set; }

        public Guid Reference { get; set; }

        public DateTime OpeningDate { get; set; }
    }
}
