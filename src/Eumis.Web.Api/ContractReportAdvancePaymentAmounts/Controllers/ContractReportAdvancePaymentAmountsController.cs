using Eumis.ApplicationServices.Services.CertReportCheck;
using Eumis.ApplicationServices.Services.ContractReportAdvancePaymentAmount;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.ContractReportAdvancePaymentAmounts.Repositories;
using Eumis.Data.ContractReports.Repositories;
using Eumis.Data.Core.Relations;
using Eumis.Data.Users.Repositories;
using Eumis.Domain.Contracts.DataObjects;
using Eumis.Domain.Contracts.ViewObjects;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.Core;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.ContractReportAdvancePaymentAmounts.Controllers
{
    [RoutePrefix("api/contractReports/{contractReportId:int}/advancePaymentAmounts")]
    public class ContractReportAdvancePaymentAmountsController : ApiController
    {
        private IRelationsRepository relationsRepository;
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IAccessContext accessContext;
        private IUsersRepository usersRepository;
        private IContractReportPaymentsRepository contractReportPaymentsRepository;
        private IContractReportAdvancePaymentAmountsRepository contractReportAdvancePaymentAmountsRepository;
        private IContractReportAdvancePaymentAmountService contractReportAdvancePaymentAmountService;
        private ICertReportCheckService certReportCheckService;

        public ContractReportAdvancePaymentAmountsController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IAccessContext accessContext,
            IUsersRepository usersRepository,
            IContractReportPaymentsRepository contractReportPaymentsRepository,
            IContractReportAdvancePaymentAmountsRepository contractReportAdvancePaymentAmountsRepository,
            IContractReportAdvancePaymentAmountService contractReportAdvancePaymentAmountService,
            ICertReportCheckService certReportCheckService,
            IRelationsRepository relationsRepository)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.accessContext = accessContext;
            this.usersRepository = usersRepository;
            this.contractReportPaymentsRepository = contractReportPaymentsRepository;
            this.contractReportAdvancePaymentAmountsRepository = contractReportAdvancePaymentAmountsRepository;
            this.contractReportAdvancePaymentAmountService = contractReportAdvancePaymentAmountService;
            this.certReportCheckService = certReportCheckService;
            this.relationsRepository = relationsRepository;
        }

        [Route("")]
        public IList<ContractReportAdvancePaymentAmountsVO> GetContractReportAdvancePaymentAmounts(int contractReportId)
        {
            this.authorizer.AssertCanDo(ContractReportCheckActions.View, contractReportId);

            return this.contractReportAdvancePaymentAmountsRepository.GetContractReportAdvancePaymentAmounts(contractReportId);
        }

        [Route("~/api/certReports/{certReportId:int}/advancedPayments/{contractReportId:int}/attachedAmounts")]
        public IList<ContractReportAdvancePaymentAmountsVO> GetCertReportContractReportAttachedAdvancePaymentAmounts(int certReportId, int contractReportId)
        {
            this.authorizer.AssertCanDoAny(
                Tuple.Create<Enum, int?>(CertReportActions.View, certReportId),
                Tuple.Create<Enum, int?>(CertReportCheckActions.View, certReportId));

            return this.contractReportAdvancePaymentAmountsRepository.GetContractReportAdvancePaymentAmounts(contractReportId, isAttachedToCertReport: true, certReportId: certReportId);
        }

        [Route("~/api/certReports/{certReportId:int}/advancedPayments/{contractReportId:int}/unattachedAmounts")]
        public IList<ContractReportAdvancePaymentAmountsVO> GetCertReportContractReportUnattachedAdvancePaymentAmounts(int certReportId, int contractReportId)
        {
            this.authorizer.AssertCanDo(CertReportActions.Edit, certReportId);
            this.relationsRepository.AssertCertReportHasAdvancePaymentAmount(certReportId, contractReportId);

            return this.contractReportAdvancePaymentAmountsRepository.GetContractReportAdvancePaymentAmounts(contractReportId, isAttachedToCertReport: false);
        }

        [Route("{contractReportAdvancePaymentAmountId:int}")]
        public ContractReportAdvancePaymentAmountDO GetContractReportAdvancePaymentAmount(int contractReportId, int contractReportAdvancePaymentAmountId)
        {
            this.authorizer.AssertCanDo(ContractReportCheckActions.View, contractReportId);

            var contractReportAdvancePaymentAmount = this.contractReportAdvancePaymentAmountsRepository.Find(contractReportAdvancePaymentAmountId);

            var payment = this.contractReportPaymentsRepository.Find(contractReportAdvancePaymentAmount.ContractReportPaymentId);

            string checkedByUser = string.Empty;

            if (contractReportAdvancePaymentAmount.CheckedByUserId.HasValue)
            {
                var user = this.usersRepository.Find(contractReportAdvancePaymentAmount.CheckedByUserId.Value);
                checkedByUser = string.Format("{0} ({1})", user.Fullname, user.Username);
            }

            return new ContractReportAdvancePaymentAmountDO(contractReportAdvancePaymentAmount, payment, checkedByUser);
        }

        [Route("~/api/certReports/{certReportId:int}/advancedPayments/{contractReportId:int}/amounts/{contractReportAdvancePaymentAmountId:int}")]
        public ContractReportAdvancePaymentAmountDO GetCertReportContractReportAdvancePaymentAmount(int certReportId, int contractReportId, int contractReportAdvancePaymentAmountId)
        {
            this.authorizer.AssertCanDoAny(
                Tuple.Create<Enum, int?>(CertReportActions.View, certReportId),
                Tuple.Create<Enum, int?>(CertReportCheckActions.View, certReportId));

            this.relationsRepository.AssertCertReportHasAdvancePaymentAmount(certReportId, contractReportId, contractReportAdvancePaymentAmountId);

            var contractReportAdvancePaymentAmount = this.contractReportAdvancePaymentAmountsRepository.Find(contractReportAdvancePaymentAmountId);

            var payment = this.contractReportPaymentsRepository.Find(contractReportAdvancePaymentAmount.ContractReportPaymentId);

            string checkedByUser = string.Empty;
            string certCheckedByUser = string.Empty;

            if (contractReportAdvancePaymentAmount.CheckedByUserId.HasValue)
            {
                var user = this.usersRepository.Find(contractReportAdvancePaymentAmount.CheckedByUserId.Value);
                checkedByUser = string.Format("{0} ({1})", user.Fullname, user.Username);
            }

            if (contractReportAdvancePaymentAmount.CertCheckedByUserId.HasValue)
            {
                var user = this.usersRepository.Find(contractReportAdvancePaymentAmount.CertCheckedByUserId.Value);
                certCheckedByUser = string.Format("{0} ({1})", user.Fullname, user.Username);
            }

            return new ContractReportAdvancePaymentAmountDO(contractReportAdvancePaymentAmount, payment, checkedByUser, certCheckedByUser);
        }

        [HttpPut]
        [Route("{contractReportAdvancePaymentAmountId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReports.Edit.ContractReportAdvancePaymentAmount.Update), IdParam = "contractReportAdvancePaymentAmountId")]
        public void UpdateContractReportAdvancePaymentAmount(int contractReportId, int contractReportAdvancePaymentAmountId, ContractReportAdvancePaymentAmountDO contractReportAdvancePaymentAmount)
        {
            this.authorizer.AssertCanDo(ContractReportCheckActions.EditFinancial, contractReportId);

            this.contractReportAdvancePaymentAmountService.UpdateContractReportAdvancePaymentAmount(
                contractReportAdvancePaymentAmountId,
                contractReportAdvancePaymentAmount.Version,
                contractReportAdvancePaymentAmount.Approval,
                contractReportAdvancePaymentAmount.Notes,
                contractReportAdvancePaymentAmount.ApprovedEuAmount,
                contractReportAdvancePaymentAmount.ApprovedBgAmount,
                contractReportAdvancePaymentAmount.ApprovedBfpTotalAmount);
        }

        [HttpPut]
        [Route("~/api/certReports/{certReportId:int}/advancedPayments/{contractReportId:int}/amounts/{contractReportAdvancePaymentAmountId:int}/certUpdate")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReports.Edit.ContractReportAdvancePaymentAmount.CertUpdate), IdParam = "contractReportAdvancePaymentAmountId")]
        public void CertUpdateContractReportAdvancePaymentAmount(int certReportId, int contractReportId, int contractReportAdvancePaymentAmountId, ContractReportAdvancePaymentAmountDO contractReportAdvancePaymentAmount)
        {
            this.authorizer.AssertCanDo(CertReportCheckActions.Edit, certReportId);
            this.relationsRepository.AssertCertReportHasAdvancePaymentAmount(certReportId, contractReportId, contractReportAdvancePaymentAmountId);

            this.certReportCheckService.UpdateContractReportAdvancePaymentAmount(
                certReportId,
                contractReportAdvancePaymentAmountId,
                contractReportAdvancePaymentAmount.Version,
                contractReportAdvancePaymentAmount.UncertifiedApprovedEuAmount,
                contractReportAdvancePaymentAmount.UncertifiedApprovedBgAmount,
                contractReportAdvancePaymentAmount.UncertifiedApprovedBfpTotalAmount,
                contractReportAdvancePaymentAmount.CertifiedApprovedEuAmount,
                contractReportAdvancePaymentAmount.CertifiedApprovedBgAmount,
                contractReportAdvancePaymentAmount.CertifiedApprovedBfpTotalAmount);
        }

        [HttpPost]
        [Route("{contractReportAdvancePaymentAmountId:int}/changeStatusToEnded")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReports.Edit.ContractReportAdvancePaymentAmount.ChangeStatusToEnded), IdParam = "contractReportAdvancePaymentAmountId")]
        public void ChangeContractReportAdvancePaymentAmountStatusToEnded(int contractReportId, int contractReportAdvancePaymentAmountId, string version)
        {
            this.authorizer.AssertCanDo(ContractReportCheckActions.EditFinancial, contractReportId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.contractReportAdvancePaymentAmountService.ChangeContractReportAdvancePaymentAmountStatus(contractReportAdvancePaymentAmountId, vers, Domain.Contracts.ContractReportAdvancePaymentAmountStatus.Ended);
        }

        [HttpPost]
        [Route("{contractReportAdvancePaymentAmountId:int}/canChangeStatusToEnded")]
        public ErrorsDO CanChangeContractReportAdvancePaymentAmountStatusToEnded(int contractReportId, int contractReportAdvancePaymentAmountId)
        {
            this.authorizer.AssertCanDo(ContractReportCheckActions.EditFinancial, contractReportId);

            var errors = this.contractReportAdvancePaymentAmountService.CanChangeContractReportAdvancePaymentAmountStatusToEnded(contractReportAdvancePaymentAmountId);

            return new ErrorsDO(errors);
        }

        [HttpPost]
        [Route("{contractReportAdvancePaymentAmountId:int}/changeStatusToDraft")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReports.Edit.ContractReportAdvancePaymentAmount.ChangeStatusToDraft), IdParam = "contractReportAdvancePaymentAmountId")]
        public void ChangeContractReportAdvancePaymentAmountStatusToDraft(int contractReportId, int contractReportAdvancePaymentAmountId, string version)
        {
            this.authorizer.AssertCanDo(ContractReportCheckActions.EditFinancial, contractReportId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.contractReportAdvancePaymentAmountService.ChangeContractReportAdvancePaymentAmountStatus(contractReportAdvancePaymentAmountId, vers, Domain.Contracts.ContractReportAdvancePaymentAmountStatus.Draft);
        }

        [HttpPost]
        [Route("{contractReportAdvancePaymentAmountId:int}/canChangeStatusToDraft")]
        public ErrorsDO CanChangeContractReportAdvancePaymentAmountStatusToDraft(int contractReportId, int contractReportAdvancePaymentAmountId)
        {
            this.authorizer.AssertCanDo(ContractReportCheckActions.EditFinancial, contractReportId);

            var errors = this.contractReportAdvancePaymentAmountService.CanChangeContractReportAdvancePaymentAmountStatusToDraft(contractReportAdvancePaymentAmountId);

            return new ErrorsDO(errors);
        }

        [HttpPost]
        [Route("~/api/certReports/{certReportId:int}/advancedPayments/{contractReportId:int}/amounts/{contractReportAdvancePaymentAmountId:int}/changeCertStatusToEnded")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReports.Edit.ContractReportAdvancePaymentAmount.ChangeCertStatusToEnded), IdParam = "contractReportAdvancePaymentAmountId")]
        public void ChangeContractReportAdvancePaymentAmountCertStatusToEnded(int certReportId, int contractReportId, int contractReportAdvancePaymentAmountId, string version)
        {
            this.authorizer.AssertCanDo(CertReportCheckActions.Edit, certReportId);
            this.relationsRepository.AssertCertReportHasAdvancePaymentAmount(certReportId, contractReportId, contractReportAdvancePaymentAmountId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.certReportCheckService.ChangeContractReportAdvancePaymentAmountCertStatus(contractReportAdvancePaymentAmountId, vers, Domain.Contracts.ContractReportAdvancePaymentAmountCertStatus.Ended);
        }

        [HttpPost]
        [Route("~/api/certReports/{certReportId:int}/advancedPayments/{contractReportId:int}/amounts/{contractReportAdvancePaymentAmountId:int}/changeCertStatusToDraft")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReports.Edit.ContractReportAdvancePaymentAmount.ChangeCertStatusToEnded), IdParam = "contractReportAdvancePaymentAmountId")]
        public void ChangeContractReportAdvancePaymentAmountCertStatusToDraft(int certReportId, int contractReportId, int contractReportAdvancePaymentAmountId, string version)
        {
            this.authorizer.AssertCanDo(CertReportCheckActions.Edit, certReportId);
            this.relationsRepository.AssertCertReportHasAdvancePaymentAmount(certReportId, contractReportId, contractReportAdvancePaymentAmountId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.certReportCheckService.ChangeContractReportAdvancePaymentAmountCertStatus(contractReportAdvancePaymentAmountId, vers, Domain.Contracts.ContractReportAdvancePaymentAmountCertStatus.Draft);
        }

        [HttpPost]
        [Route("~/api/certReports/{certReportId:int}/advancedPayments/{contractReportId:int}/amounts/{contractReportAdvancePaymentAmountId:int}/canChangeCertStatusToEnded")]
        public ErrorsDO CanChangeContractReportAdvancePaymentAmountCertStatusToEnded(int certReportId, int contractReportId, int contractReportAdvancePaymentAmountId)
        {
            this.authorizer.AssertCanDo(CertReportCheckActions.Edit, certReportId);
            this.relationsRepository.AssertCertReportHasAdvancePaymentAmount(certReportId, contractReportId, contractReportAdvancePaymentAmountId);

            var errors = this.certReportCheckService.CanChangeContractReportAdvancePaymentAmountCertStatusToEnded(contractReportAdvancePaymentAmountId);

            return new ErrorsDO(errors);
        }
    }
}
