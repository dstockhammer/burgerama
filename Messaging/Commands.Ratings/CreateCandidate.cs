using System;
using Burgerama.Messaging.Commands.Configuration;

namespace Burgerama.Messaging.Commands.Ratings
{
    [Serializable]
    [EndpointQueue("burgerama.services.ratings.endpoint")]
    public sealed class CreateCandidate : ICommand
    {
        public string ContextKey { get; set; }

        public Guid Reference { get; set; }

        public DateTime? OpeningDate { get; set; }

        public DateTime? CloseDate { get; set; }
    }
}
