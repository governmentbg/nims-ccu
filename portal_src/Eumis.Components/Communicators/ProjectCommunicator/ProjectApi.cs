using System;
using Newtonsoft.Json.Linq;
using System.Configuration;
using Eumis.Common.Helpers;

namespace Eumis.Components.Communicators
{
    public class ProjectApi
    {
        //[Route("{gid:guid}")]
        public static JObject GetProject(Guid gid, string token)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}projects/{1}/",
                    serverLocation,
                    gid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.GetAuthorizationRequest<JObject>(url, token);
        }

        //[Route("{gid:guid}")]
        public static JObject PutProject(Guid gid, string token, string body)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}projects/{1}/",
                    serverLocation,
                    gid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.PutAuthorizationRequest<JObject>(url, token, body);
        }

        //[HttpPost]
        //[Route("{projectXmlGid:guid}/activate")]
        public static void Submit(Guid gid, string token, string body)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}projects/{1}/activate",
                    serverLocation,
                    gid
                );

            url = url.RemoveWhiteSpaces();

            ApiRequest.PostAuthorizationRequest<JObject>(url, token, body);
        }

        //[HttpPost]
        //[Route("drafts/validate")]
        public static JArray ValidateDraft(string body, string accessToken)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}projects/validate",
                    serverLocation
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.PostAuthorizationRequest<JArray>(url, accessToken, body);
        }

        public static void ResurrectFiles(string body)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}projects/resurrectFiles",
                    serverLocation
                );

            url = url.RemoveWhiteSpaces();

            ApiRequest.PostAnonymousRequest<JObject>(url, body);
        }

        public static JObject GetProjectFilesZip(string projectGid, string accessToken)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}projects/{1}/getFilesZip",
                    serverLocation,
                    projectGid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.GetAuthorizationRequest<JObject>(url, accessToken);
        }
    }
}
