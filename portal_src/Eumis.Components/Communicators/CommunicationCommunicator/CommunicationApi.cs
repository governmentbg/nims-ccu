using System;
using Newtonsoft.Json.Linq;
using System.Configuration;
using Eumis.Common.Helpers;

namespace Eumis.Components.Communicators
{
    public class CommunicationApi
    {
        #region Report

        public static JObject GetCommunications(Guid contractGid, string type, string token, int limit, int offset)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractregs/contracts/{1}/communications?type={2}&limit={3}&offset={4}",
                    serverLocation,
                    contractGid,
                    type,
                    limit,
                    offset
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.GetAuthorizationRequest<JObject>(url, token);
        }

        public static JObject GetCommunication(Guid contractGid, Guid communicationGid, string token)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractregs/contracts/{1}/communications/{2}",
                    serverLocation,
                    contractGid,
                    communicationGid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.GetAuthorizationRequest<JObject>(url, token);
        }

        public static void DeleteCommunication(Guid contractGid, Guid communicationGid, string body, string accessToken)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractregs/contracts/{1}/communications/{2}",
                    serverLocation,
                    contractGid,
                    communicationGid
                );

            url = url.RemoveWhiteSpaces();

            ApiRequest.DeleteAuthorizationRequest<JObject>(url, accessToken, body);
        }

        public static JObject CreateCommunication(Guid contractGid, string type, string token, string body)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractregs/contracts/{1}/communications?type={2}",
                    serverLocation,
                    contractGid,
                    type
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.PostAuthorizationRequest<JObject>(url, token, body);
        }

        public static JObject PutCommunication(Guid contractGid, Guid communicationGid, string token, string body)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractregs/contracts/{1}/communications/{2}",
                    serverLocation,
                    contractGid,
                    communicationGid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.PutAuthorizationRequest<JObject>(url, token, body);
        }

        public static JObject SubmitCommunication(Guid contractGid, Guid communicationGid, string token, string body)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractregs/contracts/{1}/communications/{2}/send",
                    serverLocation,
                    contractGid,
                    communicationGid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.PostAuthorizationRequest<JObject>(url, token, body);
        }

        #endregion

        #region Private

        public static JObject PrivateGetCommunication(Guid communicationGid, string token)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractCommunications/{1}",
                    serverLocation,
                    communicationGid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.GetAuthorizationRequest<JObject>(url, token);
        }

        public static JObject PrivatePutCommunication(Guid communicationGid, string token, string body)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractCommunications/{1}",
                    serverLocation,
                    communicationGid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.PutAuthorizationRequest<JObject>(url, token, body);
        }

        public static JObject PrivateSubmitCommunication(Guid communicationGid, string token, string body)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}contractCommunications/{1}/send",
                    serverLocation,
                    communicationGid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.PostAuthorizationRequest<JObject>(url, token, body);
        }

        #endregion
    }
}