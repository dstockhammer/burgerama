using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Burgerama.Services.Voting.Data.MongoDB.Models
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

        public IEnumerable<VoteModel> Votes { get; set; }
    }
}
