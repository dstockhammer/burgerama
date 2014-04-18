using System.Configuration;

namespace Burgerama.Common.Configuration
{
    public sealed class ServiceConfiguration : ConfigurationSection
    {
        [ConfigurationProperty("key", IsRequired = true)]
        public string Key
        {
            get { return (string)this["key"]; }
            set { this["key"] = value; }
        }

        [ConfigurationProperty("mongoDb")]
        public MongoDbConfiguration MongoDb
        {
            get { return (MongoDbConfiguration)this["mongoDb"]; }
            set { this["mongoDb"] = value; }
        }

        [ConfigurationProperty("auth0")]
        public Auth0Configuration Auth0
        {
            get { return (Auth0Configuration)this["auth0"]; }
            set { this["auth0"] = value; }
        }
    }
}
