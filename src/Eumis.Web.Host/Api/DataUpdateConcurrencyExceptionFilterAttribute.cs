using Eumis.Data;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace Eumis.Web.Host.Api
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class DataUpdateConcurrencyExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            if (context.Exception is DataUpdateConcurrencyException)
            {
                context.Response = new HttpResponseMessage(HttpStatusCode.Conflict);
            }
        }
    }
}