using System;
using NUnit.Framework;
using Doberman.Checks;

namespace Doberman.Tests.Unit.Checks
{
    public class SavingFileCheckTests
    {
        /// <summary>
        /// Tests that when creating SavingFileCheck, the directory passed into the constructor is
        /// set as the Directory.
        /// </summary>
        [Test]
        public void Constructor_ValidDirectory_SetsDirectory()
        {
            const string directory = "www.google.co.uk";

            Assert.That(new SavingFileCheck(directory).Directory, Is.EqualTo(directory));
        }
    }
}
