using System;
using System.Net.Configuration;
using System.Configuration;
using Doberman.Model;

namespace Doberman.Services
{
    public class ConfigurationProvider : IConfigurationProvider
    {
        /// <summary>
        /// Attempts to locate a MongoServer key in the AppSettings, if there isn't one then null is returned, 
        /// otherwise the value is returned.
        /// </summary>
        /// <returns>MongoServer key value in the application configuration.</returns>
        public string GetMongoConnectionString()
        {
            return ConfigurationManager.AppSettings["MongoServer"];
        }

        /// <summary>
        /// Gets the SQL connection string that matches the machine name. If there isn't a connection string
        /// in the config, then null is returned.
        /// </summary>
        /// <returns>SQL connection string from the configuration.</returns>
        public string GetSqlConnectionString()
        {
            try
            {
                return ConfigurationManager.ConnectionStrings[Environment.MachineName].ConnectionString;
            }
            catch { return null; }
        }

        /// <summary>
        /// Retrieves the Smtp settings from the configuration file.
        /// </summary>
        /// <returns>Host & Port of the smtp network if exists in configuration file, otherwise false.</returns>
        public SmtpSettings GetSmtpMailSettings()
        {
            var network = (ConfigurationManager.GetSection("system.net/mailSettings/smtp") as SmtpSection).Network;

            if (network == null)
                return null;

            return new SmtpSettings { Host = network.Host, Port = network.Port, Ssl = network.EnableSsl };
        }
    }
}
