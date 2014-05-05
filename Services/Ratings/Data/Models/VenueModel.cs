using System;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace Burgerama.Services.Ratings.Data.Models
{
    internal sealed class VenueModel
    {
        [BsonId]
        public string Id { get; set; }

        public string Title { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? LatestOuting { get; set; }

        public IEnumerable<RatingModel> Ratings { get; set; }
    }
}
