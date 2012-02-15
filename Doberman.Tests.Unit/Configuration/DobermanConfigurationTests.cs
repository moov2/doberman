using System;
using NUnit.Framework;
using Doberman.Configuration;
using Moq;
using Doberman.Services;
using Doberman.Model;

namespace Doberman.Tests.Unit.Configuration
{
    public class DobermanConfigurationTests
    {
        /// <summary>
        /// Tests that by default the HasPageLoads is false.
        /// </summary>
        [Test]
        public void Default_HasPageLoadsIsFalse()
        {
            Assert.That(new DobermanConfiguration().HasPagesToLoad, Is.False);
        }

        /// <summary>
        /// Tests that by default the Pages collection is not null but empty.
        /// </summary>
        [Test]
        public void Default_PagesIsEmptyList()
        {
            Assert.That(new DobermanConfiguration().Pages.Count, Is.EqualTo(0));
        }

        /// <summary>
        /// Tests adding a page load with a string URL that URL is stored in the Pages list.
        /// </summary>
        [Test]
        public void CheckPageLoad_WithUrl_AddsUrlToPages()
        {
            const string url = "www.google.co.uk";

            var dobermanConfiguration = new DobermanConfiguration();
            dobermanConfiguration.CheckPageLoad(url);

            Assert.That(dobermanConfiguration.Pages.Contains(url), Is.True);
        }

        /// <summary>
        /// Tests that adding a page load sets HasPagesToLoad to true.
        /// </summary>
        [Test]
        public void CheckPageLoad_SetsHasPagesToLoadToTrue()
        {
            const string Url = "www.google.co.uk";

            var dobermanConfiguration = new DobermanConfiguration();
            dobermanConfiguration.CheckPageLoad(Url);

            Assert.That(dobermanConfiguration.HasPagesToLoad, Is.True);
        }

        /// <summary>
        /// Tests adding a page load with a Uri object, sets HasPagesToLoad to true.
        /// </summary>
        [Test]
        public void CheckPageLoad_WithUri_SetsHasPagesToLoadToTrue()
        {
            Uri url = new Uri("http://www.google.co.uk/");

            var dobermanConfiguration = new DobermanConfiguration();
            dobermanConfiguration.CheckPageLoad(url);

            Assert.That(dobermanConfiguration.HasPagesToLoad, Is.True);
        }

        /// <summary>
        /// Tests adding a page load with Uri object, adds a URL created from properties on the URI to the Pages list.
        /// </summary>
        [Test]
        public void CheckPageLoad_WithUri_AddsUrlFromUriToPages()
        {
            Uri url = new Uri("http://www.google.co.uk/testing/a/page/");
            const string expectedUrl = "http://www.google.co.uk";

            var dobermanConfiguration = new DobermanConfiguration();
            dobermanConfiguration.CheckPageLoad(url);

            Assert.That(dobermanConfiguration.Pages.Contains(expectedUrl), Is.True);
        }

        /// <summary>
        /// Tests that by default the HasDirectoriesToSave is false.
        /// </summary>
        [Test]
        public void Default_HasDirectoriesToSave()
        {
            Assert.That(new DobermanConfiguration().HasDirectoriesToSave, Is.False);
        }

        /// <summary>
        /// Tests that by default the Directories is empty.
        /// </summary>
        [Test]
        public void Default_DirectoriesIsEmpty()
        {
            Assert.That(new DobermanConfiguration().Directories.Count, Is.EqualTo(0));
        }

        /// <summary>
        /// Tests CheckFileSave adds directory to the Directories collection.
        /// </summary>
        [Test]
        public void CheckFileSave_WithDirectory_AddsToDirectories()
        {
            const string directory = "diretory/to/save/to";

            var dobermanConfiguration = new DobermanConfiguration();
            dobermanConfiguration.CheckFileSave(directory);

            Assert.That(dobermanConfiguration.Directories.Contains(directory), Is.True);
        }

