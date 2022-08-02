using System.Configuration;
using Eumis.Common.Helpers;
using Newtonsoft.Json.Linq;
using System;

namespace Eumis.Components.Communicators
{
    public class ContractRegistrationAccessCodesApi
    {
        //[Route("api/token")]
        public static JObject Login(string body)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}token",
                    serverLocation
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.PostAccessTokenRequest<JObject>(url, body);
        }

        //[Route("api/registration/info")]
        public static JObject GetRegistrationInfo(string accessToken)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}accesscode/info/",
                    serverLocation
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.GetAuthorizationRequest<JObject>(url, accessToken);
        }

        public static JObject GetContractRegistrationAccessCodes(string accessToken, Guid contractGid, int offset = 0,
            int? limit = null)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractreg/contracts/{1}/accesscodes?limit={2}&offset={3}",
                    serverLocation,
                    contractGid,
                    limit,
                    offset
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.GetAuthorizationRequest<JObject>(url, accessToken);
        }

        public static JObject GetContractRegistrationAccessCode(string accessToken, Guid contractGid, Guid accessCodeGid)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractreg/contracts/{1}/accesscodes/{2}",
                    serverLocation,
                    contractGid,
                    accessCodeGid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.GetAuthorizationRequest<JObject>(url, accessToken);
        }

        public static JObject CreateContractRegistrationAccessCode(string token, Guid contractGid, string body)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractreg/contracts/{1}/accesscodes",
                    serverLocation,
                    contractGid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.PostAuthorizationRequest<JObject>(url, token, body);
        }

        public static JObject UpdateContractRegistrationAccessCode(string token, Guid contractGid,
            Guid accessCodeGid, string body)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractreg/contracts/{1}/accesscodes/{2}",
                    serverLocation,
                    contractGid,
                    accessCodeGid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.PutAuthorizationRequest<JObject>(url, token, body);
        }
    }
}