using System.Web.Http;
using Eumis.ApplicationServices.Communicators;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.ContractReportFinancialCertCorrections.Controllers
{
    [RoutePrefix("api/contractReportFinancialCertCorrections/{id:int}/files")]
    public class ContractReportFinancialCertCorrectionBlobsController : BlobsController
    {
        private IAuthorizer authorizer;

        public ContractReportFinancialCertCorrectionBlobsController(IAuthorizer authorizer, IBlobServerCommunicator blobServerCommunicator)
            : base(blobServerCommunicator)
        {
            this.authorizer = authorizer;
        }

        public override void AssertPermissions(int id)
        {
            this.authorizer.AssertCanDo(ContractReportFinancialCertCorrectionActions.View, id);
        }
    }
}