        /// <summary>
        /// Tests when CheckFileSave is given a directory, the CheckSavesFile is set to true.
        /// </summary>
        [Test]
        public void CheckFileSave_WithDirectory_SetsHasDirectoriesToSaveToTrue()
        {
            const string directory = "diretory/to/save/to";

            var dobermanConfiguration = new DobermanConfiguration();
            dobermanConfiguration.CheckFileSave(directory);

            Assert.That(dobermanConfiguration.HasDirectoriesToSave, Is.True);
        }

        /// <summary>
        /// Tests that by default the HasSmtpSettings is false.
        /// </summary>
        [Test]
        public void Default_HasSmtpSettingsIsFalse()
        {
            Assert.That(new DobermanConfiguration().HasSmtpSettings, Is.False);
        }

        /// <summary>
        /// Tests that by default SmtpSettings is empty.
        /// </summary>
        [Test]
        public void Default_SmtpSettings_IsEmpty()
        {
            Assert.That(new DobermanConfiguration().SmtpSettings, Is.Empty);
        }

        /// <summary>
        /// Tests that by default HasSqlConnectionStrings is false.
        /// </summary>
        [Test]
        public void Default_HasSqlConnectionStrings_IsFalse()
        {
            Assert.That(new DobermanConfiguration().HasSqlConnectionStrings, Is.False);
        }

        /// <summary>
        /// Tests that by default the SqlConnectionStrings is an empty list.
        /// </summary>
        [Test]
        public void Default_SqlConnectionStrings_IsEmpty()
        {
            Assert.That(new DobermanConfiguration().SqlConnectionStrings.Count, Is.EqualTo(0));
        }

        /// <summary>
        /// Tests that CheckSql with a valid connection string sets HasSqlConnectionStrings to true.
        /// </summary>
        [Test]
        public void CheckSql_WithConnectionString_HasSqlConnectionStringsToTrue()
        {
            var connectionString = @"Data Source=.\SQLEXPRESS;Integrated Security=True;database=DobermanTest";

            var dobermanConfiguration = new DobermanConfiguration();
            dobermanConfiguration.CheckSql(connectionString);

            Assert.That(dobermanConfiguration.HasSqlConnectionStrings, Is.True);
        }

        /// <summary>
        /// Tests that CheckSql with a valid connection string adds the connection string to the SqlConnectionStrings
        /// list stored on the configuration.
        /// </summary>
        [Test]
        public void CheckSql_WithConnectionString_SetsSqlServerConnectionString()
        {
            var connectionString = @"Data Source=.\SQLEXPRESS;Integrated Security=True;database=DobermanTest";

            var dobermanConfiguration = new DobermanConfiguration();
            dobermanConfiguration.CheckSql(connectionString);

            Assert.That(dobermanConfiguration.SqlConnectionStrings.Contains(connectionString), Is.True);
        }

        /// <summary>
        /// Tests that by default HasMongoConnectionStrings is false.
        /// </summary>
        [Test]
        public void Default_HasMongoConnectionStrings_IsFalse()
        {
            Assert.That(new DobermanConfiguration().HasMongoConnectionStrings, Is.False);
        }

        /// <summary>
        /// Tests that by default MongoConnectionString is an empty list.
        /// </summary>
        [Test]
        public void Default_MongoConnectionStrings_IsEmpty()
        {
            Assert.That(new DobermanConfiguration().MongoConnectionStrings.Count, Is.EqualTo(0));
        }

        /// <summary>
        /// Tests that CheckMongo with a valid connection string sets HasMongoConnectionStrings to true.
        /// </summary>
        [Test]
        public void CheckMongo_WithConnectionString_HasMongoConnectionStringsToTrue()
        {
            var connectionString = @"mongodb://localhost/doberman-test";

            var dobermanConfiguration = new DobermanConfiguration();
            dobermanConfiguration.CheckMongo(connectionString);

            Assert.That(dobermanConfiguration.HasMongoConnectionStrings, Is.True);
        }

