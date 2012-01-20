using System;
using Doberman.Model;
using System.Data.SqlClient;

namespace Doberman.Checks
{
    public class ConnectToSqlServerCheck : ICheck
    {
        private const string CheckName = "Connect to Sql Server";

        public string ConnectionString { get; private set; }

        public ConnectToSqlServerCheck(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public DobermanResult Execute()
        {
            var result = new DobermanResult { Check = CheckName };

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                }

                result.Detail = "Can connect to SQL Server just fine.";
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
