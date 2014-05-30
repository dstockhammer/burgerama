using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Burgerama.Services.Voting.Data.MongoDB.Models
{
    internal class VoteModel
    {
        [BsonId]
        public string Id { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CreatedOn { get; set; }

        public string CreatedBy { get; set; }
    }
}
