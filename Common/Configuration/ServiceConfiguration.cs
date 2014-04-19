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
    }
}
