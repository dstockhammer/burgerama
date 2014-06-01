using System.Configuration;

namespace Burgerama.Common.Configuration
{
    public sealed class MongoDbConfiguration : ConfigurationSection
    {
        public static MongoDbConfiguration Load()
        {
            return ConfigurationManager.GetSection("burgerama/mongoDb") as MongoDbConfiguration;
        }

        [ConfigurationProperty("database", IsRequired = true)]
        public string Database
        {
            get { return (string)this["database"]; }
            set { this["database"] = value; }
        }

        [ConfigurationProperty("connectionString", IsRequired = true)]
        public string ConnectionString
        {
            get { return (string)this["connectionString"]; }
            set { this["connectionString"] = value; }
        }
    }
}
