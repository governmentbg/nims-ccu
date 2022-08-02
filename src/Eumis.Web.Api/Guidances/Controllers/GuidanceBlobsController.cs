using System.Web.Http;
using Eumis.ApplicationServices.Communicators;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.Guidances.Controllers
{
    [RoutePrefix("api/guidances/{id:int}/files")]
    public class GuidanceBlobsController : BlobsController
    {
        private IAuthorizer authorizer;

        public GuidanceBlobsController(IAuthorizer authorizer, IBlobServerCommunicator blobServerCommunicator)
            : base(blobServerCommunicator)
        {
            this.authorizer = authorizer;
        }

        public override void AssertPermissions(int id)
        {
            this.authorizer.AssertCanDo(GuidanceActions.View, id);
        }
    }
}
