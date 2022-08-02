using System;
using System.Collections.Generic;
using System.Web.Http;
using Eumis.ApplicationServices.Services.ContractSpendingPlan;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Authentication.Authorization.ClaimsContexts.User;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.Contracts.Repositories;
using Eumis.Data.Contracts.ViewObjects;
using Eumis.Data.Core.Relations;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.Contracts.DataObjects;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.Contracts.Controllers
{
    [RoutePrefix("api/contracts/{contractId:int}/spendingPlans")]
    public class ContractSpendingPlansController : ApiController
    {
        private IRelationsRepository relationsRepository;
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IContractSpendingPlansRepository contractSpendingPlansRepository;
        private IContractSpendingPlanService contractSpendingPlanService;
        private IUserClaimsContext currentUserClaimsContext;
        private UserClaimsContextFactory userClaimsContextFactory;

        public ContractSpendingPlansController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IAccessContext accessContext,
            IContractSpendingPlansRepository contractSpendingPlansRepository,
            IContractSpendingPlanService contractSpendingPlanService,
            UserClaimsContextFactory userClaimsContextFactory,
            IRelationsRepository relationsRepository)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.contractSpendingPlansRepository = contractSpendingPlansRepository;
            this.contractSpendingPlanService = contractSpendingPlanService;
            this.userClaimsContextFactory = userClaimsContextFactory;
            this.relationsRepository = relationsRepository;

            if (accessContext.IsUser)
            {
                this.currentUserClaimsContext = userClaimsContextFactory(accessContext.UserId);
            }
        }

        [Route("")]
        public IList<ContractSpendingPlanVO> GetContractSpendingPlans(int contractId)
        {
            this.authorizer.AssertCanDo(ContractActions.View, contractId);

            return this.contractSpendingPlansRepository.GetContractSpendingPlans(contractId);
        }

        [Route("~/api/projectDossier/{projectId:int}/contract/{contractId:int}/spendingPlans")]
        public IList<ContractSpendingPlanVO> GetProjectDossierContractSpendingPlans(int projectId, int contractId)
        {
            this.authorizer.AssertCanDo(ProjectDossierActions.View, projectId);
            this.relationsRepository.AssertProjectHasContract(projectId, contractId);

            return this.contractSpendingPlansRepository.GetContractSpendingPlans(contractId);
        }

        [Route("{spendingPlanId:int}")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public ContractSpendingPlanDO GetSpendingPlan(int contractId, int spendingPlanId)
        {
            this.authorizer.AssertCanDo(ContractSpendingPlanActions.View, spendingPlanId);

            var spendingPlan = this.contractSpendingPlansRepository.Find(spendingPlanId);

            var userClaimsContext = this.userClaimsContextFactory(spendingPlan.CreatedByUserId);
            var username = string.Format("{0} ({1})", userClaimsContext.Fullname, userClaimsContext.Username);

            return new ContractSpendingPlanDO(spendingPlan, username);
        }

        [Route("~/api/projectDossier/{projectId:int}/contract/{contractId:int}/spendingPlans/{spendingPlanId:int}")]
        public ContractSpendingPlanDO GetSpendingPlan(int projectId, int contractId, int spendingPlanId)
        {
            this.authorizer.AssertCanDo(ProjectDossierActions.View, projectId);
            this.relationsRepository.AssertProjectHasContract(projectId, contractId);
            this.relationsRepository.AssertContractHasSpendingPlan(contractId, spendingPlanId);

            var spendingPlan = this.contractSpendingPlansRepository.Find(spendingPlanId);

            var userClaimsContext = this.userClaimsContextFactory(spendingPlan.CreatedByUserId);
            var username = string.Format("{0} ({1})", userClaimsContext.Fullname, userClaimsContext.Username);

            return new ContractSpendingPlanDO(spendingPlan, username);
        }

        [HttpGet]
        [Route("new")]
        public ContractSpendingPlanDO NewSpendingPlan(int contractId)
        {
            this.authorizer.AssertCanDo(ContractActions.Edit, contractId);

            var username = string.Format("{0} ({1})", this.currentUserClaimsContext.Fullname, this.currentUserClaimsContext.Username);

            return new ContractSpendingPlanDO(username);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Contracts.Edit.SpendingPlans.Create), IdParam = "contractId")]
        public object CreateSpendingPlan(int contractId, ContractSpendingPlanDO spendingPlanDO)
        {
            this.authorizer.AssertCanDo(ContractActions.Edit, contractId);

            if (this.contractSpendingPlanService.CanCreateSpendingPlan(contractId).Count != 0)
            {
                throw new InvalidOperationException("Cannot create new contract spendingPlan.");
            }

            var newSpendingPlan = this.contractSpendingPlanService.CreateSpendingPlanFromAdministrativeAuthority(contractId, spendingPlanDO.CreateNote);

            return new { ContractId = newSpendingPlan.ContractId, ContractSpendingPlanId = newSpendingPlan.ContractSpendingPlanXmlId };
        }

        [HttpPut]
        [Route("{spendingPlanId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Contracts.Edit.SpendingPlans.Edit), IdParam = "contractId", ChildIdParam = "spendingPlanId")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public void UpdateSpendingPlan(int contractId, int spendingPlanId, ContractSpendingPlanDO spendingPlanDO)
        {
            this.authorizer.AssertCanDo(ContractSpendingPlanActions.Edit, spendingPlanId);

            var spendingPlan = this.contractSpendingPlansRepository.FindForUpdate(spendingPlanId, spendingPlanDO.Version);

            spendingPlan.SetAttributes(spendingPlanDO.CreateNote);

            this.unitOfWork.Save();
        }

        [HttpDelete]
        [Route("{spendingPlanId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Contracts.Edit.SpendingPlans.Delete), IdParam = "contractId", ChildIdParam = "spendingPlanId")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public void DeleteSpendingPlan(int contractId, int spendingPlanId, string version)
        {
            this.authorizer.AssertCanDo(ContractSpendingPlanActions.Delete, spendingPlanId);

            byte[] vers = System.Convert.FromBase64String(version);
            var contractSpendingPlan = this.contractSpendingPlansRepository.FindForUpdate(spendingPlanId, vers);

            this.contractSpendingPlansRepository.Remove(contractSpendingPlan);

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{spendingPlanId:int}/changeStatusToDraft")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Contracts.Edit.SpendingPlans.ChangeStatusToDraft), IdParam = "contractId", ChildIdParam = "spendingPlanId")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public void ChnageStatusToDraft(int contractId, int spendingPlanId, string version)
        {
            this.authorizer.AssertCanDo(ContractSpendingPlanActions.ChangeStatusToDraft, spendingPlanId);

            byte[] vers = System.Convert.FromBase64String(version);

            var contractSpendingPlan = this.contractSpendingPlansRepository.FindForUpdate(spendingPlanId, vers);

            contractSpendingPlan.ChangeStatusToDraft();

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{spendingPlanId:int}/markAsChecked")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Contracts.Edit.SpendingPlans.MarkAsChecked), IdParam = "contractId", ChildIdParam = "spendingPlanId")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public void MarkAsChecked(int contractId, int spendingPlanId, string version)
        {
            this.authorizer.AssertCanDo(ContractSpendingPlanActions.MarkAsChecked, spendingPlanId);

            this.contractSpendingPlanService.ActivateSpendingPlan(spendingPlanId, Convert.FromBase64String(version));
        }

        [HttpPost]
        [Route("canCreate")]
        public ErrorsDO CanCreateSpendingPlan(int contractId)
        {
            this.authorizer.AssertCanDo(ContractActions.Edit, contractId);

            var errorList = this.contractSpendingPlanService.CanCreateSpendingPlan(contractId);

            return new ErrorsDO(errorList);
        }
    }
}
