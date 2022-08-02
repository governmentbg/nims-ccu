using System.Web.Http;
using Eumis.ApplicationServices.Communicators;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.ContractReports.Controllers
{
    [RoutePrefix("api/projectDossier/{id:int}/files")]
    public class ProjectDossierBlobsController : BlobsController
    {
        private IAuthorizer authorizer;

        public ProjectDossierBlobsController(IAuthorizer authorizer, IBlobServerCommunicator blobServerCommunicator)
            : base(blobServerCommunicator)
        {
            this.authorizer = authorizer;
        }

        public override void AssertPermissions(int id)
        {
            this.authorizer.AssertCanDo(ProjectDossierActions.View, id);
        }
    }
}
