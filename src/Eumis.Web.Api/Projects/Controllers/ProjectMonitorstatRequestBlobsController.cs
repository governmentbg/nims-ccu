using System.Web.Http;
using Eumis.ApplicationServices.Communicators;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.Projects.Controllers
{
    [RoutePrefix("api/projectMonitorstatRequest/{id:int}/files")]
    public class ProjectMonitorstatRequestBlobsController : BlobsController
    {
        private IAuthorizer authorizer;

        public ProjectMonitorstatRequestBlobsController(IAuthorizer authorizer, IBlobServerCommunicator blobServerCommunicator)
            : base(blobServerCommunicator)
        {
            this.authorizer = authorizer;
        }

        public override void AssertPermissions(int id)
        {
            this.authorizer.AssertCanDo(ProjectActions.View, id);
        }
    }
}
