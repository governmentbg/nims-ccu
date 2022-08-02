using Eumis.Domain;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace Eumis.PortalIntegration.Host.Api
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class KnownExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            if (context.Exception is DomainValidationException)
            {
                context.Response = context.Request.CreateResponse(
                    HttpStatusCode.InternalServerError,
                    new { error = string.Format("{0}: {1}", context.Exception.GetType().Name, context.Exception.Message) });
            }
        }
    }
}