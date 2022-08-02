using System.Web.Http;
using Eumis.ApplicationServices.Communicators;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.SapInterfaces.Controllers
{
    [RoutePrefix("api/sapFiles/{id:int}/blobFiles")]
    public class SapFileBlobsController : BlobsController
    {
        private IAuthorizer authorizer;

        public SapFileBlobsController(IAuthorizer authorizer, IBlobServerCommunicator blobServerCommunicator)
            : base(blobServerCommunicator)
        {
            this.authorizer = authorizer;
        }

        public override void AssertPermissions(int id)
        {
            this.authorizer.AssertCanDo(SapFileActions.View, id);
        }
    }
}
