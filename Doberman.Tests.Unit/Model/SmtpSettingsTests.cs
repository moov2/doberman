using System;
using NUnit.Framework;
using Doberman.Model;

namespace Doberman.Tests.Unit.Model
{
    public class SmtpSettingsTests
    {
        /// <summary>
        /// Tests that by default the Ssl value is false.
        /// </summary>
        [Test]
        public void Default_SslIsFalse()
        {
            Assert.That(new SmtpSettings().Ssl, Is.False);
        }
    }
}
