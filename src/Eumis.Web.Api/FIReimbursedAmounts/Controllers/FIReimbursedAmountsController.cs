using Eumis.ApplicationServices.Services.FIReimbursedAmount;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.Contracts.Repositories;
using Eumis.Data.Core.Permissions;
using Eumis.Data.Core.Relations;
using Eumis.Data.Counters;
using Eumis.Data.FIReimbursedAmounts.Repositories;
using Eumis.Data.FIReimbursedAmounts.ViewObjects;
using Eumis.Domain;
using Eumis.Domain.FIReimbursedAmounts;
using Eumis.Domain.Users.ProgrammePermissions;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.Core;
using Eumis.Web.Api.ReimbursedAmounts.DataObjects;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.ReimbursedAmounts.Controllers
{
    [RoutePrefix("api/fiReimbursedAmounts")]
    public class FIReimbursedAmountsController : ApiController
    {
        private IRelationsRepository relationsRepository;
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IAccessContext accessContext;
        private IPermissionsRepository permissionsRepository;
        private ICountersRepository countersRepository;
        private IFIReimbursedAmountsRepository fiReimbursedAmountsRepository;
        private IContractsRepository contractsRepository;
        private IFIReimbursedAmountService fiReimbursedAmountService;

        public FIReimbursedAmountsController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IAccessContext accessContext,
            IPermissionsRepository permissionsRepository,
            ICountersRepository countersRepository,
            IFIReimbursedAmountsRepository fiReimbursedAmountsRepository,
            IContractsRepository contractsRepository,
            IFIReimbursedAmountService fiReimbursedAmountService,
            IRelationsRepository relationsRepository)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.accessContext = accessContext;
            this.permissionsRepository = permissionsRepository;
            this.countersRepository = countersRepository;
            this.fiReimbursedAmountsRepository = fiReimbursedAmountsRepository;
            this.contractsRepository = contractsRepository;
            this.fiReimbursedAmountService = fiReimbursedAmountService;
            this.relationsRepository = relationsRepository;
        }

        [Route("")]
        public IList<FIReimbursedAmountVO> GetReimbursedAmounts(int? contractId = null, FIReimbursementType? type = null)
        {
            this.authorizer.AssertCanDo(FIReimbursedAmountListActions.Search);

            var programmeIds = this.permissionsRepository.GetProgrammeIdsByPermission(this.accessContext.UserId, MonitoringFinancialControlPermissions.CanRead);

            return this.fiReimbursedAmountsRepository.GetReimbursedAmounts(programmeIds, contractId, type);
        }

        [Route("~/api/projectDossier/{projectId:int}/contract/{contractId:int}/fiReimbursedAmounts")]
        public IList<FIReimbursedAmountVO> GetReimbursedAmountsForProjectDossier(int projectId, int contractId)
        {
            this.authorizer.AssertCanDo(ProjectDossierActions.View, projectId);
            this.relationsRepository.AssertProjectHasContract(projectId, contractId);

            return this.fiReimbursedAmountsRepository.GetReimbursedAmountsForProjectDossier(contractId);
        }

        [HttpGet]
        [Route("new")]
        public NewFIReimbursedAmountDO NewReimbursedAmount(string contractNum)
        {
            this.authorizer.AssertCanDo(FIReimbursedAmountListActions.Create);

            var contract = this.contractsRepository.FindByRegNumber(contractNum);

            return new NewFIReimbursedAmountDO(contract);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.FIReimbursedAmounts.Create))]
        public object CreateReimbursedAmount(NewFIReimbursedAmountDO newReimbursedAmountDO)
        {
            this.authorizer.AssertCanDo(FIReimbursedAmountListActions.Create);

            var contract = this.contractsRepository.Find(newReimbursedAmountDO.Contract.ContractId);
            if (!this.fiReimbursedAmountService.CanCreateFIReimbursedAmount(this.accessContext.UserId, contract))
            {
                throw new DomainValidationException("Cannot create reimbursed amount.");
            }

            var newReimbursedAmount = new FIReimbursedAmount(
                contract.ProgrammeId,
                contract.ContractId,
                newReimbursedAmountDO.ReimbursementDate.Value,
                newReimbursedAmountDO.Type.Value,
                newReimbursedAmountDO.Reimbursement.Value,
                newReimbursedAmountDO.ProgrammePriorityId.Value);

            this.fiReimbursedAmountsRepository.Add(newReimbursedAmount);
            this.unitOfWork.Save();

            return new { ReimbursedAmountId = newReimbursedAmount.FIReimbursedAmountId };
        }

        [Route("{reimbursedAmountId:int}/info")]
        public FIReimbursedAmountInfoVO GetReimbursedAmountInfo(int reimbursedAmountId)
        {
            this.authorizer.AssertCanDo(FIReimbursedAmountActions.View, reimbursedAmountId);

            return this.fiReimbursedAmountsRepository.GetInfo(reimbursedAmountId);
        }

        [Route("{reimbursedAmountId:int}/data")]
        public FIReimbursedAmountBasicDataVO GetReimbursedAmountBasicData(int reimbursedAmountId)
        {
            this.authorizer.AssertCanDo(FIReimbursedAmountActions.View, reimbursedAmountId);

            return this.fiReimbursedAmountsRepository.GetBasicData(reimbursedAmountId);
        }

        [Route("{reimbursedAmountId:int}")]
        public FIReimbursedAmountDO GetReimbursedAmountData(int reimbursedAmountId)
        {
            this.authorizer.AssertCanDo(FIReimbursedAmountActions.View, reimbursedAmountId);

            var reimbursedAmount = this.fiReimbursedAmountsRepository.Find(reimbursedAmountId);

            return new FIReimbursedAmountDO(reimbursedAmount);
        }

        [HttpPut]
        [Route("{reimbursedAmountId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.FIReimbursedAmounts.Edit), IdParam = "reimbursedAmountId")]
        public void UpdateReimbursedAmountData(int reimbursedAmountId, FIReimbursedAmountDO reimbursedAmountDO)
        {
            this.authorizer.AssertCanDo(FIReimbursedAmountActions.Edit, reimbursedAmountId);

            var reimbursedAmount = this.fiReimbursedAmountsRepository.FindForUpdate(reimbursedAmountId, reimbursedAmountDO.Version);

            reimbursedAmount.UpdateData(
                reimbursedAmountDO.ReimbursementDate.Value,
                reimbursedAmountDO.Type.Value,
                reimbursedAmountDO.Reimbursement.Value,
                reimbursedAmountDO.PrincipalBfpEuAmount,
                reimbursedAmountDO.PrincipalBfpBgAmount,
                reimbursedAmountDO.InterestBfpEuAmount,
                reimbursedAmountDO.InterestBfpBgAmount,
                reimbursedAmountDO.Comment,
                reimbursedAmountDO.ProgrammePriorityId.Value);
            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{reimbursedAmountId:int}/enter")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.FIReimbursedAmounts.ChangeStatusToEntered), IdParam = "reimbursedAmountId")]
        public void EnterReimbursedAmount(int reimbursedAmountId, string version)
        {
            this.authorizer.AssertCanDo(FIReimbursedAmountActions.Edit, reimbursedAmountId);

            byte[] vers = System.Convert.FromBase64String(version);
            var reimbursedAmount = this.fiReimbursedAmountsRepository.FindForUpdate(reimbursedAmountId, vers);

            if (reimbursedAmount.IsActivated)
            {
                reimbursedAmount.ChangeStatusToEntered();
            }
            else
            {
                this.countersRepository.CreateFIReimbursedAmountCounter(reimbursedAmount.ContractId);
                var regNum = this.countersRepository.GetNextFIReimbursedAmountNumber(reimbursedAmount.ContractId);

                reimbursedAmount.ChangeStatusToEntered(regNum);
            }

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{reimbursedAmountId:int}/setToDraft")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.FIReimbursedAmounts.ChangeStatusToDraft), IdParam = "reimbursedAmountId")]
        public void MakeDraft(int reimbursedAmountId, string version)
        {
            this.authorizer.AssertCanDo(FIReimbursedAmountActions.Edit, reimbursedAmountId);

            byte[] vers = System.Convert.FromBase64String(version);
            var reimbursedAmount = this.fiReimbursedAmountsRepository.FindForUpdate(reimbursedAmountId, vers);

            reimbursedAmount.ChangeStatusToDraft();

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{reimbursedAmountId:int}/setToRemoved")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.FIReimbursedAmounts.ChangeStatusToRemoved), IdParam = "reimbursedAmountId")]
        public void MakeDraft(int reimbursedAmountId, string version, ConfirmDO confirm)
        {
            this.authorizer.AssertCanDo(FIReimbursedAmountActions.Edit, reimbursedAmountId);

            byte[] vers = System.Convert.FromBase64String(version);
            var reimbursedAmount = this.fiReimbursedAmountsRepository.FindForUpdate(reimbursedAmountId, vers);

            reimbursedAmount.ChangeStatusToDeleted(confirm.Note);

            this.unitOfWork.Save();
        }

        [HttpDelete]
        [Route("{reimbursedAmountId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.FIReimbursedAmounts.Delete), IdParam = "reimbursedAmountId")]
        public void DeleteReimbursedAmount(int reimbursedAmountId, string version)
        {
            this.authorizer.AssertCanDo(FIReimbursedAmountActions.Edit, reimbursedAmountId);

            byte[] vers = System.Convert.FromBase64String(version);
            var reimbursedAmount = this.fiReimbursedAmountsRepository.FindForUpdate(reimbursedAmountId, vers);

            this.fiReimbursedAmountsRepository.Remove(reimbursedAmount);

            this.unitOfWork.Save();
        }
    }
}
