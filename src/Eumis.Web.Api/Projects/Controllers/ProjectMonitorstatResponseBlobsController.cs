using System.Web.Http;
using Eumis.ApplicationServices.Communicators;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.Projects.Controllers
{
    [RoutePrefix("api/projectMonitorstatResponse/{id:int}/files")]
    public class ProjectMonitorstatResponseBlobsController : BlobsController
    {
        private IAuthorizer authorizer;

        public ProjectMonitorstatResponseBlobsController(IAuthorizer authorizer, IBlobServerCommunicator blobServerCommunicator)
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
