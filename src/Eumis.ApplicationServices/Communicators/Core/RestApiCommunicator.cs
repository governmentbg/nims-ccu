using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Eumis.ApplicationServices.Communicators
{
    public class RestApiCommunicator
    {
        private Uri baseUrl;

        public RestApiCommunicator(Uri baseUrl)
        {
            if (baseUrl.Scheme.Equals("https", StringComparison.OrdinalIgnoreCase))
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            }

            this.baseUrl = baseUrl;
        }

        public TResult PostJson<TResult>(string relativePath, object o)
        {
            HttpWebRequest request =
                (HttpWebRequest)WebRequest.Create(
                    new Uri(
                        this.baseUrl,
                        relativePath));

            request.AllowAutoRedirect = false;

            request.Method = "POST";
            request.ContentType = "application/json; charset=utf-8";
#if DEBUG
            request.Timeout = System.Threading.Timeout.Infinite;
#endif

            using (var stream = request.GetRequestStream())
            using (var streamWriter = new StreamWriter(stream))
            {
                string data = JsonConvert.SerializeObject(o);

                streamWriter.Write(data);
            }

            try
            {
                using (var response = (HttpWebResponse)request.GetResponse())
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    var body = reader.ReadToEnd();

                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        throw this.CreateStatusNotOKException(response.StatusCode, body);
                    }

                    return JsonConvert.DeserializeObject<TResult>(body);
                }
            }
            catch (WebException e)
            {
                if (e.Response == null)
                {
                    throw;
                }

                using (var reader = new StreamReader(e.Response.GetResponseStream()))
                {
                    var body = reader.ReadToEnd();

                    throw this.CreateStatusNotOKException(((HttpWebResponse)e.Response).StatusCode, body);
                }
            }
        }

        public async Task<TResult> PostJsonAsync<TResult>(string relativePath, object o)
        {
            HttpWebRequest request =
                (HttpWebRequest)WebRequest.Create(
                    new Uri(
                        this.baseUrl,
                        relativePath));

            request.AllowAutoRedirect = false;

            request.Method = "POST";
            request.ContentType = "application/json; charset=utf-8";
#if DEBUG
            request.Timeout = System.Threading.Timeout.Infinite;
#endif

            using (var stream = await request.GetRequestStreamAsync())
            using (var streamWriter = new StreamWriter(stream))
            {
                string data = JsonConvert.SerializeObject(o);

                await streamWriter.WriteAsync(data);
            }

            try
            {
                using (var response = (HttpWebResponse)await request.GetResponseAsync())
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    var body = await reader.ReadToEndAsync();

                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        throw this.CreateStatusNotOKException(response.StatusCode, body);
                    }

                    return JsonConvert.DeserializeObject<TResult>(body);
                }
            }
            catch (WebException e)
            {
                if (e.Response == null)
                {
                    throw;
                }

                using (var reader = new StreamReader(e.Response.GetResponseStream()))
                {
                    var body = await reader.ReadToEndAsync();

                    throw this.CreateStatusNotOKException(((HttpWebResponse)e.Response).StatusCode, body);
                }
            }
        }

        public void PostJson(string relativePath, object o)
        {
            HttpWebRequest request =
                (HttpWebRequest)WebRequest.Create(
                    new Uri(
                        this.baseUrl,
                        relativePath));

            request.AllowAutoRedirect = false;

            request.Method = "POST";
            request.ContentType = "application/json; charset=utf-8";
#if DEBUG
            request.Timeout = System.Threading.Timeout.Infinite;
#endif

            using (var stream = request.GetRequestStream())
            using (var streamWriter = new StreamWriter(stream))
            {
                string data = JsonConvert.SerializeObject(o);

                streamWriter.Write(data);
            }

            try
            {
                using (var response = (HttpWebResponse)request.GetResponse())
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    var body = reader.ReadToEnd();

                    if (response.StatusCode != HttpStatusCode.NoContent)
                    {
                        throw this.CreateStatusNotOKException(response.StatusCode, body);
                    }
                }
            }
            catch (WebException e)
            {
                if (e.Response == null)
                {
                    throw;
                }

                using (var reader = new StreamReader(e.Response.GetResponseStream()))
                {
                    var body = reader.ReadToEnd();

                    throw this.CreateStatusNotOKException(((HttpWebResponse)e.Response).StatusCode, body);
                }
            }
        }

        private Exception CreateStatusNotOKException(HttpStatusCode status, string body)
        {
            return new Exception(
                $"Status code 200 expected, instead got {Enum.GetName(typeof(HttpStatusCode), status)}!\n{body}");
        }
    }
}
