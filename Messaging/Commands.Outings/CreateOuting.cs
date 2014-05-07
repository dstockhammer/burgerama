using System;

namespace Burgerama.Messaging.Commands.Outings
{
    [EndpointName("burgerama.services.outings.endpoint")]
    public sealed class CreateOuting : ICommand
    {
        public Guid VenueId { get; set; }

        public DateTime Date { get; set; }
    }
}
