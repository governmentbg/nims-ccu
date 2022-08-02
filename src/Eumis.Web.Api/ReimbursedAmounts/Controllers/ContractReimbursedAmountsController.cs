using Eumis.ApplicationServices.Services.ReimbursedAmount;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.Contracts.Repositories;
using Eumis.Data.Core.Permissions;
using Eumis.Data.Core.Relations;
using Eumis.Data.Counters;
using Eumis.Data.ReimbursedAmounts.Repositories;
using Eumis.Domain;
using Eumis.Domain.MonitoringFinancialControl.ReimbursedAmounts;
using Eumis.Domain.MonitoringFinancialControl.ReimbursedAmounts.DataObjects;
using Eumis.Domain.MonitoringFinancialControl.ReimbursedAmounts.ViewObjects;
using Eumis.Domain.Users.ProgrammePermissions;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.Core;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.ReimbursedAmounts.Controllers
{
    [RoutePrefix("api/contarctReimbursedAmounts")]
    public class ContractReimbursedAmountsController : ApiController
    {
        private IRelationsRepository relationsRepository;
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IAccessContext accessContext;
        private IPermissionsRepository permissionsRepository;
        private ICountersRepository countersRepository;
        private IContractReimbursedAmountsRepository contractReimbursedAmountsRepository;
        private IDebtReimbursedAmountsRepository debtReimbursedAmountsRepository;
        private IContractsRepository contractsRepository;
        private IReimbursedAmountService reimbursedAmountService;

        public ContractReimbursedAmountsController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IAccessContext accessContext,
            IPermissionsRepository permissionsRepository,
            ICountersRepository countersRepository,
            IContractReimbursedAmountsRepository contractReimbursedAmountsRepository,
            IDebtReimbursedAmountsRepository debtReimbursedAmountsRepository,
            IContractsRepository contractsRepository,
            IReimbursedAmountService reimbursedAmountService,
            IRelationsRepository relationsRepository)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.accessContext = accessContext;
            this.permissionsRepository = permissionsRepository;
            this.countersRepository = countersRepository;
            this.contractReimbursedAmountsRepository = contractReimbursedAmountsRepository;
            this.debtReimbursedAmountsRepository = debtReimbursedAmountsRepository;
            this.contractsRepository = contractsRepository;
            this.reimbursedAmountService = reimbursedAmountService;
            this.relationsRepository = relationsRepository;
        }

        [Route("")]
        public IList<ContractReimbursedAmountVO> GetReimbursedAmounts(int? contractId = null, ReimbursementType? type = null)
        {
            this.authorizer.AssertCanDo(ContractReimbursedAmountListActions.Search);

            var programmeIds = this.permissionsRepository.GetProgrammeIdsByPermission(this.accessContext.UserId, MonitoringFinancialControlPermissions.CanRead);

            return this.contractReimbursedAmountsRepository.GetReimbursedAmounts(programmeIds, this.accessContext.UserId, contractId, type);
        }

        [Route("~/api/projectDossier/{projectId:int}/contract/{contractId:int}/contractReimbursedAmounts")]
        public IList<ContractReimbursedAmountVO> GetReimbursedAmountsForProjectDossier(int projectId, int contractId)
        {
            this.authorizer.AssertCanDo(ProjectDossierActions.View, projectId);
            this.relationsRepository.AssertProjectHasContract(projectId, contractId);

            return this.contractReimbursedAmountsRepository.GetReimbursedAmountsForProjectDossier(contractId);
        }

        [Route("~/api/contracts/{contractId:int}/reimbursedAmounts")]
        public IList<ContractReimbursedAmountVO> GetReimbursedAmountsForProjectDossier(int contractId)
        {
            this.authorizer.AssertCanDo(ContractActions.View, contractId);

            return this.contractReimbursedAmountsRepository.GetReimbursedAmountsForProjectDossier(contractId);
        }

        [HttpGet]
        [Route("new")]
        public NewContractReimbursedAmountDO NewReimbursedAmount(string contractNum)
        {
            this.authorizer.AssertCanDo(ContractReimbursedAmountListActions.Create);

            var contract = this.contractsRepository.FindByRegNumber(contractNum);

            return new NewContractReimbursedAmountDO(contract);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReimbursedAmounts.Create))]
        public object CreateReimbursedAmount(NewContractReimbursedAmountDO newReimbursedAmountDO)
        {
            this.authorizer.AssertCanDo(ContractReimbursedAmountListActions.Create);

            var contract = this.contractsRepository.Find(newReimbursedAmountDO.Contract.ContractId);
            if (!this.reimbursedAmountService.CanCreateContractReimbursedAmount(this.accessContext.UserId, contract))
            {
                throw new DomainValidationException("Cannot create reimbursed amount.");
            }

            var newReimbursedAmount = new ContractReimbursedAmount(
                contract.ProgrammeId,
                contract.ContractId,
                newReimbursedAmountDO.ReimbursementDate.Value,
                newReimbursedAmountDO.Type.Value,
                newReimbursedAmountDO.Reimbursement.Value,
                newReimbursedAmountDO.ProgrammePriorityId.Value);

            this.contractReimbursedAmountsRepository.Add(newReimbursedAmount);
            this.unitOfWork.Save();

            return new { ReimbursedAmountId = newReimbursedAmount.ReimbursedAmountId };
        }

        [Route("{reimbursedAmountId:int}/info")]
        public ContractReimbursedAmountInfoVO GetReimbursedAmountInfo(int reimbursedAmountId)
        {
            this.authorizer.AssertCanDo(ContractReimbursedAmountActions.View, reimbursedAmountId);

            return this.contractReimbursedAmountsRepository.GetInfo(reimbursedAmountId);
        }

        [Route("{reimbursedAmountId:int}/data")]
        public ContractReimbursedAmountBasicDataVO GetReimbursedAmountBasicData(int reimbursedAmountId)
        {
            this.authorizer.AssertCanDo(ContractReimbursedAmountActions.View, reimbursedAmountId);

            return this.contractReimbursedAmountsRepository.GetBasicData(reimbursedAmountId);
        }

        [Route("{reimbursedAmountId:int}")]
        public ReimbursedAmountDO GetReimbursedAmountData(int reimbursedAmountId)
        {
            this.authorizer.AssertCanDo(ContractReimbursedAmountActions.View, reimbursedAmountId);

            var reimbursedAmount = this.contractReimbursedAmountsRepository.Find(reimbursedAmountId);

            return new ReimbursedAmountDO(reimbursedAmount);
        }

        [HttpPut]
        [Route("{reimbursedAmountId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReimbursedAmounts.Edit), IdParam = "reimbursedAmountId")]
        public void UpdateReimbursedAmountData(int reimbursedAmountId, ReimbursedAmountDO reimbursedAmountDO)
        {
            this.authorizer.AssertCanDo(ContractReimbursedAmountActions.Edit, reimbursedAmountId);

            var reimbursedAmount = this.contractReimbursedAmountsRepository.FindForUpdate(reimbursedAmountId, reimbursedAmountDO.Version);

            reimbursedAmount.UpdateContractData(
                reimbursedAmountDO.ReimbursementDate.Value,
                reimbursedAmountDO.Type.Value,
                reimbursedAmountDO.Reimbursement.Value,
                0,
                reimbursedAmountDO.PrincipalBfpTotalAmount,
                0,
                reimbursedAmountDO.InterestBfpTotalAmount,
                reimbursedAmountDO.Comment,
                reimbursedAmountDO.ProgrammePriorityId.Value,
                reimbursedAmountDO.PaymentIds);
            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{reimbursedAmountId:int}/enter")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReimbursedAmounts.ChangeStatusToEntered), IdParam = "reimbursedAmountId")]
        public void EnterReimbursedAmount(int reimbursedAmountId, string version)
        {
            this.authorizer.AssertCanDo(ContractReimbursedAmountActions.Edit, reimbursedAmountId);

            byte[] vers = System.Convert.FromBase64String(version);
            var reimbursedAmount = this.contractReimbursedAmountsRepository.FindForUpdate(reimbursedAmountId, vers);

            if (reimbursedAmount.IsActivated)
            {
                reimbursedAmount.ChangeStatusToEntered();
            }
            else
            {
                this.countersRepository.CreateContractReimbursedAmountCounter(reimbursedAmount.ContractId);
                var regNum = this.countersRepository.GetNextContractReimbursedAmountNumber(reimbursedAmount.ContractId);

                reimbursedAmount.ChangeStatusToEntered(regNum);
            }

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{reimbursedAmountId:int}/setToDraft")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReimbursedAmounts.ChangeStatusToDraft), IdParam = "reimbursedAmountId")]
        public void MakeDraft(int reimbursedAmountId, string version)
        {
            this.authorizer.AssertCanDo(ContractReimbursedAmountActions.Edit, reimbursedAmountId);

            byte[] vers = System.Convert.FromBase64String(version);
            var reimbursedAmount = this.contractReimbursedAmountsRepository.FindForUpdate(reimbursedAmountId, vers);

            reimbursedAmount.ChangeStatusToDraft();

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{reimbursedAmountId:int}/setToRemoved")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReimbursedAmounts.ChangeStatusToRemoved), IdParam = "reimbursedAmountId")]
        public void MakeDraft(int reimbursedAmountId, string version, ConfirmDO confirm)
        {
            this.authorizer.AssertCanDo(ContractReimbursedAmountActions.Edit, reimbursedAmountId);

            byte[] vers = System.Convert.FromBase64String(version);
            var reimbursedAmount = this.contractReimbursedAmountsRepository.FindForUpdate(reimbursedAmountId, vers);

            reimbursedAmount.ChangeStatusToDeleted(confirm.Note);

            this.unitOfWork.Save();
        }

        [HttpDelete]
        [Route("{reimbursedAmountId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReimbursedAmounts.Delete), IdParam = "reimbursedAmountId")]
        public void DeleteReimbursedAmount(int reimbursedAmountId, string version)
        {
            this.authorizer.AssertCanDo(ContractReimbursedAmountActions.Edit, reimbursedAmountId);

            byte[] vers = System.Convert.FromBase64String(version);
            var reimbursedAmount = this.contractReimbursedAmountsRepository.FindForUpdate(reimbursedAmountId, vers);

            this.contractReimbursedAmountsRepository.Remove(reimbursedAmount);

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{reimbursedAmountId:int}/attachToContractDebt")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReimbursedAmounts.AttachToContractDebt), IdParam = "reimbursedAmountId")]
        public void AttachToContractDebt(int reimbursedAmountId, int contractDebtId, string version)
        {
            this.authorizer.AssertCanDo(ContractReimbursedAmountActions.Edit, reimbursedAmountId);

            byte[] vers = System.Convert.FromBase64String(version);
            var reimbursedAmount = this.contractReimbursedAmountsRepository.FindForUpdate(reimbursedAmountId, vers);

            if (reimbursedAmount.Status != ReimbursedAmountStatus.Draft)
            {
                throw new Exception("Cannot attach ContractReimbursedAmount to ContractDebt when its status is different from Draft");
            }

            reimbursedAmount.ContractReimbursedAmountPayments.Clear();
            reimbursedAmount.ProgrammePriorityId = null;

            this.unitOfWork.Save();

            this.contractReimbursedAmountsRepository.SwitchToDebtReimbursedAmount(reimbursedAmountId, contractDebtId);

            this.unitOfWork.Save();
        }
    }
}
