using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Burgerama.Services.Venues.Api.Models
{
    [Serializable, DataContract, XmlRoot]
    public sealed class VenueModel
    {
        [DataMember, XmlElement]
        public string Id { get; set; }

        [Required]
        [DataMember, XmlElement]
        public string Title { get; set; }

        [Required]
        [DataMember, XmlElement]
        public LocationModel Location { get; set; }

        [DataMember, XmlElement]
        public string Url { get; set; }

        [DataMember, XmlElement]
        public string Description { get; set; }

        [DataMember, XmlElement]
        public string Address { get; set; }

        [DataMember, XmlElement]
        public string CreatedByUser { get; set; }

        [DataMember, XmlElement]
        public DateTime CreatedOn { get; set; }

        [DataMember, XmlElement]
        public int TotalVotes { get; set; }

        [DataMember, XmlElement]
        public double TotalRating { get; set; }
    }
}