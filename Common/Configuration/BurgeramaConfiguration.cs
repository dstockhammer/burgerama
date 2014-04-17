using System.Configuration;

namespace Burgerama.Common.Configuration
{
    public sealed class BurgeramaConfiguration : ConfigurationSection
    {
        [ConfigurationProperty("serviceKey", IsRequired = true)]
        public string ServiceKey
        {
            get { return (string)this["serviceKey"]; }
            set { this["serviceKey"] = value; }
        }
    }
}
