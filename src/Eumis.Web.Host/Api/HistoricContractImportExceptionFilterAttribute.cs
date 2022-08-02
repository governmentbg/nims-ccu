using Eumis.Domain;
using Eumis.Domain.HistoricContracts.DataObjects;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace Eumis.Web.Host.Api
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class HistoricContractImportExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            if (context.Exception is HistoricContractImportException)
            {
                var res = context.Exception.Message;

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new HistoricContractErrorDO(context.Exception.Message, context.Exception.InnerException.Message))),
                };

                context.Response = response;
            }
        }
    }
}