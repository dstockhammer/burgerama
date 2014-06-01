using System.Configuration;

namespace Burgerama.Common.Configuration
{
    public sealed class ServiceConfiguration : ConfigurationSection
    {
        public static ServiceConfiguration Load()
        {
            return ConfigurationManager.GetSection("burgerama/service") as ServiceConfiguration;
        }

        [ConfigurationProperty("key", IsRequired = true)]
        public string Key
        {
            get { return (string)this["key"]; }
            set { this["key"] = value; }
        }
    }
}
