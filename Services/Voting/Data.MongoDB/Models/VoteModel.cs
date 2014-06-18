using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Burgerama.Services.Voting.Data.MongoDB.Models
{
    internal sealed class VoteModel
    {
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CreatedOn { get; set; }

        [BsonId]
        public string UserId { get; set; }
    }
}
