using System.Collections.Generic;
using System.Linq;
using Burgerama.Common.MongoDb;
using Burgerama.Services.Users.Data.Models;
using Burgerama.Services.Users.Domain;
using Burgerama.Services.Users.Domain.Contracts;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace Burgerama.Services.Users.Data
{
    public sealed class UserRepository : MongoDbRepository, IUserRepository
    {
        private MongoCollection<UserModel> Users
        {
            get { return GetCollection<UserModel>("users"); }
        }

        public User Get(string id)
        {
            var query = Query<UserModel>.EQ(v => v.Id, id);
            var model = Users.FindOne(query);
            return new User(model.Id, model.Email);
        }

        public IEnumerable<User> GetAll()
        {
            return Users.FindAll().Select(m => new User(m.Id, m.Email));
        }

        public void SaveOrUpdate(User user)
        {
            Users.Save(new UserModel
            {
                Id = user.Id,
                Email = user.Email
            });
        }
    }
}
