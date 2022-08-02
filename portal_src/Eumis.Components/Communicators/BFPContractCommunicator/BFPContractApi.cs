using System;
using Newtonsoft.Json.Linq;
using System.Configuration;
using Eumis.Common.Helpers;

namespace Eumis.Components.Communicators
{
    public class BFPContractApi
    {
        public static JObject GetContractData(Guid procedureGid, string programmeCode)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}procedures/{1}/contractdata?programmeCode={2}",
                    serverLocation,
                    procedureGid,
                    programmeCode
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.GetAnonymousRequest<JObject>(url);
        }

        public static JObject GetBFPContract(Guid gid, string token)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contracts/{1}",
                    serverLocation,
                    gid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.GetAuthorizationRequest<JObject>(url, token);
        }

        public static JObject PutBFPContract(Guid gid, string token, string body)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contracts/{1}",
                    serverLocation,
                    gid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.PutAuthorizationRequest<JObject>(url, token, body);
        }

        public static JObject SubmitBFPContract(Guid gid, string token, string body)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contracts/{1}/enter",
                    serverLocation,
                    gid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.PostAuthorizationRequest<JObject>(url, token, body);
        }

        public static JArray GetBFPContractApplicationSections(Guid gid, string token)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractreg/contracts/{1}/applicationSections",
                    serverLocation,
                    gid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.GetAuthorizationRequest<JArray>(url, token);
        }

        public static JObject GetContracts(string accessToken, int offset, int? limit)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractreg/contracts?limit={1}&offset={2}",
                    serverLocation,
                    limit,
                    offset
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.GetAuthorizationRequest<JObject>(url, accessToken);
        }

        public static JObject GetContractMetadata(string accessToken, Guid gid)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractreg/contracts/{1}",
                    serverLocation,
                    gid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.GetAuthorizationRequest<JObject>(url, accessToken);
        }

        public static JObject GetContractVersions(string accessToken, Guid contractGid, int offset, int? limit)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractreg/contracts/{1}/versions?limit={2}&offset={3}",
                    serverLocation,
                    contractGid,
                    limit,
                    offset
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.GetAuthorizationRequest<JObject>(url, accessToken);
        }

        public static JObject GetContractVersion(string accessToken, Guid contractGid, Guid versionGid)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractreg/contracts/{1}/versions/{2}",
                    serverLocation,
                    contractGid,
                    versionGid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.GetAuthorizationRequest<JObject>(url, accessToken);
        }

        public static JObject GetActualContractVersion(string accessToken, Guid contractGid)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractreg/contracts/{1}/actual",
                    serverLocation,
                    contractGid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.GetAuthorizationRequest<JObject>(url, accessToken);
        }
    }
}