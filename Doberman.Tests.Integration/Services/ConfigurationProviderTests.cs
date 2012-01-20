using System;
using NUnit.Framework;
using Doberman.Services;
using System.Configuration;

namespace Doberman.Tests.Integration.Services
{
    public class ConfigurationProviderTests
    {
        /// <summary>
        /// Tests that GetSqlConnectionString is able to retrieve the connection string for this computer.
        /// </summary>
        [Test]
        public void GetSqlConnectionString_ShouldRetrieveConnectionStringForComputerNameFromConfig()
        {
            string expectedSqlConnectionString = ConfigurationManager.ConnectionStrings[Environment.MachineName].ConnectionString;
            Assert.That(new ConfigurationProvider().GetSqlConnectionString(), Is.EqualTo(expectedSqlConnectionString));
        }

        /// <summary>
        /// Tests that GetMailSettings is able to retrieve the network host from the mail settings in the config.
        /// </summary>
        [Test]
        public void HasSmtpMailSettings_MailSettingsInConfig_ReturnsTrue()
        {
            Assert.That(new ConfigurationProvider().HasSmtpMailSettings(), Is.True);
        }

        /// <summary>
        /// Tests that GetMongoConnectionString is able to retrieve the connection string from the appSettings
        /// in the configuration file.
        /// </summary>
        [Test]
        public void GetMongoConnectionString_ShouldRetrieveConnectionStringFromConfig()
        {
            const string expectedSqlConnectionString = "mongodb://localhost/";
            Assert.That(new ConfigurationProvider().GetMongoConnectionString(), Is.EqualTo(expectedSqlConnectionString));
        }
    }
}
