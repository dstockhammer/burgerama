using System;

namespace Burgerama.Messaging.Events.Voting
{
    [Serializable]
    public sealed class VoteQueued : IEvent
    {
        public string Key { get; set; }

        public Guid ReferenceId { get; set; }
    }
}