using Burgerama.Messaging.Commands.Configuration;
using System;

namespace Burgerama.Messaging.Commands.Voting
{
    [Serializable]
    [EndpointQueue("burgerama.services.voting.endpoint")]
    public sealed class CreateCandidate : ICommand
    {
        public string Context { get; set; }

        public string Reference { get; set; }

        public DateTime ExpiryDate { get; set; }
    }
}
