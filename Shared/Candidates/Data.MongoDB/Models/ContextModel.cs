using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace Burgerama.Shared.Candidates.Data.MongoDB.Models
{
    internal sealed class ContextModel
    {
        [BsonId]
        public string ContextKey { get; set; }

        public bool GracefullyHandleUnknownCandidates { get; set; }

        public IEnumerable<string> Candidates { get; set; }
    }
}
