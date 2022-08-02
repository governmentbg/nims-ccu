using System.Web.Http;
using Eumis.ApplicationServices.Communicators;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.Procedures.Controllers
{
    [RoutePrefix("api/procedureMassCommunications/{id:int}/files")]
    public class ProcedureMassCommunicationBlobsController : BlobsController
    {
        private IAuthorizer authorizer;

        public ProcedureMassCommunicationBlobsController(IAuthorizer authorizer, IBlobServerCommunicator blobServerCommunicator)
            : base(blobServerCommunicator)
        {
            this.authorizer = authorizer;
        }

        public override void AssertPermissions(int id)
        {
            this.authorizer.AssertCanDo(ProcedureMassCommunicationActions.View, id);
        }
    }
}
