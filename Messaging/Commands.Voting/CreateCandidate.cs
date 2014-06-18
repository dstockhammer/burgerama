using Burgerama.Messaging.Commands.Configuration;
using System;

namespace Burgerama.Messaging.Commands.Voting
{
    [Serializable]
    [EndpointQueue("burgerama.services.voting.endpoint")]
    public sealed class CreateCandidate : ICommand
    {
        public string ContextKey { get; set; }

        public string Reference { get; set; }

        public string UserId { get; set; }

        public DateTime VotedOn { get; set; }
    }
}
