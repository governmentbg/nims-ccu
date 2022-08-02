using System.Collections.Generic;
using System.Web.Http;
using Eumis.ApplicationServices.Services.ContractReportTechnicalCorrection;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Db;
using Eumis.Data.ContractReportIndicators.Repositories;
using Eumis.Data.ContractReports.Repositories;
using Eumis.Data.ContractReportTechnicalCorrections.Repositories;
using Eumis.Data.Contracts.Repositories;
using Eumis.Data.Indicators.Repositories;
using Eumis.Data.Users.Repositories;
using Eumis.Domain.Contracts;
using Eumis.Domain.Contracts.DataObjects;
using Eumis.Domain.Contracts.ViewObjects;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.ContractReportTechnicalCorrections.Controllers
{
    [RoutePrefix("api/contractReportTechnicalCorrections/{contractReportTechnicalCorrectionId:int}/technicalCorrectionIndicators")]
    public class ContractReportTechnicalCorrectionIndicatorsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IUsersRepository usersRepository;
        private IIndicatorsRepository indicatorsRepository;
        private IContractsRepository contractsRepository;
        private IContractReportsRepository contractReportsRepository;
        private IContractReportIndicatorsRepository contractReportIndicatorsRepository;
        private IContractReportTechnicalCorrectionsRepository contractReportTechnicalCorrectionsRepository;
        private IContractReportTechnicalCorrectionIndicatorsRepository contractReportTechnicalCorrectionIndicatorsRepository;
        private IContractReportTechnicalCorrectionService contractReportTechnicalCorrectionService;

        public ContractReportTechnicalCorrectionIndicatorsController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IUsersRepository usersRepository,
            IIndicatorsRepository indicatorsRepository,
            IContractsRepository contractsRepository,
            IContractReportsRepository contractReportsRepository,
            IContractReportIndicatorsRepository contractReportIndicatorsRepository,
            IContractReportTechnicalCorrectionsRepository contractReportTechnicalCorrectionsRepository,
            IContractReportTechnicalCorrectionIndicatorsRepository contractReportTechnicalCorrectionIndicatorsRepository,
            IContractReportTechnicalCorrectionService contractReportTechnicalCorrectionService)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.usersRepository = usersRepository;
            this.indicatorsRepository = indicatorsRepository;
            this.contractsRepository = contractsRepository;
            this.contractReportsRepository = contractReportsRepository;
            this.contractReportIndicatorsRepository = contractReportIndicatorsRepository;
            this.contractReportTechnicalCorrectionsRepository = contractReportTechnicalCorrectionsRepository;
            this.contractReportTechnicalCorrectionIndicatorsRepository = contractReportTechnicalCorrectionIndicatorsRepository;
            this.contractReportTechnicalCorrectionService = contractReportTechnicalCorrectionService;
        }

        [Route("")]
        public IList<ContractReportTechnicalCorrectionIndicatorVO> GetContractReportTechnicalCorrectionIndicators(int contractReportTechnicalCorrectionId)
        {
            this.authorizer.AssertCanDo(ContractReportTechnicalCorrectionActions.View, contractReportTechnicalCorrectionId);

            return this.contractReportTechnicalCorrectionIndicatorsRepository.GetContractReportTechnicalCorrectionIndicators(contractReportTechnicalCorrectionId);
        }

        [Route("{contractReportTechnicalCorrectionIndicatorId:int}")]
        public ContractReportTechnicalCorrectionIndicatorDO GetContractReportTechnicalCorrectionIndicator(int contractReportTechnicalCorrectionId, int contractReportTechnicalCorrectionIndicatorId)
        {
            this.authorizer.AssertCanDo(ContractReportTechnicalCorrectionActions.View, contractReportTechnicalCorrectionId);

            var technicalCorrectionIndicator = this.contractReportTechnicalCorrectionIndicatorsRepository.Find(contractReportTechnicalCorrectionIndicatorId);

            var previousContractReports = this.contractReportsRepository.GetPreviousContractReport(technicalCorrectionIndicator.ContractReportId);

            var existsCorrectionForPreviousContractReport = false;

            foreach (var previousContractReport in previousContractReports)
            {
                existsCorrectionForPreviousContractReport = this.contractReportTechnicalCorrectionsRepository.ExistsCorrectionForContractReport(previousContractReport.ContractReportId);

                if (existsCorrectionForPreviousContractReport)
                {
                    break;
                }
            }

            var contractReportIndicator = this.contractReportIndicatorsRepository.Find(technicalCorrectionIndicator.ContractReportIndicatorId);

            var contractIndicator = this.contractsRepository.GetContractIndicator(contractReportIndicator.ContractId, contractReportIndicator.ContractIndicatorId);

            var indicator = this.indicatorsRepository.Find(contractIndicator.IndicatorId);

            string checkedByUser = string.Empty;
            string indicatorCheckedByUser = string.Empty;

            if (technicalCorrectionIndicator.CheckedByUserId.HasValue)
            {
                var user = this.usersRepository.Find(technicalCorrectionIndicator.CheckedByUserId.Value);
                checkedByUser = $"{user.Fullname} ({user.Username})";
            }

            if (contractReportIndicator.CheckedByUserId.HasValue)
            {
                var user = this.usersRepository.Find(contractReportIndicator.CheckedByUserId.Value);
                indicatorCheckedByUser = $"{user.Fullname} ({user.Username})";
            }

            return new ContractReportTechnicalCorrectionIndicatorDO(
                technicalCorrectionIndicator,
                contractReportIndicator,
                contractIndicator,
                indicator,
                checkedByUser,
                indicatorCheckedByUser,
                existsCorrectionForPreviousContractReport);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportTechnicalCorrection.Edit.Indicator.Create), IdParam = "contractReportTechnicalCorrectionId")]
        public void CreateContractReportTechnicalCorrectionIndicator(int contractReportTechnicalCorrectionId, int contractReportIndicatorId)
        {
            this.authorizer.AssertCanDo(ContractReportTechnicalCorrectionActions.Edit, contractReportTechnicalCorrectionId);

            this.contractReportTechnicalCorrectionService.CreateContractReportTechnicalCorrectionIndicator(contractReportTechnicalCorrectionId, contractReportIndicatorId);
        }

        [HttpDelete]
        [Route("{contractReportTechnicalCorrectionIndicatorId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportTechnicalCorrection.Edit.Indicator.Delete), IdParam = "contractReportTechnicalCorrectionIndicatorId")]
        public void DeleteContractReportTechnicalCorrectionIndicator(int contractReportTechnicalCorrectionId, int contractReportTechnicalCorrectionIndicatorId, string version)
        {
            this.authorizer.AssertCanDo(ContractReportTechnicalCorrectionActions.Edit, contractReportTechnicalCorrectionId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.contractReportTechnicalCorrectionService.DeleteContractReportTechnicalCorrectionIndicator(contractReportTechnicalCorrectionIndicatorId, vers);
        }

        [HttpPut]
        [Route("{contractReportTechnicalCorrectionIndicatorId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportTechnicalCorrection.Edit.Indicator.Update), IdParam = "contractReportTechnicalCorrectionIndicatorId")]
        public void UpdateContractReportTechnicalIndicator(int contractReportTechnicalCorrectionId, int contractReportTechnicalCorrectionIndicatorId, ContractReportTechnicalCorrectionIndicatorDO contractReportTechnicalCorrectionIndicator)
        {
            this.authorizer.AssertCanDo(ContractReportTechnicalCorrectionActions.Edit, contractReportTechnicalCorrectionId);

            this.contractReportTechnicalCorrectionService.UpdateContractReportTechnicalCorrectionIndicator(
                contractReportTechnicalCorrectionIndicatorId,
                contractReportTechnicalCorrectionIndicator.Version,
                contractReportTechnicalCorrectionIndicator.Notes,
                contractReportTechnicalCorrectionIndicator.CorrectedApprovedPeriodAmountMen,
                contractReportTechnicalCorrectionIndicator.CorrectedApprovedPeriodAmountWomen,
                contractReportTechnicalCorrectionIndicator.CorrectedApprovedPeriodAmountTotal.Value,
                contractReportTechnicalCorrectionIndicator.CorrectedApprovedCumulativeAmountMen,
                contractReportTechnicalCorrectionIndicator.CorrectedApprovedCumulativeAmountWomen,
                contractReportTechnicalCorrectionIndicator.CorrectedApprovedCumulativeAmountTotal.Value,
                contractReportTechnicalCorrectionIndicator.CorrectedApprovedResidueAmountMen,
                contractReportTechnicalCorrectionIndicator.CorrectedApprovedResidueAmountWomen,
                contractReportTechnicalCorrectionIndicator.CorrectedApprovedResidueAmountTotal.Value);
        }

        [HttpPost]
        [Route("{contractReportTechnicalCorrectionIndicatorId:int}/changeStatusToEnded")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportTechnicalCorrection.Edit.Indicator.ChangeStatusToEnded), IdParam = "contractReportTechnicalCorrectionIndicatorId")]
        public void ChangeContractReportTechnicalCorrectionIndicatorStatusToEnded(int contractReportTechnicalCorrectionId, int contractReportTechnicalCorrectionIndicatorId, string version)
        {
            this.authorizer.AssertCanDo(ContractReportTechnicalCorrectionActions.Edit, contractReportTechnicalCorrectionId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.contractReportTechnicalCorrectionService.ChangeContractReportTechnicalCorrectionIndicatorStatus(contractReportTechnicalCorrectionIndicatorId, vers, ContractReportTechnicalCorrectionIndicatorStatus.Ended);
        }

        [HttpPost]
        [Route("{contractReportTechnicalCorrectionIndicatorId:int}/canChangeStatusToEnded")]
        public ErrorsDO CanChangeContractReportTechnicalCorrectionIndicatorStatusToEnded(int contractReportTechnicalCorrectionId, int contractReportTechnicalCorrectionIndicatorId)
        {
            this.authorizer.AssertCanDo(ContractReportTechnicalCorrectionActions.Edit, contractReportTechnicalCorrectionId);

            var errors = this.contractReportTechnicalCorrectionService.CanChangeContractReportTechnicalCorrectionIndicatorStatusToEnded(contractReportTechnicalCorrectionIndicatorId);

            return new ErrorsDO(errors);
        }

        [HttpPost]
        [Route("{contractReportTechnicalCorrectionIndicatorId:int}/changeStatusToDraft")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ContractReportTechnicalCorrection.Edit.Indicator.ChangeStatusToDraft), IdParam = "contractReportTechnicalCorrectionIndicatorId")]
        public void ChangeContractReportTechnicalCorrectionIndicatorStatusToDraft(int contractReportTechnicalCorrectionId, int contractReportTechnicalCorrectionIndicatorId, string version)
        {
            this.authorizer.AssertCanDo(ContractReportTechnicalCorrectionActions.Edit, contractReportTechnicalCorrectionId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.contractReportTechnicalCorrectionService.ChangeContractReportTechnicalCorrectionIndicatorStatus(contractReportTechnicalCorrectionIndicatorId, vers, ContractReportTechnicalCorrectionIndicatorStatus.Draft);
        }
    }
}
