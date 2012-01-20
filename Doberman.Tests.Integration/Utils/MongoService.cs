using System;
using System.ServiceProcess;

namespace Doberman.Tests.Integration.Utils
{
    public class MongoService
    {
        private static int MongoTimeout = 20000;
        private static string MongoServiceName = "Mongo DB";

        /// <summary>
        /// Checks to see if the Mongo DB service is currently running.
        /// </summary>
        /// <returns>True if currently running, otherwise false.</returns>
        public static bool Running()
        {
            var service = new ServiceController(MongoServiceName);
            var result = (service.Status == ServiceControllerStatus.Running);
            service.Dispose();
            return result;
        }

        /// <summary>
        /// Starts the Mongo.
        /// </summary>
        public static void Start()
        {
            var service = new ServiceController(MongoServiceName);

            if (service.Status == ServiceControllerStatus.Running)
            {
                service.Dispose();
                return;
            }

            try
            {
                TimeSpan timeout = TimeSpan.FromMilliseconds(MongoTimeout);
                service.Start();
                service.WaitForStatus(ServiceControllerStatus.Running, timeout);
            }
            finally
            {
                service.Dispose();
            }
        }

        /// <summary>
        /// Stops the Mongo service.
        /// </summary>
        public static void Stop()
        {
            var service = new ServiceController(MongoServiceName);

            if (service.Status == ServiceControllerStatus.Stopped)
            {
                service.Dispose();
                return;
            }

            try
            {
                TimeSpan timeout = TimeSpan.FromMilliseconds(MongoTimeout);
                service.Stop();
                service.WaitForStatus(ServiceControllerStatus.Running, timeout);
            }
            catch { }
            finally
            {
                service.Dispose();
            }
        }
    }
}
