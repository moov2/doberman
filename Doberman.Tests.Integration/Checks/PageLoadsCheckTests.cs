using System;
using NUnit.Framework;
using Doberman.Checks;

namespace Doberman.Tests.Integration.Checks
{
    public class PageLoadsCheckTests
    {
        /// <summary>
        /// Tests that when a valid url is passed to the page loads check, the execute returns
        /// a successful result.
        /// </summary>
        [Test]
        public void Execute_ValidUrl_Success()
        {
            const string ValidUrl = "http://www.google.co.uk";

            Assert.That(new PageLoadsCheck(ValidUrl).Execute().Success, Is.True);
        }

        /// <summary>
        /// Tests that when an invalid url is passed to the page loads check, the execute returns
        /// a failure result.
        /// </summary>
        [Test]
        public void Execute_InvalidUrl_Fail()
        {
            string ValidUrl = "http://www.thiswillneverwork" + Guid.NewGuid() + "-ever.co.uk";

            Assert.That(new PageLoadsCheck(ValidUrl).Execute().Success, Is.False);
        }
    }
}
