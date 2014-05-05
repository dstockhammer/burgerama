using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Burgerama.Services.Ratings.Api.Models
{
    [Serializable, DataContract, XmlRoot]
    public sealed class VenueModel
    {
        [DataMember, XmlElement]
        public string Id { get; set; }

        [DataMember, XmlElement]
        public double Rating { get; set; }

        [DataMember, XmlElement]
        public bool CanUserRate { get; set; }
    }
}