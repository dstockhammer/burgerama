using System;
using MongoDB.Bson.Serialization.Attributes;

namespace Burgerama.Services.Ratings.Data.Models
{
    internal sealed class RatingModel
    {
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CreatedOn { get; set; }

        [BsonId]
        public string UserId { get; set; }

        public double Value { get; set; }

        public string Text { get; set; }
    }
}
