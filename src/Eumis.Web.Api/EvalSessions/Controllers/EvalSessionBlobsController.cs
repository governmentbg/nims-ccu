using System.Web.Http;
using Eumis.ApplicationServices.Communicators;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.EvalSessions.Controllers
{
    [RoutePrefix("api/evalSessions/{id:int}/files")]
    public class EvalSessionBlobsController : BlobsController
    {
        private IAuthorizer authorizer;

        public EvalSessionBlobsController(IAuthorizer authorizer, IBlobServerCommunicator blobServerCommunicator)
            : base(blobServerCommunicator)
        {
            this.authorizer = authorizer;
        }

        public override void AssertPermissions(int id)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.ViewSessionData, id);
        }
    }
}
