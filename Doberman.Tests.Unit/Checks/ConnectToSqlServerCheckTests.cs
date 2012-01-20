using System;
using NUnit.Framework;
using Doberman.Checks;

namespace Doberman.Tests.Unit.Checks
{
    public class ConnectToSqlServerCheckTests
    {
        /// <summary>
        /// Tests that when creating PageLoadsCheck, the url passed into the constructor is
        /// set as the PageUrl.
        /// </summary>
        [Test]
        public void Constructor_ValidConnectionString_SetsConnectionString()
        {
            const string connectionString = @"Data Source=.\SQLEXPRESS;Integrated Security=True;database=DobermanTest";

            Assert.That(new ConnectToSqlServerCheck(connectionString).ConnectionString, Is.EqualTo(connectionString));
        }
    }
}
