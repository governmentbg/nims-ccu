using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Db;
using Eumis.Data.ContractReports.Repositories;
using Eumis.Data.ContractReports.ViewObjects.ContractBudgetTree;
using System.Web.Http;

namespace Eumis.Web.Api.ContractReports.Controllers
{
    [RoutePrefix("api/contractReports/{contractReportId:int}/paymentRequests")]
    public class ContractReportPaymentRequestsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IContractReportsRepository contractReportsRepository;

        public ContractReportPaymentRequestsController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IContractReportsRepository contractReportsRepository)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.contractReportsRepository = contractReportsRepository;
        }

        [Route("tree")]
        public ContractBudgetTreeVO GetContractReportPaymentRequests(int contractReportId)
        {
            this.authorizer.AssertCanDo(ContractReportCheckActions.View, contractReportId);

            return this.contractReportsRepository.GetContractReportPaymentRequests(contractReportId);
        }
    }
}
