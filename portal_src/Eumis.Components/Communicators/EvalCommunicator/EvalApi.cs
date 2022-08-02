using System;
using Newtonsoft.Json.Linq;
using System.Configuration;
using Eumis.Common.Helpers;

namespace Eumis.Components.Communicators
{
    public class EvalApi
    {
        //[RoutePrefix("api/procedureRatingTemplates")]
        //[Route("{templateXmlGid:guid}")]
        public static JObject GetEvalTable(Guid gid, string token)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}procedureEvalTables/{1}",
                    serverLocation,
                    gid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.GetAuthorizationRequest<JObject>(url, token);
        }

        //[HttpPut]
        //[RoutePrefix("api/procedureRatingTemplates")]
        //[Route("{templateXmlGid:guid}")]
        public static JObject PutEvalTable(Guid gid, string token, string body)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}procedureEvalTables/{1}",
                    serverLocation,
                    gid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.PutAuthorizationRequest<JObject>(url, token, body);
        }

        //[HttpPost]
        //[RoutePrefix("api/evalSessionTables")]
        //[Route("{xmlGid:guid}/submit")]
        public static JObject SubmitEvalTable(Guid gid, string token, string body)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}procedureEvalTables/{1}/submit",
                    serverLocation,
                    gid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.PostAuthorizationRequest<JObject>(url, token, body);
        }

        //[RoutePrefix("api/evalSessionSheets")]
        //[Route("{xmlGid:guid}")]
        public static JObject GetEvalSheet(Guid gid, string token)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}evalSessionSheets/{1}",
                    serverLocation,
                    gid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.GetAuthorizationRequest<JObject>(url, token);
        }


        //[HttpPut]
        //[RoutePrefix("api/evalSessionSheets")]
        //[Route("{xmlGid:guid}")]
        public static JObject PutEvalSheet(Guid gid, string token, string body)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}evalSessionSheets/{1}",
                    serverLocation,
                    gid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.PutAuthorizationRequest<JObject>(url, token, body);
        }

        //[HttpPost]
        //[RoutePrefix("api/evalSessionSheets")]
        //[Route("{xmlGid:guid}/submit")]
        public static JObject SubmitEvalSheet(Guid gid, string token, string body)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}evalSessionSheets/{1}/submit",
                    serverLocation,
                    gid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.PostAuthorizationRequest<JObject>(url, token, body);
        }

        //[HttpPost]
        //[RoutePrefix("api/evalSessionSheets")]
        //[Route("{xmlGid:guid}/pause")]
        public static JObject PauseEvalSheet(Guid gid, string token, string body)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}evalSessionSheets/{1}/pause",
                    serverLocation,
                    gid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.PostAuthorizationRequest<JObject>(url, token, body);
        }
    }
}