using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Burgerama.Services.Outings.Data.MongoDB.Models
{
    internal sealed class OutingModel
    {
        [BsonId]
        public string Id { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime Date { get; set; }

        public string VenueId { get; set; }
    }
}