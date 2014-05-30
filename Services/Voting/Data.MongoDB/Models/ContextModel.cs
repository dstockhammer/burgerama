using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace Burgerama.Services.Voting.Data.MongoDB.Models
{
    internal class ContextModel
    {
        [BsonId]
        public string ContextKey { get; set; }

        public ICollection<string> Candidates { get; set; }
    }
}
