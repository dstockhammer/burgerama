using System;
using Burgerama.Messaging.Commands.Configuration;

namespace Burgerama.Messaging.Commands.Voting
{
    [Serializable]
    [EndpointQueue("burgerama.services.voting.endpoint")]
    public sealed class CreateContext : ICommand
    {
        public string ContextKey { get; set; }

        public bool GracefullyHandleUnknownCandidates { get; set; }
    }
}
