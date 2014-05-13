using System;

namespace Burgerama.Services.Outings.Domain
{
    public sealed class Outing
    {
        public Guid Id { get; private set; }

        public DateTime Date { get; private set; }

        public Guid VenueId { get; private set; }

        public Outing(DateTime date, Guid venueId)
        {
            Id = Guid.NewGuid();
            Date = date;
            VenueId = venueId;
        }

        public Outing(Guid id, DateTime date, Guid venueId)
        {
            Id = id;
            Date = date;
            VenueId = venueId;
        }
    }
}
