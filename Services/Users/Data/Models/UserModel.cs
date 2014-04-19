using MongoDB.Bson.Serialization.Attributes;

namespace Burgerama.Services.Users.Data.Models
{
    internal sealed class UserModel
    {
        [BsonId]
        public string Id { get; set; }

        public string Email { get; set; }
    }
}
