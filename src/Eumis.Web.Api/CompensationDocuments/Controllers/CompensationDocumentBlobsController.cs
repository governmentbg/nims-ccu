using System.Web.Http;
using Eumis.ApplicationServices.Communicators;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.CompensationDocuments.Controllers
{
    [RoutePrefix("api/compensationDocuments/{id:int}/files")]
    public class CompensationDocumentBlobsController : BlobsController
    {
        private IAuthorizer authorizer;

        public CompensationDocumentBlobsController(IAuthorizer authorizer, IBlobServerCommunicator blobServerCommunicator)
            : base(blobServerCommunicator)
        {
            this.authorizer = authorizer;
        }

        public override void AssertPermissions(int id)
        {
            this.authorizer.AssertCanDo(CompensationDocumentActions.View, id);
        }
    }
}
