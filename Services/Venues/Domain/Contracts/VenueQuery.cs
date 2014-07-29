namespace Burgerama.Services.Venues.Domain.Contracts
{
    public sealed class VenueQuery
    {
        public bool? HasOuting { get; set; }

        public string OutingId { get; set; }

        public string CreatedByUser { get; set; }
    }
}