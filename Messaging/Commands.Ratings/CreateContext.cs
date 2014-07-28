using System;
using Burgerama.Messaging.Commands.Configuration;

namespace Burgerama.Messaging.Commands.Ratings
{
    [Serializable]
    [EndpointQueue("burgerama.services.ratings.endpoint")]
    public sealed class CreateContext : ICommand
    {
        public string ContextKey { get; set; }

        public bool GracefullyHandleUnknownCandidates { get; set; }
    }
}
