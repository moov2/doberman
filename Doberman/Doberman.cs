using System;
using Doberman.Configuration;
using System.Collections.Generic;
using Doberman.Checks;
using Doberman.Model;

namespace Doberman
{
    public class Doberman
    {
        /// <summary>
        /// Current version of Doberman.
        /// </summary>
        public const string Version = "v0.1";

        /// <summary>
        /// Fetches a list of ICheck that can be passed to the Run method. The list is
        /// dictated by the configuration that is passed in.
        /// </summary>
        /// <param name="configuration">Dictates the list returned.</param>
        /// <returns>List of checks to be performed on a project.</returns>
        public IList<ICheck> Fetch(IConfiguration configuration)
        {
            if (configuration == null)
                throw (new Exception("Configuration must not be null."));

            var checks = new List<ICheck>();

            if (configuration.HasPagesToLoad)
            {
                foreach (var url in configuration.Pages)
                    checks.Add(new PageLoadsCheck(url));
            }

            if (configuration.HasPathsToExist)
            {
                foreach (var path in configuration.Paths)
                    checks.Add(new FileExistsCheck(path));
            }

            if (configuration.HasDirectoriesToSave)
            {
                foreach (var directory in configuration.Directories)
                    checks.Add(new SavingFileCheck(directory));
            }

            if (configuration.HasSqlConnectionStrings)
            {
                foreach (var sqlConnectionString in configuration.SqlConnectionStrings)
                    checks.Add(new ConnectToSqlServerCheck(sqlConnectionString));
            }

            if (configuration.HasMongoConnectionStrings)
            {
                foreach (var mongoConnectionString in configuration.MongoConnectionStrings)
                    checks.Add(new ConnectToMongoCheck(mongoConnectionString));
            }

            if (configuration.HasSmtpSettings)
            {
                foreach (var smtpSettings in configuration.SmtpSettings)
                    checks.Add(new SendingEmailCheck(smtpSettings));
            }

            return checks;
        }

        /// <summary>
        /// Loops over all the checks executing them, placing the result of the execute
        /// into a list that is returned.
        /// </summary>
        /// <param name="checks">Collection of checks to be performed.</param>
        /// <returns>Results from the checks.</returns>
        public IList<DobermanResult> Run(IList<ICheck> checks)
        {
            if (checks == null)
                throw (new Exception("Checks must not be null."));

            var results = new List<DobermanResult>();

            foreach (var check in checks)
                results.Add(check.Execute());

            return results;
        }
    }
}
