using System.Web.Http;
using Eumis.ApplicationServices.Communicators;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.ContractReportRevalidations.Controllers
{
    [RoutePrefix("api/contractReportRevalidations/{id:int}/files")]
    public class ContractReportRevalidationBlobsController : BlobsController
    {
        private IAuthorizer authorizer;

        public ContractReportRevalidationBlobsController(IAuthorizer authorizer, IBlobServerCommunicator blobServerCommunicator)
            : base(blobServerCommunicator)
        {
            this.authorizer = authorizer;
        }

        public override void AssertPermissions(int id)
        {
            this.authorizer.AssertCanDo(ContractReportRevalidationActions.View, id);
        }
    }
}
