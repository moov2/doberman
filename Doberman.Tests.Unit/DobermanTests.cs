using System;
using System.Linq;
using NUnit.Framework;
using AutoMoq;
using Doberman.Configuration;
using Doberman.Checks;
using System.Collections.Generic;
using Moq;
using Doberman.Model;
using Doberman.Services;

namespace Doberman.Tests.Unit
{
    public class DobermanTests
    {
        private AutoMoqer _autoMoqer;

        [SetUp]
        public void SetUp()
        {
            _autoMoqer = new AutoMoqer();
        }

        /// <summary>
        /// Tests that when the configuration parameter, passed to the Fetch method, is
        /// null an exception is thrown.
        /// </summary>
        [Test]
        public void Fetch_ConfigurationIsNull_Throws()
        {
            Assert.Throws<Exception>(() => new Doberman().Fetch(null));
        }

        /// <summary>
        /// Tests that when the configuration has pages, the checks returned from Fetch has a PageLoadsCheck per
        /// page in the Pages list.
        /// </summary>
        [Test]
        public void Fetch_ConfigurationWithPages_ReturnsPageLoadsCheckEqualToTheNumberOfPages()
        {
            var configuration = _autoMoqer.GetMock<IConfiguration>();
            configuration.Setup(x => x.HasPagesToLoad).Returns(true);
            configuration.Setup(x => x.Pages).Returns(new List<string>() { "www.google.co.uk", "www.moov2.com" });

            Assert.That(GetCountOfInstanceFromFetch<PageLoadsCheck>(configuration.Object), Is.EqualTo(2));
        }

        /// <summary>
        /// Tests that when the configuration has no pages, the checks returned from Fetch has no PageLoadsCheck.
        /// </summary>
        [Test]
        public void Fetch_ConfigurationWithNoPages_ReturnDoesntContainPageLoadsCheck()
        {
            var configuration = _autoMoqer.GetMock<IConfiguration>();
            configuration.Setup(x => x.HasPagesToLoad).Returns(false);
            configuration.Setup(x => x.Pages).Returns(new List<string>());

            Assert.That(GetCountOfInstanceFromFetch<PageLoadsCheck>(configuration.Object), Is.EqualTo(0));
        }

        /// <summary>
        /// Tests that when CheckHomePageLoads is true in the configuration passed to the Fetch method, the instance
        /// of HomePageLoadsCheck has the HomePageUrl that is stored in the configuration.
        /// </summary>
        [Test]
        public void Fetch_ConfigurationHasPages_PageLoadsCheckHasUrlsFromConfiguration()
        {
            const string ExpectedUrl = "www.google.co.uk";

            var configuration = _autoMoqer.GetMock<IConfiguration>();
            configuration.Setup(x => x.HasPagesToLoad).Returns(true);
            configuration.Setup(x => x.Pages).Returns(new List<string>() { ExpectedUrl });

            var pageLoadsCheck = GetInstanceFromFetch<PageLoadsCheck>(configuration.Object)[0] as PageLoadsCheck;

            Assert.That(pageLoadsCheck.PageUrl, Is.EqualTo(ExpectedUrl));
        }

        /// <summary>
        /// Tests that when Run is passed null it throws an exception.
        /// </summary>
        [Test]
        public void Run_ChecksIsNull_Throws()
        {
            Assert.Throws<Exception>(() => new Doberman().Run(null));
        }

        /// <summary>
        /// Tests that when Run is given a list of ICheck's, they are all executed.
        /// </summary>
        [Test]
        public void Run_WithChecks_ShouldExecuteAllOfThem()
        {
            var mockChecks = CreateMockChecks();
            var checks = new List<ICheck>();

            foreach (var mockCheck in mockChecks)
                checks.Add(mockCheck.Object);

            new Doberman().Run(checks);

            foreach (var mockCheck in mockChecks)
                mockCheck.Mock.Verify(x => x.Execute(), Times.Once());
        }

