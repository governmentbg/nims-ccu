using Eumis.Common.Helpers;
using Newtonsoft.Json.Linq;
using System;

namespace Eumis.Components.Communicators
{
    public class ProjectCommunicationApi
    {
        #region Portal

        public static JObject GetCommunications(Guid registeredGid, string accessToken, int limit, int offset)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}registration/projectCommunications/getAll?registeredGid={1}&limit={2}&offset={3}",
                    serverLocation,
                    registeredGid,
                    limit,
                    offset
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.GetAuthorizationRequest<JObject>(url, accessToken);
        }

        public static JObject GetNewProjectCommunication(Guid registeredGid, string accessToken)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}registration/projectCommunications/new?registeredGid={1}",
                    serverLocation,
                    registeredGid.ToString()
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.PostAuthorizationRequest<JObject>(url, accessToken, string.Empty);
        }

        public static void DeleteProjectCommunication(Guid communicationGid, string accessToken)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}registration/projectCommunications/{1}",
                    serverLocation,
                    communicationGid.ToString()
                );

            url = url.RemoveWhiteSpaces();

            ApiRequest.DeleteAuthorizationRequest<JObject>(url, accessToken, string.Empty);
        }

        public static void CancelProjectCommunication(Guid communicationGid, string accessToken, string body)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}registration/projectCommunications/{1}/cancel",
                    serverLocation,
                    communicationGid.ToString()
                );

            url = url.RemoveWhiteSpaces();

            ApiRequest.PostAuthorizationRequest<JObject>(url, accessToken, body);
        }

        public static JObject GetProjectCommunication(Guid communicatioGid, string accessToken)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}registration/projectCommunications/{1}",
                    serverLocation,
                    communicatioGid.ToString()
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.GetAuthorizationRequest<JObject>(url, accessToken);
        }

        public static JObject PutProjectCommunication(Guid communicationGid, string accessToken, string body)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}registration/projectCommunications/{1}",
                    serverLocation,
                    communicationGid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.PutAuthorizationRequest<JObject>(url, accessToken, body);
        }

        public static void SubmitProjectCommunication(Guid communicationGid, string accessToken, string body)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}registration/projectCommunications/{1}/submit",
                    serverLocation,
                    communicationGid
                );

            url = url.RemoveWhiteSpaces();

            ApiRequest.PostAuthorizationRequest<JObject>(url, accessToken, body);
        }

        public static JObject GetSentProjectCommunicationInfo(Guid answerGid, string accessToken)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}registration/projectCommunications/{1}/sentInfo",
                    serverLocation,
                    answerGid.ToString()
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.GetAuthorizationRequest<JObject>(url, accessToken);
        }

        public static JObject GetProjectCommunicationAnswer(Guid communicationGid, Guid answerGid, string accessToken)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}registration/projectCommunicationAnswers/{1}?communicationGid={2}",
                    serverLocation,
                    answerGid.ToString(),
                    communicationGid.ToString()
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.GetAuthorizationRequest<JObject>(url, accessToken);
        }

        public static JObject GetNewProjectCommunicationAnswer(Guid communicationGid, string accessToken)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}registration/projectCommunicationAnswers/new?communicationGid={1}",
                    serverLocation,
                    communicationGid.ToString()
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.PostAuthorizationRequest<JObject>(url, accessToken, string.Empty);
        }

        public static void DeleteProjectCommunicationAnswer(Guid communicationGid, Guid answerGid, string accessToken, string body)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}registration/projectCommunicationAnswers/{1}?communicationGid={2}",
                    serverLocation,
                    answerGid.ToString(),
                    communicationGid.ToString()
                );

            url = url.RemoveWhiteSpaces();

            ApiRequest.DeleteAuthorizationRequest<JObject>(url, accessToken, body);
        }

        public static JObject PutProjectCommunicationAnswer(Guid communicationGid, Guid answerGid, string accessToken, string body)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}registration/projectCommunicationAnswers/{1}?communicationGid={2}",
                    serverLocation,
                    answerGid,
                    communicationGid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.PutAuthorizationRequest<JObject>(url, accessToken, body);
        }

        public static void SubmitProjectCommunicationAnswer(Guid communicationGid, Guid answerGid, string accessToken, string body)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}registration/projectCommunicationAnswers/{1}/submit?communicationGid={2}",
                    serverLocation,
                    answerGid,
                    communicationGid
                );

            url = url.RemoveWhiteSpaces();

            ApiRequest.PostAuthorizationRequest<JObject>(url, accessToken, body);
        }

        public static JObject GetSentProjectCommunicationAnswerInfo(Guid answerGid, string accessToken)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}registration/projectCommunicationAnswers/{1}/sentInfo",
                    serverLocation,
                    answerGid.ToString()
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.GetAuthorizationRequest<JObject>(url, accessToken);
        }

        public static bool AssertProjectHasCommunications(Guid registeredGid, string accessToken)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}registration/projectCommunications/hasCommunications?registeredGid={1}",
                    serverLocation,
                    registeredGid.ToString()
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.GetAuthorizationRequest<bool>(url, accessToken);
        }

        public static bool UserHasNewCommunnications(string accessToken)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}registration/projectCommunications/hasNewCommunications",
                    serverLocation
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.GetAuthorizationRequest<bool>(url, accessToken);
        }

        #endregion

        #region Private

        public static JObject PrivateGetProjectCommunication(Guid gid, string accessToken)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}projectManagingAuthorityCommunications/{1}",
                    serverLocation,
                    gid.ToString()
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.GetAuthorizationRequest<JObject>(url, accessToken);
        }

        public static JObject PrivatePutProjectCommunication(Guid gid, string token, string body)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}projectManagingAuthorityCommunications/{1}/",
                    serverLocation,
                    gid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.PutAuthorizationRequest<JObject>(url, token, body);
        }

        public static void PrivateSubmitProjectCommunication(Guid gid, string token, string body)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}projectManagingAuthorityCommunications/{1}/activate",
                    serverLocation,
                    gid
                );

            url = url.RemoveWhiteSpaces();

            ApiRequest.PostAuthorizationRequest<JObject>(url, token, body);
        }

        public static JObject PrivateGetProjectCommunicationAnswer(Guid communicationGid, Guid answerGid, string accessToken)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}projectManagingAuthorityCommunicationAnswers/{1}?communicationGid={2}",
                    serverLocation,
                    answerGid.ToString(),
                    communicationGid.ToString()
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.GetAuthorizationRequest<JObject>(url, accessToken);
        }

        public static JObject PrivatePutProjectCommunicationAnswer(Guid communicationGid, Guid answerGid, string accessToken, string body)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}projectManagingAuthorityCommunicationAnswers/{1}?communicationGid={2}",
                    serverLocation,
                    answerGid,
                    communicationGid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.PutAuthorizationRequest<JObject>(url, accessToken, body);
        }

        public static void PrivateSubmitProjectCommunicationAnswer(Guid communicationGid, Guid answerGid, string token, string body)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}projectManagingAuthorityCommunicationAnswers/{1}/activate?communicationGid={2}",
                    serverLocation,
                    answerGid,
                    communicationGid
                );

            url = url.RemoveWhiteSpaces();

            ApiRequest.PostAuthorizationRequest<JObject>(url, token, body);
        }

        #endregion
    }
}
