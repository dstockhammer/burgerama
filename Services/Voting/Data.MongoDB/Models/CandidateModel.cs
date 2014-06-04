using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Burgerama.Services.Voting.Data.MongoDB.Models
{
    internal class CandidateModel
    {
        [BsonId]
        public string Id { get; set; }

        public string Reference { get; set; }

        public string ContextKey { get; set; }

        public ICollection<VoteModel> Votes { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? Expiry { get; set; }
    }
}
