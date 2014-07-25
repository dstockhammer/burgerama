using System;
using AspNet.Identity.MongoDB;
using Burgerama.Common.Configuration;
using MongoDB.Driver;

namespace Burgerama.Web.Maintenance.Identity
{
    public class ApplicationIdentityContext : IdentityContext, IDisposable
    {
        public ApplicationIdentityContext(MongoCollection users, MongoCollection roles)
            : base(users, roles)
        {
        }

        public static ApplicationIdentityContext Create()
        {
            var config = MongoDbConfiguration.Load();

            var client = new MongoClient(config.ConnectionString);
            var database = client.GetServer().GetDatabase(config.Database);

            var users = database.GetCollection<IdentityUser>("maintenance.users");
            var roles = database.GetCollection<IdentityRole>("maintenance.roles");
            
            return new ApplicationIdentityContext(users, roles);
        }

        public void Dispose()
        {
            // nothing to do
        }
    }
}