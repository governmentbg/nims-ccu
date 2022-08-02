using Monitorstat.IntegrationEumis.Host.Helpers;
using Monitorstat.IntegrationEumis.Host.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;

namespace Monitorstat.IntegrationEumis.Host.Communicators
{
    public class BlobCommunicator : IBlobCommunicator
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public BlobInfo UploadBlob(string filename, Stream blobStream)
        {
            Logger.Info($"{nameof(this.UploadBlob)}({filename})");

            using (var client = new HttpClient())
            {
                HttpContent fileStreamContent = new StreamContent(blobStream);

                using (var formData = new MultipartFormDataContent())
                {
                    formData.Add(fileStreamContent, "file", filename);

                    string blobServerLocation = Configuration.InternalBlobServerLocation;

                    var blobServerResponse =
                        client.PostAsync(new Uri(blobServerLocation, UriKind.Absolute), formData).Result;

                    if (!blobServerResponse.IsSuccessStatusCode)
                    {
                        Logger.Error($"Blob upload fail: {blobServerResponse.StatusCode}");
                        throw this.CreateStatusNotOKException(blobServerResponse.StatusCode);
                    }

                    var blobServerResponseString = blobServerResponse.Content.ReadAsStringAsync().Result;

                    var blobServerResponseObject = JsonConvert.DeserializeObject<JObject>(blobServerResponseString);

                    var blobInfo = new BlobInfo
                    {
                        FileKey = Guid.Parse(blobServerResponseObject.SelectToken("fileKey").Value<string>()),
                        AccessToken = blobServerResponseObject.SelectToken("accessToken").Value<string>(),
                        Size = blobServerResponseObject.SelectToken("size").Value<long>(),
                        Hash = blobServerResponseObject.SelectToken("hash").Value<string>(),
                        FileName = filename,
                    };

                    Logger.Info($"{nameof(this.UploadBlob)} <= {blobInfo}");

                    return blobInfo;
                }
            }
        }

        public BlobInfo UploadBlob(Monitorstat.Common.MonitorstatService.File file)
        {
            using (var stream = new MemoryStream(file.Content))
            {
                return this.UploadBlob(file.Name, stream);
            }
        }

        private Exception CreateStatusNotOKException(HttpStatusCode status)
        {
            return new Exception($"Status code 200 expected, instead got error!\n{Enum.GetName(typeof(HttpStatusCode), status)}");
        }
    }
}
