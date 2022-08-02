using System.Web.Http;
using Eumis.ApplicationServices.Communicators;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.ContractReportFinancialCorrections.Controllers
{
    [RoutePrefix("api/contractReportFinancialCorrections/{id:int}/files")]
    public class ContractReportFinancialCorrectionBlobsController : BlobsController
    {
        private IAuthorizer authorizer;

        public ContractReportFinancialCorrectionBlobsController(IAuthorizer authorizer, IBlobServerCommunicator blobServerCommunicator)
            : base(blobServerCommunicator)
        {
            this.authorizer = authorizer;
        }

        public override void AssertPermissions(int id)
        {
            this.authorizer.AssertCanDo(ContractReportFinancialCorrectionActions.View, id);
        }
    }
}
