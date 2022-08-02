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
using Eumis.Data.Debts.Repositories;
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
using System.Linq;
using System.Web.Http;

namespace Eumis.Web.Api.ReimbursedAmounts.Controllers
{
    [RoutePrefix("api/debtReimbursedAmounts")]
    public class DebtReimbursedAmountsController : ApiController
    {
        private IRelationsRepository relationsRepository;
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IAccessContext accessContext;
        private IPermissionsRepository permissionsRepository;
        private ICountersRepository countersRepository;
        private IDebtReimbursedAmountsRepository debtReimbursedAmountsRepository;
        private IContractDebtsRepository contractDebtsRepository;
        private IContractsRepository contractsRepository;
        private IReimbursedAmountService reimbursedAmountService;

        public DebtReimbursedAmountsController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IAccessContext accessContext,
            IPermissionsRepository permissionsRepository,
            ICountersRepository countersRepository,
            IDebtReimbursedAmountsRepository debtReimbursedAmountsRepository,
            IContractDebtsRepository contractDebtsRepository,
            IContractsRepository contractsRepository,
            IReimbursedAmountService reimbursedAmountService,
            IRelationsRepository relationsRepository)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.accessContext = accessContext;
            this.permissionsRepository = permissionsRepository;
            this.countersRepository = countersRepository;
            this.debtReimbursedAmountsRepository = debtReimbursedAmountsRepository;
            this.contractDebtsRepository = contractDebtsRepository;
            this.contractsRepository = contractsRepository;
            this.reimbursedAmountService = reimbursedAmountService;
            this.relationsRepository = relationsRepository;
        }

        [Route("")]
        public IList<DebtReimbursedAmountVO> GetReimbursedAmounts(int? contractId = null, ReimbursementType? type = null)
        {
            this.authorizer.AssertCanDo(DebtReimbursedAmountListActions.Search);

            var programmeIds = this.permissionsRepository.GetProgrammeIdsByPermission(this.accessContext.UserId, MonitoringFinancialControlPermissions.CanRead);

            return this.debtReimbursedAmountsRepository.GetReimbursedAmounts(programmeIds, this.accessContext.UserId, contractId, type);
        }

        [Route("~/api/projectDossier/{projectId:int}/contract/{contractId:int}/debtReimbursedAmounts")]
        public IList<DebtReimbursedAmountVO> GetReimbursedAmountsForProjectDossier(int projectId, int contractId)
        {
            this.authorizer.AssertCanDo(ProjectDossierActions.View, projectId);
            this.relationsRepository.AssertProjectHasContract(projectId, contractId);

            return this.debtReimbursedAmountsRepository.GetReimbursedAmountsForProjectDossier(contractId);
        }

        [HttpGet]
        [Route("new")]
        public NewDebtReimbursedAmountDO NewReimbursedAmount()
        {
            this.authorizer.AssertCanDo(DebtReimbursedAmountListActions.Create);

            return new NewDebtReimbursedAmountDO();
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.DebtReimbursedAmounts.Create))]
        public object CreateReimbursedAmount(NewDebtReimbursedAmountDO newReimbursedAmountDO)
        {
            this.authorizer.AssertCanDo(DebtReimbursedAmountListActions.Create);

            var debt = this.contractDebtsRepository.Find(newReimbursedAmountDO.ContractDebtId.Value);
            if (!this.reimbursedAmountService.CanCreateDebtReimbursedAmount(this.accessContext.UserId, debt))
            {
                throw new DomainValidationException("Cannot create actually reimbursed amount.");
            }

            var programmeId = this.contractsRepository.GetProgrammeId(debt.ContractId);
            var newReimbursedAmount = new DebtReimbursedAmount(
                programmeId,
                debt.ContractId,
                newReimbursedAmountDO.ContractDebtId.Value,
                newReimbursedAmountDO.ReimbursementDate.Value,
                newReimbursedAmountDO.Type.Value,
                newReimbursedAmountDO.Reimbursement.Value);

            this.debtReimbursedAmountsRepository.Add(newReimbursedAmount);
            this.unitOfWork.Save();

            return new { ReimbursedAmountId = newReimbursedAmount.ReimbursedAmountId };
        }

        [Route("{reimbursedAmountId:int}/info")]
        public DebtReimbursedAmountInfoVO GetReimbursedAmountInfo(int reimbursedAmountId)
        {
            this.authorizer.AssertCanDo(DebtReimbursedAmountActions.View, reimbursedAmountId);

            return this.debtReimbursedAmountsRepository.GetInfo(reimbursedAmountId);
        }

        [Route("{reimbursedAmountId:int}/data")]
        public DebtReimbursedAmountBasicDataVO GetReimbursedAmountBasicData(int reimbursedAmountId)
        {
            this.authorizer.AssertCanDo(DebtReimbursedAmountActions.View, reimbursedAmountId);

            return this.debtReimbursedAmountsRepository.GetBasicData(reimbursedAmountId);
        }

        [Route("{reimbursedAmountId:int}")]
        public ReimbursedAmountDO GetReimbursedAmountData(int reimbursedAmountId)
        {
            this.authorizer.AssertCanDo(DebtReimbursedAmountActions.View, reimbursedAmountId);

            var reimbursedAmount = this.debtReimbursedAmountsRepository.Find(reimbursedAmountId);
            var contractDebtInfo = this.contractDebtsRepository.GetInfo(reimbursedAmount.ContractDebtId);

            return new ReimbursedAmountDO(reimbursedAmount, contractDebtInfo.ProgrammePriorityId);
        }

        [HttpPut]
        [Route("{reimbursedAmountId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.DebtReimbursedAmounts.Edit), IdParam = "reimbursedAmountId")]
        public void UpdateReimbursedAmountData(int reimbursedAmountId, ReimbursedAmountDO reimbursedAmountDO)
        {
            this.authorizer.AssertCanDo(DebtReimbursedAmountActions.Edit, reimbursedAmountId);

            var reimbursedAmount = this.debtReimbursedAmountsRepository.FindForUpdate(reimbursedAmountId, reimbursedAmountDO.Version);

            reimbursedAmount.UpdateData(
                reimbursedAmountDO.ReimbursementDate.Value,
                reimbursedAmountDO.Type.Value,
                reimbursedAmountDO.Reimbursement.Value,
                reimbursedAmountDO.PrincipalBfpEuAmount,
                reimbursedAmountDO.PrincipalBfpBgAmount,
                reimbursedAmountDO.InterestBfpEuAmount,
                reimbursedAmountDO.InterestBfpBgAmount,
                reimbursedAmountDO.Comment);
            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{reimbursedAmountId:int}/enter")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.DebtReimbursedAmounts.ChangeStatusToEntered), IdParam = "reimbursedAmountId")]
        public void EnterReimbursedAmount(int reimbursedAmountId, string version)
        {
            this.authorizer.AssertCanDo(DebtReimbursedAmountActions.Edit, reimbursedAmountId);

            if (this.debtReimbursedAmountsRepository.CanEnter(reimbursedAmountId).Any())
            {
                throw new Exception("Cannot enter ReimbursedAmount");
            }

            byte[] vers = System.Convert.FromBase64String(version);
            var reimbursedAmount = this.debtReimbursedAmountsRepository.FindForUpdate(reimbursedAmountId, vers);

            if (reimbursedAmount.IsActivated)
            {
                reimbursedAmount.ChangeStatusToEntered();
            }
            else
            {
                this.countersRepository.CreateDebtReimbursedAmountCounter(reimbursedAmount.ContractDebtId);
                var regNum = this.countersRepository.GetNextDebtReimbursedAmountNumber(reimbursedAmount.ContractDebtId);

                reimbursedAmount.ChangeStatusToEntered(regNum);
            }

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{reimbursedAmountId:int}/canEnter")]
        public ErrorsDO CanEnterReimbursedAmount(int reimbursedAmountId)
        {
            this.authorizer.AssertCanDo(DebtReimbursedAmountActions.Edit, reimbursedAmountId);

            var errors = this.debtReimbursedAmountsRepository.CanEnter(reimbursedAmountId);

            return new ErrorsDO(errors);
        }

        [HttpPost]
        [Route("{reimbursedAmountId:int}/setToDraft")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.DebtReimbursedAmounts.ChangeStatusToDraft), IdParam = "reimbursedAmountId")]
        public void MakeDraft(int reimbursedAmountId, string version)
        {
            this.authorizer.AssertCanDo(DebtReimbursedAmountActions.Edit, reimbursedAmountId);

            if (this.debtReimbursedAmountsRepository.CanSetToDraft(reimbursedAmountId).Any())
            {
                throw new Exception("Cannot set to draft ReimbursedAmount");
            }

            byte[] vers = System.Convert.FromBase64String(version);
            var reimbursedAmount = this.debtReimbursedAmountsRepository.FindForUpdate(reimbursedAmountId, vers);

            reimbursedAmount.ChangeStatusToDraft();

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{reimbursedAmountId:int}/canSetToDraft")]
        public ErrorsDO CanMakeDraft(int reimbursedAmountId)
        {
            this.authorizer.AssertCanDo(DebtReimbursedAmountActions.Edit, reimbursedAmountId);

            var errors = this.debtReimbursedAmountsRepository.CanSetToDraft(reimbursedAmountId);

            return new ErrorsDO(errors);
        }

        [HttpPost]
        [Route("{reimbursedAmountId:int}/setToRemoved")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.DebtReimbursedAmounts.ChangeStatusToRemoved), IdParam = "reimbursedAmountId")]
        public void MakeDraft(int reimbursedAmountId, string version, ConfirmDO confirm)
        {
            this.authorizer.AssertCanDo(DebtReimbursedAmountActions.Edit, reimbursedAmountId);

            byte[] vers = System.Convert.FromBase64String(version);
            var reimbursedAmount = this.debtReimbursedAmountsRepository.FindForUpdate(reimbursedAmountId, vers);

            reimbursedAmount.ChangeStatusToDeleted(confirm.Note);

            this.unitOfWork.Save();
        }

        [HttpDelete]
        [Route("{reimbursedAmountId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.DebtReimbursedAmounts.Delete), IdParam = "reimbursedAmountId")]
        public void DeleteReimbursedAmount(int reimbursedAmountId, string version)
        {
            this.authorizer.AssertCanDo(DebtReimbursedAmountActions.Edit, reimbursedAmountId);

            byte[] vers = System.Convert.FromBase64String(version);
            var reimbursedAmount = this.debtReimbursedAmountsRepository.FindForUpdate(reimbursedAmountId, vers);

            this.debtReimbursedAmountsRepository.Remove(reimbursedAmount);

            this.unitOfWork.Save();
        }
    }
}
