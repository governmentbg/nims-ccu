using System;
using Newtonsoft.Json.Linq;
using System.Configuration;
using Eumis.Common.Helpers;

namespace Eumis.Components.Communicators
{
    public class PackageApi
    {
        public static JObject GetPackages(Guid contractGid, string token, int limit, int offset)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractreg/contracts/{1}/packages?limit={2}&offset={3}",
                    serverLocation,
                    contractGid,
                    limit,
                    offset
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.GetAuthorizationRequest<JObject>(url, token);
        }

        public static JObject GetPackage(Guid contractGid, Guid packageGid, string token)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractreg/contracts/{1}/packages/{2}",
                    serverLocation,
                    contractGid,
                    packageGid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.GetAuthorizationRequest<JObject>(url, token);
        }

        public static JObject CanCopyPackage(Guid contractGid, Guid packageGid, string token)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractreg/contracts/{1}/packages/{2}/canCopy",
                    serverLocation,
                    contractGid,
                    packageGid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.PostAuthorizationRequest<JObject>(url, token, string.Empty);
        }

        public static JObject CopyPackage(Guid contractGid, Guid packageGid, string token)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractreg/contracts/{1}/packages/{2}/copy",
                    serverLocation,
                    contractGid,
                    packageGid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.PostAuthorizationRequest<JObject>(url, token, string.Empty);
        }

        public static JObject CanUpdatePackage(Guid contractGid, Guid packageGid, string token, string body)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractreg/contracts/{1}/packages/{2}/canUpdate",
                    serverLocation,
                    contractGid,
                    packageGid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.PostAuthorizationRequest<JObject>(url, token, body);
        }

        public static JObject UpdatePackage(Guid contractGid, Guid packageGid, string token, string body)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractreg/contracts/{1}/packages/{2}",
                    serverLocation,
                    contractGid,
                    packageGid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.PutAuthorizationRequest<JObject>(url, token, body);
        }

        public static JObject CanDeletePackage(Guid contractGid, Guid packageGid, string token)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractreg/contracts/{1}/packages/{2}/canDelete",
                    serverLocation,
                    contractGid,
                    packageGid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.PostAuthorizationRequest<JObject>(url, token, String.Empty);
        }

        public static void DeletePackage(Guid contractGid, Guid packageGid, string accessToken, string body)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractreg/contracts/{1}/packages/{2}",
                    serverLocation,
                    contractGid,
                    packageGid
                );

            url = url.RemoveWhiteSpaces();

            ApiRequest.DeleteAuthorizationRequest<JObject>(url, accessToken, body);
        }

        public static JObject CanCreatePackage(Guid contractGid, string token)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractreg/contracts/{1}/packages/canCreate",
                    serverLocation,
                    contractGid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.PostAuthorizationRequest<JObject>(url, token, String.Empty);
        }

        public static JObject CreatePackage(Guid contractGid, string token, string body)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractreg/contracts/{1}/packages",
                    serverLocation,
                    contractGid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.PostAuthorizationRequest<JObject>(url, token, body);
        }

        public static JObject CanMakeDraftPackage(Guid contractGid, Guid packageGid, string token)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractreg/contracts/{1}/packages/{2}/canMakeDraft",
                    serverLocation,
                    contractGid,
                    packageGid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.PostAuthorizationRequest<JObject>(url, token, String.Empty);
        }

        public static void MakeDraftPackage(Guid contractGid, Guid packageGid, string token, string body)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractreg/contracts/{1}/packages/{2}/makeDraft",
                    serverLocation,
                    contractGid,
                    packageGid
                );

            url = url.RemoveWhiteSpaces();

            ApiRequest.PostAuthorizationRequest<JObject>(url, token, body);
        }

        public static JObject CanSubmitPackage(Guid contractGid, Guid packageGid, string token)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractreg/contracts/{1}/packages/{2}/canSubmit",
                    serverLocation,
                    contractGid,
                    packageGid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.PostAuthorizationRequest<JObject>(url, token, String.Empty);
        }

        public static void SubmitPackage(Guid contractGid, Guid packageGid, string token, string body)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractreg/contracts/{1}/packages/{2}/submit",
                    serverLocation,
                    contractGid,
                    packageGid
                );

            url = url.RemoveWhiteSpaces();

            ApiRequest.PostAuthorizationRequest<JObject>(url, token, body);
        }
    }
}