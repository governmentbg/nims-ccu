using System;
using System.Collections.Generic;
using System.Web.Http;
using Eumis.ApplicationServices.Services.ContractReportTechnicalCorrection;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.ContractReports.Repositories;
using Eumis.Data.ContractReportTechnicalCorrections.Repositories;
using Eumis.Data.Contracts.Repositories;
using Eumis.Data.Core.Permissions;
using Eumis.Data.Core.Relations;
using Eumis.Data.Users.Repositories;
using Eumis.Domain.Contracts.DataObjects;
using Eumis.Domain.Contracts.ViewObjects;
using Eumis.Domain.Users.ProgrammePermissions;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.ContractReportTechnicalCorrections.Controllers
{
    [RoutePrefix("api/contractReportTechnicalCorrections")]
    public class ContractReportTechnicalCorrectionsController : ApiController
    {
        private IRelationsRepository relationsRepository;
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IAccessContext accessContext;
        private IContractsRepository contractsRepository;
        private IContractReportsRepository contractReportsRepository;
        private IContractReportTechnicalCorrectionsRepository contractReportTechnicalCorrectionsRepository;
        private IContractReportTechnicalsRepository contractReportTechnicalsRepository;
        private IUsersRepository usersRepository;
        private IPermissionsRepository permissionsRepository;
        private IContractReportTechnicalCorrectionService contractReportTechnicalCorrectionService;

        public ContractReportTechnicalCorrectionsController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IAccessContext accessContext,
            IContractsRepository contractsRepository,
            IContractReportsRepository contractReportsRepository,
            IContractReportTechnicalCorrectionsRepository contractReportTechnicalCorrectionsRepository,
            IContractReportTechnicalsRepository contractReportTechnicalsRepository,
            IUsersRepository usersRepository,
            IPermissionsRepository permissionsRepository,
            IContractReportTechnicalCorrectionService contractReportTechnicalCorrectionService,
            IRelationsRepository relationsRepository)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.accessContext = accessContext;
            this.contractsRepository = contractsRepository;
            this.contractReportsRepository = contractReportsRepository;
            this.contractReportTechnicalCorrectionsRepository = contractReportTechnicalCorrectionsRepository;
            this.contractReportTechnicalsRepository = contractReportTechnicalsRepository;
            this.usersRepository = usersRepository;
            this.permissionsRepository = permissionsRepository;
            this.contractReportTechnicalCorrectionService = contractReportTechnicalCorrectionService;
            this.relationsRepository = relationsRepository;
        }

        [Route("")]
        public IList<ContractReportTechnicalCorrectionVO> GetContractReportTechnicalCorrections(string contractRegNum = null, DateTime? fromDate = null, DateTime? toDate = null)
        {
            this.authorizer.AssertCanDo(ContractReportTechnicalCorrectionListActions.Search);

            var programmeIds = this.permissionsRepository.GetProgrammeIdsByPermission(this.accessContext.UserId, MonitoringFinancialControlPermissions.CanRead);

            return this.contractReportTechnicalCorrectionsRepository.GetContractReportTechnicalCorrections(programmeIds, this.accessContext.UserId, contractRegNum, fromDate, toDate);
        }

        [Route("~/api/projectDossier/{projectId:int}/contract/{contractId:int}/reportTechnicalCorrection")]
        public IList<ContractReportTechnicalCorrectionVO> GetContractReportTechnicalCorrectionsForProjectDossier(int projectId, int contractId)
        {
            this.authorizer.AssertCanDo(ProjectDossierActions.View, projectId);
            this.relationsRepository.AssertProjectHasContract(projectId, contractId);

            return this.contractReportTechnicalCorrectionsRepository.GetContractReportTechnicalCorrectionsForProjectDossier(contractId);
        }

        [Route("{contractReportTechnicalCorrectionId:int}")]
        public ContractReportTechnicalCorrectionDO GetContractReportTechnicalCorrection(int contractReportTechnicalCorrectionId)
        {
            this.authorizer.AssertCanDo(ContractReportTechnicalCorrectionActions.View, contractReportTechnicalCorrectionId);

            var technicalCorrection = this.contractReportTechnicalCorrectionsRepository.Find(contractReportTechnicalCorrectionId);

            var technical = this.contractReportTechnicalsRepository.Find(technicalCorrection.ContractReportTechnicalId);

            string checkedByUser = string.Empty;

            if (technicalCorrection.CheckedByUserId.HasValue)
            {
                var user = this.usersRepository.Find(technicalCorrection.CheckedByUserId.Value);
                checkedByUser = $"{user.Fullname} ({user.Username})";
            }

            return new ContractReportTechnicalCorrectionDO(technicalCorrection, technical, checkedByUser);
        }

        [Route("{contractReportTechnicalCorrectionId:int}/info")]
        public ContractReportTechnicalCorrectionInfoDO GetContractReportTechnicalCorrectionInfo(int contractReportTechnicalCorrectionId)
        {
            this.authorizer.AssertCanDo(ContractReportTechnicalCorrectionActions.View, contractReportTechnicalCorrectionId);

            var technicalCorrection = this.contractReportTechnicalCorrectionsRepository.Find(contractReportTechnicalCorrectionId);

            return new ContractReportTechnicalCorrectionInfoDO(technicalCorrection);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportTechnicalCorrection.Create))]
        public object CreateContractReportTechnicalCorrection(string contractNum, string contractReportNum)
        {
            this.authorizer.AssertCanDo(ContractReportTechnicalCorrectionListActions.Create);

            var newContractReportTechnicalCorrection = this.contractReportTechnicalCorrectionService.CreateContractReportTechnicalCorrection(contractNum, contractReportNum);

            return new { ContractReportTechnicalCorrectionId = newContractReportTechnicalCorrection.ContractReportTechnicalCorrectionId };
        }

        [HttpPut]
        [Route("{contractReportTechnicalCorrectionId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportTechnicalCorrection.Edit.BasicData), IdParam = "contractReportTechnicalCorrectionId")]
        public void UpdateContractReportTechnicalCorrection(int contractReportTechnicalCorrectionId, ContractReportTechnicalCorrectionDO contractReportTechnicalCorrection)
        {
            this.authorizer.AssertCanDo(ContractReportTechnicalCorrectionActions.Edit, contractReportTechnicalCorrectionId);

            this.contractReportTechnicalCorrectionService.UpdateContractReportTechnicalCorrection(
                contractReportTechnicalCorrection.ContractReportTechnicalCorrectionId,
                contractReportTechnicalCorrection.Version,
                contractReportTechnicalCorrection.CorrectionDate,
                contractReportTechnicalCorrection.File != null ? contractReportTechnicalCorrection.File.Key : (Guid?)null,
                contractReportTechnicalCorrection.Notes);
        }

        [HttpDelete]
        [Route("{contractReportTechnicalCorrectionId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportTechnicalCorrection.Delete), IdParam = "contractReportTechnicalCorrectionId")]
        public void DeleteContractReportTechnicalCorrection(int contractReportTechnicalCorrectionId, string version)
        {
            this.authorizer.AssertCanDo(ContractReportTechnicalCorrectionActions.Delete, contractReportTechnicalCorrectionId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.contractReportTechnicalCorrectionService.DeleteContractReportTechnicalCorrection(contractReportTechnicalCorrectionId, vers);
        }

        [HttpPost]
        [Route("canCreate")]
        public ErrorsDO CanCreateContractReportTechnicalCorrection(string contractNum, string contractReportNum)
        {
            this.authorizer.AssertCanDo(ContractReportTechnicalCorrectionListActions.Create);

            var errors = this.contractReportTechnicalCorrectionService.CanCreateContractReportTechnicalCorrection(contractNum, contractReportNum);

            return new ErrorsDO(errors);
        }

        [HttpPost]
        [Route("existsEnded")]
        public bool ExistsEndedContractReportTechnicalCorrectionForContractReport(string contractNum, string contractReportNum)
        {
            this.authorizer.AssertCanDo(ContractReportTechnicalCorrectionListActions.Create);

            var contract = this.contractsRepository.FindByRegNumber(contractNum);
            var contractReport = this.contractReportsRepository.FindByNum(contract.ContractId, contractReportNum);
            var existsEndedCorrectionForContractReport = this.contractReportTechnicalCorrectionsRepository.ExistsEndedCorrectionForContractReport(contractReport.ContractReportId);

            return existsEndedCorrectionForContractReport;
        }

        [HttpPost]
        [Route("{contractReportTechnicalCorrectionId:int}/canDelete")]
        public ErrorsDO CanDeleteContractReportTechnicalCorrection(int contractReportTechnicalCorrectionId)
        {
            this.authorizer.AssertCanDo(ContractReportTechnicalCorrectionActions.Edit, contractReportTechnicalCorrectionId);

            var errors = this.contractReportTechnicalCorrectionService.CanDeleteContractReportTechnicalCorrection(contractReportTechnicalCorrectionId);

            return new ErrorsDO(errors);
        }

        [HttpPost]
        [Route("{contractReportTechnicalCorrectionId:int}/changeStatusToEnded")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportTechnicalCorrection.ChangeStatusToEnded), IdParam = "contractReportTechnicalCorrectionId")]
        public void ChangeContractReportTechnicalCorrectionStatusToEnded(int contractReportTechnicalCorrectionId, string version)
        {
            this.authorizer.AssertCanDo(ContractReportTechnicalCorrectionActions.Edit, contractReportTechnicalCorrectionId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.contractReportTechnicalCorrectionService.ChangeContractReportTechnicalCorrectionStatus(contractReportTechnicalCorrectionId, vers, Domain.Contracts.ContractReportTechnicalCorrectionStatus.Ended);
        }

        [HttpPost]
        [Route("{contractReportTechnicalCorrectionId:int}/canChangeStatusToEnded")]
        public ErrorsDO CanChangeContractReportTechnicalCorrectionStatusToEnded(int contractReportTechnicalCorrectionId)
        {
            this.authorizer.AssertCanDo(ContractReportTechnicalCorrectionActions.Edit, contractReportTechnicalCorrectionId);

            var errors = this.contractReportTechnicalCorrectionService.CanChangeContractReportTechnicalCorrectionStatusToEnded(contractReportTechnicalCorrectionId);

            return new ErrorsDO(errors);
        }

        [HttpPost]
        [Route("{contractReportTechnicalCorrectionId:int}/changeStatusToDraft")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportTechnicalCorrection.ChangeStatusToDraft), IdParam = "contractReportTechnicalCorrectionId")]
        public void ChangeContractReportTechnicalCorrectionStatusToDraft(int contractReportTechnicalCorrectionId, string version)
        {
            this.authorizer.AssertCanDo(ContractReportTechnicalCorrectionActions.ChangeStatusToDraft, contractReportTechnicalCorrectionId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.contractReportTechnicalCorrectionService.ChangeContractReportTechnicalCorrectionStatus(contractReportTechnicalCorrectionId, vers, Domain.Contracts.ContractReportTechnicalCorrectionStatus.Draft);
        }
    }
}
