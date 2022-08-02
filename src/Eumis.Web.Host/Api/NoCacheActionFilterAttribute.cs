using Eumis.Common.Api;
using System;
using System.Net.Http;
using System.Web.Http.Filters;

namespace Eumis.Web.Host.Api
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class NoCacheActionFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Request.Method == HttpMethod.Get &&
                actionExecutedContext.Exception == null &&
                actionExecutedContext.Response != null)
            {
                actionExecutedContext.Response.AddNoCacheHeaders();
            }
        }
    }
}