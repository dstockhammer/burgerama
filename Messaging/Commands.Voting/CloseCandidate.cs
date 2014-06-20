using System;
using Burgerama.Messaging.Commands.Configuration;

namespace Burgerama.Messaging.Commands.Voting
{
    [Serializable]
    [EndpointQueue("burgerama.services.voting.endpoint")]
    public sealed class CloseCandidate : ICommand
    {
        public string ContextKey { get; set; }

        public Guid Reference { get; set; }

        public DateTime ClosingDate { get; set; }
    }
}
