using System.Web.Http;
using Eumis.ApplicationServices.Communicators;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.ContractReportTechnicalCorrections.Controllers
{
    [RoutePrefix("api/contractReportTechnicalCorrections/{id:int}/files")]
    public class ContractReportTechnicalCorrectionBlobsController : BlobsController
    {
        private IAuthorizer authorizer;

        public ContractReportTechnicalCorrectionBlobsController(IAuthorizer authorizer, IBlobServerCommunicator blobServerCommunicator)
            : base(blobServerCommunicator)
        {
            this.authorizer = authorizer;
        }

        public override void AssertPermissions(int id)
        {
            this.authorizer.AssertCanDo(ContractReportTechnicalCorrectionActions.View, id);
        }
    }
}
