using System;
using System.Configuration;
using System.Diagnostics.Contracts;
using Burgerama.Common.Configuration;
using MongoDB.Driver;

namespace Burgerama.Common.MongoDb
{
    public abstract class MongoDbRepository
    {
        private static ServiceConfiguration Config
        {
            get { return (ServiceConfiguration)ConfigurationManager.GetSection("burgerama.service"); }
        }
        
        private readonly Lazy<MongoDatabase> _database = new Lazy<MongoDatabase>(() =>
            new MongoClient(Config.MongoDb.ConnectionString).GetServer().GetDatabase(Config.MongoDb.Database));

        protected MongoCollection<T> GetCollection<T>(string name)
        {
            Contract.Requires<ArgumentNullException>(name != null);

            var collectionName = string.Format("{0}.{1}", Config.Key, name);
            return _database.Value.GetCollection<T>(collectionName); 
        }
    }
}
