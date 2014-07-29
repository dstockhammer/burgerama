using Burgerama.Common.Configuration;
using MongoDB.Driver;
using System;
using System.Diagnostics.Contracts;

namespace Burgerama.Common.DataAccess.MongoDB
{
    public abstract class MongoDbRepository
    {
        private static readonly Lazy<ServiceConfiguration> ServiceConfig = new Lazy<ServiceConfiguration>(() => ServiceConfiguration.Load());
        private static readonly Lazy<MongoDbConfiguration> MongoDbConfig = new Lazy<MongoDbConfiguration>(() => MongoDbConfiguration.Load());
        private readonly Lazy<MongoDatabase> _database = new Lazy<MongoDatabase>(GetDatabase);

        protected MongoCollection<T> GetCollection<T>(string name)
        {
            Contract.Requires<ArgumentNullException>(name != null);

            var collectionName = string.Format("{0}.{1}", ServiceConfig.Value.Key, name);
            return _database.Value.GetCollection<T>(collectionName); 
        }

        private static MongoDatabase GetDatabase()
        {
            return new MongoClient(MongoDbConfig.Value.ConnectionString)
                .GetServer()
                .GetDatabase(MongoDbConfig.Value.Database);
        }
    }
}
