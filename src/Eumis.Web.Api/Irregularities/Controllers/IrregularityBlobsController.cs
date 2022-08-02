using System.Web.Http;
using Eumis.ApplicationServices.Communicators;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.Irregularities.Controllers
{
    [RoutePrefix("api/irregularities/{id:int}/files")]
    public class IrregularityBlobsController : BlobsController
    {
        private IAuthorizer authorizer;

        public IrregularityBlobsController(IAuthorizer authorizer, IBlobServerCommunicator blobServerCommunicator)
            : base(blobServerCommunicator)
        {
            this.authorizer = authorizer;
        }

        public override void AssertPermissions(int id)
        {
            this.authorizer.AssertCanDo(IrregularityActions.View, id);
        }
    }
}
