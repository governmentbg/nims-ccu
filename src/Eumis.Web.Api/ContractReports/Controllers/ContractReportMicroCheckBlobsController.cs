using System.Web.Http;
using Eumis.ApplicationServices.Communicators;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Data.ContractReports.Repositories;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.ContractReports.Controllers
{
    [RoutePrefix("api/contractReportMicroChecks/{id:int}/files")]
    public class ContractReportMicroCheckBlobsController : BlobsController
    {
        private IAuthorizer authorizer;
        private IContractReportMicroChecksRepository contractReportMicroChecksRepository;

        public ContractReportMicroCheckBlobsController(
            IAuthorizer authorizer,
            IBlobServerCommunicator blobServerCommunicator,
            IContractReportMicroChecksRepository contractReportMicroChecksRepository)
            : base(blobServerCommunicator)
        {
            this.authorizer = authorizer;
            this.contractReportMicroChecksRepository = contractReportMicroChecksRepository;
        }

        public override void AssertPermissions(int id)
        {
            var contractReportId = this.contractReportMicroChecksRepository.Find(id).ContractReportId;
            this.authorizer.AssertCanDo(ContractReportCheckActions.View, contractReportId);
        }
    }
}
