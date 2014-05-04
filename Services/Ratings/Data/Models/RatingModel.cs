using MongoDB.Bson.Serialization.Attributes;

namespace Burgerama.Services.Ratings.Data.Models
{
    internal sealed class RatingModel
    {
        [BsonId]
        public string User { get; set; }

        public double Value { get; set; }

        public string Text { get; set; }
    }
}
