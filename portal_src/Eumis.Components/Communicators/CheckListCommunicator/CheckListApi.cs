using Eumis.Common.Helpers;
using Newtonsoft.Json.Linq;
using System;

namespace Eumis.Components.Communicators
{
    public class CheckListApi
    {
        #region CheckList template
        
        public static JObject GetCheckList(Guid gid, string token)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}programmeCheckListVersions/{1}",
                    serverLocation,
                    gid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.GetAuthorizationRequest<JObject>(url, token);
        }

        public static JObject PutCheckList(Guid gid, string token, string body)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}programmeCheckListVersions/{1}",
                    serverLocation,
                    gid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.PutAuthorizationRequest<JObject>(url, token, body);
        }

        public static JObject SubmitCheckList(Guid gid, string token, string body)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}programmeCheckListVersions/{1}/submit",
                    serverLocation,
                    gid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.PostAuthorizationRequest<JObject>(url, token, body);
        }

        #endregion

        #region CheckSheet

        //[RoutePrefix("api/checkSheetVersions")]
        //[Route("{checkSheetGid:guid}")]
        public static JObject GetCheckSheet(Guid gid, string token)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}checkSheetVersions/{1}",
                    serverLocation,
                    gid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.GetAuthorizationRequest<JObject>(url, token);
        }

        //[RoutePrefix("api/checkSheetVersions")]
        //[Route("{checkSheetGid:guid}/procurementPlans")]
        public static JObject GetCheckSheetProcurementPlans(Guid gid, string token)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}checkSheetVersions/{1}/procurementPlans",
                    serverLocation,
                    gid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.GetAuthorizationRequest<JObject>(url, token);
        }

        //[RoutePrefix("api/checkSheetVersions")]
        //[Route("{checkSheetGid:guid}/verificationData")]
        public static JObject GetUserVerificationData(Guid gid, string token)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}checkSheetVersions/{1}/verificationData",
                    serverLocation,
                    gid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.GetAuthorizationRequest<JObject>(url, token);
        }

        //[HttpPut]
        //[RoutePrefix("api/checkSheetVersions")]
        //[Route("{checkSheetGid:guid}")]
        public static JObject PutCheckSheet(Guid gid, string token, string body)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}checkSheetVersions/{1}",
                    serverLocation,
                    gid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.PutAuthorizationRequest<JObject>(url, token, body);
        }

        //[HttpPost]
        //[RoutePrefix("api/checkSheetVersions")]
        //[Route("{checkSheetGid:guid}/submit")]
        public static JObject SubmitCheckSheet(Guid gid, string token, string body)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}checkSheetVersions/{1}/submit",
                    serverLocation,
                    gid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.PostAuthorizationRequest<JObject>(url, token, body);
        }

        //[HttpPost]
        //[RoutePrefix("api/checkSheetVersions")]
        //[Route("{checkSheetGid:guid}/pause")]
        public static JObject PauseCheckSheet(Guid gid, string token, string body)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}checkSheetVersions/{1}/pause",
                    serverLocation,
                    gid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.PostAuthorizationRequest<JObject>(url, token, body);
        }

        #endregion
    }
}
