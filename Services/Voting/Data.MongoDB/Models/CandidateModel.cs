using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace Burgerama.Services.Voting.Data.MongoDB.Models
{
    internal class CandidateModel
    {
        [BsonId]
        public string Reference { get; set; }

        public ICollection<VoteModel> Votes { get; set; }
    }
}
