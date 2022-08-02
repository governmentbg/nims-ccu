using NLog.Config;
using NLog.Targets;
using System;

namespace Eumis.Common.NLog
{
    public static class NLogExtensions
    {
        public static void SetDatabaseTargetConnectionString(this LoggingConfiguration loggingConfiguration, string targetName, string connectionString)
        {
            var dbTarget = loggingConfiguration.FindTargetByName(targetName) as DatabaseTarget;

            if (dbTarget == null)
            {
                throw new Exception("Cannot find DatabaseTarget with name \"" + targetName + "\"");
            }

            dbTarget.ConnectionString = connectionString;
        }
    }
}
