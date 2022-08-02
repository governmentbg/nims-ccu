using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Http;
using Eumis.Common.Config;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Eumis.ApplicationServices.Communicators
{
    public class BlobServerCommunicator : IBlobServerCommunicator
    {
        public string GetBlobAccessToken(Guid blobKey, bool useUploaderToken)
        {
            HttpWebRequest request =
                (HttpWebRequest)HttpWebRequest.Create(
                    new Uri(
                        ConfigurationManager.AppSettings.GetWithEnv("Eumis.Web.Host:BlobServerTokenLocation"),
                        UriKind.Absolute));
            request.Method = "POST";
            request.AllowAutoRedirect = false;
            request.Accept = "application/json, text/plain, */*";
            request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";

            var credentials = ConfigurationManager.AppSettings.GetWithEnv("Eumis.Web.Host:BlobServerCredentials").Split(',');
            var body = string.Format("client_id={0}&client_secret={1}&grant_type=client_credentials&scope={2}", credentials[0], credentials[1], blobKey.ToString());

            if (useUploaderToken)
            {
                body += ":uploader";
            }

            using (StreamWriter stOut = new StreamWriter(request.GetRequestStream(), System.Text.Encoding.UTF8))
            {
                stOut.Write(body);
                stOut.Close();
            }

            string accessToken;
            using (var response = request.GetResponse())
            using (var responseStream = response.GetResponseStream())
            using (var reader = new StreamReader(responseStream))
            {
                var responseFromServer = reader.ReadToEnd();
                var responseFromServerDes = JsonConvert.DeserializeObject<JObject>(responseFromServer);
                accessToken = responseFromServerDes.SelectToken("access_token").Value<string>();
            }

            return accessToken;
        }

        public Uri GetBlobUriWithAccessToken(Guid blobKey, bool useUploaderToken)
        {
            string accessToken = this.GetBlobAccessToken(blobKey, useUploaderToken);

            string blobServerLocation = ConfigurationManager.AppSettings.GetWithEnv("Eumis.Web.Host:InternalBlobServerLocation");
            if (!blobServerLocation.EndsWith("/"))
            {
                blobServerLocation += "/";
            }

            return new Uri(
                new Uri(blobServerLocation),
                blobKey.ToString() + string.Format("?access_token={0}", accessToken));
        }

        public Stream GetBlobStream(Guid blobKey, bool useUploaderToken)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(this.GetBlobUriWithAccessToken(blobKey, useUploaderToken));
            request.AllowAutoRedirect = false;
            request.Method = "GET";
            request.Accept = "application/json";

            try
            {
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        throw this.CreateStatusNotOKException(response.StatusCode);
                    }

                    var stream = new MemoryStream();

                    response.GetResponseStream().CopyTo(stream);

                    return stream;
                }
            }
            catch (WebException e)
            {
                using (var reader = new StreamReader(e.Response.GetResponseStream()))
                {
                    var respBody = reader.ReadToEnd();

                    throw this.CreateStatusNotOKException(((HttpWebResponse)e.Response).StatusCode);
                }
            }
        }

        public BlobInfo UploadBlob(string filename, Stream blobStream)
        {
            using (var client = new HttpClient())
            {
                HttpContent fileStreamContent = new StreamContent(blobStream);

                using (var formData = new MultipartFormDataContent())
                {
                    formData.Add(fileStreamContent, "file", filename);

                    string blobServerLocation = ConfigurationManager.AppSettings.GetWithEnv("Eumis.Web.Host:InternalBlobServerLocation");

                    var blobServerResponse =
                        client.PostAsync(new Uri(blobServerLocation, UriKind.Absolute), formData).Result;

                    if (!blobServerResponse.IsSuccessStatusCode)
                    {
                        throw this.CreateStatusNotOKException(blobServerResponse.StatusCode);
                    }

                    var blobServerResponseString = blobServerResponse.Content.ReadAsStringAsync().Result;

                    var blobServerResponseObject = JsonConvert.DeserializeObject<JObject>(blobServerResponseString);

                    return new BlobInfo
                    {
                        FileKey = Guid.Parse(blobServerResponseObject.SelectToken("fileKey").Value<string>()),
                        AccessToken = blobServerResponseObject.SelectToken("accessToken").Value<string>(),
                        Size = blobServerResponseObject.SelectToken("size").Value<long>(),
                        Hash = blobServerResponseObject.SelectToken("hash").Value<string>(),
                    };
                }
            }
        }

        private Exception CreateStatusNotOKException(HttpStatusCode status)
        {
            return new Exception($"Status code 200 expected, instead got error!\n{Enum.GetName(typeof(HttpStatusCode), status)}");
        }
    }
}
