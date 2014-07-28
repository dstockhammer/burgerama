using System.Configuration;

namespace Burgerama.Common.Configuration
{
    public sealed class ApiConfiguration : ConfigurationSection
    {
        public static ApiConfiguration Load(bool isRequired = true)
        {
            var config = ConfigurationManager.GetSection("burgerama/api") as ApiConfiguration;

            if (isRequired && config == null)
                throw new ConfigurationErrorsException("Configuration section not found: burgerama/api");

            return config;
        }

        [ConfigurationProperty("routeAddress", IsRequired = true)]
        public string RouteAddress
        {
            get { return (string)this["routeAddress"]; }
            set { this["routeAddress"] = value; }
        }
    }
}
