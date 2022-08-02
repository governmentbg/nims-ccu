using Eumis.ApplicationServices.Services.ActuallyPaidAmount;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.ActuallyPaidAmounts.Repositories;
using Eumis.Data.ActuallyPaidAmounts.ViewObjects;
using Eumis.Data.ContractReports.Repositories;
using Eumis.Data.ContractReports.ViewObjects;
using Eumis.Data.Contracts.Repositories;
using Eumis.Data.Core.Permissions;
using Eumis.Data.Core.Relations;
using Eumis.Data.Counters;
using Eumis.Domain;
using Eumis.Domain.MonitoringFinancialControl;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.Users.ProgrammePermissions;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.ActuallyPaidAmounts.DataObjects;
using Eumis.Web.Api.Core;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.ActuallyPaidAmounts.Controllers
{
    [RoutePrefix("api/actuallyPaidAmounts")]
    public class ActuallyPaidAmountsController : ApiController
    {
        private IRelationsRepository relationsRepository;
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IAccessContext accessContext;
        private IPermissionsRepository permissionsRepository;
        private ICountersRepository countersRepository;
        private IActuallyPaidAmountsRepository actuallyPaidAmountsRepository;
        private IContractsRepository contractsRepository;
        private IActuallyPaidAmountService actuallyPaidAmountService;
        private IContractReportPaymentsRepository contractReportPaymentsRepository;

        public ActuallyPaidAmountsController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IAccessContext accessContext,
            IPermissionsRepository permissionsRepository,
            ICountersRepository countersRepository,
            IActuallyPaidAmountsRepository actuallyPaidAmountsRepository,
            IContractsRepository contractsRepository,
            IActuallyPaidAmountService actuallyPaidAmountService,
            IContractReportPaymentsRepository contractReportPaymentsRepository,
            IRelationsRepository relationsRepository)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.accessContext = accessContext;
            this.permissionsRepository = permissionsRepository;
            this.countersRepository = countersRepository;
            this.actuallyPaidAmountsRepository = actuallyPaidAmountsRepository;
            this.contractsRepository = contractsRepository;
            this.actuallyPaidAmountService = actuallyPaidAmountService;
            this.contractReportPaymentsRepository = contractReportPaymentsRepository;
            this.relationsRepository = relationsRepository;
        }

        [Route("")]
        public IList<ActuallyPaidAmountVO> GetActuallyPaidAmounts(int? contractId = null, PaymentReason? paymentReason = null)
        {
            this.authorizer.AssertCanDo(ActuallyPaidAmountListActions.Search);

            var programmeIds = this.permissionsRepository.GetProgrammeIdsByPermission(this.accessContext.UserId, MonitoringFinancialControlPermissions.CanRead);

            return this.actuallyPaidAmountsRepository.GetActuallyPaidAmounts(programmeIds, this.accessContext.UserId, contractId, paymentReason);
        }

        [Route("~/api/projectDossier/{projectId:int}/contract/{contractId:int}/actuallyPaidAmounts")]
        public IList<ActuallyPaidAmountVO> GetActuallyPaidAmountsForProjectDossier(int projectId, int contractId)
        {
            this.authorizer.AssertCanDo(ProjectDossierActions.View, projectId);
            this.relationsRepository.AssertProjectHasContract(projectId, contractId);

            return this.actuallyPaidAmountsRepository.GetActuallyPaidAmountsForProjectDossier(contractId);
        }

        [Route("~/api/contracts/{contractId:int}/actuallyPaidAmounts")]
        public IList<ActuallyPaidAmountVO> GetActuallyPaidAmountsForContract(int contractId)
        {
            this.authorizer.AssertCanDo(ContractActions.View, contractId);

            return this.actuallyPaidAmountsRepository.GetActuallyPaidAmountsForProjectDossier(contractId);
        }

        [HttpPost]
        [Route("canCreate")]
        public object CanCreateActuallyPaidAmount(string contractRegNumber)
        {
            this.authorizer.AssertCanDo(ActuallyPaidAmountListActions.Create);

            var errorList = this.actuallyPaidAmountService.CanCreate(contractRegNumber, this.accessContext.UserId);

            return new ErrorsDO(errorList);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ActuallyPaidAmounts.Create))]
        public object CreateActuallyPaidAmount(
            int contractId,
            int programmePriorityId,
            PaymentReason paymentReason,
            int? contractReportPaymentId = null)
        {
            this.authorizer.AssertCanDo(ActuallyPaidAmountListActions.Create);

            if (!this.actuallyPaidAmountService.CanCreate(this.accessContext.UserId, contractId, contractReportPaymentId))
            {
                throw new DomainValidationException("Cannot create actually paid amount.");
            }

            var programmeId = this.contractsRepository.GetProgrammeId(contractId);
            var newPaidAmount = new ActuallyPaidAmount(
                programmeId,
                programmePriorityId,
                contractId,
                contractReportPaymentId,
                paymentReason);

            this.actuallyPaidAmountsRepository.Add(newPaidAmount);
            this.unitOfWork.Save();

            return new { PaidAmountId = newPaidAmount.ActuallyPaidAmountId };
        }

        [Route("{paidAmountId:int}/info")]
        public ActuallyPaidAmountInfoVO GetActuallyPaidAmountInfo(int paidAmountId)
        {
            this.authorizer.AssertCanDo(ActuallyPaidAmountActions.View, paidAmountId);

            return this.actuallyPaidAmountsRepository.GetInfo(paidAmountId);
        }

        [Route("{paidAmountId:int}/data")]
        public ActuallyPaidAmountBasicDataVO GetActuallyPaidAmountBasicData(int paidAmountId)
        {
            this.authorizer.AssertCanDo(ActuallyPaidAmountActions.View, paidAmountId);

            return this.actuallyPaidAmountsRepository.GetBasicData(paidAmountId);
        }

        [Route("{paidAmountId:int}")]
        public ActuallyPaidAmountDO GetActuallyPaidAmountData(int paidAmountId)
        {
            this.authorizer.AssertCanDo(ActuallyPaidAmountActions.View, paidAmountId);

            var paidAmount = this.actuallyPaidAmountsRepository.Find(paidAmountId);
            var basicData = this.actuallyPaidAmountsRepository.GetBasicData(paidAmountId);

            return new ActuallyPaidAmountDO(paidAmount, basicData);
        }

        [HttpPut]
        [Route("{paidAmountId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ActuallyPaidAmounts.Edit), IdParam = "paidAmountId")]
        public void UpdateActuallyPaidAmountData(int paidAmountId, ActuallyPaidAmountDO paidAmountDO)
        {
            this.authorizer.AssertCanDo(ActuallyPaidAmountActions.Edit, paidAmountId);

            var paidAmount = this.actuallyPaidAmountsRepository.FindForUpdate(paidAmountId, paidAmountDO.Version);

            if (paidAmount.SapFileId == null)
            {
                paidAmount.UpdateData(
                    paidAmountDO.ProgrammePriorityId,
                    paidAmountDO.PaymentReason.Value,
                    paidAmountDO.PaymentDate,
                    paidAmountDO.Comment,
                    0,
                    paidAmountDO.PaidTotalAmount,
                    0,
                    0);
            }
            else
            {
                paidAmount.UpdateSapData(
                    paidAmountDO.ProgrammePriorityId,
                    paidAmountDO.PaymentReason.Value,
                    paidAmountDO.Comment,
                    paidAmountDO.PaidSelfAmount,
                    paidAmountDO.PaidBfpCrossAmount);
            }

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{paidAmountId:int}/assignContractReportPayment")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ActuallyPaidAmounts.AssignContractReportPayment), IdParam = "paidAmountId")]
        public void AssignContractReportPayment(int paidAmountId, int contractReportPaymentId, string version)
        {
            this.authorizer.AssertCanDo(ActuallyPaidAmountActions.Edit, paidAmountId);

            byte[] vers = System.Convert.FromBase64String(version);
            var paidAmount = this.actuallyPaidAmountsRepository.FindForUpdate(paidAmountId, vers);

            if (!this.actuallyPaidAmountService.CanAssignContractReportPaymentId(paidAmount.ContractId, contractReportPaymentId))
            {
                throw new DomainValidationException("Cannot assign this contractReportPaymentId.");
            }

            paidAmount.AssignContractReportPayment(contractReportPaymentId);
            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{paidAmountId:int}/changeContractReportPayment")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ActuallyPaidAmounts.ChangeContractReportPayment), IdParam = "paidAmountId")]
        public void ChangeContractReportPayment(int paidAmountId, int contractReportPaymentId, string version)
        {
            this.authorizer.AssertCanDo(ActuallyPaidAmountActions.Edit, paidAmountId);

            byte[] vers = System.Convert.FromBase64String(version);
            var paidAmount = this.actuallyPaidAmountsRepository.FindForUpdate(paidAmountId, vers);

            if (!this.actuallyPaidAmountService.CanAssignContractReportPaymentId(paidAmount.ContractId, contractReportPaymentId))
            {
                throw new DomainValidationException("Cannot change to this contractReportPaymentId.");
            }

            paidAmount.ChangeContractReportPayment(contractReportPaymentId);
            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{paidAmountId:int}/dissociateContractReportPayment")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ActuallyPaidAmounts.DissociateContractReportPayment), IdParam = "paidAmountId")]
        public void DissociateContractReportPayment(int paidAmountId, string version)
        {
            this.authorizer.AssertCanDo(ActuallyPaidAmountActions.Edit, paidAmountId);

            byte[] vers = System.Convert.FromBase64String(version);
            var paidAmount = this.actuallyPaidAmountsRepository.FindForUpdate(paidAmountId, vers);

            paidAmount.DissociateContractReportPayment();
            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{paidAmountId:int}/canChangeStatusToEntered")]
        public ErrorsDO CanEnterActuallyPaidAmount(int paidAmountId)
        {
            this.authorizer.AssertCanDo(ActuallyPaidAmountActions.Edit, paidAmountId);

            var paidAmount = this.actuallyPaidAmountsRepository.Find(paidAmountId);

            var errors = this.actuallyPaidAmountService.CanChangeStatusToEntered(paidAmount);

            return new ErrorsDO(errors);
        }

        [HttpPost]
        [Route("{paidAmountId:int}/changeStatusToEntered")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ActuallyPaidAmounts.ChangeStatusToEntered), IdParam = "paidAmountId")]
        public void EnterActuallyPaidAmount(int paidAmountId, string version)
        {
            this.authorizer.AssertCanDo(ActuallyPaidAmountActions.Edit, paidAmountId);

            byte[] vers = System.Convert.FromBase64String(version);
            var paidAmount = this.actuallyPaidAmountsRepository.FindForUpdate(paidAmountId, vers);

            this.actuallyPaidAmountService.ChangeStatusToEntered(paidAmount);
        }

        [HttpPost]
        [Route("{paidAmountId:int}/setToDraft")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ActuallyPaidAmounts.ChangeStatusToDraft), IdParam = "paidAmountId")]
        public void MakeDraft(int paidAmountId, string version)
        {
            this.authorizer.AssertCanDo(ActuallyPaidAmountActions.Edit, paidAmountId);

            byte[] vers = System.Convert.FromBase64String(version);
            var paidAmount = this.actuallyPaidAmountsRepository.FindForUpdate(paidAmountId, vers);

            paidAmount.ChangeStatusToDraft();

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{paidAmountId:int}/setToRemoved")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ActuallyPaidAmounts.ChangeStatusToRemoved), IdParam = "paidAmountId")]
        public void MakeDraft(int paidAmountId, string version, ConfirmDO confirm)
        {
            this.authorizer.AssertCanDo(ActuallyPaidAmountActions.Edit, paidAmountId);

            byte[] vers = System.Convert.FromBase64String(version);
            var paidAmount = this.actuallyPaidAmountsRepository.FindForUpdate(paidAmountId, vers);

            paidAmount.ChangeStatusToDeleted(confirm.Note);

            this.unitOfWork.Save();
        }

        [HttpDelete]
        [Route("{paidAmountId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ActuallyPaidAmounts.Delete), IdParam = "paidAmountId")]
        public void DeleteActuallyPaidAmount(int paidAmountId, string version)
        {
            this.authorizer.AssertCanDo(ActuallyPaidAmountActions.Edit, paidAmountId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.actuallyPaidAmountService.Delete(paidAmountId, vers);
        }

        [Route("{paidAmountId:int}/contractReportPayments")]
        public IList<ActuallyPaidAmountContractReportPaymentVO> GetContractReportPayments(int paidAmountId)
        {
            this.authorizer.AssertCanDo(ActuallyPaidAmountActions.Edit, paidAmountId);

            var contractId = this.actuallyPaidAmountsRepository.GetContractId(paidAmountId);

            return this.contractReportPaymentsRepository.GetActuallyPaidAmountContractReportPayments(contractId);
        }
    }
}
