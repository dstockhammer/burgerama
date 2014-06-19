using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace Burgerama.Shared.Candidates.Data.MongoDB.Models
{
    internal sealed class CandidateModel<T> where T : class
    {
        public string ContextKey { get; set; }

        [BsonId]
        public string Reference { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? OpeningDate { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? ClosingDate { get; set; }
        
        public IEnumerable<T> Items { get; set; }
    }
}
