using System;
using Newtonsoft.Json.Linq;
using System.Configuration;
using Eumis.Common.Helpers;

namespace Eumis.Components.Communicators
{
    public class StandpointApi
    {
        #region Private

        public static JObject GetEvalSessionStandpointXml(string accessToken, Guid standpointGid)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}standpoints/{1}",
                    serverLocation,
                    standpointGid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.GetAuthorizationRequest<JObject>(url, accessToken);
        }

        public static JObject UpdateEvalSessionStandpointXml(string accessToken, Guid standpointGid, string body)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}standpoints/{1}",
                    serverLocation,
                    standpointGid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.PutAuthorizationRequest<JObject>(url, accessToken, body);
        }

        public static JObject SubmitEvalSessionStandpointXml(string accessToken, Guid standpointGid, string body)
        {
            string serverLocation = ApiCommon.ServerLocation;

            var url = string.Format
                (
                    "{0}standpoints/{1}/submit",
                    serverLocation,
                    standpointGid
                );

            url = url.RemoveWhiteSpaces();

            return ApiRequest.PostAuthorizationRequest<JObject>(url, accessToken, body);
        }

        #endregion
    }
}