using Eumis.Common.Log;
using System.Data.Common;
using System.Data.Entity.Infrastructure.Interception;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Eumis.Data.Core.Interception
{
    public class LoggingEumisDbCommandInterceptor : IEumisDbCommandInterceptor
    {
        private ILogger logger;
        private Stopwatch sw = new Stopwatch();

        public LoggingEumisDbCommandInterceptor(ILogger logger)
        {
            this.logger = logger;
        }

        public int Order
        {
            get { return 1000; }
        }

        public void NonQueryExecuted(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            this.CommandExecuted(command, interceptionContext);
        }

        public void NonQueryExecuting(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            this.CommandExecuting(command, interceptionContext);
        }

        public void ReaderExecuted(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            this.CommandExecuted(command, interceptionContext);
        }

        public void ReaderExecuting(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            this.CommandExecuting(command, interceptionContext);
        }

        public void ScalarExecuted(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            this.CommandExecuted(command, interceptionContext);
        }

        public void ScalarExecuting(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            this.CommandExecuting(command, interceptionContext);
        }

        [SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "May be used in the future.")]
        private void CommandExecuting<T>(DbCommand command, DbCommandInterceptionContext<T> interceptionContext)
        {
            if (this.sw.IsRunning)
            {
                this.logger.Log(LogLevel.Warn, "Executing more than one queries/commands at the same time for a single DbContext.");
            }

            this.sw.Restart();
        }

        [SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "May be used in the future.")]
        private void CommandExecuted<T>(DbCommand command, DbCommandInterceptionContext<T> interceptionContext)
        {
            this.sw.Stop();

            if (this.sw.Elapsed.Milliseconds >= 10000)
            {
                this.logger.Log(LogLevel.Warn, "Slow query!\n" + this.CreateCommandText(command, this.sw.Elapsed.Milliseconds));
            }
        }

        private string CreateCommandText(DbCommand command, int ms)
        {
            StringBuilder commandText = new StringBuilder();

            commandText.AppendFormat("Query executed in {0}ms\n", ms);

            commandText.AppendLine(command.CommandText);

            foreach (DbParameter param in command.Parameters)
            {
                commandText.AppendFormat("@{0} = {1}\n", param.ParameterName, param.Value.ToString());
            }

            return commandText.ToString();
        }
    }
}