        /// <summary>
        /// Tests that CheckMongo adds the connection string to the MongoConnectionStrings list.
        /// </summary>
        [Test]
        public void CheckMongo_WithConnectionString_AddsConnectionStringToMongoConnectionStrings()
        {
            var connectionString = @"mongodb://localhost/doberman-test";

            var dobermanConfiguration = new DobermanConfiguration();
            dobermanConfiguration.CheckMongo(connectionString);

            Assert.That(dobermanConfiguration.MongoConnectionStrings.Contains(connectionString), Is.True);
        }

        /// <summary>
        /// Tests that when EnableEmailCheck is called when the web configuration has smtp mail settings,
        /// the CheckSendingEmail flag is set to true.
        /// </summary>
        [Test]
        public void Construct_ConfigurationHasSmtpSettings_AddsSmtpSetting()
        {
            var configurationProvider = new Mock<IConfigurationProvider>();
            configurationProvider.Setup(x => x.GetSmtpMailSettings()).Returns(new SmtpSettings { Host = "test.mailserver.com", Port = 434 });

            Assert.That(new DobermanConfiguration(configurationProvider.Object).SmtpSettings.Count, Is.EqualTo(1));
        }

        /// <summary>
        /// Tests that when the web configuration doesn't have smtp mail settings and EnableEmailCheck 
        /// is called, the CheckSendingEmail flag stays as false because the check can't be run if there are
        /// no SMTP settings.
        /// </summary>
        [Test]
        public void Construct_ConfigurationDoesntHaveSmtpSettings_SmtpSettingsIsEmpty()
        {
            var configurationProvider = new Mock<IConfigurationProvider>();
            configurationProvider.Setup(x => x.GetSmtpMailSettings()).Returns((SmtpSettings)null);

            Assert.That(new DobermanConfiguration(configurationProvider.Object).SmtpSettings, Is.Empty);
        }

        /// <summary>
        /// Tests that constructing DobermanConfiguration with an instance of ConfigurationProvider that retrieves a SQL connection
        /// string from the config successfully, adds that connection string to the SqlConnectionStrings list.
        /// </summary>
        [Test]
        public void ConstructWithConfigurationProvider_HasSqlConnectionString_AddsToSqlConnectionStrings()
        {
            const string expectedConnectionString = @"Data Source=.\SQLEXPRESS;Integrated Security=True;database=DobermanTests";

            var configurationProvider = new Mock<IConfigurationProvider>();
            configurationProvider.Setup(x => x.GetSqlConnectionString()).Returns(expectedConnectionString);

            Assert.That(new DobermanConfiguration(configurationProvider.Object).SqlConnectionStrings.Contains(expectedConnectionString), Is.True);
        }

        /// <summary>
        /// Tests that constructing DobermanConfiguration with an instance of ConfigurationProvider that fails to retrieve a SQL
        /// connection string from the config keeps the SqlConnectionStrings list empty.
        /// </summary>
        [Test]
        public void ConstructWithConfigurationProvider_HasNoSqlConnectionStringInConfig_SqlConnectionStringsStaysEmpty()
        {
            Assert.That(new DobermanConfiguration(new Mock<IConfigurationProvider>().Object).SqlConnectionStrings.Count, Is.EqualTo(0));
        }

        /// <summary>
        /// Tests that constructing DobermanConfiguration with an instance of ConfigurationProvider that says there is a
        /// Mongo connection string in the application config should add said connection string to the MongoConnectionStrings
        /// list.
        /// </summary>
        [Test]
        public void ConstructWithConfigurationProvider_HasMongoConnectionString_AddsToMongoConnectionStrings()
        {
            const string expectedConnectionString = @"mongodb://localhost/";

            var configurationProvider = new Mock<IConfigurationProvider>();
            configurationProvider.Setup(x => x.GetMongoConnectionString()).Returns(expectedConnectionString);

            Assert.That(new DobermanConfiguration(configurationProvider.Object).MongoConnectionStrings.Contains(expectedConnectionString), Is.True);
        }

