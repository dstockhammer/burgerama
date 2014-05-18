using System;
using System.Configuration;
using System.Diagnostics.Contracts;
using System.Reflection;
using System.Web;
using Autofac;
using Burgerama.Common.Configuration;
using Seq;
using Serilog;

namespace Burgerama.Common.Logging
{
    public sealed class LoggingModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(GetLogger).As<ILogger>().SingleInstance();
        }

        private static ILogger GetLogger(IComponentContext context)
        {
            Contract.Requires<ArgumentNullException>(context != null);
            Contract.Ensures(Contract.Result<ILogger>() != null);

            var config = ConfigurationManager.GetSection("burgerama/logging") as LoggingConfiguration;
            if (config == null)
                throw new ConfigurationErrorsException("Logging must be configured.");

            var loggerConfig = new LoggerConfiguration();

            if (config.UseConsole)
            {
                loggerConfig.WriteTo.ColoredConsole();
            }

            if (config.UseLogentries)
            {
                if (string.IsNullOrWhiteSpace(config.LogentriesKey))
                    throw new ConfigurationErrorsException("To use Logentries, a key must be specified.");

                loggerConfig.WriteTo.Logentries(config.LogentriesKey);
            }

            if (config.UseSeq)
            {
                if (string.IsNullOrWhiteSpace(config.SeqUrl))
                    throw new ConfigurationErrorsException("To use Seq, a url must be specified.");

                loggerConfig.WriteTo.Seq(config.SeqUrl, apiKey: config.SeqKey);
            }

            loggerConfig.Enrich.WithProperty("Machine", Environment.MachineName);
            loggerConfig.Enrich.WithProperty("Application", GetEntryAssembly().GetName().Name);
                
            var logger = loggerConfig.CreateLogger();

            // assigning the logger to the global static logger is a necessary evil, because some
            // external libraries depend on this. the logger should never be used this way;
            // always resolve it using dependency injection!
            Log.Logger = logger;
            
            return logger;
        }

        private static Assembly GetEntryAssembly()
        {
            return Assembly.GetEntryAssembly() ?? GetWebEntryAssembly();
        }

        private static Assembly GetWebEntryAssembly()
        {
            if (HttpContext.Current == null || HttpContext.Current.ApplicationInstance == null)
                return null;

            var type = HttpContext.Current.ApplicationInstance.GetType();
            while (type != null && type.Namespace == "ASP")
            {
                type = type.BaseType;
            }

            return type == null ? null : type.Assembly;
        }
    }
}
