using System;
using Doberman.Model;
using MongoDB.Driver;

namespace Doberman.Checks
{
    public class ConnectToMongoCheck : ICheck
    {
        private const string CheckName = "Connect to Mongo Database";

        public string ConnectionString { get; private set; }

        public ConnectToMongoCheck(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public DobermanResult Execute()
        {
            var result = new DobermanResult { Check = CheckName };

            try
            {
                MongoServer server = MongoServer.Create(ConnectionString);
                server.Connect();
                server.Disconnect();

                result.Detail = "Can connect to Mongo DB just fine.";
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Detail = ex.Message;
                result.Success = false;
            }

            return result;
        }
    }
}
