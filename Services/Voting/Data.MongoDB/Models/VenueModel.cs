using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Burgerama.Services.Voting.Data.MongoDB.Models
{
    internal class VenueModel
    {
        [BsonId]
        public string Id { get; set; }

        public string Title { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? LatestOuting { get; set; }

        public ICollection<string> Votes { get; set; }
    }
}
