using System.Web.Http;
using Eumis.ApplicationServices.Communicators;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Data.ContractReportFinancialCSDs.Repositories;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.ContractReports.Controllers
{
    [RoutePrefix("api/contractReportFinancialCSDFiles/{id:int}/files")]
    public class ContractReportFinancialCSDBlobsController : BlobsController
    {
        private IAuthorizer authorizer;
        private IContractReportFinancialCSDBudgetItemsRepository contractReportFinancialCSDBudgetItemsRepository;

        public ContractReportFinancialCSDBlobsController(
            IAuthorizer authorizer,
            IBlobServerCommunicator blobServerCommunicator,
            IContractReportFinancialCSDBudgetItemsRepository contractReportFinancialCSDBudgetItemsRepository)
            : base(blobServerCommunicator)
        {
            this.authorizer = authorizer;
            this.contractReportFinancialCSDBudgetItemsRepository = contractReportFinancialCSDBudgetItemsRepository;
        }

        public override void AssertPermissions(int id)
        {
            var contractReportId = this.contractReportFinancialCSDBudgetItemsRepository.Find(id).ContractReportId;
            this.authorizer.AssertCanDo(ContractReportCheckActions.View, contractReportId);
        }
    }
}
