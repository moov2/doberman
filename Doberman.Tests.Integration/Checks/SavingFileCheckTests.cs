using System;
using NUnit.Framework;
using System.IO;
using Doberman.Checks;

namespace Doberman.Tests.Integration.Checks
{
    public class SavingFileCheckTests
    {
        [SetUp]
        public void SetUp()
        {
            var validDirectory = AppDomain.CurrentDomain.BaseDirectory + "\\valid\\";

            if (!Directory.Exists(validDirectory))
                Directory.CreateDirectory(validDirectory);
        }

        [TearDown]
        public void TearDown()
        {
            try
            {
                Directory.Delete(AppDomain.CurrentDomain.BaseDirectory + "\\valid\\", true);
            } catch { }
        }

        /// <summary>
        /// Performing a SavingFileCheck should fail when the directory doesn't exist.
        /// </summary>
        [Test]
        public void Execute_DirectoryDoesntExists_Fail()
        {
            var invalidDirectory = AppDomain.CurrentDomain.BaseDirectory + "\\fake\\directory\\";
            Assert.That(new SavingFileCheck(invalidDirectory).Execute().Success, Is.False);
        }

        /// <summary>
        /// Performing a SavingFileCheck should succeed when the directory exists, and the file
        /// is able to save.
        /// </summary>
        [Test]
        public void Execute_DirectoryExists_Success()
        {
            var validDirectory = AppDomain.CurrentDomain.BaseDirectory + "\\valid\\";
           
            Assert.That(new SavingFileCheck(validDirectory).Execute().Success, Is.True);
        }

        /// <summary>
        /// Tests that the file that is saved to check if a file can be saved to the disk should be
        /// deleted after use.
        /// </summary>
        [Test]
        public void Execute_ShouldDeleteTestFile()
        {
            var validDirectory = AppDomain.CurrentDomain.BaseDirectory + "\\valid\\";

            new SavingFileCheck(validDirectory).Execute();

            Assert.That(File.Exists(validDirectory + SavingFileCheck.DummyFileName), Is.False);
        }

        /// <summary>
        /// Tests that when the directory passed to the SaveFileChecks class doesn't have the base 
        /// directory but has slashes either side, the check still succeeds.
        /// </summary>
        [Test]
        public void Execute_WithoutBaseDirectoryPathButSlashesEitherSide_Success()
        {
            var validDirectory = "\\valid\\";

            Assert.That(new SavingFileCheck(validDirectory).Execute().Success, Is.True);
        }

        /// <summary>
        /// Tests that when the directory passed to the SaveFileChecks class doesn't have the base 
        /// directory but has slashes at the front, the check still succeeds.
        /// </summary>
        [Test]
        public void Execute_WithoutBaseDirectoryPathButSlashesInFront_Success()
        {
            var validDirectory = "\\valid";

            Assert.That(new SavingFileCheck(validDirectory).Execute().Success, Is.True);
        }

        /// <summary>
        /// Tests that when the directory passed to the SaveFileChecks class doesn't have the base 
        /// directory but has slashes at the back, the check still succeeds.
        /// </summary>
        [Test]
        public void Execute_WithoutBaseDirectoryPathButSlashesAtBack_Success()
        {
            var validDirectory = "valid//";

            Assert.That(new SavingFileCheck(validDirectory).Execute().Success, Is.True);
        }

        /// <summary>
        /// Tests that when the directory passed to the SaveFileChecks class doesn't have the base 
        /// directory and no slashes, the check still succeeds.
        /// </summary>
        [Test]
        public void Execute_WithoutBaseDirectoryPathAndNoSlashes_Success()
        {
            var validDirectory = "valid";

            Assert.That(new SavingFileCheck(validDirectory).Execute().Success, Is.True);
        }
    }
}
