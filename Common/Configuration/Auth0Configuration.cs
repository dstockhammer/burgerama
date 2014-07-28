using System.Configuration;

namespace Burgerama.Common.Configuration
{
    public sealed class Auth0Configuration : ConfigurationSection
    {
        public static Auth0Configuration Load(bool isRequired = true)
        {
            var config = ConfigurationManager.GetSection("burgerama/auth0") as Auth0Configuration;

            if (isRequired && config == null)
                throw new ConfigurationErrorsException("Configuration section not found: burgerama/auth0");

            return config;
        }

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
