﻿using System;
using Doberman.Services;
using System.Collections.Generic;
using Doberman.Model;

namespace Doberman.Configuration
{
    public class DobermanConfiguration : IConfiguration, IDobermanConfigurator
    {
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
        /// Flag indicating if there are paths to be tested.
        /// </summary>
        public bool HasPathsToExist
        {
            get { return Paths.Count > 0; }
        }

        /// <summary>
        /// Flag indicating whether a check should be done for sending an email.
        /// </summary>
        public bool HasSmtpSettings
        {
            get { return SmtpSettings.Count > 0; }
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
        /// List of Smtp network settings.
        /// </summary>
        public IList<SmtpSettings> SmtpSettings { get; private set; }

        /// <summary>
        /// List of pages to perform load tests on.
        /// </summary>
        public IList<string> Pages { get; private set; }

        /// <summary>
        /// List of path urls to perform file exist checks on.
        /// </summary>
        public IList<string> Paths { get; private set; }

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
            Paths = new List<string>();
            SqlConnectionStrings = new List<string>();
            SmtpSettings = new List<SmtpSettings>();
        }

        public DobermanConfiguration(IConfigurationProvider configurationProvider) : this()
        {
            CheckMongo(configurationProvider.GetMongoConnectionString());
            CheckEmail(configurationProvider.GetSmtpMailSettings());
            CheckSql(configurationProvider.GetSqlConnectionString());
        }

        /// <summary>
        /// Adds an SmtpSettings to the SmtpSettings collection.
        /// </summary>
        /// <param name="smtpSettings">Contains details about an Smtp server to check.</param>
        /// <returns>Itself.</returns>
        private DobermanConfiguration CheckEmail(SmtpSettings smtpSettings)
        {
            if (smtpSettings != null)
                SmtpSettings.Add(smtpSettings);

            return this;
        }

        /// <summary>
        /// Enables the checking that an email can be sent.
        /// </summary>
        /// <param name="host">Network host of SMTP server.</param>
        /// <param name="port">Port of the SMTP server.</param>
        /// <returns>Itself.</returns>
        public IDobermanConfigurator CheckEmail(string host, int port)
        {
            return CheckEmail(host, port, false);
        }

        /// <summary>
        /// Enables the checking that an email can be sent.
        /// </summary>
        /// <param name="host">Network host of SMTP server.</param>
        /// <param name="port">Port of the SMTP server.</param>
        /// <param name="enableSsl">Flag indicating whether to use ssl.</param>
        /// <returns>Itself.</returns>
        public IDobermanConfigurator CheckEmail(string host, int port, bool enableSsl)
        {
            return CheckEmail(new SmtpSettings { Host = host, Port = port, Ssl = enableSsl });
        }

        /// <summary>
        /// Adds a path to a file or directory to check if it exists.
        /// </summary>
        /// <param name="path">Path to check if it exists as a file or directory.</param>
        /// <returns>Itself.</returns>
        public IDobermanConfigurator CheckFileExists(string path)
        {
            Paths.Add(path);
            return this;
        }

        /// <summary>
        /// Adds a directory path to the Directories to be saved too.
        /// </summary>
        /// <param name="directory">Directory path to be saved too.</param>
        /// <returns>Itself.</returns>
        public IDobermanConfigurator CheckFileSave(string directory)
        {
            Directories.Add(directory);
            return this;
        }

        /// <summary>
        /// Adds a MongoConnectionString to be used to attempt to connect to the database.
        /// </summary>
        /// <param name="connectionString">Connection string for the database.</param>
        /// <returns>Itself.</returns>
        public IDobermanConfigurator CheckMongo(string connectionString)
        {
            if (!String.IsNullOrEmpty(connectionString))
                MongoConnectionStrings.Add(connectionString);

            return this;
        }

        /// <summary>
        /// Adds a URL to the Pages to be loaded.
        /// </summary>
        /// <param name="url">URL to a web page.</param>
        /// <returns>Itself.</returns>
        public IDobermanConfigurator CheckPageLoad(string url)
        {
            Pages.Add(url);
            return this;
        }

        /// <summary>
        /// Adds a URL to the Pages to be loaded.
        /// </summary>
        /// <param name="url">URL to a web page.</param>
        /// <returns>Itself.</returns>
        public IDobermanConfigurator CheckPageLoad(Uri url)
        {
            return CheckPageLoad(url.Scheme + "://" + url.Authority);
        }

        /// <summary>
        /// Adds a connection string to be used to connect to a SQL database.
        /// </summary>
        /// <param name="connectionString">Connection string.</param>
        /// <returns>Itself.</returns>
        public IDobermanConfigurator CheckSql(string connectionString)
        {
            if (!String.IsNullOrEmpty(connectionString))
                SqlConnectionStrings.Add(connectionString);

            return this;
        }
    }

    public interface IDobermanConfigurator
    {
        IDobermanConfigurator CheckEmail(string host, int port);
        IDobermanConfigurator CheckEmail(string host, int port, bool enableSsl);
        IDobermanConfigurator CheckFileExists(string path);
        IDobermanConfigurator CheckFileSave(string directory);
        IDobermanConfigurator CheckMongo(string connectionString);
        IDobermanConfigurator CheckPageLoad(string url);
        IDobermanConfigurator CheckPageLoad(Uri url);
        IDobermanConfigurator CheckSql(string connectionString);
    }
}
