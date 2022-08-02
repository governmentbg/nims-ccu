using System;
using System.Web.Http;
using Eumis.ApplicationServices.Communicators;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.Procurements.Controllers
{
    [RoutePrefix("api/procurements/{id:int}/files")]
    public class ProcurementBlobsController : BlobsController
    {
        private IAuthorizer authorizer;

        public ProcurementBlobsController(IAuthorizer authorizer, IBlobServerCommunicator blobServerCommunicator)
            : base(blobServerCommunicator)
        {
            this.authorizer = authorizer;
        }

        public override void AssertPermissions(int id)
        {
        }
    }
}
