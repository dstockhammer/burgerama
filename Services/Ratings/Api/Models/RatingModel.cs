using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Burgerama.Services.Ratings.Api.Models
{
    [Serializable, DataContract, XmlRoot]
    public class RatingModel
    {
        [DataMember, XmlElement]
        public DateTime CreatedOn { get; set; }

        [DataMember, XmlElement]
        public string UserId { get; set; }

        [Required]
        [DataMember, XmlElement]
        public double Rating { get; set; }

        [DataMember, XmlElement]
        public string Text { get; set; }
    }
}