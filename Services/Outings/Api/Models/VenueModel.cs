using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Burgerama.Services.Outings.Domain;

namespace Burgerama.Services.Outings.Api.Models
{
    [Serializable, DataContract, XmlRoot]
    internal sealed class VenueModel
    {
        [DataMember, XmlElement]
        public string Id { get; set; }

        [DataMember, XmlElement]
        public string Name { get; set; }

        [DataMember, XmlElement]
        public LocationModel Location { get; set; }

        [DataMember, XmlElement]
        public string Url { get; set; }

        [DataMember, XmlElement]
        public string Description { get; set; }

        [DataMember, XmlElement]
        public string Address { get; set; }

        [DataMember, XmlElement]
        public double Rating { get; set; }
    }
}
