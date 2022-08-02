using Autofac;
using Autofac.Integration.Owin;
using Eumis.Common.Db;
using System;
using System.Data.Entity;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Eumis.Common.Api
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class TransactionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var owinContext = actionContext.Request.GetOwinContext();
            var transaction = owinContext.GetAutofacLifetimeScope().Resolve<IUnitOfWork>().BeginTransaction();

            owinContext.Set<DbContextTransaction>("eumis.Transaction", transaction);
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            var owinContext = actionExecutedContext.Request.GetOwinContext();
            var transaction = owinContext.Get<DbContextTransaction>("eumis.Transaction");

            try
            {
                if (actionExecutedContext.Exception != null)
                {
                    transaction.Rollback();
                }
                else
                {
                    transaction.Commit();
                }
            }
            finally
            {
                transaction.Dispose();
            }
        }
    }
}