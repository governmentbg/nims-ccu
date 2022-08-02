using Eumis.ApplicationServices.Services.ContractReportAdvanceNVPaymentAmount;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.ContractReportAdvanceNVPaymentAmounts.Repositories;
using Eumis.Data.ContractReports.Repositories;
using Eumis.Data.Users.Repositories;
using Eumis.Domain.Contracts.DataObjects;
using Eumis.Domain.Contracts.ViewObjects;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.Core;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.ContractReportAdvanceNVPaymentAmounts.Controllers
{
    [RoutePrefix("api/contractReports/{contractReportId:int}/advanceNVPaymentAmounts")]
    public class ContractReportAdvanceNVPaymentAmountsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IAccessContext accessContext;
        private IUsersRepository usersRepository;
        private IContractReportPaymentsRepository contractReportPaymentsRepository;
        private IContractReportAdvanceNVPaymentAmountsRepository contractReportAdvanceNVPaymentAmountsRepository;
        private IContractReportAdvanceNVPaymentAmountService contractReportAdvanceNVPaymentAmountService;

        public ContractReportAdvanceNVPaymentAmountsController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IAccessContext accessContext,
            IUsersRepository usersRepository,
            IContractReportPaymentsRepository contractReportPaymentsRepository,
            IContractReportAdvanceNVPaymentAmountsRepository contractReportAdvanceNVPaymentAmountsRepository,
            IContractReportAdvanceNVPaymentAmountService contractReportAdvanceNVPaymentAmountService)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.accessContext = accessContext;
            this.usersRepository = usersRepository;
            this.contractReportPaymentsRepository = contractReportPaymentsRepository;
            this.contractReportAdvanceNVPaymentAmountsRepository = contractReportAdvanceNVPaymentAmountsRepository;
            this.contractReportAdvanceNVPaymentAmountService = contractReportAdvanceNVPaymentAmountService;
        }

        [Route("")]
        public IList<ContractReportAdvanceNVPaymentAmountsVO> GetContractReportAdvanceNVPaymentAmounts(int contractReportId)
        {
            this.authorizer.AssertCanDo(ContractReportCheckActions.View, contractReportId);

            return this.contractReportAdvanceNVPaymentAmountsRepository.GetContractReportAdvanceNVPaymentAmounts(contractReportId);
        }

        [Route("{contractReportAdvanceNVPaymentAmountId:int}")]
        public ContractReportAdvanceNVPaymentAmountDO GetContractReportAdvanceNVPaymentAmount(int contractReportId, int contractReportAdvanceNVPaymentAmountId)
        {
            this.authorizer.AssertCanDo(ContractReportCheckActions.View, contractReportId);

            var contractReportAdvanceNVPaymentAmount = this.contractReportAdvanceNVPaymentAmountsRepository.Find(contractReportAdvanceNVPaymentAmountId);

            var payment = this.contractReportPaymentsRepository.Find(contractReportAdvanceNVPaymentAmount.ContractReportPaymentId);

            string checkedByUser = string.Empty;

            if (contractReportAdvanceNVPaymentAmount.CheckedByUserId.HasValue)
            {
                var user = this.usersRepository.Find(contractReportAdvanceNVPaymentAmount.CheckedByUserId.Value);
                checkedByUser = string.Format("{0} ({1})", user.Fullname, user.Username);
            }

            return new ContractReportAdvanceNVPaymentAmountDO(contractReportAdvanceNVPaymentAmount, payment, checkedByUser);
        }

        [HttpPut]
        [Route("{contractReportAdvanceNVPaymentAmountId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReports.Edit.ContractReportAdvanceNVPaymentAmount.Update), IdParam = "contractReportAdvanceNVPaymentAmountId")]
        public void UpdateContractReportAdvanceNVPaymentAmount(int contractReportId, int contractReportAdvanceNVPaymentAmountId, ContractReportAdvanceNVPaymentAmountDO contractReportAdvanceNVPaymentAmount)
        {
            this.authorizer.AssertCanDo(ContractReportCheckActions.EditFinancial, contractReportId);

            this.contractReportAdvanceNVPaymentAmountService.UpdateContractReportAdvanceNVPaymentAmount(
                contractReportAdvanceNVPaymentAmountId,
                contractReportAdvanceNVPaymentAmount.Version,
                contractReportAdvanceNVPaymentAmount.Approval,
                contractReportAdvanceNVPaymentAmount.Notes,
                contractReportAdvanceNVPaymentAmount.EuAmount,
                contractReportAdvanceNVPaymentAmount.BgAmount,
                contractReportAdvanceNVPaymentAmount.BfpTotalAmount);
        }

        [HttpPost]
        [Route("{contractReportAdvanceNVPaymentAmountId:int}/changeStatusToEnded")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReports.Edit.ContractReportAdvanceNVPaymentAmount.ChangeStatusToEnded), IdParam = "contractReportAdvanceNVPaymentAmountId")]
        public void ChangeContractReportAdvanceNVPaymentAmountStatusToEnded(int contractReportId, int contractReportAdvanceNVPaymentAmountId, string version)
        {
            this.authorizer.AssertCanDo(ContractReportCheckActions.EditFinancial, contractReportId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.contractReportAdvanceNVPaymentAmountService.ChangeContractReportAdvanceNVPaymentAmountStatus(contractReportAdvanceNVPaymentAmountId, vers, Domain.Contracts.ContractReportAdvanceNVPaymentAmountStatus.Ended);
        }

        [HttpPost]
        [Route("{contractReportAdvanceNVPaymentAmountId:int}/canChangeStatusToEnded")]
        public ErrorsDO CanChangeContractReportAdvanceNVPaymentAmountStatusToEnded(int contractReportId, int contractReportAdvanceNVPaymentAmountId)
        {
            this.authorizer.AssertCanDo(ContractReportCheckActions.EditFinancial, contractReportId);

            var errors = this.contractReportAdvanceNVPaymentAmountService.CanChangeContractReportAdvanceNVPaymentAmountStatusToEnded(contractReportAdvanceNVPaymentAmountId);

            return new ErrorsDO(errors);
        }

        [HttpPost]
        [Route("{contractReportAdvanceNVPaymentAmountId:int}/changeStatusToDraft")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReports.Edit.ContractReportAdvanceNVPaymentAmount.ChangeStatusToDraft), IdParam = "contractReportAdvanceNVPaymentAmountId")]
        public void ChangeContractReportAdvanceNVPaymentAmountStatusToDraft(int contractReportId, int contractReportAdvanceNVPaymentAmountId, string version)
        {
            this.authorizer.AssertCanDo(ContractReportCheckActions.EditFinancial, contractReportId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.contractReportAdvanceNVPaymentAmountService.ChangeContractReportAdvanceNVPaymentAmountStatus(contractReportAdvanceNVPaymentAmountId, vers, Domain.Contracts.ContractReportAdvanceNVPaymentAmountStatus.Draft);
        }

        [HttpPost]
        [Route("{contractReportAdvanceNVPaymentAmountId:int}/canChangeStatusToDraft")]
        public ErrorsDO CanChangeContractReportAdvanceNVPaymentAmountStatusToDraft(int contractReportId, int contractReportAdvanceNVPaymentAmountId)
        {
            this.authorizer.AssertCanDo(ContractReportCheckActions.EditFinancial, contractReportId);

            var errors = this.contractReportAdvanceNVPaymentAmountService.CanChangeContractReportAdvanceNVPaymentAmountStatusToDraft(contractReportAdvanceNVPaymentAmountId);

            return new ErrorsDO(errors);
        }
    }
}
