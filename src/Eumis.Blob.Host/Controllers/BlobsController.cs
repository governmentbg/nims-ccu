using Eumis.Blob.Host.Api;
using Eumis.Blob.Host.Auth;
using Eumis.Common.Api;
using Eumis.Common.Config;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Eumis.Blob.Host.Controllers
{
    public class BlobsController : ApiController
    {
        public static readonly string CurrentBlobDbConnectionStringName = ConfigurationManager.AppSettings.GetWithEnv("Eumis.Blob.Host:CurrentBlobDbConnectionString");
        public static readonly string CurrentBlobDbConnectionString = ConfigurationManager.ConnectionStrings[CurrentBlobDbConnectionStringName].ConnectionString.ExpandEnv();
        public static readonly string MainDbConnectionString = ConfigurationManager.ConnectionStrings["DbContext"].ConnectionString.ExpandEnv();
        public static readonly bool AssertExistsBlobReference = bool.Parse(ConfigurationManager.AppSettings.GetWithEnv("Eumis.Blob.Host:AssertExistsBlobReference"));

        public BlobsController()
        {
        }

        [Route("{fileKey:guid}")]
        public async Task<HttpResponseMessage> Head(Guid fileKey, CancellationToken cancellationToken)
        {
            try
            {
                var blobAccessContext = this.Request.GetBlobAccessContext();
                this.Request.GetBlobAccessContext().AssertCanAccess(fileKey);

                BlobInfo blobInfo = await this.GetBlobInfo(fileKey, blobAccessContext.IsUploader, cancellationToken);
                if (blobInfo == null)
                {
                    throw new HttpResponseException(this.Request.CreateResponse(HttpStatusCode.NotFound));
                }

                RangeInfo rangeInfo = this.GetRangeInfo(this.Request, blobInfo.FileLength);

                var response = this.Request.CreateResponse();

                // create a dummy response as the headers in WebAPI are added to a Content
                response.Content = new StringContent(string.Empty);

                this.SetResponseHeaders(response, rangeInfo, blobInfo);

                return response;
            }
            catch
            {
                // throw an OperationCanceledException if we have been canceled
                cancellationToken.ThrowIfCancellationRequested();

                // otherwise rethrow the original exception
                throw;
            }
        }

        [HttpGet]
        [Route("{fileKey:guid}")]
        public async Task<HttpResponseMessage> GetFile(Guid fileKey, CancellationToken cancellationToken)
        {
            try
            {
                var blobAccessContext = this.Request.GetBlobAccessContext();
                blobAccessContext.AssertCanAccess(fileKey);

                BlobInfo blobInfo = await this.GetBlobInfo(fileKey, blobAccessContext.IsUploader, cancellationToken);
                if (blobInfo == null)
                {
                    throw new HttpResponseException(this.Request.CreateResponse(HttpStatusCode.NotFound));
                }

                RangeInfo rangeInfo = this.GetRangeInfo(this.Request, blobInfo.FileLength);

                var response = this.Request.CreateResponse();
                response.Content = new PushStreamContent(
                    async (responseStream, httpContent, transportContext) =>
                    {
                        using (responseStream)
                        {
                            try
                            {
                                await this.CopyBlobContentToResponseStream(responseStream, rangeInfo, blobInfo, cancellationToken);
                            }
                            catch (HttpException ex)
                            {
                                // throw an OperationCanceledException if we have been canceled
                                cancellationToken.ThrowIfCancellationRequested();

                                // swallow communication exceptions, we can't do anything about them
                                if (!ex.Message.StartsWith("The remote host closed the connection.") &&
                                    !ex.Message.StartsWith("An error occurred while communicating with the remote host."))
                                {
                                    throw;
                                }
                            }
                        }
                    });

                this.SetResponseHeaders(response, rangeInfo, blobInfo);

                return response;
            }
            catch
            {
                // throw an OperationCanceledException if we have been canceled
                cancellationToken.ThrowIfCancellationRequested();

                // otherwise rethrow the original exception
                throw;
            }
        }

        [AllowAnonymous]
        [Route("")]
        public HttpResponseMessage Get()
        {
            return this.Request.CreateResponse(HttpStatusCode.MethodNotAllowed);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("")]
        public async Task<HttpResponseMessage> PostFile(CancellationToken cancellationToken)
        {
            try
            {
                if (!this.Request.Content.IsMimeMultipartContent())
                {
                    throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
                }

                using (SqlConnection blobDbConn = new SqlConnection(BlobsController.CurrentBlobDbConnectionString))
                using (SqlConnection mainDbConn = new SqlConnection(BlobsController.MainDbConnectionString))
                {
                    await blobDbConn.OpenAsync(cancellationToken);
                    await mainDbConn.OpenAsync(cancellationToken);

                    MultipartBlobData uploadedBlobData;
                    using (var multipartProvider = new MultipartBlobStreamProvider(blobDbConn, mainDbConn))
                    {
                        MultipartBlobStreamProvider blobProvider = await this.Request.Content.ReadAsMultipartAsync(multipartProvider, cancellationToken);
                        uploadedBlobData = blobProvider.BlobData.First();
                    }

                    Guid blobKey = Guid.NewGuid();
                    var createBlobCmd = mainDbConn.CreateCommand();
                    createBlobCmd.CommandText =
                        @"INSERT INTO Blobs ([Key], FileName, BlobContentLocationId, IsDeleted, CreateDate, DeleteDate) VALUES (@blobKey, @filename, @blobContentLocationId, 0 , @createDate, NULL)";
                    createBlobCmd.Parameters.AddWithValue("@blobKey", blobKey);
                    createBlobCmd.Parameters.AddWithValue("@blobContentLocationId", uploadedBlobData.BlobInfo.BlobContentLocationId);
                    createBlobCmd.Parameters.AddWithValue("@filename", HttpHelpers.UnquoteToken(uploadedBlobData.Headers.ContentDisposition.FileName));
                    createBlobCmd.Parameters.AddWithValue("@createDate", DateTime.Now);

                    await createBlobCmd.ExecuteNonQueryAsync();

                    string token = this.Request.GetOwinContext().CreateUploaderAccessToken(blobKey);

                    if (string.IsNullOrEmpty(token))
                    {
                        throw new NullReferenceException("Token is empty string, check Eumis.Blob.Host:OAuthAllowInsecureHttp key");
                    }

                    var result =
                        new
                        {
                            fileKey = blobKey,
                            accessToken = token,
                            size = uploadedBlobData.BlobInfo.Size,
                            hash = uploadedBlobData.BlobInfo.Hash,
                        };

                    // fix ie9 not supporting json
                    if (this.Request.Headers.Accept.Any(a => a.MediaType == "application/json"))
                    {
                        return this.Request.CreateResponse(HttpStatusCode.OK, result);
                    }
                    else
                    {
                        var response = this.Request.CreateResponse(HttpStatusCode.OK, result, "text/html");

                        // fix IE9 replacing the contents of the iframe, used by iframe-transport
                        response.Headers.Add("X-Frame-Options", "SameOrigin");
                        return response;
                    }
                }
            }
            catch
            {
                // throw an OperationCanceledException if we have been canceled
                cancellationToken.ThrowIfCancellationRequested();

                // otherwise rethrow the original exception
                throw;
            }
        }

        #region Private

        private async Task CopyBlobContentToResponseStream(Stream responseStream, RangeInfo rangeInfo, BlobInfo blobInfo, CancellationToken cancellationToken)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings[blobInfo.ContentDbCSName].ConnectionString.ExpandEnv()))
            {
                await connection.OpenAsync(cancellationToken);

                using (SqlCommand command = connection.CreateCommand())
                {
                    if (rangeInfo != null)
                    {
                        command.CommandText = "SELECT SUBSTRING([Content], @offset , @length) FROM BlobContents WHERE BlobContentId = @blobContentId AND PartitionId = @partitionId AND IsDeleted = 0";
                        command.Parameters.AddWithValue("@offset", rangeInfo.From + 1);
                        command.Parameters.AddWithValue("@length", rangeInfo.Length);
                        command.Parameters.AddWithValue("@blobContentId", blobInfo.BlobContentId);
                        command.Parameters.AddWithValue("@partitionId", blobInfo.PartitionId);
                    }
                    else
                    {
                        command.CommandText = "SELECT [Content] FROM BlobContents WHERE BlobContentId = @blobContentId AND PartitionId = @partitionId AND IsDeleted = 0";
                        command.Parameters.AddWithValue("@blobContentId", blobInfo.BlobContentId);
                        command.Parameters.AddWithValue("@partitionId", blobInfo.PartitionId);
                    }

                    // The reader needs to be executed with the SequentialAccess behavior to enable network streaming
                    // Otherwise ReadAsync will buffer the entire BLOB into memory which can cause scalability issues or even OutOfMemoryExceptions
                    using (SqlDataReader reader = await command.ExecuteReaderAsync(CommandBehavior.SequentialAccess, cancellationToken))
                    {
                        if (await reader.ReadAsync(cancellationToken))
                        {
                            if (!(await reader.IsDBNullAsync(0, cancellationToken)))
                            {
                                using (Stream data = reader.GetStream(0))
                                {
                                    await data.CopyToAsync(responseStream, 4096/*default*/, cancellationToken);
                                }
                            }
                        }
                    }
                }
            }
        }

        private RangeInfo GetRangeInfo(HttpRequestMessage request, long contentLength)
        {
            RangeHeaderValue rangeHeader = request.Headers.Range;
            if (rangeHeader != null && rangeHeader.Ranges.Count > 0)
            {
                // we support only one range
                if (rangeHeader.Ranges.Count > 1)
                {
                    throw new HttpResponseException(HttpStatusCode.RequestedRangeNotSatisfiable);
                }

                RangeItemHeaderValue range = rangeHeader.Ranges.First();

                // check if range is satisfiable
                // http://www.w3.org/Protocols/rfc2616/rfc2616-sec14.html#sec14.35.1
                if (range.From < 0 ||
                    range.From > range.To ||
                    (range.From == null && range.To == null))
                {
                    throw new HttpResponseException(HttpStatusCode.RequestedRangeNotSatisfiable);
                }

                long from;
                long to;
                long length;

                if (range.From == null)
                {
                    from = contentLength - range.To.Value;
                    to = contentLength - 1;
                }
                else if (range.To == null)
                {
                    from = range.From.Value;
                    to = contentLength - 1;
                }
                else
                {
                    from = range.From.Value;
                    to = Math.Min(range.To.Value, contentLength - 1);
                }

                length = to - from + 1;

                return new RangeInfo
                {
                    From = from,
                    To = to,
                    Length = length,
                };
            }

            return null;
        }

        private void SetResponseHeaders(HttpResponseMessage response, RangeInfo rangeInfo, BlobInfo blobInfo)
        {
            if (rangeInfo != null)
            {
                response.StatusCode = HttpStatusCode.PartialContent;
                response.Content.Headers.ContentLength = rangeInfo.Length;
                response.Content.Headers.ContentRange = new ContentRangeHeaderValue(rangeInfo.From, rangeInfo.To, blobInfo.FileLength);
            }
            else
            {
                response.StatusCode = HttpStatusCode.OK;
                response.Content.Headers.ContentLength = blobInfo.FileLength;
            }

            response.Headers.AcceptRanges.Add("bytes");

            // do not use the class version of the ContentDisposition as it incorrectly implements UTF8 filenames
            response.Content.Headers.Add(
                "Content-Disposition",
                "inline; filename=\"" + blobInfo.FileName + "\"; filename*=UTF-8''" + Uri.EscapeDataString(blobInfo.FileName));

            string mimeType = MimeTypeHelper.GetFileMimeTypeByExtenstion(Path.GetExtension(blobInfo.FileName));
            response.Content.Headers.ContentType = new MediaTypeHeaderValue(string.IsNullOrEmpty(mimeType) ? MimeTypeHelper.MIME_APPLICATION_OCTET_STREAM : mimeType);
        }

        private async Task<BlobInfo> GetBlobInfo(Guid fileKey, bool isUploader, CancellationToken cancellationToken)
        {
            BlobInfo blobInfo;
            using (SqlConnection connection = new SqlConnection(BlobsController.MainDbConnectionString))
            {
                await connection.OpenAsync(cancellationToken);

                if (AssertExistsBlobReference && !isUploader)
                {
                    SqlCommand assertCmd = connection.CreateCommand();
                    assertCmd.CommandType = CommandType.StoredProcedure;
                    assertCmd.CommandText = @"spExistsBlobReference";
                    assertCmd.Parameters.AddWithValue("@blobKey", fileKey);

                    using (var assertReader = assertCmd.ExecuteReader())
                    {
                        if (!assertReader.HasRows)
                        {
                            return null;
                        }
                    }
                }

                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @"
                        SELECT bcl.BlobContentId, bcl.PartitionId, bcl.ContentDbCSName, bcl.Size, b.FileName
                        FROM Blobs b
                            INNER JOIN BlobContentLocations bcl ON b.BlobContentLocationId = bcl.BlobContentLocationId
                        WHERE b.[Key] = @blobKey AND b.[IsDeleted] = 0 AND bcl.[IsDeleted] = 0";
                cmd.Parameters.AddWithValue("@blobKey", fileKey);

                using (SqlDataReader reader = await cmd.ExecuteReaderAsync(cancellationToken))
                {
                    if (!reader.HasRows)
                    {
                        return null;
                    }

                    await reader.ReadAsync(cancellationToken);

                    blobInfo = new BlobInfo
                    {
                        BlobContentId = reader.GetInt64(reader.GetOrdinal("BlobContentId")),
                        PartitionId = reader.GetInt32(reader.GetOrdinal("PartitionId")),
                        ContentDbCSName = reader.GetString(reader.GetOrdinal("ContentDbCSName")),
                        FileName = reader.GetString(reader.GetOrdinal("FileName")),
                        FileLength = reader.GetInt64(reader.GetOrdinal("Size")),
                    };
                }
            }

            if (!(await this.BlobContentExists(blobInfo, cancellationToken)))
            {
                return null;
            }

            return blobInfo;
        }

        private async Task<bool> BlobContentExists(BlobInfo blobInfo, CancellationToken cancellationToken)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings[blobInfo.ContentDbCSName].ConnectionString.ExpandEnv()))
            {
                await connection.OpenAsync(cancellationToken);

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT 1 FROM BlobContents WHERE BlobContentId = @blobContentId AND PartitionId = @partitionId AND IsDeleted = 0";
                    command.Parameters.AddWithValue("@blobContentId", blobInfo.BlobContentId);
                    command.Parameters.AddWithValue("@partitionId", blobInfo.PartitionId);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync(cancellationToken))
                    {
                        return reader.HasRows;
                    }
                }
            }
        }

        private class RangeInfo
        {
            public long From { get; set; }

            public long To { get; set; }

            public long Length { get; set; }
        }

        private class BlobInfo
        {
            public long BlobContentId { get; set; }

            public int PartitionId { get; set; }

            public string ContentDbCSName { get; set; }

            public string FileName { get; set; }

            public long FileLength { get; set; }
        }

        #endregion //Private
    }
}
