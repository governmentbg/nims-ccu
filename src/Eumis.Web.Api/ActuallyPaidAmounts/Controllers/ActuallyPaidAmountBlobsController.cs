using System.Web.Http;
using Eumis.ApplicationServices.Communicators;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.ActuallyPaidAmounts.Controllers
{
    [RoutePrefix("api/actuallyPaidAmounts/{id:int}/files")]
    public class ActuallyPaidAmountBlobsController : BlobsController
    {
        private IAuthorizer authorizer;

        public ActuallyPaidAmountBlobsController(IAuthorizer authorizer, IBlobServerCommunicator blobServerCommunicator)
            : base(blobServerCommunicator)
        {
            this.authorizer = authorizer;
        }

        public override void AssertPermissions(int id)
        {
            this.authorizer.AssertCanDo(ActuallyPaidAmountActions.View, id);
        }
    }
}
