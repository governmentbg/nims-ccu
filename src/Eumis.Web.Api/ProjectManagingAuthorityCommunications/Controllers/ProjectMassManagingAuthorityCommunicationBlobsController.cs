using Eumis.ApplicationServices.Communicators;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Web.Api.Core;
using System.Web.Http;

namespace Eumis.Web.Api.ProjectManagingAuthorityCommunications.Controllers
{
    [RoutePrefix("api/projectMassManagingAuthorityCommunications/{id:int}/files")]
    public class ProjectMassManagingAuthorityCommunicationBlobsController : BlobsController
    {
        private IAuthorizer authorizer;

        public ProjectMassManagingAuthorityCommunicationBlobsController(
            IAuthorizer authorizer,
            IBlobServerCommunicator blobServerCommunicator)
            : base(blobServerCommunicator)
        {
            this.authorizer = authorizer;
        }

        public override void AssertPermissions(int id)
        {
            this.authorizer.AssertCanDo(ProjectMassManagingAuthorityCommunicationActions.View, id);
        }
    }
}
