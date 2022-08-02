using System.Web.Http;
using Eumis.ApplicationServices.Communicators;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.ContractReportRevalidationCertAuthorityFinancialCorrections.Controllers
{
    [RoutePrefix("api/contractReportRevalidationCertAuthorityFinancialCorrections/{id:int}/files")]
    public class ContractReportRevalidationCertAuthorityFinancialCorrectionBlobsController : BlobsController
    {
        private IAuthorizer authorizer;

        public ContractReportRevalidationCertAuthorityFinancialCorrectionBlobsController(IAuthorizer authorizer, IBlobServerCommunicator blobServerCommunicator)
            : base(blobServerCommunicator)
        {
            this.authorizer = authorizer;
        }

        public override void AssertPermissions(int id)
        {
            this.authorizer.AssertCanDo(ContractReportRevalidationCertAuthorityFinancialCorrectionActions.View, id);
        }
    }
}
