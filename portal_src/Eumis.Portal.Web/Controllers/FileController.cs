using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Eumis.Components.Web;
using System.IO;
using Eumis.Documents.Mappers;
using Eumis.Portal.Model.Repositories;
using Eumis.Common.Linq;
using Eumis.Portal.Model.Entities;
using Eumis.Documents.Enums;
using System.Configuration;
using System.Net.Http.Headers;
using Eumis.Portal.Web.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using R_10019;
using Eumis.Components.Communicators;
using System.Threading.Tasks;
using Eumis.Documents.Interfaces;
using System.Security.Claims;
using AppContext = Eumis.Portal.Web.Helpers.AppContext;

namespace Eumis.Portal.Web.Controllers
{
    //[Authorize]
    //[RoutePrefix("api/file")]
    public class FileController : ApiController
    {
        [HttpGet]
        [AllowAnonymous]
        public HttpResponseMessage Download(Guid id, string uploaderAccessToken = null)
        {
            string accessToken = null;

            var stringId = id.ToString();
            if (AppContext.Current != null &&
                AppContext.Current.Document != null &&
                (AppContext.Current.Document is IEumisDocumentWithFiles) &&
                ((IEumisDocumentWithFiles)AppContext.Current.Document).Files.Where(f => f.AttachedDocumentContent.BlobContentId == stringId).Any())
            {
                accessToken = BlobApi.GetAccessToken(id);
            }
            else if (!string.IsNullOrEmpty(uploaderAccessToken))
            {
                accessToken = uploaderAccessToken;
            }
            else
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }

            var redirectResponse = Request.CreateResponse(HttpStatusCode.Redirect);
            redirectResponse.Headers.Location = BlobApi.CreateRedirectUri(id, accessToken);
            redirectResponse.Headers.CacheControl = new CacheControlHeaderValue()
            {
                NoCache = true,
                NoStore = true,
                MustRevalidate = true
            };

            return redirectResponse;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<HttpResponseMessage> SignatureUpload()
        {
            if (!Request.Content.IsMimeMultipartContent())
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);

            var provider = new MultipartMemoryStreamProvider();

            await Request.Content.ReadAsMultipartAsync(provider);

            var file = provider.Contents.Last();
            var filename = file.Headers.ContentDisposition.FileName.Trim('\"');
            var signature = await file.ReadAsByteArrayAsync();

            if (signature.Length < 256000) // Restrict to 250 Kilobytes
                if (SubmissionState.Current != null)
                {
                    var certificate = X509SignatureHelper.IsDetachedSignatureValid(signature);

                    if (certificate != null)
                    {
                        var fileKey = Guid.NewGuid();
                        SubmissionState.Current.signatureFiles.Add(fileKey.ToString(), new KeyValuePair<string, byte[]>(filename, signature));

                        return Request.CreateResponse(HttpStatusCode.OK,
                            new Eumis.Portal.Web.Models.Submit.SignatureVM
                            {
                                fileKey = fileKey.ToString(),
                                fileName = filename,
                                serialNumber = certificate.SerialNumber,
                                effectiveDate = certificate.GetEffectiveDateString(),
                                expirationDate = certificate.GetExpirationDateString(),
                                issuer = certificate.Issuer,
                                subject = certificate.Subject
                            });
                    }
                }

            throw new HttpResponseException(HttpStatusCode.BadRequest);
        }
    }
}
