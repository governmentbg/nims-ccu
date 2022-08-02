using DbUp;
using System;
using System.Configuration;

namespace Eumis.Database.Updater
{
    public static class Program
    {
        public static void Main()
        {
            var dbup = DeployChanges.To
                .SqlDatabase(ConfigurationManager.ConnectionStrings["DbContext"].ConnectionString.ExpandEnv())
                .JournalToSqlTable("dbo", "UpdateScripts")
                .WithScripts(new CustomEmbeddedScriptProvider())
                .WithExecutionTimeout(TimeSpan.FromHours(1))
                .WithTransactionPerScript()
                .LogToConsole()
                .Build();

            var result = dbup.PerformUpgrade();

            if (!result.Successful)
            {
                Environment.ExitCode = 1;
            }
        }
    }
}
