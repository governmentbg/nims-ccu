using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.Web;
using System.Web.Http;
using System.Web.Http.Batch;
using System.Web.Http.WebHost;
using System.Web.Routing;
using System.Web.SessionState;

namespace Eumis.Portal.Web
{
    public static class WebApiConfig
    {
        public static JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings()
        {
            DateFormatString = "dd.MM.yyyy",
        };

        public static JsonSerializerSettings JsonSerializerDateTimeFormatSettings = new JsonSerializerSettings()
        {
            DateFormatString = "dd.MM.yyy HH:mm:ss",
        };

        public static void Register(HttpConfiguration config)
        {
            config.Formatters.JsonFormatter.SerializerSettings = JsonSerializerSettings;
            JsonConvert.DefaultSettings = () => JsonSerializerSettings;

            RouteTable.Routes.MapHttpRoute(
                name: "TimeNowApi",
                routeTemplate: "api/{session}/time/now",
                defaults: new { controller = "time", action = "now" });

            RouteTable.Routes.MapHttpRoute(
                name: "PrivateApi",
                routeTemplate: "api/private/{session}/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional })
                      .RouteHandler = new SessionRouteHandler();

            RouteTable.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{session}/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional })
                      .RouteHandler = new SessionRouteHandler();

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);
        }
    }

    public class SessionControllerHandler : HttpControllerHandler, IRequiresSessionState
    {
        public SessionControllerHandler(RouteData routeData)
            : base(routeData)
        { }
    }

    public class SessionRouteHandler : IRouteHandler
    {
        IHttpHandler IRouteHandler.GetHttpHandler(RequestContext requestContext)
        {
            return new SessionControllerHandler(requestContext.RouteData);
        }
    }
}
