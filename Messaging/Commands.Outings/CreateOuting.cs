using System;
using Burgerama.Messaging.Commands.Configuration;

namespace Burgerama.Messaging.Commands.Outings
{
    [Serializable]
    [EndpointQueue("burgerama.services.outings.endpoint")]
    public sealed class CreateOuting : ICommand
    {
        public Guid VenueId { get; set; }

        public DateTime Date { get; set; }
    }
}
