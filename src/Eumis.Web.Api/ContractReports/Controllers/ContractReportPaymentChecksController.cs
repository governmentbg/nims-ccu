using Eumis.ApplicationServices.Services.ContractReport;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.ContractReports.Repositories;
using Eumis.Data.Contracts.Repositories;
using Eumis.Data.Core.Relations;
using Eumis.Data.Users.Repositories;
using Eumis.Domain.Contracts.DataObjects;
using Eumis.Domain.Contracts.ViewObjects;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Eumis.Web.Api.ContractReports.Controllers
{
    [RoutePrefix("api/contractReports/{contractReportId:int}/paymentChecks")]
    public class ContractReportPaymentChecksController : ApiController
    {
        private IRelationsRepository relationsRepository;
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IAccessContext accessContext;
        private IContractReportPaymentChecksRepository contractReportPaymentChecksRepository;
        private IContractReportPaymentsRepository contractReportPaymentsRepository;
        private IContractReportsRepository contractReportsRepository;
        private IUsersRepository usersRepository;
        private IContractReportService contractReportService;

        public ContractReportPaymentChecksController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IAccessContext accessContext,
            IContractReportPaymentChecksRepository contractReportPaymentChecksRepository,
            IContractReportPaymentsRepository contractReportPaymentsRepository,
            IContractReportsRepository contractReportsRepository,
            IUsersRepository usersRepository,
            IContractReportService contractReportService,
            IRelationsRepository relationsRepository)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.accessContext = accessContext;
            this.contractReportPaymentChecksRepository = contractReportPaymentChecksRepository;
            this.contractReportPaymentsRepository = contractReportPaymentsRepository;
            this.contractReportsRepository = contractReportsRepository;
            this.usersRepository = usersRepository;
            this.contractReportService = contractReportService;
            this.relationsRepository = relationsRepository;
        }

        [Route("")]
        public IList<ContractReportPaymentCheckVO> GetContractReportPaymentChecks(int contractReportId)
        {
            this.authorizer.AssertCanDo(ContractReportCheckActions.View, contractReportId);

            return this.contractReportPaymentChecksRepository.GetContractReportPaymentChecks(contractReportId);
        }

        [Route("{contractReportPaymentCheckId:int}")]
        public ContractReportPaymentCheckDO GetContractReportPaymentCheck(int contractReportId, int contractReportPaymentCheckId)
        {
            this.authorizer.AssertCanDo(ContractReportCheckActions.View, contractReportId);

            var paymentCheck = this.contractReportPaymentChecksRepository.Find(contractReportPaymentCheckId);

            var payment = this.contractReportPaymentsRepository.Find(paymentCheck.ContractReportPaymentId);

            string checkedByUser = string.Empty;

            if (paymentCheck.CheckedByUserId.HasValue)
            {
                var user = this.usersRepository.Find(paymentCheck.CheckedByUserId.Value);
                checkedByUser = string.Format("{0} ({1})", user.Fullname, user.Username);
            }

            return new ContractReportPaymentCheckDO(paymentCheck, payment, checkedByUser);
        }

        [Route("~/api/certReports/{certReportId:int}/payments/{contractReportId:int}/check")]
        public ContractReportPaymentCheckDO GetCertReportPaymentContractReportPaymentCheck(int certReportId, int contractReportId)
        {
            this.authorizer.AssertCanDoAny(
                Tuple.Create<Enum, int?>(CertReportActions.View, certReportId),
                Tuple.Create<Enum, int?>(CertReportCheckActions.View, certReportId));

            this.relationsRepository.CertReportHasFinancialCSDBudgetItem(certReportId, contractReportId);

            var paymentCheck = this.contractReportPaymentChecksRepository.GetActualContractReportPaymentCheck(contractReportId);

            var payment = this.contractReportPaymentsRepository.Find(paymentCheck.ContractReportPaymentId);

            string checkedByUser = string.Empty;

            if (paymentCheck.CheckedByUserId.HasValue)
            {
                var user = this.usersRepository.Find(paymentCheck.CheckedByUserId.Value);
                checkedByUser = string.Format("{0} ({1})", user.Fullname, user.Username);
            }

            return new ContractReportPaymentCheckDO(paymentCheck, payment, checkedByUser);
        }

        [Route("~/api/certReports/{certReportId:int}/advancePayments/{contractReportId:int}/check")]
        public ContractReportPaymentCheckDO GetCertReportAdvancePaymentContractReportPaymentCheck(int certReportId, int contractReportId)
        {
            this.authorizer.AssertCanDoAny(
                Tuple.Create<Enum, int?>(CertReportActions.View, certReportId),
                Tuple.Create<Enum, int?>(CertReportCheckActions.View, certReportId));

            this.relationsRepository.CertReportHasAdvancePaymentAmount(certReportId, contractReportId);

            var paymentCheck = this.contractReportPaymentChecksRepository.GetActualContractReportPaymentCheck(contractReportId);

            var payment = this.contractReportPaymentsRepository.Find(paymentCheck.ContractReportPaymentId);

            string checkedByUser = string.Empty;

            if (paymentCheck.CheckedByUserId.HasValue)
            {
                var user = this.usersRepository.Find(paymentCheck.CheckedByUserId.Value);
                checkedByUser = string.Format("{0} ({1})", user.Fullname, user.Username);
            }

            return new ContractReportPaymentCheckDO(paymentCheck, payment, checkedByUser);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReports.Edit.ContractReportPaymentCheck.Create), IdParam = "contractReportId")]
        public object CreateContractReportPaymentCheck(int contractReportId)
        {
            this.authorizer.AssertCanDo(ContractReportCheckActions.EditFinancial, contractReportId);

            var newContractReportPaymentCheck = this.contractReportService.CreateContractReportPaymentCheck(contractReportId);

            return new { ContractReportPaymentCheckId = newContractReportPaymentCheck.ContractReportPaymentCheckId };
        }

        [HttpPut]
        [Route("{contractReportPaymentCheckId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReports.Edit.ContractReportPaymentCheck.Update), IdParam = "contractReportId", ChildIdParam = "contractReportPaymentCheckId")]
        public void UpdateContractReportPaymentCheck(int contractReportId, int contractReportPaymentCheckId, ContractReportPaymentCheckDO contractReportPaymentCheck)
        {
            this.authorizer.AssertCanDo(ContractReportCheckActions.EditFinancial, contractReportId);

            this.contractReportService.UpdateContractReportPaymentCheck(
                contractReportPaymentCheckId,
                contractReportPaymentCheck.Version,
                contractReportPaymentCheck.Approval,
                contractReportPaymentCheck.File != null ? contractReportPaymentCheck.File.Key : (Guid?)null,
                contractReportPaymentCheck.CheckedDate,
                contractReportPaymentCheck.ContractReportPaymentCheckAmounts.Select(t => new Eumis.Domain.Contracts.DataObjects.ContractReportPaymentCheckAmountDO()
                {
                    ContractReportPaymentCheckAmountId = t.ContractReportPaymentCheckAmountId,
                    ContractReportPaymentCheckId = t.ContractReportPaymentCheckId,
                    PaidEuAmount = t.PaidEuAmount,
                    PaidBgAmount = t.PaidBgAmount,
                    PaidBfpTotalAmount = t.PaidBfpTotalAmount,
                    PaidCrossAmount = t.PaidCrossAmount,
                }).ToList());
        }

        [HttpDelete]
        [Route("{contractReportPaymentCheckId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReports.Edit.ContractReportPaymentCheck.Delete), IdParam = "contractReportId", ChildIdParam = "contractReportPaymentCheckId")]
        public void DeleteContractReportPaymentCheck(int contractReportId, int contractReportPaymentCheckId, string version)
        {
            this.authorizer.AssertCanDo(ContractReportCheckActions.EditFinancial, contractReportId);
            this.relationsRepository.AssertContractReportHasContractReportPaymentCheck(contractReportId, contractReportPaymentCheckId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.contractReportService.DeleteContractReportPaymentCheck(contractReportId, contractReportPaymentCheckId, vers);
        }

        [HttpPost]
        [Route("{contractReportPaymentCheckId:int}/changeStatusToActive")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReports.Edit.ContractReportPaymentCheck.ChangeStatusToActive), IdParam = "contractReportId", ChildIdParam = "contractReportPaymentCheckId")]
        public void ChangeContractReportPaymentCheckStatusToActive(int contractReportId, int contractReportPaymentCheckId, string version)
        {
            this.authorizer.AssertCanDo(ContractReportCheckActions.EditFinancial, contractReportId);
            this.relationsRepository.AssertContractReportHasContractReportPaymentCheck(contractReportId, contractReportPaymentCheckId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.contractReportService.ChangeContractReportPaymentCheckStatus(contractReportId, contractReportPaymentCheckId, vers, Domain.Contracts.ContractReportPaymentCheckStatus.Active);
        }

        [HttpPost]
        [Route("canCreate")]
        public ErrorsDO CanCreateContractReportPaymentCheck(int contractReportId)
        {
            this.authorizer.AssertCanDo(ContractReportCheckActions.EditFinancial, contractReportId);

            var errors = this.contractReportService.CanCreateContractReportPaymentCheck(contractReportId);

            return new ErrorsDO(errors);
        }

        [HttpPost]
        [Route("{contractReportPaymentCheckId:int}/canChangeStatusToActive")]
        public ErrorsDO CanChangeContractReportPaymentCheckStatusToActive(int contractReportId, int contractReportPaymentCheckId)
        {
            this.authorizer.AssertCanDo(ContractReportCheckActions.EditFinancial, contractReportId);
            this.relationsRepository.AssertContractReportHasContractReportPaymentCheck(contractReportId, contractReportPaymentCheckId);

            var errors = this.contractReportService.CanChangeContractReportPaymentCheckStatusToActive(contractReportPaymentCheckId);

            return new ErrorsDO(errors);
        }

        [HttpPost]
        [Route("{contractReportPaymentCheckId:int}/changeStatusToArchived")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReports.Edit.ContractReportPaymentCheck.ChangeStatusToArchived), IdParam = "contractReportId", ChildIdParam = "contractReportPaymentCheckId")]
        public void ChangeContractReportPaymentCheckStatusToArchived(int contractReportId, int contractReportPaymentCheckId, string version)
        {
            this.authorizer.AssertCanDo(ContractReportCheckActions.EditFinancial, contractReportId);
            this.relationsRepository.AssertContractReportHasContractReportPaymentCheck(contractReportId, contractReportPaymentCheckId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.contractReportService.ChangeContractReportPaymentCheckStatus(contractReportId, contractReportPaymentCheckId, vers, Domain.Contracts.ContractReportPaymentCheckStatus.Archived);
        }

        [HttpPost]
        [Route("{contractReportPaymentCheckId:int}/canChangeStatusToArchived")]
        public ErrorsDO CanChangeContractReportPaymentCheckStatusToArchived(int contractReportId, int contractReportPaymentCheckId)
        {
            this.authorizer.AssertCanDo(ContractReportCheckActions.EditFinancial, contractReportId);
            this.relationsRepository.AssertContractReportHasContractReportPaymentCheck(contractReportId, contractReportPaymentCheckId);

            var errors = this.contractReportService.CanChangeContractReportPaymentCheckStatusToArchived(contractReportPaymentCheckId);

            return new ErrorsDO(errors);
        }
    }
}
