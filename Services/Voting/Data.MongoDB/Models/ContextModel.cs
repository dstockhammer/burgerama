using System;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace Burgerama.Services.Voting.Data.MongoDB.Models
{
    internal sealed class ContextModel
    {
        [BsonId]
        public string ContextKey { get; set; }

        public bool AllowToVoteForUnknownCandidates { get; set; }

        public IEnumerable<Guid> Candidates { get; set; }
    }
}
