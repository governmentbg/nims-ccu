using System;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Eumis.Public.Web.InfrastructureClasses
{
    public class JsonResultCameCase : JsonResult
    {
        public JsonResultCameCase()
            : base()
        {
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var response = context.HttpContext.Response;

            response.ContentType = !string.IsNullOrEmpty(this.ContentType) ? this.ContentType : "application/json";

            if (this.ContentEncoding != null)
            {
                response.ContentEncoding = this.ContentEncoding;
            }

            // If you need special handling, you can call another form of SerializeObject below
            var serializedObject = JsonConvert.SerializeObject(
                this.Data,
                new JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver() });
            response.Write(serializedObject);
        }
    }
}