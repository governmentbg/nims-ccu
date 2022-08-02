using System.Collections.Generic;
using System.Web.Http;
using Eumis.ApplicationServices.Services.ContractReportIndicator;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.ContractReportIndicators.Repositories;
using Eumis.Data.ContractReportIndicators.ViewObjects;
using Eumis.Data.ContractReports.Repositories;
using Eumis.Data.ContractReportTechnicalCorrections.Repositories;
using Eumis.Data.Contracts.Repositories;
using Eumis.Data.Core.Relations;
using Eumis.Data.Indicators.Repositories;
using Eumis.Data.Users.Repositories;
using Eumis.Domain.Contracts.DataObjects;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.ContractReportIndicators.Controllers
{
    [RoutePrefix("api/contractReports/{contractReportId:int}/indicators")]
    public class ContractReportIndicatorsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IAccessContext accessContext;
        private IUsersRepository usersRepository;
        private IContractsRepository contractsRepository;
        private IIndicatorsRepository indicatorsRepository;
        private IContractReportIndicatorsRepository contractReportIndicatorsRepository;
        private IContractReportTechnicalCorrectionIndicatorsRepository contractReportTechnicalCorrectionIndicatorsRepository;
        private IContractReportIndicatorService contractReportIndicatorService;
        private IRelationsRepository relationsRepository;
        private IContractReportTechnicalsRepository contractReportTechnicalsRepository;

        public ContractReportIndicatorsController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IAccessContext accessContext,
            IUsersRepository usersRepository,
            IContractsRepository contractsRepository,
            IIndicatorsRepository indicatorsRepository,
            IContractReportIndicatorsRepository contractReportIndicatorsRepository,
            IContractReportTechnicalCorrectionIndicatorsRepository contractReportTechnicalCorrectionIndicatorsRepository,
            IContractReportIndicatorService contractReportIndicatorService,
            IRelationsRepository relationsRepository,
            IContractReportTechnicalsRepository contractReportTechnicalsRepository)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.accessContext = accessContext;
            this.usersRepository = usersRepository;
            this.contractsRepository = contractsRepository;
            this.indicatorsRepository = indicatorsRepository;
            this.contractReportIndicatorsRepository = contractReportIndicatorsRepository;
            this.contractReportTechnicalCorrectionIndicatorsRepository = contractReportTechnicalCorrectionIndicatorsRepository;
            this.contractReportIndicatorService = contractReportIndicatorService;
            this.relationsRepository = relationsRepository;
            this.contractReportTechnicalsRepository = contractReportTechnicalsRepository;
        }

        [Route("")]
        public IList<ContractReportIndicatorsVO> GetContractReportIndicators(int contractReportId)
        {
            this.authorizer.AssertCanDo(ContractReportCheckActions.View, contractReportId);

            return this.contractReportIndicatorsRepository.GetContractReportIndicators(contractReportId);
        }

        [Route("~/api/projectDossier/{projectId:int}/contract/{contractId:int}/physicalExecutionIndicators")]
        public IList<ContractPhysicalExecutionIndicatorVO> GetContractPhysicalExecutionIndicatorsForProjectDossier(int projectId, int contractId)
        {
            this.authorizer.AssertCanDo(ProjectDossierActions.View, projectId);
            this.relationsRepository.AssertProjectHasContract(projectId, contractId);

            return this.contractReportIndicatorsRepository.GetContractPhysicalExecutionIndicatorsForProjectDossier(contractId);
        }

        [Route("~/api/contractReportTechnicalCorrections/{contractReportTechnicalCorrectionId:int}/contractReportIndicators")]
        public IList<ContractReportIndicatorsVO> GetContractReportIndicatorsForContractReportTechnicalCorrection(int contractReportTechnicalCorrectionId, int contractReportId)
        {
            this.authorizer.AssertCanDo(ContractReportTechnicalCorrectionActions.View, contractReportTechnicalCorrectionId);

            return this.contractReportIndicatorsRepository.GetContractReportIndicatorsForContractReportTechnicalCorrection(contractReportId, contractReportTechnicalCorrectionId);
        }

        [Route("{contractReportIndicatorId:int}")]
        public ContractReportIndicatorDO GetContractReportIndicator(int contractReportId, int contractReportIndicatorId)
        {
            this.authorizer.AssertCanDo(ContractReportCheckActions.View, contractReportId);

            var contractReportIndicator = this.contractReportIndicatorsRepository.Find(contractReportIndicatorId);

            var contractIndicator = this.contractsRepository.GetContractIndicator(contractReportIndicator.ContractId, contractReportIndicator.ContractIndicatorId);

            var indicator = this.indicatorsRepository.Find(contractIndicator.IndicatorId);

            var contractReportTechnicalCorrectionIndicator = this.contractReportTechnicalCorrectionIndicatorsRepository.FindActualContractReportTechnicalCorrectionIndicator(contractReportIndicatorId);

            string checkedByUser = string.Empty;

            if (contractReportIndicator.CheckedByUserId.HasValue)
            {
                var user = this.usersRepository.Find(contractReportIndicator.CheckedByUserId.Value);
                checkedByUser = string.Format("{0} ({1})", user.Fullname, user.Username);
            }

            var contractReportTechnicalStatus = this.contractReportTechnicalsRepository.FindWithoutIncludes(contractReportIndicator.ContractReportTechnicalId).Status;

            return new ContractReportIndicatorDO(contractReportIndicator, contractIndicator, indicator, checkedByUser, contractReportTechnicalCorrectionIndicator, contractReportTechnicalStatus);
        }

        [Route("~/api/contractReportTechnicalCorrections/{contractReportTechnicalCorrectionId:int}/contractReportIndicators/{contractReportIndicatorId:int}")]
        public ContractReportIndicatorDO GetContractReportIndicatorForContractReportTechnicalCorrection(int contractReportTechnicalCorrectionId, int contractReportIndicatorId)
        {
            this.authorizer.AssertCanDo(ContractReportTechnicalCorrectionActions.View, contractReportTechnicalCorrectionId);
            this.relationsRepository.AssertContractReportTechnicalCorrectionHasContractReportIndicator(contractReportTechnicalCorrectionId, contractReportIndicatorId);

            var contractReportIndicator = this.contractReportIndicatorsRepository.Find(contractReportIndicatorId);

            var contractIndicator = this.contractsRepository.GetContractIndicator(contractReportIndicator.ContractId, contractReportIndicator.ContractIndicatorId);

            var indicator = this.indicatorsRepository.Find(contractIndicator.IndicatorId);

            var contractReportTechnicalCorrectionIndicator = this.contractReportTechnicalCorrectionIndicatorsRepository.FindActualContractReportTechnicalCorrectionIndicator(contractReportIndicatorId);

            string checkedByUser = string.Empty;

            if (contractReportIndicator.CheckedByUserId.HasValue)
            {
                var user = this.usersRepository.Find(contractReportIndicator.CheckedByUserId.Value);
                checkedByUser = string.Format("{0} ({1})", user.Fullname, user.Username);
            }

            return new ContractReportIndicatorDO(contractReportIndicator, contractIndicator, indicator, checkedByUser, contractReportTechnicalCorrectionIndicator);
        }

        [HttpPut]
        [Route("{contractReportIndicatorId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReports.Edit.ContractReportIndicator.Update), IdParam = "contractReportIndicatorId")]
        public void UpdateContractReportIndicator(int contractReportId, int contractReportIndicatorId, ContractReportIndicatorDO contractReportIndicator)
        {
            this.authorizer.AssertCanDo(ContractReportCheckActions.EditTechnical, contractReportId);

            this.contractReportIndicatorService.UpdateContractReportIndicator(
                contractReportIndicatorId,
                contractReportIndicator.Version,
                contractReportIndicator.Approval,
                contractReportIndicator.Notes,
                contractReportIndicator.ApprovedPeriodAmountMen,
                contractReportIndicator.ApprovedPeriodAmountWomen,
                contractReportIndicator.ApprovedPeriodAmountTotal,
                contractReportIndicator.ApprovedCumulativeAmountMen,
                contractReportIndicator.ApprovedCumulativeAmountWomen,
                contractReportIndicator.ApprovedCumulativeAmountTotal,
                contractReportIndicator.ApprovedResidueAmountMen,
                contractReportIndicator.ApprovedResidueAmountWomen,
                contractReportIndicator.ApprovedResidueAmountTotal,
                contractReportIndicator.LastReportCumulativeAmountMen,
                contractReportIndicator.LastReportCumulativeAmountWomen,
                contractReportIndicator.LastReportCumulativeAmountTotal);
        }

        [HttpPost]
        [Route("{contractReportIndicatorId:int}/changeStatusToEnded")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReports.Edit.ContractReportIndicator.ChangeStatusToEnded), IdParam = "contractReportIndicatorId")]
        public void ChangeContractReportIndicatorStatusToEnded(int contractReportId, int contractReportIndicatorId, string version)
        {
            this.authorizer.AssertCanDo(ContractReportCheckActions.EditTechnical, contractReportId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.contractReportIndicatorService.ChangeContractReportIndicatorStatus(contractReportIndicatorId, vers, Domain.Contracts.ContractReportIndicatorStatus.Ended);
        }

        [HttpPost]
        [Route("{contractReportIndicatorId:int}/canChangeStatusToEnded")]
        public ErrorsDO CanChangeContractReportIndicatorStatusToEnded(int contractReportId, int contractReportIndicatorId)
        {
            this.authorizer.AssertCanDo(ContractReportCheckActions.EditTechnical, contractReportId);

            var errors = this.contractReportIndicatorService.CanChangeContractReportIndicatorStatusToEnded(contractReportIndicatorId);

            return new ErrorsDO(errors);
        }

        [HttpPost]
        [Route("{contractReportIndicatorId:int}/changeStatusToDraft")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReports.Edit.ContractReportIndicator.ChangeStatusToDraft), IdParam = "contractReportIndicatorId")]
        public void ChangeContractReportIndicatorStatusToDraft(int contractReportId, int contractReportIndicatorId, string version)
        {
            this.authorizer.AssertCanDo(ContractReportCheckActions.EditTechnical, contractReportId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.contractReportIndicatorService.ChangeContractReportIndicatorStatus(contractReportIndicatorId, vers, Domain.Contracts.ContractReportIndicatorStatus.Draft);
        }
    }
}
