using System.Web.Http;
using Eumis.ApplicationServices.Communicators;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.OperationalMap.ProgrammePriorities.Controllers
{
    [RoutePrefix("api/programmePriorities/{id:int}/files")]
    public class ProgrammePriorityBlobsController : BlobsController
    {
        private IAuthorizer authorizer;

        public ProgrammePriorityBlobsController(IAuthorizer authorizer, IBlobServerCommunicator blobServerCommunicator)
            : base(blobServerCommunicator)
        {
            this.authorizer = authorizer;
        }

        public override void AssertPermissions(int id)
        {
            this.authorizer.AssertCanDo(ProgrammeActions.View, id);
        }
    }
}
