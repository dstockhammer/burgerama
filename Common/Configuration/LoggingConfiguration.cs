using System.Configuration;

namespace Burgerama.Common.Configuration
{
    public sealed class LoggingConfiguration : ConfigurationSection
    {
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

        [ConfigurationProperty("logentriesKey", IsRequired = false)]
        public string LogentriesKey
        {
            get { return (string)this["logentriesKey"]; }
            set { this["logentriesKey"] = value; }
        }

        [ConfigurationProperty("useSeq", IsRequired = false, DefaultValue = false)]
        public bool UseSeq
        {
            get { return (bool)this["useSeq"]; }
            set { this["useSeq"] = value; }
        }

        [ConfigurationProperty("seqUrl", IsRequired = false)]
        public string SeqUrl
        {
            get { return (string)this["seqUrl"]; }
            set { this["seqUrl"] = value; }
        }

        [ConfigurationProperty("seqKey", IsRequired = false)]
        public string SeqKey
        {
            get { return (string)this["seqKey"]; }
            set { this["seqKey"] = value; }
        }
    }
}
