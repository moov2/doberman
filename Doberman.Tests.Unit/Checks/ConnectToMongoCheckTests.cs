using System;
using NUnit.Framework;
using Doberman.Checks;

namespace Doberman.Tests.Unit.Checks
{
    public class ConnectToMongoCheckTests
    {
        /// <summary>
        /// Tests that when creating PageLoadsCheck, the url passed into the constructor is
        /// set as the PageUrl.
        /// </summary>
        [Test]
        public void Constructor_ValidConnectionString_SetsConnectionString()
        {
            const string connectionString = @"mongodb://localhost/doberman-test";

            Assert.That(new ConnectToMongoCheck(connectionString).ConnectionString, Is.EqualTo(connectionString));
        }
    }
}
