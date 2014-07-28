using System.Configuration;

namespace Burgerama.Common.Configuration
{
    public sealed class LoggingConfiguration : ConfigurationSection
    {
        public static LoggingConfiguration Load(bool isRequired = true)
        {
            var config = ConfigurationManager.GetSection("burgerama/logging") as LoggingConfiguration;

            if (isRequired && config == null)
                throw new ConfigurationErrorsException("Configuration section not found: burgerama/logging");

            return config;
        }

        [ConfigurationProperty("useConsole", IsRequired = false, DefaultValue = false)]
        public bool UseConsole
        {
            get { return (bool)this["useConsole"]; }
            set { this["useConsole"] = value; }
        }

        [ConfigurationProperty("useLogentries", IsRequired = false, DefaultValue = false)]
        public bool UseLogentries
        {
            get { return (bool)this["useLogentries"]; }
            set { this["useLogentries"] = value; }
        }

        [ConfigurationProperty("logentriesKey", IsRequired = false, DefaultValue = "")]
        public string LogentriesKey
        {
            get { return (string)this["logentriesKey"]; }
            set { this["logentriesKey"] = value; }
        }
    }
}
