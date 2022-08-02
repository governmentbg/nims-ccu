using Eumis.ApplicationServices.Communicators;
using Eumis.Common.Api;
using Eumis.Common.Config;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Eumis.Web.Api.Core
{
    public abstract class BlobsController : ApiController
    {
        private IBlobServerCommunicator blobServerCommunicator;

        public BlobsController(IBlobServerCommunicator blobServerCommunicator)
        {
            this.blobServerCommunicator = blobServerCommunicator;
        }

        public abstract void AssertPermissions(int id);

        [HttpGet]
        [Route("{fileKey:guid}")]
        public virtual HttpResponseMessage Download(int id, Guid fileKey)
        {
            this.AssertPermissions(id);

            var redirectUri = this.blobServerCommunicator.GetBlobUriWithAccessToken(fileKey, false);

            var redirectResponse = this.Request.CreateResponse(HttpStatusCode.Redirect);
            redirectResponse.Headers.Location = redirectUri;
            redirectResponse.AddNoCacheHeaders();

            return redirectResponse;
        }
    }
}
