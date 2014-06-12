using System;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace Burgerama.Services.Ratings.Data.Models
{
    internal sealed class CandidateModel
    {
        public string ContextKey { get; set; }

        [BsonId]
        public string Reference { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? OpeningDate { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? ClosingDate { get; set; }
        
        public IEnumerable<RatingModel> Ratings { get; set; }
    }
}
