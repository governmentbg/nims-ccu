using System.Web.Http;
using Eumis.ApplicationServices.Communicators;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.Contracts.Controllers
{
    [RoutePrefix("api/contracts/{id:int}/files")]
    public class ContractBlobsController : BlobsController
    {
        private IAuthorizer authorizer;

        public ContractBlobsController(IAuthorizer authorizer, IBlobServerCommunicator blobServerCommunicator)
            : base(blobServerCommunicator)
        {
            this.authorizer = authorizer;
        }

        public override void AssertPermissions(int id)
        {
            this.authorizer.AssertCanDo(ContractActions.View, id);
        }
    }
}
