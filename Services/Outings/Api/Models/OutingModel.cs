using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Burgerama.Services.Outings.Api.Models
{
    [Serializable, DataContract, XmlRoot]
    internal sealed class OutingModel
    {
        [DataMember, XmlElement]
        public string Id { get; set; }

        [DataMember, XmlElement]
        public DateTime Date { get; set; }

        [DataMember, XmlElement]
        public string VenueId { get; set; }

        [DataMember, XmlElement]
        public VenueModel Venue { get; set; }
    }
}