using System;
using System.Configuration;
using System.Net.Configuration;

namespace Doberman.Services
{
    public interface IConfigurationProvider
    {
        string GetMongoConnectionString();
        string GetSqlConnectionString();
        bool HasSmtpMailSettings();
    }
}