        /// <summary>
        /// Tests that when Run is given a list of checks, it returns a collection containing
        /// each of the results from the checks.
        /// </summary>
        [Test]
        public void Run_WithChecks_ReturnsListOfResultsFromChecks()
        {
            var mockChecks = CreateMockChecks();
            var checks = new List<ICheck>();

            foreach (var mockCheck in mockChecks)
                checks.Add(mockCheck.Object);

            var result = new Doberman().Run(checks);

            Assert.That(result.Count, Is.EqualTo(mockChecks.Count));

            for (var i = 0; i < result.Count; i++)
            {
                Assert.That(result[i], Is.EqualTo(mockChecks[i].ExpectedResult));
            }
        }

        /// <summary>
        /// Tests that the collection returned from Fetch contains a SavingFileCheck in the collection of checks, when
        /// the configuration passed to Fetch has Directories.
        /// </summary>
        [Test]
        public void Fetch_ConfigurationHasDirectories_ReturnsSavingFileCheckEqualToTheNumberOfDirectoriess()
        {
            var configuration = _autoMoqer.GetMock<IConfiguration>();
            configuration.Setup(x => x.HasDirectoriesToSave).Returns(true);
            configuration.Setup(x => x.Directories).Returns(new List<string>() { "directory/to/save", "another/directory" });

            Assert.That(GetCountOfInstanceFromFetch<SavingFileCheck>(configuration.Object), Is.EqualTo(2));
        }

        /// <summary>
        /// Tests that the collection returned from Fetch doesn't contain a SavingFileCheck in the collection of
        /// checks, when the configuration passed to Fetch has no Directories.
        /// </summary>
        [Test]
        public void Fetch_ConfigurationHasNoDirectories_ReturnsDoesntContainSavingFileCheck()
        {
            var configuration = _autoMoqer.GetMock<IConfiguration>();
            configuration.Setup(x => x.HasDirectoriesToSave).Returns(false);
            configuration.Setup(x => x.Directories).Returns(new List<string>());

            Assert.That(GetCountOfInstanceFromFetch<SavingFileCheck>(configuration.Object), Is.EqualTo(0));
        }

        /// <summary>
        /// Tests that when the configuration given to Fetch has Directories, the SavingFileCheck in the checks
        /// returned has the same directory value as in the Directories in the configuration.
        /// </summary>
        [Test]
        public void Fetch_ConfigurationHasCheckSaveFileAsTrue_SavingFileCheckHasSaveFileDirectoryFromConfiguration()
        {
            const string ExpectedDirectory = "/directory/to/save/to";

            var configuration = _autoMoqer.GetMock<IConfiguration>();
            configuration.Setup(x => x.HasDirectoriesToSave).Returns(true);
            configuration.Setup(x => x.Directories).Returns(new List<string>() { ExpectedDirectory });

            var savingFileCheck = GetInstanceFromFetch<SavingFileCheck>(configuration.Object)[0] as SavingFileCheck;

            Assert.That(savingFileCheck.Directory, Is.EqualTo(ExpectedDirectory));
        }

        /// <summary>
        /// Tests that the collection returned from Fetch contains a ConnectToSqlServerCheck in the collection 
        /// of checks, when the configuration passed to Fetch has sql connection strings.
        /// </summary>
        [Test]
        public void Fetch_ConfigurationHasSqlConnectioNStrings_ReturnsSameNumberOfConnectToSqlServerCheckAsConfiguration()
        {
            var configuration = _autoMoqer.GetMock<IConfiguration>();
            configuration.Setup(x => x.HasSqlConnectionStrings).Returns(true);
            configuration.Setup(x => x.SqlConnectionStrings).Returns(new List<string>() { "sql-connection/string", "another-connection-string" });

            Assert.That(GetCountOfInstanceFromFetch<ConnectToSqlServerCheck>(configuration.Object), Is.EqualTo(2));
        }

        /// <summary>
        /// Tests that the collection returned from Fetch doesn't contain a ConnectToSqlServerCheck in the 
        /// collection of checks, when the configuration passed to Fetch has no sql connection strings.
        /// </summary>
        [Test]
        public void Fetch_ConfigurationHasCheckConnectToSqlServerAsFalse_ReturnsDoesntContainConnectToSqlServerCheck()
        {
            var configuration = _autoMoqer.GetMock<IConfiguration>();
            configuration.Setup(x => x.HasSqlConnectionStrings).Returns(false);
            configuration.Setup(x => x.SqlConnectionStrings).Returns(new List<string>());

            Assert.That(GetCountOfInstanceFromFetch<ConnectToSqlServerCheck>(configuration.Object), Is.EqualTo(0));
        }

