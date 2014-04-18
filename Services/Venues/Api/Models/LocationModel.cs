using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Burgerama.Services.Venues.Api.Models
{
    [Serializable, DataContract, XmlRoot]
    public sealed class LocationModel
    {
        [Required]
        [DataMember, XmlElement]
        public string Reference { get; set; }

        [Required]
        [DataMember, XmlElement]
        public double Latitiude { get; set; }

        [Required]
        [DataMember, XmlElement]
        public double Longitude { get; set; }
    }
}