using System;
using NUnit.Framework;
using Doberman.Checks;

namespace Doberman.Tests.Unit.Checks
{
    public class PageLoadsCheckTests
    {
        /// <summary>
        /// Tests that when creating PageLoadsCheck, the url passed into the constructor is
        /// set as the PageUrl.
        /// </summary>
        [Test]
        public void Constructor_ValidUrl_SetsPageUrl()
        {
            const string Url = "http://www.google.co.uk";

            Assert.That(new PageLoadsCheck(Url).PageUrl, Is.EqualTo(Url));
        }
    }
}
