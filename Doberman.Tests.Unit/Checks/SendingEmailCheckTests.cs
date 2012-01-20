using System;
using NUnit.Framework;
using Moq;
using Doberman.Services;
using Doberman.Checks;

namespace Doberman.Tests.Unit.Checks
{
    public class SendingEmailCheckTests
    {
        /// <summary>
        /// Tests that when creating SendingEmailCheck, the email provider passed into the constructor is
        /// set as the EmailProvider.
        /// </summary>
        [Test]
        public void Constructor_ValidEmailProvider_SetsEmailProvider()
        {
            var emailProvider = new Mock<IEmailProvider>().Object;

            Assert.That(new SendingEmailCheck(emailProvider).EmailProvider, Is.EqualTo(emailProvider));
        }

        /// <summary>
        /// Tests that when executing the SendingEmailCheck and the EmailProvider throws an error.
        /// </summary>
        [Test]
        public void Execute_EmailProviderThrows_Fail()
        {
            var emailProvider = new Mock<IEmailProvider>();
            emailProvider.Setup(x => x.Send(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Throws(new Exception("Unable to send an email."));

            var sendingEmailCheck = new SendingEmailCheck(emailProvider.Object);
            Assert.That(sendingEmailCheck.Execute().Success, Is.False);
        }

        /// <summary>
        /// Tests that when executing the SendingEmailCheck and the EmailProvider sends the email successfully, the
        /// result given is consider successful.
        /// </summary>
        [Test]
        public void Execute_EmailProviderNoProblems_Success()
        {
            var emailProvider = new Mock<IEmailProvider>();

            var sendingEmailCheck = new SendingEmailCheck(emailProvider.Object);
            Assert.That(sendingEmailCheck.Execute().Success, Is.True);

            emailProvider.Verify(x => x.Send(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once());
        }
    }
}
