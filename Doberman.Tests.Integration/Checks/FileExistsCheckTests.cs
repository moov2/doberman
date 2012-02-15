using System;
using NUnit.Framework;
using Doberman.Checks;

namespace Doberman.Tests.Integration.Checks
{
    public class FileExistsCheckTests
    {
        /// <summary>
        /// Tests that when the file exists it returns a successful
        /// doberman result.
        /// </summary>
        [Test]
        public void Execute_FileExists_Success()
        {
            Assert.That(new FileExistsCheck("Doberman.dll").Execute().Success, Is.True);
        }

        /// <summary>
        /// Tests that when the file doesn't exist a failed doberman
        /// result is returned.
        /// </summary>
        [Test]
        public void Execute_FileDoesntExist_Fail()
        {
            Assert.That(new FileExistsCheck("Web.config").Execute().Success, Is.False);
        }

        /// <summary>
        /// Tests that when the path to a file contains the base directory &
        /// the file exists the check still returns successful as expected.
        /// </summary>
        [Test]
        public void Execute_PathContainsFullPathAndFileExists_Success()
        {
            Assert.That(new FileExistsCheck(AppDomain.CurrentDomain.BaseDirectory + "\\Doberman.dll").Execute().Success, Is.True);
        }

        /// <summary>
        /// Tests that when the path is to a directory that exists,
        /// the check returns successful.
        /// </summary>
        [Test]
        public void Execute_PathIsADirectoryThatExists_Success()
        {
            Assert.That(new FileExistsCheck(AppDomain.CurrentDomain.BaseDirectory).Execute().Success, Is.True);
        }
    }
}
