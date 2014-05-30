using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace Burgerama.Services.Voting.Data.MongoDB.Models
{
    internal class ContextModel
    {
        [BsonId]
        public string Id { get; set; }

        public string Key { get; set; }

        public ICollection<CampaignModel> Campaigns { get; set; }
    }
}
