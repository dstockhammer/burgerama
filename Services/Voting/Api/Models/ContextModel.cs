using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Burgerama.Services.Voting.Api.Models
{
    [Serializable, DataContract, XmlRoot]
    public class ContextModel
    {
        [Required]
        [DataMember, XmlElement]
        public string ContextKey { get; set; }

        [DataMember, XmlElement]
        public IEnumerable<Guid> Candidates { get; set; }
    }
}