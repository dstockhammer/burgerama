using System;
using Burgerama.Messaging.Commands.Configuration;

namespace Burgerama.Messaging.Commands.Ratings
{
    [Serializable]
    [EndpointQueue("burgerama.services.ratings.endpoint")]
    public sealed class CloseCandidate : ICommand
    {
        public string ContextKey { get; set; }

        public Guid Reference { get; set; }

        public DateTime ClosingDate { get; set; }
    }
}
