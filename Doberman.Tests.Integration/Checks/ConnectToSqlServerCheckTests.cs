using System;
using NUnit.Framework;
using Doberman.Checks;
using System.Configuration;

namespace Doberman.Tests.Integration.Checks
{
    public class ConnectToSqlServerCheckTests
    {
        /// <summary>
        /// Tests that when executing check can't connect to SQL server the check is
        /// deemed to have failed.
        /// </summary>
        [Test]
        public void Execute_CantConnectToSqlServer_Fail()
        {
            const string invalidConnectionString = @"Data Source=.\SQLEXPRESS;Integrated Security=True;database=DobermanDoesntExistTest";
            Assert.That(new ConnectToSqlServerCheck(invalidConnectionString).Execute().Success, Is.False);
        }

        /// <summary>
        /// Tests that when executing check can connect to SQL server the check is deemed
        /// to be a success.
        /// </summary>
        [Test]
        public void Execute_ConnectToSqlServer_Success()
        {
            string connectionString = ConfigurationManager.ConnectionStrings[Environment.MachineName].ConnectionString;

            Assert.That(new ConnectToSqlServerCheck(connectionString).Execute().Success, Is.True);
        }
    }
}
