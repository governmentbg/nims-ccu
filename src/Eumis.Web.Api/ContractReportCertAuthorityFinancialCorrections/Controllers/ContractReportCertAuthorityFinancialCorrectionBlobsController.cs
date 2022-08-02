using System.Web.Http;
using Eumis.ApplicationServices.Communicators;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.ContractReportFinancialCertCorrections.Controllers
{
    [RoutePrefix("api/contractReportCertAuthorityFinancialCorrections/{id:int}/files")]
    public class ContractReportCertAuthorityFinancialCorrectionBlobsController : BlobsController
    {
        private IAuthorizer authorizer;

        public ContractReportCertAuthorityFinancialCorrectionBlobsController(IAuthorizer authorizer, IBlobServerCommunicator blobServerCommunicator)
            : base(blobServerCommunicator)
        {
            this.authorizer = authorizer;
        }

        public override void AssertPermissions(int id)
        {
            this.authorizer.AssertCanDo(ContractReportCertAuthorityFinancialCorrectionActions.View, id);
        }
    }
}
