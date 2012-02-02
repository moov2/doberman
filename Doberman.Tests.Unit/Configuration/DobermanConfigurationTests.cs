using System;
using NUnit.Framework;
using Doberman.Configuration;
using Moq;
using Doberman.Services;

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
        public void AddPageLoad_WithUrl_AddsUrlToPages()
        {
            const string Url = "www.google.co.uk";
            Assert.That(new DobermanConfiguration().AddPageLoad(Url).Pages.Contains(Url), Is.True);
        }

        /// <summary>
        /// Tests that adding a page load sets HasPagesToLoad to true.
        /// </summary>
        [Test]
        public void AddPageLoad_SetsHasPagesToLoadToTrue()
        {
            const string Url = "www.google.co.uk";
            Assert.That(new DobermanConfiguration().AddPageLoad(Url).HasPagesToLoad, Is.True);
        }

        /// <summary>
        /// Tests adding a page load with a Uri object, sets HasPagesToLoad to true.
        /// </summary>
        [Test]
        public void AddPageLoad_WithUri_SetsHasPagesToLoadToTrue()
        {
            Uri url = new Uri("http://www.google.co.uk/");
            Assert.That(new DobermanConfiguration().AddPageLoad(url).HasPagesToLoad, Is.True);
        }

        /// <summary>
        /// Tests adding a page load with Uri object, adds a URL created from properties on the URI to the Pages list.
        /// </summary>
        [Test]
        public void AddPageLoad_WithUri_AddsUrlFromUriToPages()
        {
            Uri url = new Uri("http://www.google.co.uk/testing/a/page/");
            const string expectedUrl = "http://www.google.co.uk";

            Assert.That(new DobermanConfiguration().AddPageLoad(url).Pages.Contains(expectedUrl), Is.True);
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
        /// Tests AddDirectorySave adds directory to the Directories collection.
        /// </summary>
        [Test]
        public void AddDirectorySave_WithDirectory_AddsToDirectories()
        {
            const string directory = "diretory/to/save/to";
            Assert.That(new DobermanConfiguration().AddDirectorySave(directory).Directories.Contains(directory), Is.True);
        }

        /// <summary>
        /// Tests when SetSaveFileDirectory is given a directory, the CheckSavesFile is set to true.
        /// </summary>
        [Test]
        public void AddDirectorySave_WithDirectory_SetsHasDirectoriesToSaveToTrue()
        {
            const string directory = "diretory/to/save/to";
            Assert.That(new DobermanConfiguration().AddDirectorySave(directory).HasDirectoriesToSave, Is.True);
        }

        /// <summary>
        /// Tests that by default the CheckSendingEmail is false.
        /// </summary>
        [Test]
        public void Default_CheckSendingEmailIsFalse()
        {
            Assert.That(new DobermanConfiguration().CheckSendingEmail, Is.False);
        }

        /// <summary>
        /// Tests when SetEmailProvider is given a provider, the EmailProvider is set.
        /// </summary>
        [Test]
        public void SetEmailProvider_WithProvider_SetsEmailProvider()
        {
            var provider = new Mock<IEmailProvider>();
            Assert.That(new DobermanConfiguration().SetEmailProvider(provider.Object).EmailProvider, Is.EqualTo(provider.Object));
        }

        /// <summary>
        /// Tests when SetEmailProvider is given null, an exception is thrown.
        /// </summary>
        [Test]
        public void SetEmailProvider_ProviderIsNull_Throws()
        {
            var configuration = new DobermanConfiguration();
            Assert.Throws<Exception>(() => configuration.SetEmailProvider(null));
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
        /// Tests that AddSqlConnectionString with a valid connection string sets HasSqlConnectionStrings to true.
        /// </summary>
        [Test]
        public void AddSqlConnectionString_WithConnectionString_HasSqlConnectionStringsToTrue()
        {
            var connectionString = @"Data Source=.\SQLEXPRESS;Integrated Security=True;database=DobermanTest";
            Assert.That(new DobermanConfiguration().AddSqlConnectionString(connectionString).HasSqlConnectionStrings, Is.True);
        }

        /// <summary>
        /// Tests that AddSqlConnectionString with a valid connection string adds the connection string to the SqlConnectionStrings
        /// list stored on the configuration.
        /// </summary>
        [Test]
        public void AddSqlConnectionString_WithConnectionString_SetsSqlServerConnectionString()
        {
            var connectionString = @"Data Source=.\SQLEXPRESS;Integrated Security=True;database=DobermanTest";
            Assert.That(new DobermanConfiguration().AddSqlConnectionString(connectionString).SqlConnectionStrings.Contains(connectionString), Is.True);
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
        /// Tests that AddMongoConnectionString with a valid connection string sets HasMongoConnectionStrings to true.
        /// </summary>
        [Test]
        public void AddMongoConnectionString_WithConnectionString_HasMongoConnectionStringsToTrue()
        {
            var connectionString = @"mongodb://localhost/doberman-test";
            Assert.That(new DobermanConfiguration().AddMongoConnectionString(connectionString).HasMongoConnectionStrings, Is.True);
        }

        /// <summary>
        /// Tests that AddMongoConnectionString adds the connection string to the MongoConnectionStrings list.
        /// </summary>
        [Test]
        public void AddMongoConnectionString_WithConnectionString_AddsConnectionStringToMongoConnectionStrings()
        {
            var connectionString = @"mongodb://localhost/doberman-test";
            Assert.That(new DobermanConfiguration().AddMongoConnectionString(connectionString).MongoConnectionStrings.Contains(connectionString), Is.True);
        }

        /// <summary>
        /// Tests that by default the EmailProvider is an instance of DobermanEmailProvider.
        /// </summary>
        [Test]
        public void Default_EmailProvider_InstanceOfDobermanEmailProvider()
        {
            Assert.That(new DobermanConfiguration().EmailProvider, Is.InstanceOf<DobermanEmailProvider>());
        }

        /// <summary>
        /// Tests that when EnableEmailCheck is called when the web configuration has smtp mail settings,
        /// the CheckSendingEmail flag is set to true.
        /// </summary>
        [Test]
        public void EnableEmailCheck_WhenConfigurationHasSmtpMailSettings_SetsCheckSendingEmailToTrue()
        {
            var configurationProvider = new Mock<IConfigurationProvider>();
            configurationProvider.Setup(x => x.HasSmtpMailSettings()).Returns(true);

            Assert.That(new DobermanConfiguration(configurationProvider.Object).EnableEmailCheck("from@email.com", "to@email.com").CheckSendingEmail, Is.True);
        }

        /// <summary>
        /// Tests that when the web configuration doesn't have smtp mail settings and EnableEmailCheck 
        /// is called, the CheckSendingEmail flag stays as false because the check can't be run if there are
        /// no SMTP settings.
        /// </summary>
        [Test]
        public void EnableEmailCheck_WhenConfigurationDoesntHaveSmtpMailSettings_KeepsCheckSendingEmailAsFalse()
        {
            var configurationProvider = new Mock<IConfigurationProvider>();
            configurationProvider.Setup(x => x.HasSmtpMailSettings()).Returns(false);

            Assert.That(new DobermanConfiguration(configurationProvider.Object).EnableEmailCheck("from@email.com", "to@email.com").CheckSendingEmail, Is.False);
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
        /// Tests that constructing DobermanConfiguration with an instance of ConfigurationProvider that says there are mail
        /// settings in the config, will set CheckSendingEmail to true.
        /// </summary>
        [Test]
        public void ConstructWithConfigurationProvider_HasMailSettings_SetsHasSmtpMailSettingsToTrue()
        {
            var configurationProvider = new Mock<IConfigurationProvider>();
            configurationProvider.Setup(x => x.HasSmtpMailSettings()).Returns(true);

            Assert.That(new DobermanConfiguration(configurationProvider.Object).HasSmtpMailSettings, Is.True);
        }

        /// <summary>
        /// Tests that constructing DobermanConfiguration with an instance of ConfigurationProvider that says there aren't 
        /// any mailSettings in the config, will keep CheckSendingEmail to false.
        /// </summary>
        [Test]
        public void ConstructWithConfigurationProvider_DoesntHaveMailSettings_SetsCheckSendingEmailToFalse()
        {
            Assert.That(new DobermanConfiguration(new Mock<IConfigurationProvider>().Object).CheckSendingEmail, Is.False);
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
        /// Tests that constructing DobermanConfiguration with an instance of ConfigurationProvider that says there isn't a Mongo
        /// connection string in the application config should keep MongoConnectionStrings list empty.
        /// </summary>
        [Test]
        public void ConstructWithConfigurationProvider_DoesntHaveMongoConnectionString_KeepsMongoConnectionStringsEmpty()
        {
            Assert.That(new DobermanConfiguration(new Mock<IConfigurationProvider>().Object).MongoConnectionStrings.Count, Is.EqualTo(0));
        }
    }
}
