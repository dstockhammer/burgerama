using System.Configuration;

namespace Burgerama.Common.Configuration
{
    public sealed class ApiConfiguration : ConfigurationSection
    {
        [ConfigurationProperty("routeAddress", IsRequired = true)]
        public string RouteAddress
        {
            get { return (string)this["routeAddress"]; }
            set { this["routeAddress"] = value; }
        }
    }
}
