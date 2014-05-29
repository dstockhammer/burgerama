using Burgerama.Common.Configuration;
using MongoDB.Driver;
using System;
using System.Configuration;
using System.Diagnostics.Contracts;

namespace Burgerama.Common.DataAccess.MongoDB
{
    public abstract class MongoDbRepository
    {
        private static ServiceConfiguration ServiceConfig
        {
            get { return (ServiceConfiguration)ConfigurationManager.GetSection("burgerama/service"); }
        }

        private static MongoDbConfiguration MongoDbConfig
        {
            get { return (MongoDbConfiguration)ConfigurationManager.GetSection("burgerama/mongoDb"); }
        }
        
        private readonly Lazy<MongoDatabase> _database = new Lazy<MongoDatabase>(() =>
            new MongoClient(MongoDbConfig.ConnectionString).GetServer().GetDatabase(MongoDbConfig.Database));

        protected MongoCollection<T> GetCollection<T>(string name)
        {
            Contract.Requires<ArgumentNullException>(name != null);

            var collectionName = string.Format("{0}.{1}", ServiceConfig.Key, name);
            return _database.Value.GetCollection<T>(collectionName); 
        }
    }
}
