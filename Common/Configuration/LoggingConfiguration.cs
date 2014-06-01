using System.Configuration;

namespace Burgerama.Common.Configuration
{
    public sealed class LoggingConfiguration : ConfigurationSection
    {
        public static LoggingConfiguration Load()
        {
            return ConfigurationManager.GetSection("burgerama/logging") as LoggingConfiguration;
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
