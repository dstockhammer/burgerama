﻿using System;

namespace Burgerama.Messaging.Events.Voting
{
    [Serializable]
    public sealed class VoteAdded : IEvent
    {
        public string ContextKey { get; set; }

        public Guid Reference { get; set; }

        public string UserId { get; set; }
    }
}
