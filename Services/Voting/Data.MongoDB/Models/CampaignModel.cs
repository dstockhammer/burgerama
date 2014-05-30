using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Burgerama.Services.Voting.Data.MongoDB.Models
{
    internal class CampaignModel
    {
        [BsonId]
        public string Id { get; set; }

        public ICollection<CandidateModel> Candidates { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? Start { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? End { get; set; }
    }
}
