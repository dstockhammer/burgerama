using System;
using MongoDB.Bson.Serialization.Attributes;

namespace Burgerama.Services.Venues.Data.Models
{
    internal sealed class OutingModel
    {
        public string Id { get; set; }
        
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime DateTime { get; set; }
    }
}
