using System;
using Burgerama.Messaging.Commands.Configuration;

namespace Burgerama.Messaging.Commands.Voting
{
    [Serializable]
    [EndpointQueue("burgerama.services.voting.endpoint")]
    public sealed class OpenCandidate : ICommand
    {
        public string ContextKey { get; set; }

        public Guid Reference { get; set; }

        public DateTime OpeningDate { get; set; }
    }
}
