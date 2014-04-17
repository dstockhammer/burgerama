using System;
using System.Configuration;
using System.Diagnostics.Contracts;
using Burgerama.Common.Configuration;
using Burgerama.Common.Configuration.Data;
using MongoDB.Driver;

namespace Burgerama.Common.MongoDb
{
    public abstract class MongoDbRepository
    {
        private static MongoDbConfiguration DbConfig
        {
            get { return (MongoDbConfiguration)ConfigurationManager.GetSection("database"); }
        }
        
        private static BurgeramaConfiguration Config
        {
            get { return (BurgeramaConfiguration)ConfigurationManager.GetSection("burgerama"); }
        }

        private readonly Lazy<MongoDatabase> _database = new Lazy<MongoDatabase>(() =>
            new MongoClient(DbConfig.ConnectionString).GetServer().GetDatabase(DbConfig.Name));

        protected MongoCollection<T> GetCollection<T>(string name)
        {
            Contract.Requires<ArgumentNullException>(name != null);

            var collectionName = string.Format("{0}.{1}", Config.ServiceKey, name);
            return _database.Value.GetCollection<T>(collectionName); 
        }
    }
}
