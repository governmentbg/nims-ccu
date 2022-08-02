using System;
using Newtonsoft.Json.Linq;
using System.Configuration;
using System.IO;
using Newtonsoft.Json;
using System.Net;
using Eumis.Common.Helpers;
using System.Web;
using Eumis.Common.Config;

namespace Eumis.Components.Communicators
{
    public class BlobApi
    {
        public static string GetAccessToken(Guid fileKey)
        {
            string serverLocation = ConfigurationManager.AppSettings.GetWithEnv("Eumis.Components:InternalBlobServerLocation");

            if (!serverLocation.EndsWith("/"))
            {
                serverLocation += "/";
            }

            var url = string.Format
                (
                    "{0}api/token",
                    serverLocation
                );

            url = url.RemoveWhiteSpaces();

            var credentials = ConfigurationManager.AppSettings.GetWithEnv("Eumis.Components:BlobServerCredentials").Split(',');
            var body = string.Format("client_id={0}&client_secret={1}&grant_type=client_credentials&scope={2}",
                HttpUtility.UrlEncode(credentials[0]),
                HttpUtility.UrlEncode(credentials[1]),
                HttpUtility.UrlEncode(fileKey.ToString()));

            return ApiRequest.PostAccessTokenRequest<JObject>(url, body).SelectToken("access_token").Value<string>();
        }

        public static Uri CreateRedirectUri(Guid fileKey, string accessToken)
        {
            string blobServerLocation = ConfigurationManager.AppSettings.GetWithEnv("Eumis.Components:InternalBlobServerLocation");

            if (!blobServerLocation.EndsWith("/"))
            {
                blobServerLocation += "/";
            }

            return new Uri(new Uri(blobServerLocation), fileKey.ToString() + String.Format("?access_token={0}", accessToken));
        }
    }
}