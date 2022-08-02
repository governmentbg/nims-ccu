using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Common.Json;
using Eumis.Data.ContractReportIndicators.Repositories;
using Eumis.Data.ContractReports.Repositories;
using Eumis.Data.Contracts.Repositories;
using Eumis.Domain;
using Eumis.Domain.Contracts;
using Eumis.Domain.Indicators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Eumis.ApplicationServices.Services.ContractReportIndicator
{
    public class ContractReportIndicatorService : IContractReportIndicatorService
    {
        private IUnitOfWork unitOfWork;
        private IAccessContext accessContext;
        private IContractsRepository contractsRepository;
        private IContractReportsRepository contractReportsRepository;
        private IContractProcurementsRepository contractProcurementsRepository;
        private IContractReportIndicatorsRepository contractReportIndicatorsRepository;
        private IContractReportTechnicalsRepository contractReportTechnicalsRepository;

        public ContractReportIndicatorService(
            IUnitOfWork unitOfWork,
            IAccessContext accessContext,
            IContractsRepository contractsRepository,
            IContractReportsRepository contractReportsRepository,
            IContractProcurementsRepository contractProcurementsRepository,
            IContractReportIndicatorsRepository contractReportIndicatorsRepository,
            IContractReportTechnicalsRepository contractReportTechnicalsRepository)
        {
            this.unitOfWork = unitOfWork;
            this.accessContext = accessContext;
            this.contractsRepository = contractsRepository;
            this.contractReportsRepository = contractReportsRepository;
            this.contractProcurementsRepository = contractProcurementsRepository;
            this.contractReportIndicatorsRepository = contractReportIndicatorsRepository;
            this.contractReportTechnicalsRepository = contractReportTechnicalsRepository;
        }

        public void CreateContractReportIndicators(ContractReportTechnical technical)
        {
            var contract = this.contractsRepository.Find(technical.ContractId);

            var contractIndicators = contract.ContractIndicators.ToDictionary(g => g.Gid.ToString());

            var indicatorsCollection = technical.GetDocument().Indicators.IndicatorCollection.Where(cri => cri.BFPContractIndicator.IsLocked == false);
            IList<Eumis.Domain.Contracts.ContractReportIndicator> newContractReportIndicators = new List<Eumis.Domain.Contracts.ContractReportIndicator>();

            foreach (var indicator in indicatorsCollection)
            {
                var contractIndicator = contractIndicators[indicator.BFPContractIndicator.gid];

                var newContractReportIndicator = new Eumis.Domain.Contracts.ContractReportIndicator(
                    technical.ContractReportTechnicalId,
                    contractIndicator,
                    technical.ContractReportId,
                    technical.ContractId,
                    indicator.BFPContractIndicator.SelectedIndicator.Name,
                    indicator.BFPContractIndicator.SelectedIndicator.HasGenderDivision,
                    indicator.BFPContractIndicator.SelectedIndicator.MeasureName,
                    indicator.PeriodAmountMen,
                    indicator.PeriodAmountWomen,
                    indicator.PeriodAmountTotal,
                    indicator.CumulativeAmountMen,
                    indicator.CumulativeAmountWomen,
                    indicator.CumulativeAmountTotal,
                    indicator.ResidueAmountMen,
                    indicator.ResidueAmountWomen,
                    indicator.ResidueAmountTotal,
                    indicator.LastReportCumulativeAmountMen,
                    indicator.LastReportCumulativeAmountWomen,
                    indicator.LastReportCumulativeAmountTotal,
                    indicator.Comment);

                newContractReportIndicators.Add(newContractReportIndicator);
            }

            this.unitOfWork.BulkInsert<Eumis.Domain.Contracts.ContractReportIndicator>(newContractReportIndicators);
        }

        public async Task CreateContractReportIndicatorsAsync(ContractReportTechnical technical, CancellationToken ct)
        {
            var contract = await this.contractsRepository.FindAsync(technical.ContractId, ct);

            var contractIndicators = contract.ContractIndicators.ToDictionary(g => g.Gid.ToString());

            var indicatorsCollection = technical.GetDocument().Indicators.IndicatorCollection.Where(cri => cri.BFPContractIndicator.IsLocked == false);
            IList<Eumis.Domain.Contracts.ContractReportIndicator> newContractReportIndicators = new List<Eumis.Domain.Contracts.ContractReportIndicator>();

            foreach (var indicator in indicatorsCollection)
            {
                var contractIndicator = contractIndicators[indicator.BFPContractIndicator.gid];

                var newContractReportIndicator = new Eumis.Domain.Contracts.ContractReportIndicator(
                    technical.ContractReportTechnicalId,
                    contractIndicator,
                    technical.ContractReportId,
                    technical.ContractId,
                    indicator.BFPContractIndicator.SelectedIndicator.Name,
                    indicator.BFPContractIndicator.SelectedIndicator.HasGenderDivision,
                    indicator.BFPContractIndicator.SelectedIndicator.MeasureName,
                    indicator.PeriodAmountMen,
                    indicator.PeriodAmountWomen,
                    indicator.PeriodAmountTotal,
                    indicator.CumulativeAmountMen,
                    indicator.CumulativeAmountWomen,
                    indicator.CumulativeAmountTotal,
                    indicator.ResidueAmountMen,
                    indicator.ResidueAmountWomen,
                    indicator.ResidueAmountTotal,
                    indicator.LastReportCumulativeAmountMen,
                    indicator.LastReportCumulativeAmountWomen,
                    indicator.LastReportCumulativeAmountTotal,
                    indicator.Comment);

                newContractReportIndicators.Add(newContractReportIndicator);
            }

            this.unitOfWork.BulkInsert<Eumis.Domain.Contracts.ContractReportIndicator>(newContractReportIndicators);
        }

        public void DeleteContractReportIndicatorsInDraft(ContractReportTechnical technical)
        {
            var indicatorsToDelete = new List<int>();

            var contractReportTechnicalIndicators = this.contractReportIndicatorsRepository.FindAll(technical.ContractReportId).Where(p => p.ContractReportTechnicalId == technical.ContractReportTechnicalId);

            var contractReportTechnicalIndicatorsInDraft = contractReportTechnicalIndicators.Where(p => p.Status == ContractReportIndicatorStatus.Draft).Select(i => i.ContractReportIndicatorId);
            var contractReportTechnicalEndedIndicatorsToDelete = this.GetContractReportTechnicalEndedIndicatorsToDelete(contractReportTechnicalIndicators, technical);

            indicatorsToDelete.AddRange(contractReportTechnicalIndicatorsInDraft);
            indicatorsToDelete.AddRange(contractReportTechnicalEndedIndicatorsToDelete);

            if (indicatorsToDelete.Any())
            {
                this.unitOfWork.BulkDelete<Eumis.Domain.Contracts.ContractReportIndicator>(p => indicatorsToDelete.Contains(p.ContractReportIndicatorId));
            }
        }

        public void UpdateContractReportEndedIndicators(int contractReportId, int oldContractReportTechnicalId, int newContractReportTechnicalId)
        {
            var contractReportIndicators = this.contractReportIndicatorsRepository
                .FindAll(contractReportId)
                .Where(i => i.ContractReportTechnicalId == oldContractReportTechnicalId);

            foreach (var indicator in contractReportIndicators)
            {
                indicator.ContractReportTechnicalId = newContractReportTechnicalId;
                indicator.ModifyDate = DateTime.Now;
            }

            this.unitOfWork.Save();
        }

        public Eumis.Domain.Contracts.ContractReportIndicator UpdateContractReportIndicator(
            int contractReportIndicatorId,
            byte[] version,
            ContractReportIndicatorApproval? approval,
            string notes,
            decimal? approvedPeriodAmountMen,
            decimal? approvedPeriodAmountWomen,
            decimal? approvedPeriodAmountTotal,
            decimal? approvedCumulativeAmountMen,
            decimal? approvedCumulativeAmountWomen,
            decimal? approvedCumulativeAmountTotal,
            decimal? approvedResidueAmountMen,
            decimal? approvedResidueAmountWomen,
            decimal? approvedResidueAmountTotal,
            decimal? lastReportCumulativeAmountMen,
            decimal? lastReportCumulativeAmountWomen,
            decimal lastReportCumulativeAmountTotal)
        {
            var contractReportIndicator = this.contractReportIndicatorsRepository.FindForUpdate(contractReportIndicatorId, version);

            var contractReport = this.contractReportsRepository.Find(contractReportIndicator.ContractReportId);

            var contractIndicator =
                this.contractsRepository.GetContractIndicator(contractReportIndicator.ContractId, contractReportIndicator.ContractIndicatorId);

            if (contractReport.Status != ContractReportStatus.Unchecked)
            {
                throw new DomainException("Cannot edit a ContractReportIndicator when the ContractReport is in status other than 'Unchecked'");
            }

            this.AssertIsDraftContractReportIndicator(contractReportIndicator.Status);

            contractReportIndicator.UpdateAttributes(
                approval,
                notes,
                approvedPeriodAmountMen,
                approvedPeriodAmountWomen,
                approvedPeriodAmountTotal,
                approvedCumulativeAmountMen,
                approvedCumulativeAmountWomen,
                approvedCumulativeAmountTotal,
                approvedResidueAmountMen,
                approvedResidueAmountWomen,
                approvedResidueAmountTotal,
                lastReportCumulativeAmountMen,
                lastReportCumulativeAmountWomen,
                lastReportCumulativeAmountTotal,
                contractIndicator);

            this.unitOfWork.Save();

            return contractReportIndicator;
        }

        public void ChangeContractReportIndicatorStatus(
            int contractReportIndicatorId,
            byte[] version,
            ContractReportIndicatorStatus status)
        {
            var contractReportIndicator = this.contractReportIndicatorsRepository.FindForUpdate(contractReportIndicatorId, version);

            var contractReport = this.contractReportsRepository.Find(contractReportIndicator.ContractReportId);

            if (contractReport.Status != ContractReportStatus.Unchecked)
            {
                throw new DomainException("Cannot edit a ContractReportIndicator when the ContractReport is in status other than 'Unchecked'");
            }

            contractReportIndicator.Status = status;
            contractReportIndicator.ModifyDate = DateTime.Now;

            if (status == ContractReportIndicatorStatus.Ended)
            {
                contractReportIndicator.CheckedByUserId = this.accessContext.UserId;
                contractReportIndicator.CheckedDate = DateTime.Now;
            }

            var contractReportTechnicalIndicator = this.contractReportIndicatorsRepository.FindForUpdate(contractReportIndicatorId, version);

            var contractIndicatorGid = this.contractReportIndicatorsRepository.GetContractIndicatorGid(contractReportTechnicalIndicator.ContractIndicatorId);

            var contractReportTechnicalId = contractReportTechnicalIndicator.ContractReportTechnicalId;

            var contractReportTechnical = this.contractReportTechnicalsRepository.FindWithoutIncludes(contractReportTechnicalId);
            contractReportTechnical.ChangeIndicatorPortalAccessibility(contractIndicatorGid, status);

            this.unitOfWork.Save();
        }

        public IList<string> CanChangeContractReportIndicatorStatusToEnded(int contractReportIndicatorId)
        {
            var errors = new List<string>();

            var contractReportIndicator = this.contractReportIndicatorsRepository.Find(contractReportIndicatorId);

            if (!contractReportIndicator.Approval.HasValue)
            {
                errors.Add("Полето 'Съгласие' трябва да е попълнено");
            }

            if (contractReportIndicator.HasGenderDivision && !contractReportIndicator.ApprovedPeriodAmountMen.HasValue)
            {
                errors.Add("Полето 'Одобрена отчетена стойност за периода (мъже)' трябва да е попълнено");
            }

            if (contractReportIndicator.HasGenderDivision && !contractReportIndicator.ApprovedPeriodAmountWomen.HasValue)
            {
                errors.Add("Полето 'Одобрена отчетена стойност за периода (жени)' трябва да е попълнено");
            }

            if (!contractReportIndicator.ApprovedPeriodAmountTotal.HasValue)
            {
                errors.Add("Полето 'Одобрена отчетена стойност за периода' трябва да е попълнено");
            }

            if (contractReportIndicator.HasGenderDivision && !contractReportIndicator.ApprovedCumulativeAmountMen.HasValue)
            {
                errors.Add("Полето 'Одобрена отчетена стойност с натрупване (мъже)' трябва да е попълнено");
            }

            if (contractReportIndicator.HasGenderDivision && !contractReportIndicator.ApprovedCumulativeAmountWomen.HasValue)
            {
                errors.Add("Полето 'Одобрена отчетена стойност с натрупване (жени)' трябва да е попълнено");
            }

            if (!contractReportIndicator.ApprovedCumulativeAmountTotal.HasValue)
            {
                errors.Add("Полето 'Одобрена отчетена стойност с натрупване' трябва да е попълнено");
            }

            if (contractReportIndicator.HasGenderDivision && !contractReportIndicator.ApprovedResidueAmountMen.HasValue)
            {
                errors.Add("Полето 'Одобрен остатък/отклонение спрямо зададеното в договора (мъже)' трябва да е попълнено");
            }

            if (contractReportIndicator.HasGenderDivision && !contractReportIndicator.ApprovedResidueAmountWomen.HasValue)
            {
                errors.Add("Полето 'Одобрен остатък/отклонение спрямо зададеното в договора (жени)' трябва да е попълнено");
            }

            if (!contractReportIndicator.ApprovedResidueAmountTotal.HasValue)
            {
                errors.Add("Полето 'Одобрен остатък/отклонение спрямо зададеното в договора' трябва да е попълнено");
            }

            return errors;
        }

        private void AssertIsDraftContractReportIndicator(ContractReportIndicatorStatus status)
        {
            if (status != ContractReportIndicatorStatus.Draft)
            {
                throw new DomainException("Cannot edit ContractReportIndicator when not in 'Draft' status");
            }
        }

        private List<int> GetContractReportTechnicalEndedIndicatorsToDelete(IEnumerable<Domain.Contracts.ContractReportIndicator> contractReportTechnicalIndicators, ContractReportTechnical technical)
        {
            var indicatorsToDelete = new List<int>();

            foreach (var indicator in contractReportTechnicalIndicators)
            {
                if (indicator.Status == ContractReportIndicatorStatus.Ended)
                {
                    var indicatorGid = this.contractReportIndicatorsRepository.GetContractIndicatorGid(indicator.ContractIndicatorId);

                    var isIndicatorToDelete = technical.IsTechnicalIndicatorToDelete(indicatorGid);

                    if (isIndicatorToDelete)
                    {
                        indicatorsToDelete.Add(indicator.ContractReportIndicatorId);
                    }
                }
            }

            return indicatorsToDelete;
        }
    }
}
