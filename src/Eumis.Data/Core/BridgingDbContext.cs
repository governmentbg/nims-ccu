using Eumis.Data.Core.Interception;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.Interception;
using System.Linq;

namespace Eumis.Data.Core
{
    internal class BridgingDbContext : DbContext, IDbCommandInterceptor
    {
        private IEnumerable<IEumisDbCommandInterceptor> interceptors;

        public BridgingDbContext(
            IEnumerable<IEumisDbCommandInterceptor> interceptors,
            string nameOrConnectionString,
            DbCompiledModel model)
            : base(nameOrConnectionString, model)
        {
            this.interceptors = interceptors.OrderBy(i => i.Order);
        }

        public void NonQueryExecuted(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            foreach (var interceptor in this.interceptors)
            {
                interceptor.NonQueryExecuted(command, interceptionContext);
            }
        }

        public void NonQueryExecuting(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            foreach (var interceptor in this.interceptors)
            {
                interceptor.NonQueryExecuting(command, interceptionContext);
            }
        }

        public void ReaderExecuted(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            foreach (var interceptor in this.interceptors)
            {
                interceptor.ReaderExecuted(command, interceptionContext);
            }
        }

        public void ReaderExecuting(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            foreach (var interceptor in this.interceptors)
            {
                interceptor.ReaderExecuting(command, interceptionContext);
            }
        }

        public void ScalarExecuted(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            foreach (var interceptor in this.interceptors)
            {
                interceptor.ScalarExecuted(command, interceptionContext);
            }
        }

        public void ScalarExecuting(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            foreach (var interceptor in this.interceptors)
            {
                interceptor.ScalarExecuting(command, interceptionContext);
            }
        }
    }
}
