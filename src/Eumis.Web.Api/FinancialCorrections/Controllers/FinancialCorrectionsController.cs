using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Eumis.ApplicationServices.Services.FinancialCorrection;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.Contracts.Repositories;
using Eumis.Data.Core.Permissions;
using Eumis.Data.Core.Relations;
using Eumis.Data.FinancialCorrections.Repositories;
using Eumis.Data.FinancialCorrections.ViewObjects;
using Eumis.Data.Users.Repositories;
using Eumis.Domain.MonitoringFinancialControl;
using Eumis.Domain.Users.ProgrammePermissions;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.Core;
using Eumis.Web.Api.FinancialCorrections.DataObjects;

namespace Eumis.Web.Api.FinancialCorrections.Controllers
{
    [RoutePrefix("api/financialCorrections")]
    public class FinancialCorrectionsController : ApiController
    {
        private IRelationsRepository relationsRepository;
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IAccessContext accessContext;
        private IPermissionsRepository permissionsRepository;
        private IFinancialCorrectionService financialCorrectionService;
        private IFinancialCorrectionsRepository financialCorrectionsRepository;
        private IContractVersionsRepository contractVersionsRepository;
        private IUsersRepository usersRepository;
        private IContractsRepository contractsRepository;

        public FinancialCorrectionsController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IAccessContext accessContext,
            IPermissionsRepository permissionsRepository,
            IFinancialCorrectionService financialCorrectionService,
            IFinancialCorrectionsRepository financialCorrectionsRepository,
            IContractVersionsRepository contractVersionsRepository,
            IUsersRepository usersRepository,
            IContractsRepository contractsRepository,
            IRelationsRepository relationsRepository)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.accessContext = accessContext;
            this.permissionsRepository = permissionsRepository;
            this.financialCorrectionService = financialCorrectionService;
            this.financialCorrectionsRepository = financialCorrectionsRepository;
            this.contractVersionsRepository = contractVersionsRepository;
            this.usersRepository = usersRepository;
            this.contractsRepository = contractsRepository;
            this.relationsRepository = relationsRepository;
        }

        [Route("")]
        public IList<FinancialCorrectionVO> GetFinancialCorrections()
        {
            this.authorizer.AssertCanDo(FinancialCorrectionListActions.Search);

            var programmeIds = this.permissionsRepository.GetProgrammeIdsByPermission(this.accessContext.UserId, MonitoringFinancialControlPermissions.CanRead);

            return this.financialCorrectionsRepository.GetFinancialCorrections(programmeIds, this.accessContext.UserId);
        }

        [Route("~/api/projectDossier/{projectId:int}/contract/{contractId:int}/financialCorrections")]
        public IList<FinancialCorrectionVO> GetFinancialCorrectionsForProjectDossier(int projectId, int contractId)
        {
            this.authorizer.AssertCanDo(ProjectDossierActions.View, projectId);
            this.relationsRepository.AssertProjectHasContract(projectId, contractId);

            return this.financialCorrectionsRepository.GetFinancialCorrectionsForProjectDossier(contractId);
        }

        [Route("~/api/contracts/{contractId:int}/financialCorrections")]
        public IList<FinancialCorrectionVO> GetFinancialCorrectionsForProjectDossier(int contractId)
        {
            this.authorizer.AssertCanDo(ContractActions.View, contractId);

            return this.financialCorrectionsRepository.GetFinancialCorrectionsForProjectDossier(contractId);
        }

        [Route("{financialCorrectionId:int}")]
        public FinancialCorrectionDO GetFinancialCorrection(int financialCorrectionId)
        {
            this.authorizer.AssertCanDo(FinancialCorrectionActions.View, financialCorrectionId);

            var financialCorrection = this.financialCorrectionsRepository.Find(financialCorrectionId);

            var contract = this.contractsRepository.Find(financialCorrection.ContractId);

            return new FinancialCorrectionDO(financialCorrection, contract);
        }

        [Route("{financialCorrectionId:int}/info")]
        public FinancialCorrectionInfoVO GetFinancialCorrectionInfo(int financialCorrectionId)
        {
            this.authorizer.AssertCanDo(FinancialCorrectionActions.View, financialCorrectionId);

            return this.financialCorrectionsRepository.GetInfo(financialCorrectionId);
        }

        [HttpGet]
        [Route("new")]
        public NewFinancialCorrectionDO NewFinancialCorrection(string contractNum)
        {
            this.authorizer.AssertCanDo(FinancialCorrectionListActions.Create);

            var contract = this.contractsRepository.FindByRegNumber(contractNum);
            return new NewFinancialCorrectionDO(contract);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.FinancialCorrections.Create))]
        public object CreateFinancialCorrection(NewFinancialCorrectionDO newFinancialCorrection)
        {
            this.authorizer.AssertCanDo(FinancialCorrectionListActions.Create);

            var financialCorrection = this.financialCorrectionService.CreateFinancialCorrection(
                newFinancialCorrection.ImpositionDate.Value,
                newFinancialCorrection.Contract.ContractId,
                newFinancialCorrection.ContractContractId,
                newFinancialCorrection.ContractBudgetLevel3AmountId,
                this.accessContext.UserId);

            return new { FinancialCorrectionId = financialCorrection.FinancialCorrectionId };
        }

        [HttpPost]
        [Route("canCreate")]
        public object CanCreateFinancialCorrection(string contractRegNumber)
        {
            this.authorizer.AssertCanDo(FinancialCorrectionListActions.Create);

            var errorList = this.financialCorrectionService.CanCreate(contractRegNumber, this.accessContext.UserId);

            return new ErrorsDO(errorList);
        }

        [HttpPost]
        [Route("{financialCorrectionId:int}/cancel")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.FinancialCorrections.Edit.Cancel))]
        public void CancelFinancialCorrection(int financialCorrectionId, string version, ConfirmDO confirm)
        {
            this.authorizer.AssertCanDo(FinancialCorrectionActions.Edit, financialCorrectionId);

            byte[] vers = System.Convert.FromBase64String(version);

            var financialCorrection = this.financialCorrectionsRepository.FindForUpdate(financialCorrectionId, vers);
            financialCorrection.CancelFinancialCorrection(confirm.Note);

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{financialCorrectionId:int}/changeStatusToEntered")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.FinancialCorrections.Edit.ChangeStatusToEntered))]
        public void ActivateFinancialCorrection(int financialCorrectionId, string version)
        {
            this.authorizer.AssertCanDo(FinancialCorrectionActions.Edit, financialCorrectionId);

            byte[] vers = System.Convert.FromBase64String(version);

            var financialCorrection = this.financialCorrectionsRepository.FindForUpdate(financialCorrectionId, vers);
            financialCorrection.ChangeStatusToEntered();

            this.unitOfWork.Save();
        }

        [HttpDelete]
        [Route("{financialCorrectionId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.FinancialCorrections.Edit.Delete))]
        public void DeleteFinancialCorrection(int financialCorrectionId, string version)
        {
            this.authorizer.AssertCanDo(FinancialCorrectionActions.Edit, financialCorrectionId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.financialCorrectionService.DeleteFinancialCorrection(financialCorrectionId, vers);
        }

        [HttpPut]
        [Route("{financialCorrectionId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.FinancialCorrections.Edit.BasicData), IdParam = "financialCorrectionId")]
        public void UpdateFinancialCorrection(int financialCorrectionId, FinancialCorrectionDO financialCorrectionDO)
        {
            this.authorizer.AssertCanDo(FinancialCorrectionActions.Edit, financialCorrectionId);

            var financialCorrection = this.financialCorrectionsRepository.FindForUpdate(financialCorrectionId, financialCorrectionDO.Version);
            financialCorrection.UpdateAttributes(financialCorrectionDO.ImpositionDate.Value);

            this.unitOfWork.Save();
        }
    }
}
