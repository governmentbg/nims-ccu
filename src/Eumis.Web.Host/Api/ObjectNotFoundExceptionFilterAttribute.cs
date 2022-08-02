using Eumis.Data;
using Eumis.Domain;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace Eumis.Web.Host.Api
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class ObjectNotFoundExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            if (context.Exception is DomainObjectNotFoundException ||
                context.Exception is DataObjectNotFoundException)
            {
                context.Response = new HttpResponseMessage(HttpStatusCode.NotFound);
            }
        }
    }
}