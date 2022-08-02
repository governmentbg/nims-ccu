using System;
using Newtonsoft.Json.Linq;
using System.Configuration;
using Eumis.Common.Helpers;

namespace Eumis.Components.Communicators
{
    public class TechnicalReportApi
    {
        #region Report

        public static JObject GetTechnicalReport(Guid contractGid, Guid packageGid, Guid technicalReportGid, string token)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractreg/contracts/{1}/packages/{2}/techplan/{3}",
                    serverLocation,
                    contractGid,
                    packageGid,
                    technicalReportGid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.GetAuthorizationRequest<JObject>(url, token);
        }

        public static JObject GetTechnicalReportForEdit(Guid contractGid, Guid packageGid, Guid technicalReportGid, string token)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractreg/contracts/{1}/packages/{2}/techplan/{3}/edit",
                    serverLocation,
                    contractGid,
                    packageGid,
                    technicalReportGid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.GetAuthorizationRequest<JObject>(url, token);
        }

        public static JObject CreateTechnicalReport(Guid contractGid, Guid packageGid, string token, string body)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractreg/contracts/{1}/packages/{2}/techplan",
                    serverLocation,
                    contractGid,
                    packageGid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.PostAuthorizationRequest<JObject>(url, token, body);
        }

        public static JObject PutTechnicalReport(Guid contractGid, Guid packageGid, Guid technicalReportGid, string token, string body)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractreg/contracts/{1}/packages/{2}/techplan/{3}",
                    serverLocation,
                    contractGid,
                    packageGid,
                    technicalReportGid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.PutAuthorizationRequest<JObject>(url, token, body);
        }

        public static void DeleteTechnicalReport(Guid contractGid, Guid packageGid, Guid technicalReportGid, string token, string body)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractreg/contracts/{1}/packages/{2}/techplan/{3}",
                    serverLocation,
                    contractGid,
                    packageGid,
                    technicalReportGid
                );

            url = url.RemoveWhiteSpaces();

            ApiRequest.DeleteAuthorizationRequest<JObject>(url, token, body);
        }

        public static JObject CanCreateTechnicalReport(Guid contractGid, Guid packageGid, string token)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractreg/contracts/{1}/packages/{2}/techplan/canCreate",
                    serverLocation,
                    contractGid,
                    packageGid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.PostAuthorizationRequest<JObject>(url, token, String.Empty);
        }

        public static JObject SubmitTechnicalReport(Guid contractGid, Guid packageGid, Guid technicalReportGid, string token, string body)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractreg/contracts/{1}/packages/{2}/techplan/{3}/enter",
                    serverLocation,
                    contractGid,
                    packageGid,
                    technicalReportGid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.PostAuthorizationRequest<JObject>(url, token, body);
        }

        public static JObject MakeDraftTechnicalReport(Guid contractGid, Guid packageGid, Guid technicalReportGid, string token, string body)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractreg/contracts/{1}/packages/{2}/techplan/{3}/makedraft",
                    serverLocation,
                    contractGid,
                    packageGid,
                    technicalReportGid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.PostAuthorizationRequest<JObject>(url, token, body);
        }

        public static JObject MakeActualTechnicalReport(Guid contractGid, Guid packageGid, Guid technicalReportGid, string token, string body)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractreg/contracts/{1}/packages/{2}/techplan/{3}/makeActual",
                    serverLocation,
                    contractGid,
                    packageGid,
                    technicalReportGid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.PostAuthorizationRequest<JObject>(url, token, body);
        }

        #endregion

        #region Private

        public static JObject PrivateGetTechnicalReport(Guid gid, string token)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractReportTechnicals/{1}",
                    serverLocation,
                    gid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.GetAuthorizationRequest<JObject>(url, token);
        }

        public static JObject PrivateGetTechnicalReportForEdit(Guid gid, string token)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractReportTechnicals/{1}/edit",
                    serverLocation,
                    gid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.GetAuthorizationRequest<JObject>(url, token);
        }

        public static JObject PrivatePutTechnicalReport(Guid gid, string token, string body)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractReportTechnicals/{1}",
                    serverLocation,
                    gid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.PutAuthorizationRequest<JObject>(url, token, body);
        }

        public static JObject PrivateSubmitTechnicalReport(Guid gid, string token, string body)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractReportTechnicals/{1}/enter",
                    serverLocation,
                    gid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.PostAuthorizationRequest<JObject>(url, token, body);
        }

        #endregion
    }
}