using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace Burgerama.Services.Ratings.Data.Models
{
    internal sealed class ContextModel
    {
        [BsonId]
        public string ContextKey { get; set; }

        public bool AllowToRateUnknownCandidates { get; set; }

        public IEnumerable<Guid> Candidates { get; set; }
    }
}
