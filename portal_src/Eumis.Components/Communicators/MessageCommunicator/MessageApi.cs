using Newtonsoft.Json.Linq;
using System;
using System.Configuration;
using Eumis.Common.Helpers;

namespace Eumis.Components.Communicators
{
    public class MessageApi
    {
        #region Private

        public static JObject GetProjectMessage(Guid gid, string accessToken, string type)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}projectMessages/{1}?type={2}",
                    serverLocation,
                    gid.ToString(),
                    type
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.GetAuthorizationRequest<JObject>(url, accessToken);
        }

        public static JObject GetProjectMessageAnswer(Guid communicationGid, Guid answerGid, string accessToken)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}projectMessages/{1}/answers/{2}",
                    serverLocation,
                    communicationGid.ToString(),
                    answerGid.ToString()
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.GetAuthorizationRequest<JObject>(url, accessToken);
        }

        public static JObject PutProjectMessage(Guid gid, string token, string body)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}projectMessages/{1}/",
                    serverLocation,
                    gid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.PutAuthorizationRequest<JObject>(url, token, body);
        }

        public static void SubmitProjectMessage(Guid gid, string token, string body)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}projectMessages/{1}/activate",
                    serverLocation,
                    gid
                );

            url = url.RemoveWhiteSpaces();

            ApiRequest.PostAuthorizationRequest<JObject>(url, token, body);
        }

        public static JObject GetMessageProjectFilesZip(string messageGid, string answerGid, string messageType, string accessToken)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}projectMessages/{1}/getCommunicationProjectFilesZip?messageType={2}&answerGid={3}",
                    serverLocation,
                    messageGid,
                    messageType,
                    answerGid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.GetAuthorizationRequest<JObject>(url, accessToken);
        }

        public static JObject GetQuestionFilesZip(string messageGid, string accessToken)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}projectMessages/{1}/getCommunicationFilesZip",
                    serverLocation,
                    messageGid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.GetAuthorizationRequest<JObject>(url, accessToken);
        }

        public static JObject GetAnswerFilesZip(string communicationGid, string answerGid, string accessToken)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}projectMessages/{1}/getCommunicationAnswerFilesZip?answerGid={2}",
                    serverLocation,
                    communicationGid,
                    answerGid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.GetAuthorizationRequest<JObject>(url, accessToken);
        }

        #endregion

        #region Public

        public static JObject GetMessages(string accessToken)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}registration/messages",
                    serverLocation
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.GetAuthorizationRequest<JObject>(url, accessToken);
        }
        
        public static JObject GetMessage(Guid gid, string accessToken)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}registration/messages/{1}",
                    serverLocation,
                    gid.ToString()
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.GetAuthorizationRequest<JObject>(url, accessToken);
        }

        public static JObject UpdateAnswer(Guid communicationGid, Guid answerGid, string body, string accessToken)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}registration/messages/{1}/answers/{2}",
                    serverLocation,
                    communicationGid.ToString(),
                    answerGid.ToString()
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.PutAuthorizationRequest<JObject>(url, accessToken, body);
        }

        public static void FinalizeAnswer(Guid communicationGid, Guid answerGid, string body, string accessToken)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}registration/messages/{1}/answers/{2}/finalize",
                    serverLocation,
                    communicationGid.ToString(),
                    answerGid.ToString()
                );

            url = url.RemoveWhiteSpaces();

            ApiRequest.PostAuthorizationRequest<JObject>(url, accessToken, body);
        }

        public static void DefinalizeAnswer(Guid communicationGid, Guid answerGid, string body, string accessToken)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}registration/messages/{1}/answers/{2}/definalize",
                    serverLocation,
                    communicationGid.ToString(),
                    answerGid.ToString()
                );

            url = url.RemoveWhiteSpaces();

            ApiRequest.PostAuthorizationRequest<JObject>(url, accessToken, body);
        }

        public static JObject SubmitAnswer(Guid communicationGid, Guid answerGid, string body, string accessToken)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}registration/messages/{1}/answers/{2}/submit",
                    serverLocation,
                    communicationGid.ToString(),
                    answerGid.ToString()
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.PostAuthorizationRequest<JObject>(url, accessToken, body);
        }

        public static JObject SendAnswer(string body, string accessToken)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}registration/messages/send",
                    serverLocation
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.PostAuthorizationRequest<JObject>(url, accessToken, body);
        }

        public static JObject GetNewAnswer(Guid communicationGid, string body, string accessToken)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}registration/messages/{1}/answers/new",
                    serverLocation,
                    communicationGid.ToString()
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.PostAuthorizationRequest<JObject>(url, accessToken, body);
        }

        public static JObject GetAnswer(Guid communicationGid, Guid answerGid, string accessToken)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}registration/messages/{1}/answers/{2}",
                    serverLocation,
                    communicationGid.ToString(),
                    answerGid.ToString()
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.GetAuthorizationRequest<JObject>(url, accessToken);
        }

        public static void DeleteAnswer(Guid communicationGid, Guid answerGid, string body, string accessToken)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}registration/messages/{1}/answers/{2}/delete",
                    serverLocation,
                    communicationGid,
                    answerGid
                );

            url = url.RemoveWhiteSpaces();

            ApiRequest.DeleteAuthorizationRequest<JObject>(url, accessToken, body);
        }

        public static JObject GetCounts(string accessToken)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}registration/messages/count",
                    serverLocation
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.GetAuthorizationRequest<JObject>(url, accessToken);
        }

        #endregion
    }
}
