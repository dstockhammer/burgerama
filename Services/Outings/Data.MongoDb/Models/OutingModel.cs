using System;
using MongoDB.Bson.Serialization.Attributes;

namespace Burgerama.Services.Outings.Data.MongoDb.Models
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