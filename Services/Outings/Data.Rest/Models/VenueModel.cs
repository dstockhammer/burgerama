using Burgerama.Services.Outings.Domain;

namespace Burgerama.Services.Outings.Data.Rest.Models
{
    internal sealed class VenueModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public LocationModel Location { get; set; }

        public string Url { get; set; }

        public string Description { get; set; }

        public string Address { get; set; }

        public double Rating { get; set; }
    }
}
