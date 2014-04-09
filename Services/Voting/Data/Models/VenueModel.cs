using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace Burgerama.Services.Voting.Data.Models
{
    internal class VenueModel
    {
        [BsonId]
        public string Id { get; set; }

        public string Outing { get; set; }

        public ICollection<string> Votes { get; set; } 
    }
}
