using System;
using System.Collections.Generic;
using System.Web.Http;
using Eumis.ApplicationServices.Services.ContractProcurement;
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
    [RoutePrefix("api/contracts/{contractId:int}/procurements")]
    public class ContractProcurementsController : ApiController
    {
        private IRelationsRepository relationsRepository;
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IContractProcurementsRepository contractProcurementsRepository;
        private IContractProcurementService contractProcurementService;
        private IUserClaimsContext currentUserClaimsContext;
        private UserClaimsContextFactory userClaimsContextFactory;

        public ContractProcurementsController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IAccessContext accessContext,
            IContractProcurementsRepository contractProcurementsRepository,
            IContractProcurementService contractProcurementService,
            UserClaimsContextFactory userClaimsContextFactory,
            IRelationsRepository relationsRepository)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.contractProcurementsRepository = contractProcurementsRepository;
            this.contractProcurementService = contractProcurementService;
            this.userClaimsContextFactory = userClaimsContextFactory;
            this.relationsRepository = relationsRepository;

            if (accessContext.IsUser)
            {
                this.currentUserClaimsContext = userClaimsContextFactory(accessContext.UserId);
            }
        }

        [Route("")]
        public IList<ContractProcurementVO> GetContractProcurements(int contractId)
        {
            this.authorizer.AssertCanDo(ContractActions.View, contractId);

            return this.contractProcurementsRepository.GetContractProcurements(contractId);
        }

        [Route("~/api/projectDossier/{projectId:int}/contract/{contractId:int}/procurements")]
        public IList<ContractProcurementVO> GetProjecDossierContractProcurements(int projectId, int contractId)
        {
            this.authorizer.AssertCanDo(ProjectDossierActions.View, projectId);
            this.relationsRepository.AssertProjectHasContract(projectId, contractId);

            return this.contractProcurementsRepository.GetContractProcurements(contractId);
        }

        [Route("{procurementId:int}")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public ContractProcurementDO GetProcurement(int contractId, int procurementId)
        {
            this.authorizer.AssertCanDo(ContractProcurementActions.View, procurementId);

            var procurement = this.contractProcurementsRepository.Find(procurementId);

            var userClaimsContext = this.userClaimsContextFactory(procurement.CreatedByUserId);
            var username = string.Format("{0} ({1})", userClaimsContext.Fullname, userClaimsContext.Username);

            return new ContractProcurementDO(procurement, username);
        }

        [Route("~/api/projectDossier/{projectId:int}/contract/{contractId:int}/procurements/{procurementId:int}")]
        public ContractProcurementDO GetProjecDossierProcurement(int projectId, int contractId, int procurementId)
        {
            this.authorizer.AssertCanDo(ProjectDossierActions.View, projectId);
            this.relationsRepository.AssertProjectHasContract(projectId, contractId);
            this.relationsRepository.AssertContractHasProcurement(contractId, procurementId);

            var procurement = this.contractProcurementsRepository.Find(procurementId);

            var userClaimsContext = this.userClaimsContextFactory(procurement.CreatedByUserId);
            var username = string.Format("{0} ({1})", userClaimsContext.Fullname, userClaimsContext.Username);

            return new ContractProcurementDO(procurement, username);
        }

        [HttpGet]
        [Route("new")]
        public ContractProcurementDO NewProcurement(int contractId)
        {
            this.authorizer.AssertCanDo(ContractActions.Edit, contractId);

            var username = string.Format("{0} ({1})", this.currentUserClaimsContext.Fullname, this.currentUserClaimsContext.Username);

            return new ContractProcurementDO(username);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Contracts.Edit.Procurements.Create), IdParam = "contractId")]
        public object CreateProcurement(int contractId, ContractProcurementDO procurementDO)
        {
            this.authorizer.AssertCanDo(ContractActions.Edit, contractId);

            if (this.contractProcurementService.CanCreateProcurement(contractId).Count != 0)
            {
                throw new InvalidOperationException("Cannot create new contract procurement.");
            }

            var newProcurement = this.contractProcurementService.CreateProcurementFromAdministrativeAuthority(contractId, procurementDO.CreateNote);

            return new { ContractId = newProcurement.ContractId, ContractProcurementId = newProcurement.ContractProcurementXmlId };
        }

        [HttpPut]
        [Route("{procurementId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Contracts.Edit.Procurements.Edit), IdParam = "contractId", ChildIdParam = "procurementId")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public void UpdateProcurement(int contractId, int procurementId, ContractProcurementDO procurementDO)
        {
            this.authorizer.AssertCanDo(ContractProcurementActions.Edit, procurementId);

            var procurement = this.contractProcurementsRepository.FindForUpdate(procurementId, procurementDO.Version);

            procurement.SetAttributes(procurementDO.CreateNote);

            this.unitOfWork.Save();
        }

        [HttpDelete]
        [Route("{procurementId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Contracts.Edit.Procurements.Delete), IdParam = "contractId", ChildIdParam = "procurementId")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public void DeleteProcurement(int contractId, int procurementId, string version)
        {
            this.authorizer.AssertCanDo(ContractProcurementActions.Delete, procurementId);

            byte[] vers = System.Convert.FromBase64String(version);
            var contractProcurement = this.contractProcurementsRepository.FindForUpdate(procurementId, vers);

            this.contractProcurementsRepository.Remove(contractProcurement);

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{procurementId:int}/changeStatusToDraft")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Contracts.Edit.Procurements.ChangeStatusToDraft), IdParam = "contractId", ChildIdParam = "procurementId")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public void ChnageStatusToDraft(int contractId, int procurementId, string version)
        {
            this.authorizer.AssertCanDo(ContractProcurementActions.ChangeStatusToDraft, procurementId);

            byte[] vers = System.Convert.FromBase64String(version);

            var contractProcurement = this.contractProcurementsRepository.FindForUpdate(procurementId, vers);

            contractProcurement.ChangeStatusToDraft();

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{procurementId:int}/markAsChecked")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Contracts.Edit.Procurements.MarkAsChecked), IdParam = "contractId", ChildIdParam = "procurementId")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public void MarkAsChecked(int contractId, int procurementId, string version)
        {
            this.authorizer.AssertCanDo(ContractProcurementActions.MarkAsChecked, procurementId);

            this.contractProcurementService.ActivateProcurement(procurementId, Convert.FromBase64String(version));
        }

        [HttpPost]
        [Route("canCreate")]
        public ErrorsDO CanCreateProcurement(int contractId)
        {
            this.authorizer.AssertCanDo(ContractActions.Edit, contractId);

            var errorList = this.contractProcurementService.CanCreateProcurement(contractId);

            return new ErrorsDO(errorList);
        }
    }
}
