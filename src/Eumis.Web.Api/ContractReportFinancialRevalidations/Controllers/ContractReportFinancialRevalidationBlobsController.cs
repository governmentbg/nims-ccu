using System.Web.Http;
using Eumis.ApplicationServices.Communicators;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.ContractReportFinancialRevalidations.Controllers
{
    [RoutePrefix("api/contractReportFinancialRevalidations/{id:int}/files")]
    public class ContractReportFinancialRevalidationBlobsController : BlobsController
    {
        private IAuthorizer authorizer;

        public ContractReportFinancialRevalidationBlobsController(IAuthorizer authorizer, IBlobServerCommunicator blobServerCommunicator)
            : base(blobServerCommunicator)
        {
            this.authorizer = authorizer;
        }

        public override void AssertPermissions(int id)
        {
            this.authorizer.AssertCanDo(ContractReportFinancialRevalidationActions.View, id);
        }
    }
}