        /// <summary>
        /// Tests that constructing DobermanConfiguration with an instance of ConfigurationProvider 
        /// that says there isn't a Mongo connection string in the application config should keep 
        /// MongoConnectionStrings list empty.
        /// </summary>
        [Test]
        public void ConstructWithConfigurationProvider_DoesntHaveMongoConnectionString_KeepsMongoConnectionStringsEmpty()
        {
            Assert.That(new DobermanConfiguration(new Mock<IConfigurationProvider>().Object).MongoConnectionStrings.Count, Is.EqualTo(0));
        }

        /// <summary>
        /// Tests when CheckEmail is given valid host and port, the settings are added to the
        /// SmtpSettings list.
        /// </summary>
        [Test]
        public void CheckEmail_ValidHostAndPort_ShouldAddToSmtpSettings()
        {
            const string host = "mail.host.com";
            const int port = 445;

            var dobermanConfiguration = new DobermanConfiguration();
            dobermanConfiguration.CheckEmail(host, port);

            Assert.That(dobermanConfiguration.SmtpSettings[0].Host, Is.EqualTo(host));
            Assert.That(dobermanConfiguration.SmtpSettings[0].Port, Is.EqualTo(port));
        }

        /// <summary>
        /// Tests when CheckEmail is not given the enableSsl parameter, it saves the SmtpSettings
        /// as not using ssl.
        /// </summary>
        [Test]
        public void CheckEmail_WithNoEnableSsl_ShouldSetSslToFalse()
        {
            const string host = "mail.host.com";
            const int port = 445;

            var dobermanConfiguration = new DobermanConfiguration();
            dobermanConfiguration.CheckEmail(host, port);

            Assert.That(dobermanConfiguration.SmtpSettings[0].Ssl, Is.False);
        }

        /// <summary>
        /// Tests when CheckEmail is given the enableSsl parameter, it saves the SmtpSettings
        /// as the value of enableSsl
        /// </summary>
        [Test]
        public void CheckEmail_WithEnableSsl_ShouldSetSslToTrue()
        {
            const string host = "mail.host.com";
            const int port = 445;

            var dobermanConfiguration = new DobermanConfiguration();
            dobermanConfiguration.CheckEmail(host, port, true);

            Assert.That(dobermanConfiguration.SmtpSettings[0].Ssl, Is.True);
        }

        /// <summary>
        /// Tests that by default HasPathsToExist is false.
        /// </summary>
        [Test]
        public void Default_HasPathsToExist_IsFalse()
        {
            Assert.That(new DobermanConfiguration().HasPathsToExist, Is.False);
        }

        /// <summary>
        /// Tests that by default Paths is an empty list.
        /// </summary>
        [Test]
        public void Default_Paths_IsEmpty()
        {
            Assert.That(new DobermanConfiguration().Paths.Count, Is.EqualTo(0));
        }

        /// <summary>
        /// Tests CheckFileExists adds path to the Paths collection.
        /// </summary>
        [Test]
        public void CheckFileExists_WithPath_AddsToPath()
        {
            const string path = "diretory/to/check/exists";

            var dobermanConfiguration = new DobermanConfiguration();
            dobermanConfiguration.CheckFileExists(path);

            Assert.That(dobermanConfiguration.Paths.Contains(path), Is.True);
        }

        /// <summary>
        /// Tests when CheckFileExists is given a directory, the HasPathsToExist is set to true.
        /// </summary>
        [Test]
        public void CheckFileSave_WithPath_SetsHasPathsToExistToTrue()
        {
            const string path = "diretory/to/check/exists";

            var dobermanConfiguration = new DobermanConfiguration();
            dobermanConfiguration.CheckFileExists(path);

            Assert.That(dobermanConfiguration.HasPathsToExist, Is.True);
        }

    }
}
