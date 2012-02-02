using System;
using Doberman.Services;
using System.Collections.Generic;
using Doberman.Model;

namespace Doberman.Configuration
{
    public interface IConfiguration
    {
        bool HasDirectoriesToSave { get; }
        bool HasMongoConnectionStrings { get; }
        bool HasPagesToLoad { get; }
        bool HasSqlConnectionStrings { get; }

        bool CheckSendingEmail { get; }

        IEmailProvider EmailProvider { get; }
        EmailCheckSettings EmailSettings { get; }

        IList<string> Directories { get; }
        IList<string> MongoConnectionStrings { get; }
        IList<string> Pages { get; }
        IList<string> SqlConnectionStrings { get; }
    }
}
