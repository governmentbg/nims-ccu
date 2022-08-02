using System.Web.Http;
using Eumis.ApplicationServices.Communicators;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.RequestPackages.Controllers
{
    [RoutePrefix("api/requestPackages/{id:int}/files")]
    public class RequestPackageBlobsController : BlobsController
    {
        private IAuthorizer authorizer;

        public RequestPackageBlobsController(IAuthorizer authorizer, IBlobServerCommunicator blobServerCommunicator)
            : base(blobServerCommunicator)
        {
            this.authorizer = authorizer;
        }

        public override void AssertPermissions(int id)
        {
            this.authorizer.AssertCanDo(RequestPackageActions.View, id);
        }
    }
}
