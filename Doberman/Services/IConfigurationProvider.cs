using System;
using System.Configuration;
using System.Net.Configuration;
using Doberman.Model;

namespace Doberman.Services
{
    public interface IConfigurationProvider
    {
        string GetMongoConnectionString();
        string GetSqlConnectionString();
        SmtpSettings GetSmtpMailSettings();
    }
}
