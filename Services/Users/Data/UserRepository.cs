using System.Collections.Generic;
using Burgerama.Common.MongoDb;
using Burgerama.Services.Users.Domain;
using Burgerama.Services.Users.Domain.Contracts;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace Burgerama.Services.Users.Data
{
    public sealed class UserRepository : MongoDbRepository, IUserRepository
    {
        private MongoCollection<User> Users
        {
            get { return GetCollection<User>("users"); }
        }

        public User Get(string id)
        {
            var query = Query<User>.EQ(v => v.Id, id);
            return Users.FindOne(query);
        }

        public IEnumerable<User> GetAll()
        {
            return Users.FindAll();
        }

        public void SaveOrUpdate(User user)
        {
            Users.Save(user);
        }
    }
}
