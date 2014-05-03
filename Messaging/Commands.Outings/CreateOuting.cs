using System;

namespace Burgerama.Messaging.Commands.Outings
{
    public sealed class CreateOuting : ICommand
    {
        public Guid VenueId { get; set; }

        public DateTime Date { get; set; }
    }
}
