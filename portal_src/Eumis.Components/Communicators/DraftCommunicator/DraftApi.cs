using Newtonsoft.Json.Linq;
using System;
using System.Configuration;
using Eumis.Common.Helpers;

namespace Eumis.Components.Communicators
{
    public class DraftApi
    {
        //[RoutePrefix("api/registration")]
        //[Route("projects")]
        public static JObject GetDrafts(string accessToken, int limit, int offset)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}registration/projects?type=draft&limit={1}&offset={2}",
                    serverLocation,
                    limit,
                    offset
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.GetAuthorizationRequest<JObject>(url, accessToken);
        }

        //[RoutePrefix("api/registration")]
        //[Route("projects")]
        public static JObject GetFinalizedProjects(string accessToken, int limit, int offset)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}registration/projects?type=finalized&limit={1}&offset={2}",
                    serverLocation,
                    limit,
                    offset
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.GetAuthorizationRequest<JObject>(url, accessToken);
        }

        //[RoutePrefix("api/registration")]
        //[Route("projects")]
        public static JObject GetRegisteredProjects(string accessToken, int limit, int offset)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}registration/projects?type=registered&limit={1}&offset={2}",
                    serverLocation,
                    limit,
                    offset
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.GetAuthorizationRequest<JObject>(url, accessToken);
        }

        //[RoutePrefix("api/registration")]
        //[Route("projectsManagingAuthorityCommunications")]
        public static JObject GetRegisteredProjectsCommunications(string accessToken, int limit, int offset)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}registration/projectManagingAuthorityCommunications?limit={1}&offset={2}",
                    serverLocation,
                    limit,
                    offset
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.GetAuthorizationRequest<JObject>(url, accessToken);
        }

        //[RoutePrefix("api/registration")]
        //[Route("projects")]
        public static JObject GetSubmittedProjects(string accessToken, int limit, int offset)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}registration/projects?type=submitted&limit={1}&offset={2}",
                    serverLocation,
                    limit,
                    offset
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.GetAuthorizationRequest<JObject>(url, accessToken);
        }

        //[RoutePrefix("api/registration")]
        //[Route("projects/{gid:guid}")]
        public static JObject GetDraft(Guid gid, string accessToken)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}registration/projects/{1}",
                    serverLocation,
                    gid.ToString()
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.GetAuthorizationRequest<JObject>(url, accessToken);
        }

        //[RoutePrefix("api/registration")]
        //[Route("projectVersions/{gid:guid}")]
        public static JObject GetProjectVersion(Guid gid, string accessToken)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}registration/projectVersions/{1}",
                    serverLocation,
                    gid.ToString()
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.GetAuthorizationRequest<JObject>(url, accessToken);
        }

        //[HttpPost]
        //[RoutePrefix("api/registration")]
        //[Route("projects")]
        public static JObject CreateDraft(string body, string accessToken)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}registration/projects",
                    serverLocation
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.PostAuthorizationRequest<JObject>(url, accessToken, body);
        }

        //[HttpPut]
        //[RoutePrefix("api/registration")]
        //[Route("projects/{gid:guid}")]
        public static JObject UpdateDraft(Guid gid, string body, string accessToken)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}registration/projects/{1}",
                    serverLocation,
                    gid.ToString()
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.PutAuthorizationRequest<JObject>(url, accessToken, body);
        }

        //[HttpDelete]
        //[RoutePrefix("api/registration")]
        //[Route("projects/{gid:guid}")]
        public static void DeleteDraft(Guid gid, string body, string accessToken)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}registration/projects/{1}",
                    serverLocation,
                    gid.ToString()
                );

            url = url.RemoveWhiteSpaces();

            ApiRequest.DeleteAuthorizationRequest<JObject>(url, accessToken, body);
        }

        //[HttpPost]
        //[Route("projects/{gid:guid}/finalize")]
        public static void FinalizeDraft(Guid gid, string body, string accessToken)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}registration/projects/{1}/finalize",
                    serverLocation,
                    gid.ToString()
                );

            url = url.RemoveWhiteSpaces();

            ApiRequest.PostAuthorizationRequest<JObject>(url, accessToken, body);
        }

        //[HttpPost]
        //[Route("projects/{gid:guid}/definalize")]
        public static void DefinalizeDraft(Guid gid, string body, string accessToken)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}registration/projects/{1}/definalize",
                    serverLocation,
                    gid.ToString()
                );

            url = url.RemoveWhiteSpaces();

            ApiRequest.PostAuthorizationRequest<JObject>(url, accessToken, body);
        }

        //[HttpPost]
        //[Route("projects/{gid:guid}/submit")]
        public static JObject Submit(Guid gid, string body, string accessToken)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}registration/projects/{1}/submit",
                    serverLocation,
                    gid.ToString()
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.PostAuthorizationRequest<JObject>(url, accessToken, body);
        }

        //[HttpPost]
        //[Route("projects/register")]
        public static JObject Register(string body, string accessToken)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}registration/projects/register",
                    serverLocation
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.PostAuthorizationRequest<JObject>(url, accessToken, body);
        }
    }
}