using System.Configuration;

namespace Burgerama.Common.Configuration
{
    public sealed class Auth0Configuration : ConfigurationSection
    {
        [ConfigurationProperty("issuer", IsRequired = true)]
        public string Issuer
        {
            get { return (string)this["issuer"]; }
            set { this["issuer"] = value; }
        }

        [ConfigurationProperty("audience", IsRequired = true)]
        public string Audience
        {
            get { return (string)this["audience"]; }
            set { this["audience"] = value; }
        }

        [ConfigurationProperty("secret", IsRequired = true)]
        public string Secret
        {
            get { return (string)this["secret"]; }
            set { this["secret"] = value; }
        }
    }
}
