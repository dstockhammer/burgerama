using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Burgerama.Services.Voting.Api.Models
{
    [Serializable, DataContract, XmlRoot]
    public class CandidateModel
    {
        [Required]
        [DataMember, XmlElement]
        public Guid Reference { get; set; }

        [Required]
        [DataMember, XmlElement]
        public IEnumerable<VoteModel> Votes { get; set; }

        [DataMember, XmlElement]
        public DateTime? Expiry { get; set; }
    }
}