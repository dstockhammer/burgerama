using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Burgerama.Services.Ratings.Api.Models
{
    [Serializable, DataContract, XmlRoot]
    public sealed class VenueModel
    {
        [DataMember, XmlElement]
        public string Id { get; set; }

        [Required]
        [DataMember, XmlElement]
        public double Rating { get; set; }

        [Required]
        [DataMember, XmlElement]
        public bool CanUserRate { get; set; }
    }
}