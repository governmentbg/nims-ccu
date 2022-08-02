using Newtonsoft.Json.Linq;
using Eumis.Common.Helpers;

namespace Eumis.Components.Communicators
{
    public class NewsApi
    {
        //[AllowAnonymous]
        //[Route("active")]
        public static JObject GetNews(int offset = 0, int? limit = null)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}news/active?limit={1}&offset={2}",
                    serverLocation,
                    limit,
                    offset
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.GetAnonymousRequest<JObject>(url);
        }

        //[AllowAnonymous]
        //[Route("all")]
        public static JObject GetAllNews(int offset = 0, int? limit = null)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}news/all?limit={1}&offset={2}",
                    serverLocation,
                    limit,
                    offset
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.GetAnonymousRequest<JObject>(url);
        }

        //[AllowAnonymous]
        //[Route("{id:int}/info")]
        public static JObject GetNewsInfo(int id)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}news/{1}/info",
                    serverLocation,
                    id
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.GetAnonymousRequest<JObject>(url);
        }
    }
}
