using System;
using Newtonsoft.Json.Linq;
using System.Configuration;
using Eumis.Common.Helpers;

namespace Eumis.Components.Communicators
{
    public class FinanceReportApi
    {
        #region Report

        public static JObject GetFinanceReport(Guid contractGid, Guid packageGid, string token)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractreg/contracts/{1}/packages/{2}/finplan",
                    serverLocation,
                    contractGid,
                    packageGid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.GetAuthorizationRequest<JObject>(url, token);
        }

        public static JObject GetFinanceReport(Guid contractGid, Guid packageGid, Guid financeReportGid, string token)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractreg/contracts/{1}/packages/{2}/finplan/{3}",
                    serverLocation,
                    contractGid,
                    packageGid,
                    financeReportGid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.GetAuthorizationRequest<JObject>(url, token);
        }

        public static JObject GetFinanceReportForEdit(Guid contractGid, Guid packageGid, Guid financeReportGid, string token)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractreg/contracts/{1}/packages/{2}/finplan/{3}/edit",
                    serverLocation,
                    contractGid,
                    packageGid,
                    financeReportGid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.GetAuthorizationRequest<JObject>(url, token);
        }

        public static JObject CreateFinanceReport(Guid contractGid, Guid packageGid, string token, string body)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractreg/contracts/{1}/packages/{2}/finplan",
                    serverLocation,
                    contractGid,
                    packageGid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.PostAuthorizationRequest<JObject>(url, token, body);
        }

        public static JObject PutFinanceReport(Guid contractGid, Guid packageGid, Guid financeReportGid, string token, string body)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractreg/contracts/{1}/packages/{2}/finplan/{3}",
                    serverLocation,
                    contractGid,
                    packageGid,
                    financeReportGid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.PutAuthorizationRequest<JObject>(url, token, body);
        }

        public static void DeleteFinanceReport(Guid contractGid, Guid packageGid, Guid financeReportGid, string token, string body)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractreg/contracts/{1}/packages/{2}/finplan/{3}",
                    serverLocation,
                    contractGid,
                    packageGid,
                    financeReportGid
                );

            url = url.RemoveWhiteSpaces();

            ApiRequest.DeleteAuthorizationRequest<JObject>(url, token, body);
        }

        public static JObject CanCreateFinanceReport(Guid contractGid, Guid packageGid, string token)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractreg/contracts/{1}/packages/{2}/finplan/canCreate",
                    serverLocation,
                    contractGid,
                    packageGid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.PostAuthorizationRequest<JObject>(url, token, String.Empty);
        }

        public static JObject SubmitFinanceReport(Guid contractGid, Guid packageGid, Guid financeReportGid, string token, string body)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractreg/contracts/{1}/packages/{2}/finplan/{3}/enter",
                    serverLocation,
                    contractGid,
                    packageGid,
                    financeReportGid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.PostAuthorizationRequest<JObject>(url, token, body);
        }

        public static JObject MakeDraftFinanceReport(Guid contractGid, Guid packageGid, Guid financeReportGid, string token, string body)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractreg/contracts/{1}/packages/{2}/finplan/{3}/makedraft",
                    serverLocation,
                    contractGid,
                    packageGid,
                    financeReportGid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.PostAuthorizationRequest<JObject>(url, token, body);
        }

        public static JObject MakeActualFinanceReport(Guid contractGid, Guid packageGid, Guid financeReportGid, string token, string body)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractreg/contracts/{1}/packages/{2}/finplan/{3}/makeActual",
                    serverLocation,
                    contractGid,
                    packageGid,
                    financeReportGid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.PostAuthorizationRequest<JObject>(url, token, body);
        }

        #endregion

        #region Private

        public static JObject PrivateGetFinanceReport(Guid gid, string token)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractReportFinancials/{1}",
                    serverLocation,
                    gid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.GetAuthorizationRequest<JObject>(url, token);
        }

        public static JObject PrivateGetFinanceReportForEdit(Guid gid, string token)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractReportFinancials/{1}/edit",
                    serverLocation,
                    gid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.GetAuthorizationRequest<JObject>(url, token);
        }

        public static JObject PrivatePutFinanceReport(Guid gid, string token, string body)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractReportFinancials/{1}",
                    serverLocation,
                    gid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.PutAuthorizationRequest<JObject>(url, token, body);
        }

        public static JObject PrivateSubmitFinanceReport(Guid gid, string token, string body)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractReportFinancials/{1}/enter",
                    serverLocation,
                    gid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.PostAuthorizationRequest<JObject>(url, token, body);
        }

        #endregion
    }
}