using System.Configuration;

namespace Burgerama.Common.Configuration
{
    public sealed class RabbitMqConfiguration : ConfigurationSection
    {
        [ConfigurationProperty("server", IsRequired = true)]
        public string Server
        {
            get { return (string)this["server"]; }
            set { this["server"] = value; }
        }

        [ConfigurationProperty("vHost", IsRequired = true)]
        public string VHost
        {
            get { return (string)this["vHost"]; }
            set { this["vHost"] = value; }
        }

        [ConfigurationProperty("userName", IsRequired = true)]
        public string UserName
        {
            get { return (string)this["userName"]; }
            set { this["userName"] = value; }
        }

        [ConfigurationProperty("password", IsRequired = true)]
        public string Password
        {
            get { return (string)this["password"]; }
            set { this["password"] = value; }
        }
    }
}
