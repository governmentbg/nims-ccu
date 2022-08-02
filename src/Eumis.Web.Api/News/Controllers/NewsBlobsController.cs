using System.Web.Http;
using Eumis.ApplicationServices.Communicators;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.News.Controllers
{
    [RoutePrefix("api/news/{id:int}/files")]
    public class NewsBlobsController : BlobsController
    {
        private IAuthorizer authorizer;

        public NewsBlobsController(IAuthorizer authorizer, IBlobServerCommunicator blobServerCommunicator)
            : base(blobServerCommunicator)
        {
            this.authorizer = authorizer;
        }

        public override void AssertPermissions(int id)
        {
            this.authorizer.AssertCanDo(NewsActions.View, id);
        }
    }
}
