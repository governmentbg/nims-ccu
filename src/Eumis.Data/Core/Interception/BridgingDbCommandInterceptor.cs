using System.Data.Common;
using System.Data.Entity.Infrastructure.Interception;
using System.Linq;

namespace Eumis.Data.Core.Interception
{
    public class BridgingDbCommandInterceptor : IDbCommandInterceptor
    {
        public void NonQueryExecuted(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            foreach (var dbContext in interceptionContext.DbContexts.OfType<BridgingDbContext>())
            {
                dbContext.NonQueryExecuted(command, interceptionContext);
            }
        }

        public void NonQueryExecuting(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            foreach (var dbContext in interceptionContext.DbContexts.OfType<BridgingDbContext>())
            {
                dbContext.NonQueryExecuting(command, interceptionContext);
            }
        }

        public void ReaderExecuted(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            foreach (var dbContext in interceptionContext.DbContexts.OfType<BridgingDbContext>())
            {
                dbContext.ReaderExecuted(command, interceptionContext);
            }
        }

        public void ReaderExecuting(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            foreach (var dbContext in interceptionContext.DbContexts.OfType<BridgingDbContext>())
            {
                dbContext.ReaderExecuting(command, interceptionContext);
            }
        }

        public void ScalarExecuted(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            foreach (var dbContext in interceptionContext.DbContexts.OfType<BridgingDbContext>())
            {
                dbContext.ScalarExecuted(command, interceptionContext);
            }
        }

        public void ScalarExecuting(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            foreach (var dbContext in interceptionContext.DbContexts.OfType<BridgingDbContext>())
            {
                dbContext.ScalarExecuting(command, interceptionContext);
            }
        }
    }
}
