using Burgerama.Services.Venues.Domain;
using MongoDB.Bson.Serialization.Attributes;

namespace Burgerama.Services.Venues.Data.Models
{
    internal sealed class VenueModel
    {
        [BsonId]
        public string Id { get; set; }
        
        public string Title { get; set; }

        public Location Location { get; set; }

        public string CreatedByUser { get; set; }

        public string Url { get; set; }

        public string Description { get; set; }
    }
}
