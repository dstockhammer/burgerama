using System;

namespace Burgerama.Services.Outings.Domain.Contracts
{
    public sealed class OutingQuery
    {
        public string VenueId { get; set; }

        public DateTime? Before { get; set; }

        public DateTime? After { get; set; }
    }
}