        /// <summary>
        /// Tests that when the configuration given to Fetch has connection strings, the ConnectToSqlServerCheck in the checks 
        /// returned have the same connection string value as the property in the configuration.
        /// </summary>
        [Test]
        public void Fetch_ConfigurationHasSqlConnectionStrings_ConnectToSqlServerCheckHasConnectionStringFromConfiguration()
        {
            var expectedConnectionString = @"Data Source=.\SQLEXPRESS;Integrated Security=True;database=DobermanTest";

            var configuration = _autoMoqer.GetMock<IConfiguration>();
            configuration.Setup(x => x.HasSqlConnectionStrings).Returns(true);
            configuration.Setup(x => x.SqlConnectionStrings).Returns(new List<string>() { expectedConnectionString });

            var connectToSqlServerCheck = GetInstanceFromFetch<ConnectToSqlServerCheck>(configuration.Object)[0] as ConnectToSqlServerCheck;

            Assert.That(connectToSqlServerCheck.ConnectionString, Is.EqualTo(expectedConnectionString));
        }

        /// <summary>
        /// Tests that the collection returned from Fetch contains a ConnectToMongoCheck in the collection 
        /// of checks, when the configuration passed to Fetch has mongo connection strings.
        /// </summary>
        [Test]
        public void Fetch_ConfigurationHasMongoConnectionStrings_ReturnsSameNumberOfConnectToMongoCheckAsConfiguration()
        {
            var configuration = _autoMoqer.GetMock<IConfiguration>();
            configuration.Setup(x => x.HasMongoConnectionStrings).Returns(true);
            configuration.Setup(x => x.MongoConnectionStrings).Returns(new List<string>() { "mongodb://localhost", "mongodb://moov2.com" });

            Assert.That(GetCountOfInstanceFromFetch<ConnectToMongoCheck>(configuration.Object), Is.EqualTo(2));
        }

        /// <summary>
        /// Tests that the collection returned from Fetch doesn't contain any mongo connection strings in the 
        /// collection of checks, when the configuration passed to Fetch has no mongo connection strings.
        /// </summary>
        [Test]
        public void Fetch_ConfigurationHasNoMongoConnectionStrings_ReturnsDoesntContainConnectToMongoCheck()
        {
            var configuration = _autoMoqer.GetMock<IConfiguration>();
            configuration.Setup(x => x.HasMongoConnectionStrings).Returns(false);
            configuration.Setup(x => x.MongoConnectionStrings).Returns(new List<string>());

            Assert.That(GetCountOfInstanceFromFetch<ConnectToMongoCheck>(configuration.Object), Is.EqualTo(0));
        }

        /// <summary>
        /// Tests that when the configuration given to Fetch has a mongo connection string, the ConnectToMongoCheck in 
        /// the checks returned has the same connection string value as the property in the configuration.
        /// </summary>
        [Test]
        public void Fetch_ConfigurationHasMongoConnectionString_ConnectToMongoCheckHasConnectionStringFromConfiguration()
        {
            var expectedConnectionString = @"mongodb://localhost/doberman-test";

            var configuration = _autoMoqer.GetMock<IConfiguration>();
            configuration.Setup(x => x.HasMongoConnectionStrings).Returns(true);
            configuration.Setup(x => x.MongoConnectionStrings).Returns(new List<string>() { expectedConnectionString });

            var connectToMongoCheck = GetInstanceFromFetch<ConnectToMongoCheck>(configuration.Object)[0] as ConnectToMongoCheck;

            Assert.That(connectToMongoCheck.ConnectionString, Is.EqualTo(expectedConnectionString));
        }

        /// <summary>
        /// Tests that the collection returned from Fetch contains a SendingEmailCheck in the collection 
        /// of checks, when the configuration passed to Fetch has smtp settings.
        /// </summary>
        [Test]
        public void Fetch_ConfigurationHasSmtpSettings_ReturnsSameNumberOfSendingEmailCheckAsConfiguration()
        {
            var configuration = _autoMoqer.GetMock<IConfiguration>();
            configuration.Setup(x => x.HasSmtpSettings).Returns(true);
            configuration.Setup(x => x.SmtpSettings).Returns(new List<SmtpSettings>() { new SmtpSettings { Host = "mail.test.com", Port = 145 }, new SmtpSettings { Host = "mail.another.com", Port = 145 } });

            Assert.That(GetCountOfInstanceFromFetch<SendingEmailCheck>(configuration.Object), Is.EqualTo(2));
        }

