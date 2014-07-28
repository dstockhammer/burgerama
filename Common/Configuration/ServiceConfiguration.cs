using System.Configuration;

namespace Burgerama.Common.Configuration
{
    public sealed class ServiceConfiguration : ConfigurationSection
    {
        public static ServiceConfiguration Load(bool isRequired = true)
        {
            var config = ConfigurationManager.GetSection("burgerama/service") as ServiceConfiguration;

            if (isRequired && config == null)
                throw new ConfigurationErrorsException("Configuration section not found: burgerama/service");

            return config;
        }

        [ConfigurationProperty("key", IsRequired = true)]
        public string Key
        {
            get { return (string)this["key"]; }
            set { this["key"] = value; }
        }
    }
}
