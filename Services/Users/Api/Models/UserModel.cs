using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Burgerama.Services.Users.Api.Models
{
    [Serializable, DataContract, XmlRoot]
    public sealed class UserModel
    {
        [DataMember, XmlElement]
        public string Id { get; set; }

        [DataMember, XmlElement]
        public string Email { get; set; }
    }
}