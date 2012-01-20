using System;
using Doberman.Services;
using System.Collections.Generic;

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

        IList<string> Directories { get; }
        IList<string> MongoConnectionStrings { get; }
        IList<string> Pages { get; }
        IList<string> SqlConnectionStrings { get; }
    }
}
