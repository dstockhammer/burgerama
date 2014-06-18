using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Burgerama.Services.Ratings.Api.Models
{
    [Serializable, DataContract, XmlRoot]
    public sealed class CandidateModel
    {
        [DataMember, XmlElement]
        public string ContextKey { get; set; }

        [DataMember, XmlElement]
        public Guid Reference { get; set; }

        [DataMember, XmlElement]
        public bool IsValidated { get; set; }

        [DataMember, XmlElement]
        public DateTime? OpeningDate { get; set; }

        [DataMember, XmlElement]
        public DateTime? ClosingDate { get; set; }

        [DataMember, XmlElement]
        public int RatingsCount { get; set; }

        [DataMember, XmlElement]
        public double? TotalRating { get; set; }

        [DataMember, XmlElement]
        public bool CanUserRate { get; set; }

        [DataMember, XmlElement]
        public RatingModel UserRating { get; set; }
    }
}
