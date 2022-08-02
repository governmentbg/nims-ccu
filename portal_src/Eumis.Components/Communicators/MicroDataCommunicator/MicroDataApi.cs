using System;
using Newtonsoft.Json.Linq;
using System.Configuration;
using Eumis.Common.Helpers;
using Eumis.Documents.Contracts;

namespace Eumis.Components.Communicators
{
    public class MicroDataApi
    {
        #region Report

        public static JObject CanCreate(Guid contractGid, Guid packageGid, ContractReportMicroType type, string token)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractreg/contracts/{1}/packages/{2}/micros/canCreate?type={3}",
                    serverLocation,
                    contractGid,
                    packageGid,
                    type
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.PostAuthorizationRequest<JObject>(url, token, String.Empty);
        }

        public static void Create(Guid contractGid, Guid packageGid, ContractReportMicroType type, string token)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractreg/contracts/{1}/packages/{2}/micros?type={3}",
                    serverLocation,
                    contractGid,
                    packageGid,
                    type
                );

            url = url.RemoveWhiteSpaces();

            ApiRequest.PostAuthorizationRequest<JObject>(url, token, String.Empty);
        }

        public static JObject Get(Guid contractGid, Guid packageGid, Guid documentGid, string token, int limit, int offset)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractreg/contracts/{1}/packages/{2}/micros/{3}/items?limit={4}&offset={5}",
                    serverLocation,
                    contractGid,
                    packageGid,
                    documentGid,
                    limit,
                    offset
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.GetAuthorizationRequest<JObject>(url, token);
        }

        public static bool HasFile(Guid contractGid, Guid packageGid, Guid documentGid, Guid fileKey, string token)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractreg/contracts/{1}/packages/{2}/micros/{3}/hasFile/{4}",
                    serverLocation,
                    contractGid,
                    packageGid,
                    documentGid,
                    fileKey
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.GetAuthorizationRequest<bool>(url, token);
        }

        public static JObject Put(Guid contractGid, Guid packageGid, Guid documentGid, string token, string body)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractreg/contracts/{1}/packages/{2}/micros/{3}",
                    serverLocation,
                    contractGid,
                    packageGid,
                    documentGid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.PutAuthorizationRequest<JObject>(url, token, body);
        }

        public static JObject PutWithSimevCode(Guid contractGid, Guid packageGid, Guid documentGid, string token, string body)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractreg/contracts/{1}/packages/{2}/micros/{3}/withSimevCode",
                    serverLocation,
                    contractGid,
                    packageGid,
                    documentGid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.PutAuthorizationRequest<JObject>(url, token, body);
        }

        public static JObject CanSubmit(Guid contractGid, Guid packageGid, Guid documentGid, string token, string body)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractreg/contracts/{1}/packages/{2}/micros/{3}/canEnter",
                    serverLocation,
                    contractGid,
                    packageGid,
                    documentGid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.PostAuthorizationRequest<JObject>(url, token, body);
        }

        public static void Submit(Guid contractGid, Guid packageGid, Guid documentGid, string token, string body)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractreg/contracts/{1}/packages/{2}/micros/{3}/enter",
                    serverLocation,
                    contractGid,
                    packageGid,
                    documentGid
                );

            url = url.RemoveWhiteSpaces();

            ApiRequest.PostAuthorizationRequest<JObject>(url, token, body);
        }

        public static void Delete(Guid contractGid, Guid packageGid, Guid documentGid, string token, string body)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractreg/contracts/{1}/packages/{2}/micros/{3}",
                    serverLocation,
                    contractGid,
                    packageGid,
                    documentGid
                );

            url = url.RemoveWhiteSpaces();

            ApiRequest.DeleteAuthorizationRequest<JObject>(url, token, body);
        }

        public static void MakeDraft(Guid contractGid, Guid packageGid, Guid documentGid, string token, string body)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractreg/contracts/{1}/packages/{2}/micros/{3}/makedraft",
                    serverLocation,
                    contractGid,
                    packageGid,
                    documentGid
                );

            url = url.RemoveWhiteSpaces();

            ApiRequest.PostAuthorizationRequest<JObject>(url, token, body);
        }

        public static void MakeActual(Guid contractGid, Guid packageGid, Guid documentGid, string token, string body)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractreg/contracts/{1}/packages/{2}/micros/{3}/makeActual",
                    serverLocation,
                    contractGid,
                    packageGid,
                    documentGid
                );

            url = url.RemoveWhiteSpaces();

            ApiRequest.PostAuthorizationRequest<JObject>(url, token, body);
        }

        #endregion

        #region Private

        public static JObject PrivateGet(Guid gid, string token, int limit, int offset)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractReportMicros/{1}/items?limit={2}&offset={3}",
                    serverLocation,
                    gid,
                    limit,
                    offset
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.GetAuthorizationRequest<JObject>(url, token);
        }

        #endregion
    }
}