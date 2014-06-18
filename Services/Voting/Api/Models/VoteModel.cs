using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Burgerama.Services.Voting.Api.Models
{
    [Serializable, DataContract, XmlRoot]
    public class VoteModel
    {
        [Required]
        [DataMember, XmlElement]
        public DateTime CreatedOn { get; set; }

        [DataMember, XmlElement]
        public string UserId { get; set; }
    }
}
