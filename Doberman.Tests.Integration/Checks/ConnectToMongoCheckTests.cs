using System;
using NUnit.Framework;
using Doberman.Checks;
using System.ServiceProcess;
using MongoDB.Driver;
using Doberman.Tests.Integration.Utils;

namespace Doberman.Tests.Integration.Checks
{
    public class ConnectToMongoCheckTests
    {
        private static string MongoServerName = "mongodb://localhost/";

        private bool _mongoWasRunning = false;

        [SetUp]
        public void SetUp()
        {
            _mongoWasRunning = MongoService.Running();

            //StartMongo();
            //CreateDobermanTestDatabase();
        }

        [TearDown]
        public void TearDown()
        {
            if (_mongoWasRunning)
                MongoService.Start();
            else
                MongoService.Stop();
        }

        /// <summary>
        /// Tests that checking if can connect to a mongo database fails when the Mongo 
        /// service isn't running.
        /// </summary>
        [Test]
        public void Execute_MongoServiceIsStopped_Fail()
        {
            MongoService.Stop();

            Assert.That(new ConnectToMongoCheck(MongoServerName).Execute().Success, Is.False);
        }

        /// <summary>
        /// Tests that checking if can connect to a mongo database succeeds when the Mongo 
        /// service is running.
        /// </summary>
        [Test]
        public void Execute_MongoServiceIsRunning_Success()
        {
            MongoService.Start();

            Assert.That(new ConnectToMongoCheck(MongoServerName).Execute().Success, Is.True);
        }
    }
}
