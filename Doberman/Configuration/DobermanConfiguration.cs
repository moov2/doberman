using System;
using Doberman.Services;
using System.Collections.Generic;

namespace Doberman.Configuration
{
    public class DobermanConfiguration : IConfiguration
    {
        /// <summary>
        /// Flag indicating whether a check should be done for sending an email.
        /// </summary>
        public bool CheckSendingEmail { get; private set; }

        /// <summary>
        /// Flag indicating there are any directories to be saved too.
        /// </summary>
        public bool HasDirectoriesToSave
        {
            get { return Directories.Count > 0; }
        }

        /// <summary>
        /// Flag indicating whether there are any Mongo connection strings.
        /// </summary>
        public bool HasMongoConnectionStrings
        {
            get { return MongoConnectionStrings.Count > 0; }
        }

        /// <summary>
        /// Flag indicating if there are pages to be tested.
        /// </summary>
        public bool HasPagesToLoad
        {
            get { return Pages.Count > 0; }
        }

        /// <summary>
        /// Flag indicating whether there are any SQL connection strings.
        /// </summary>
        public bool HasSqlConnectionStrings
        {
            get { return SqlConnectionStrings.Count > 0; }
        }

        /// <summary>
        /// List of directories to perform save file checks on.
        /// </summary>
        public IList<string> Directories { get; private set; }

        /// <summary>
        /// Used to send the dummy email.
        /// </summary>
        public IEmailProvider EmailProvider { get; private set; }

        /// <summary>
        /// List of pages to perform load tests on.
        /// </summary>
        public IList<string> Pages { get; private set; }

        /// <summary>
        /// List of sql connection strings to perform connection tests on.
        /// </summary>
        public IList<string> SqlConnectionStrings { get; private set; }

        /// <summary>
        /// Connection strings to be used for connecting to a Mongo Server.
        /// </summary>
        public IList<string> MongoConnectionStrings { get; private set; }

        public DobermanConfiguration()
        {
            Directories = new List<string>();
            MongoConnectionStrings = new List<string>();
            Pages = new List<string>();
            SqlConnectionStrings = new List<string>();

            EmailProvider = new DobermanEmailProvider();
        }

        public DobermanConfiguration(IConfigurationProvider configurationProvider) : this()
        {
            AddMongoConnectionString(configurationProvider.GetMongoConnectionString());
            AddSqlConnectionString(configurationProvider.GetSqlConnectionString());
            CheckSendingEmail = configurationProvider.HasSmtpMailSettings();
        }

        /// <summary>
        /// Sets the provider used to send an email.
        /// </summary>
        /// <param name="emailProvider">Provider used to send an email.</param>
        /// <returns>Itself.</returns>
        public DobermanConfiguration SetEmailProvider(IEmailProvider emailProvider)
        {
            if (emailProvider == null)
                throw (new Exception("emailProvider must not be null."));

            EmailProvider = emailProvider;
            return this;
        }

        /// <summary>
        /// Adds a directory path to the Directories to be saved too.
        /// </summary>
        /// <param name="directory">Directory path to be saved too.</param>
        /// <returns>Itself.</returns>
        public DobermanConfiguration AddDirectorySave(string directory)
        {
            Directories.Add(directory);
            return this;
        }

        /// <summary>
        /// Adds a URL to the Pages to be loaded.
        /// </summary>
        /// <param name="url">URL to a web page.</param>
        /// <returns>Itself.</returns>
        public DobermanConfiguration AddPageLoad(string url)
        {
            Pages.Add(url);
            return this;
        }

        /// <summary>
        /// Adds a URL to the Pages to be loaded.
        /// </summary>
        /// <param name="url">URL to a web page.</param>
        /// <returns>Itself.</returns>
        public DobermanConfiguration AddPageLoad(Uri url)
        {
            return AddPageLoad(url.Scheme + "://" + url.Authority);
        }

        /// <summary>
        /// Adds a connection string to be used to connect to a SQL database.
        /// </summary>
        /// <param name="connectionString">Connection string.</param>
        /// <returns>Itself.</returns>
        public DobermanConfiguration AddSqlConnectionString(string connectionString)
        {
            if (!String.IsNullOrEmpty(connectionString))
                SqlConnectionStrings.Add(connectionString);

            return this;
        }

        /// <summary>
        /// Adds a MongoConnectionString to be used to attempt to connect to the database.
        /// </summary>
        /// <param name="connectionString">Connection string for the database.</param>
        /// <returns>Itself.</returns>
        public DobermanConfiguration AddMongoConnectionString(string connectionString)
        {
            if (!String.IsNullOrEmpty(connectionString))
                MongoConnectionStrings.Add(connectionString);

            return this;
        }

        /// <summary>
        /// Enables the checking that an email can be sent.
        /// </summary>
        /// <returns>Itself.</returns>
        public DobermanConfiguration EnableEmailCheck()
        {
            CheckSendingEmail = true;
            return this;
        }
    }
}
