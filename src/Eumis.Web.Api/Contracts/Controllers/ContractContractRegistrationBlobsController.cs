using System.Web.Http;
using Eumis.ApplicationServices.Communicators;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.Contracts.Controllers
{
    [RoutePrefix("api/contracts/{id:int}/registrationFiles")]
    public class ContractContractRegistrationBlobsController : BlobsController
    {
        private IAuthorizer authorizer;

        public ContractContractRegistrationBlobsController(IAuthorizer authorizer, IBlobServerCommunicator blobServerCommunicator)
            : base(blobServerCommunicator)
        {
            this.authorizer = authorizer;
        }

        public override void AssertPermissions(int id)
        {
            this.authorizer.AssertCanDo(ContractRegistrationActions.View, id);
        }
    }
}
