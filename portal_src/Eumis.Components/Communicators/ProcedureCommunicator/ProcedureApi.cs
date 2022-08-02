using System;
using Newtonsoft.Json.Linq;
using System.Configuration;
using Eumis.Common.Helpers;

namespace Eumis.Components.Communicators
{
    public class ProcedureApi
    {
        //[AllowAnonymous]
        //[Route("activeProceduresTree")]
        public static JArray GetActiveProcedureProgrammesTree()
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}procedures/activeProceduresTree",
                    serverLocation
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.GetAnonymousRequest<JArray>(url);
        }

        //[AllowAnonymous]
        //[Route("endedProceduresTree")]
        public static JArray GetEndedPProcedureProgrammesTree()
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}procedures/endedProceduresTree",
                    serverLocation
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.GetAnonymousRequest<JArray>(url);
        }

        //[AllowAnonymous]
        //[Route("publicDiscussionProceduresTree")]
        public static JArray GetPublicDiscussionProcedureProgrammesTree()
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}procedures/publicDiscussionProceduresTree",
                    serverLocation
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.GetAnonymousRequest<JArray>(url);
        }

        //[AllowAnonymous]
        //[Route("archivedPublicDiscussionProceduresTree")]
        public static JArray GetArchivedPublicDiscussionProcedureProgrammesTree()
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}procedures/archivedPublicDiscussionProceduresTree",
                    serverLocation
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.GetAnonymousRequest<JArray>(url);
        }

        //[AllowAnonymous]
        //[Route("{procedureGid:guid}/appdata")]
        public static JObject GetProcedureAppData(Guid procedureGid)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}procedures/{1}/appdata",
                    serverLocation,
                    procedureGid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.GetAnonymousRequest<JObject>(url);
        }

        //[AllowAnonymous]
        //[Route("{procedureGid:guid}/info")]
        public static JObject GetProcedureInfo(Guid procedureGid)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}procedures/{1}/info",
                    serverLocation,
                    procedureGid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.GetAnonymousRequest<JObject>(url);
        }

        //[AllowAnonymous]
        //[Route("{procedureGid:guid}/infoPublicDiscussion")]
        public static JObject GetProcedurePublicDiscussionInfo(Guid procedureGid)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}procedures/{1}/infoPublicDiscussion",
                    serverLocation,
                    procedureGid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.GetAnonymousRequest<JObject>(url);
        }

        //[AllowAnonymous]
        //[Route("{procedureGid:guid}/actualappdata")]
        public static JObject GetProcedureActualAppData(Guid procedureGid)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}procedures/{1}/actualappdata",
                    serverLocation,
                    procedureGid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.GetAnonymousRequest<JObject>(url);
        }

        //[AllowAnonymous]
        //[Route("{procedureGid:guid}/infoProcedureDiscussions")]
        public static JArray GetProcedureDiscussionsInfo(Guid procedureGid, string searchTerm, int limit, int offset)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}procedures/{1}/infoProcedureDiscussions?term={2}&limit={3}&offset={4}",
                    serverLocation,
                    procedureGid,
                    searchTerm,
                    limit,
                    offset
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.GetAnonymousRequest<JArray>(url);
        }

        //[HttpPost]
        //[Route("{procedureGid:guid}/submitProcedurePublicDiscussionComment")]
        public static void SubmitProcedurePublicDiscussionComment(Guid procedureGid, string accessToken, string body)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}procedures/{1}/submitProcedurePublicDiscussionComment",
                    serverLocation,
                    procedureGid
                );

            url = url.RemoveWhiteSpaces();

            ApiRequest.PostAuthorizationRequest<JObject>(url, accessToken, body);
        }

        //[HttpPost]
        //[Route("{procedureGid:guid}/submitProcedureDiscussionQuestion")]
        public static void SubmitProcedureDiscussionQuestion(Guid procedureGid, string accessToken, string body)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}procedures/{1}/submitProcedureDiscussionQuestion",
                    serverLocation,
                    procedureGid
                );

            url = url.RemoveWhiteSpaces();

            ApiRequest.PostAuthorizationRequest<JObject>(url, accessToken, body);
        }
    }
}
