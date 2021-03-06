using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.ApplicationServices.Communicators
{
    internal static class ApiRequest
    {
        public static T GetAnonymousRequest<T>(string url)
        {
            var request = GetRequest(url);

            return ReadResponce<T>(request);
        }

        public static T GetAuthorizationRequest<T>(string url, string accessToken)
        {
            var request = GetRequest(url);

            request.Headers.Add(HttpRequestHeader.Authorization, string.Format("Bearer {0}", accessToken));

            return ReadResponce<T>(request);
        }

        public static T PutAnonymousRequest<T>(string url, string body)
        {
            var request = PutRequest(url, body);

            return ReadResponce<T>(request);
        }

        public static T PutAuthorizationRequest<T>(string url, string accessToken, string body)
        {
            var request = PutRequest(url, body);

            request.Headers.Add(HttpRequestHeader.Authorization, string.Format("Bearer {0}", accessToken));

            return ReadResponce<T>(request);
        }

        public static T PostAnonymousRequest<T>(string url, string body)
        {
            var request = PostRequest(url, body);

            return ReadResponce<T>(request);
        }

        public static T PostAccessTokenRequest<T>(string url, string body)
        {
            var request = PostFormUrlEncodedRequestRequest(url, body);

            return ReadResponce<T>(request);
        }

        public static T PostAuthorizationRequest<T>(string url, string accessToken, string body)
        {
            var request = PostRequest(url, body);

            request.Headers.Add(HttpRequestHeader.Authorization, string.Format("Bearer {0}", accessToken));

            return ReadResponce<T>(request);
        }

        public static T DeleteAnonymousRequest<T>(string url, string body)
        {
            var request = DeleteRequest(url, body);

            return ReadResponce<T>(request);
        }

        public static T DeleteAuthorizationRequest<T>(string url, string accessToken, string body)
        {
            var request = DeleteRequest(url, body);

            request.Headers.Add(HttpRequestHeader.Authorization, string.Format("Bearer {0}", accessToken));

            return ReadResponce<T>(request);
        }

        #region Private

        private static HttpWebRequest GetRequest(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(url));
            request.Timeout = 5000;
#if DEBUG
            request.Timeout = System.Threading.Timeout.Infinite;
#endif
            request.Method = "GET";
            request.AllowAutoRedirect = false;
            request.Accept = "application/json, text/plain, */*";
            request.ContentType = "application/json; charset=UTF-8";

            return request;
        }

        private static HttpWebRequest PutRequest(string url, string body)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(url));
            request.Timeout = 5000;
#if DEBUG
            request.Timeout = System.Threading.Timeout.Infinite;
#endif
            request.Method = "PUT";
            request.AllowAutoRedirect = false;
            request.Accept = "application/json, text/plain, */*";
            request.ContentType = "application/json; charset=UTF-8";

            using (StreamWriter stOut = new StreamWriter(request.GetRequestStream(), System.Text.Encoding.UTF8))
            {
                stOut.Write(body);
                stOut.Close();
            }

            return request;
        }

        private static HttpWebRequest PostRequest(string url, string body)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(url));
            request.Timeout = 5000;
#if DEBUG
            request.Timeout = System.Threading.Timeout.Infinite;
#endif
            request.Method = "POST";
            request.AllowAutoRedirect = false;
            request.Accept = "application/json, text/plain, */*";
            request.ContentType = "application/json; charset=UTF-8";

            using (StreamWriter stOut = new StreamWriter(request.GetRequestStream(), System.Text.Encoding.UTF8))
            {
                stOut.Write(body);
                stOut.Close();
            }

            return request;
        }

        private static HttpWebRequest PostFormUrlEncodedRequestRequest(string url, string body)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(url));
            request.Timeout = 5000;
#if DEBUG
            request.Timeout = System.Threading.Timeout.Infinite;
#endif
            request.Method = "POST";
            request.AllowAutoRedirect = false;
            request.Accept = "application/json, text/plain, */*";
            request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";

            using (StreamWriter stOut = new StreamWriter(request.GetRequestStream(), System.Text.Encoding.UTF8))
            {
                stOut.Write(body);
                stOut.Close();
            }

            return request;
        }

        private static HttpWebRequest DeleteRequest(string url, string body)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(url));
            request.Timeout = 5000;
#if DEBUG
            request.Timeout = System.Threading.Timeout.Infinite;
#endif
            request.Method = "DELETE";
            request.AllowAutoRedirect = false;
            request.Accept = "application/json, text/plain, */*";
            request.ContentType = "application/json; charset=UTF-8";

            using (StreamWriter stOut = new StreamWriter(request.GetRequestStream(), System.Text.Encoding.UTF8))
            {
                stOut.Write(body);
                stOut.Close();
            }

            return request;
        }

        private static T ReadResponce<T>(HttpWebRequest request)
        {
            T jO;

            using (var response = request.GetResponse())
            using (var responseStream = response.GetResponseStream())
            using (var reader = new StreamReader(responseStream))
            {
                var responseFromServer = reader.ReadToEnd();
                jO = JsonConvert.DeserializeObject<T>(responseFromServer);
            }

            return jO;
        }

        #endregion
    }
}