        /// <summary>
        /// Tests that the collection returned from Fetch doesn't contain any sending email checks in the 
        /// collection of checks, when the configuration passed to Fetch has no smtp settings.
        /// </summary>
        [Test]
        public void Fetch_ConfigurationHasNoSmtpSettings_ReturnsDoesntContainSendingEmailCheck()
        {
            var configuration = _autoMoqer.GetMock<IConfiguration>();
            configuration.Setup(x => x.HasSmtpSettings).Returns(false);
            configuration.Setup(x => x.SmtpSettings).Returns(new List<SmtpSettings>());

            Assert.That(GetCountOfInstanceFromFetch<SendingEmailCheck>(configuration.Object), Is.EqualTo(0));
        }

        /// <summary>
        /// Tests that when the configuration given to Fetch has a smtp setting, the SendingEmailCheck in 
        /// the checks returned has the same smtp settings value as the property in the configuration.
        /// </summary>
        [Test]
        public void Fetch_ConfigurationHasSmtpSettings_SendingEmailCheckHasSmtpSettingsFromConfiguration()
        {
            var expectedSmtpSetting = new SmtpSettings { Host = "mail.host.com", Port = 145 };

            var configuration = _autoMoqer.GetMock<IConfiguration>();
            configuration.Setup(x => x.HasSmtpSettings).Returns(true);
            configuration.Setup(x => x.SmtpSettings).Returns(new List<SmtpSettings>() { expectedSmtpSetting });

            var sendingEmailCheck = GetInstanceFromFetch<SendingEmailCheck>(configuration.Object)[0] as SendingEmailCheck;

            Assert.That(sendingEmailCheck.SmtpSettings, Is.EqualTo(expectedSmtpSetting));
        }

        /// <summary>
        /// Creates a list of mock check objects.
        /// </summary>
        /// <returns>List of mock check objects.</returns>
        private IList<MockCheck> CreateMockChecks()
        {
            var mockChecks = new List<MockCheck>();
            mockChecks.Add(new MockCheck(new DobermanResult { Success = true, Check = "Save file to disk." }));
            mockChecks.Add(new MockCheck(new DobermanResult { Success = false, Check = "Send e-mail." }));
            mockChecks.Add(new MockCheck(new DobermanResult { Success = true, Check = "Load home page." }));

            return mockChecks;
        }

        /// <summary>
        /// Fetches the count of the instances of ICheck returned from the Fetch method that is of type T.
        /// </summary>
        /// <typeparam name="T">Expected type to be matched.</typeparam>
        /// <param name="configuration">Configuration passed to the Fetch method.</param>
        /// <returns>Count of ICheck that match the expected type.</returns>
        private int GetCountOfInstanceFromFetch<T>(IConfiguration configuration)
        {
            return GetInstanceFromFetch<T>(configuration).Count;
        }

        /// <summary>
        /// Gets a list of all the ICheck returned from the Fetch method that match an expected type.
        /// </summary>
        /// <typeparam name="T">Expected type to be matched.</typeparam>
        /// <param name="configuration">Configuration passed to the Fetch method.</param>
        /// <returns>List of ICheck that match expected type.</returns>
        private List<ICheck> GetInstanceFromFetch<T>(IConfiguration configuration)
        {
            return new Doberman().Fetch(configuration).Where(x => x is T).ToList();
        }
    }

    public class MockCheck
    {
        public DobermanResult ExpectedResult { get; set; }
        public Mock<ICheck> Mock { get; private set; }

        public ICheck Object
        {
            get { return Mock.Object; }
        }

        public MockCheck(DobermanResult expectedResult)
        {
            ExpectedResult = expectedResult;

            Mock = new Mock<ICheck>();
            Mock.Setup(x => x.Execute()).Returns(ExpectedResult);
        }
    }
}
