using System;
using Burgerama.Services.Venues.Domain;
using MongoDB.Bson.Serialization.Attributes;

namespace Burgerama.Services.Venues.Data.Models
{
    internal sealed class VenueModel
    {
        [BsonId]
        public string Id { get; set; }
        
        public string Name { get; set; }

        public Location Location { get; set; }

        public string CreatedByUser { get; set; }
        
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CreatedOn { get; set; }

        public string Url { get; set; }

        public string Description { get; set; }

        public string Address { get; set; }

        public double? TotalRating { get; set; }
    }
}
