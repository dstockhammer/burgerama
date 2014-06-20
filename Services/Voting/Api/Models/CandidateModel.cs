using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Burgerama.Services.Voting.Api.Models
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
        public int VotesCount { get; set; }

        [DataMember, XmlElement]
        public bool CanUserVote { get; set; }

        [DataMember, XmlElement]
        public VoteModel UserVote { get; set; }
    }
}
