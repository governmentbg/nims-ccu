using System;
using System.Collections.Generic;
using System.Linq;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.ContractReportIndicators.Repositories;
using Eumis.Data.ContractReports.Repositories;
using Eumis.Data.ContractReportTechnicalCorrections.Repositories;
using Eumis.Data.Contracts.Repositories;
using Eumis.Domain;
using Eumis.Domain.Contracts;
using Eumis.Domain.Core;

namespace Eumis.ApplicationServices.Services.ContractReportTechnicalCorrection
{
    internal class ContractReportTechnicalCorrectionService : IContractReportTechnicalCorrectionService
    {
        private IUnitOfWork unitOfWork;
        private IAccessContext accessContext;
        private IContractsRepository contractsRepository;
        private IContractReportsRepository contractReportsRepository;
        private IContractReportIndicatorsRepository contractReportIndicatorsRepository;
        private IContractReportTechnicalsRepository contractReportTechnicalsRepository;
        private IContractReportTechnicalCorrectionsRepository contractReportTechnicalCorrectionsRepository;
        private IContractReportTechnicalCorrectionIndicatorsRepository contractReportTechnicalCorrectionIndicatorsRepository;

        public ContractReportTechnicalCorrectionService(
            IUnitOfWork unitOfWork,
            IAccessContext accessContext,
            IContractsRepository contractsRepository,
            IContractReportsRepository contractReportsRepository,
            IContractReportIndicatorsRepository contractReportIndicatorsRepository,
            IContractReportTechnicalsRepository contractReportTechnicalsRepository,
            IContractReportTechnicalCorrectionsRepository contractReportTechnicalCorrectionsRepository,
            IContractReportTechnicalCorrectionIndicatorsRepository contractReportTechnicalCorrectionIndicatorsRepository)
        {
            this.unitOfWork = unitOfWork;
            this.accessContext = accessContext;
            this.contractsRepository = contractsRepository;
            this.contractReportsRepository = contractReportsRepository;
            this.contractReportIndicatorsRepository = contractReportIndicatorsRepository;
            this.contractReportTechnicalsRepository = contractReportTechnicalsRepository;
            this.contractReportTechnicalCorrectionsRepository = contractReportTechnicalCorrectionsRepository;
            this.contractReportTechnicalCorrectionIndicatorsRepository = contractReportTechnicalCorrectionIndicatorsRepository;
        }

        public IList<string> CanCreateContractReportTechnicalCorrection(string contractNum, string contractReportNum)
        {
            var errors = new List<string>();

            var contract = this.contractsRepository.FindByRegNumber(contractNum);
            var contractReport = this.contractReportsRepository.FindByNum(contract.ContractId, contractReportNum);

            var existsDraftCorrectionForContractReport = this.contractReportTechnicalCorrectionsRepository.ExistsDraftCorrectionForContractReport(contractReport.ContractReportId);
            if (existsDraftCorrectionForContractReport)
            {
                errors.Add("Не може да се създаде нова корекция на верифицирани индикатори, защото вече съществува такава със статус 'Чернова' към същия пакет отчетни документи.");
            }
            else if (contractReport.Status != ContractReportStatus.Accepted)
            {
                errors.Add("Не може да се създаде нова корекция на верифицирани индикатори, защото статуса на пакета отчетни документи не е 'Приет'.");
            }
            else
            {
                var actualTechnical = this.contractReportTechnicalsRepository.GetActualContractReportTechnical(contractReport.ContractReportId);
                if (actualTechnical == null)
                {
                    errors.Add("Не може да се създаде нова корекция на верифицирани индикатори, защото в пакета отчетни документи няма технически отчет.");
                }
            }

            return errors;
        }

        public Eumis.Domain.Contracts.ContractReportTechnicalCorrection CreateContractReportTechnicalCorrection(string contractNum, string contractReportNum)
        {
            if (this.CanCreateContractReportTechnicalCorrection(contractNum, contractReportNum).Any())
            {
                throw new DomainException("Cannot create ContractReportTechnicalCorrection");
            }

            var contract = this.contractsRepository.FindByRegNumber(contractNum);
            var contractReport = this.contractReportsRepository.FindByNum(contract.ContractId, contractReportNum);
            var actualTechnical = this.contractReportTechnicalsRepository.GetActualContractReportTechnical(contractReport.ContractReportId);

            var newContractReportTechnicalCorrection = new Eumis.Domain.Contracts.ContractReportTechnicalCorrection(
                actualTechnical.ContractReportTechnicalId,
                contractReport.ContractReportId,
                contractReport.ContractId,
                this.contractReportTechnicalCorrectionsRepository.GetNextOrderNum(contractReport.ContractId));

            this.contractReportTechnicalCorrectionsRepository.Add(newContractReportTechnicalCorrection);

            this.unitOfWork.Save();

            var endedContractReportTechnicalCorrection =
                this.contractReportTechnicalCorrectionsRepository.FindEndedContractReportTechnicalCorrection(contractReport.ContractReportId);
            if (endedContractReportTechnicalCorrection != null)
            {
                var existingCorrectedIndicators =
                    this.contractReportTechnicalCorrectionIndicatorsRepository
                    .FindContractReportTechnicalCorrectionIndicators(endedContractReportTechnicalCorrection.ContractReportTechnicalCorrectionId);

                foreach (var correctedIndicator in existingCorrectedIndicators)
                {
                    var previousContractReportIndicator = this.contractReportIndicatorsRepository.Find(correctedIndicator.ContractReportIndicatorId);

                    var newContractReportTechnicalCorrectionIndicator = new ContractReportTechnicalCorrectionIndicator(
                        newContractReportTechnicalCorrection.ContractReportTechnicalCorrectionId,
                        previousContractReportIndicator.LastReportCumulativeAmountMen,
                        previousContractReportIndicator.LastReportCumulativeAmountWomen,
                        previousContractReportIndicator.LastReportCumulativeAmountTotal,
                        correctedIndicator);

                    this.contractReportTechnicalCorrectionIndicatorsRepository.Add(newContractReportTechnicalCorrectionIndicator);
                }
            }

            this.unitOfWork.Save();

            return newContractReportTechnicalCorrection;
        }

        public Eumis.Domain.Contracts.ContractReportTechnicalCorrection UpdateContractReportTechnicalCorrection(
            int contractReportTechnicalCorrectionId,
            byte[] version,
            DateTime? correctionDate,
            Guid? blobKey,
            string notes)
        {
            var technicalCorrection = this.contractReportTechnicalCorrectionsRepository.FindForUpdate(contractReportTechnicalCorrectionId, version);

            this.AssertIsDraftContractReportTechnicalCorrection(technicalCorrection.Status);

            technicalCorrection.UpdateAttributes(
                correctionDate,
                blobKey,
                notes);

            this.unitOfWork.Save();

            return technicalCorrection;
        }

        public IList<string> CanDeleteContractReportTechnicalCorrection(int contractReportTechnicalCorrectionId)
        {
            var errors = new List<string>();

            var technicalCorrection = this.contractReportTechnicalCorrectionsRepository.Find(contractReportTechnicalCorrectionId);

            if (this.contractReportTechnicalCorrectionIndicatorsRepository.HasContractReportTechnicalCorrectionIndicators(contractReportTechnicalCorrectionId))
            {
                errors.Add("Не можете да изтриете корекцията, защото има създадени коригирани верифицирани индикатори");
            }

            return errors;
        }

        public Eumis.Domain.Contracts.ContractReportTechnicalCorrection DeleteContractReportTechnicalCorrection(int contractReportTechnicalCorrectionId, byte[] version)
        {
            var technicalCorrection = this.contractReportTechnicalCorrectionsRepository.FindForUpdate(contractReportTechnicalCorrectionId, version);

            if (technicalCorrection.Status != ContractReportTechnicalCorrectionStatus.Draft)
            {
                throw new DomainException("Cannot delete a ContractReportTechnicalCorrection with status different from 'Draft'");
            }

            this.contractReportTechnicalCorrectionsRepository.Remove(technicalCorrection);

            this.unitOfWork.Save();

            return technicalCorrection;
        }

        public IList<string> CanChangeContractReportTechnicalCorrectionStatusToEnded(int contractReportTechnicalCorrectionId)
        {
            var errors = new List<string>();

            var technicalCorrection = this.contractReportTechnicalCorrectionsRepository.Find(contractReportTechnicalCorrectionId);

            if (!technicalCorrection.BlobKey.HasValue)
            {
                errors.Add("Полето 'Файл' трябва да е попълнено");
            }

            if (!technicalCorrection.CorrectionDate.HasValue)
            {
                errors.Add("Полето 'Дата на корекция' трябва да е попълнено");
            }

            if (!this.contractReportTechnicalCorrectionIndicatorsRepository.HasContractReportTechnicalCorrectionIndicators(contractReportTechnicalCorrectionId))
            {
                errors.Add("Не можете да приключите корекцията, защото трябва да има поне един коригиран верифициран индикатор");
            }

            if (this.contractReportTechnicalCorrectionIndicatorsRepository.HasDraftContractReportTechnicalCorrectionIndicators(contractReportTechnicalCorrectionId))
            {
                errors.Add("Не можете да приключите корекцията, защото всички коригирани верифицирани индикатори трябва да са със статус 'Приключен'");
            }

            return errors;
        }

        public Eumis.Domain.Contracts.ContractReportTechnicalCorrection ChangeContractReportTechnicalCorrectionStatus(int contractReportTechnicalCorrectionId, byte[] version, ContractReportTechnicalCorrectionStatus status)
        {
            var technicalCorrection = this.contractReportTechnicalCorrectionsRepository.FindForUpdate(contractReportTechnicalCorrectionId, version);

            if (status == ContractReportTechnicalCorrectionStatus.Ended)
            {
                technicalCorrection.CheckedByUserId = this.accessContext.UserId;
                technicalCorrection.CheckedDate = DateTime.Now;

                var endedContractReportTechnicalCorrection =
                    this.contractReportTechnicalCorrectionsRepository.FindEndedContractReportTechnicalCorrection(technicalCorrection.ContractReportId);
                if (endedContractReportTechnicalCorrection != null)
                {
                    endedContractReportTechnicalCorrection.Status = ContractReportTechnicalCorrectionStatus.Archived;
                }
            }
            else if (status == ContractReportTechnicalCorrectionStatus.Draft)
            {
                var lastArchivedContractReportTechnicalCorrection =
                    this.contractReportTechnicalCorrectionsRepository.FindLastArchivedContractReportTechnicalCorrection(technicalCorrection.ContractReportId);
                if (lastArchivedContractReportTechnicalCorrection != null)
                {
                    lastArchivedContractReportTechnicalCorrection.Status = ContractReportTechnicalCorrectionStatus.Ended;
                }
            }

            technicalCorrection.Status = status;
            technicalCorrection.ModifyDate = DateTime.Now;

            this.unitOfWork.Save();

            return technicalCorrection;
        }

        public ContractReportTechnicalCorrectionIndicator CreateContractReportTechnicalCorrectionIndicator(
            int contractReportTechnicalCorrectionId,
            int contractReportIndicatorId)
        {
            var technicalCorrection = this.contractReportTechnicalCorrectionsRepository.Find(contractReportTechnicalCorrectionId);

            this.AssertIsDraftContractReportTechnicalCorrection(technicalCorrection.Status);

            var previousContractReports = this.contractReportsRepository.GetPreviousContractReport(technicalCorrection.ContractReportId);

            Domain.Contracts.ContractReport lastContractReportWithIndicatorCorrection = null;

            foreach (var previousContractReport in previousContractReports)
            {
                if (this.contractReportTechnicalCorrectionsRepository.ExistsCorrectionForContractReport(previousContractReport.ContractReportId))
                {
                    lastContractReportWithIndicatorCorrection = previousContractReport;
                    break;
                }
            }

            var contractReportIndicator = this.contractReportIndicatorsRepository.Find(contractReportIndicatorId);

            decimal? lastReportCorrectedCumulativeAmountMen = null;
            decimal? lastReportCorrectedCumulativeAmountWomen = null;
            decimal? lastReportCorrectedCumulativeAmountTotal = 0;

            if (lastContractReportWithIndicatorCorrection != null)
            {
                var previousContractReportTechnicalCorrectionIndicator = this.contractReportTechnicalCorrectionIndicatorsRepository.FindPreviousCorrection(lastContractReportWithIndicatorCorrection.ContractReportId, contractReportIndicator.ContractIndicatorId);

                if (previousContractReportTechnicalCorrectionIndicator != null)
                {
                    lastReportCorrectedCumulativeAmountMen = previousContractReportTechnicalCorrectionIndicator?.CorrectedApprovedCumulativeAmountMen;
                    lastReportCorrectedCumulativeAmountWomen = previousContractReportTechnicalCorrectionIndicator?.CorrectedApprovedCumulativeAmountWomen;
                    lastReportCorrectedCumulativeAmountTotal = previousContractReportTechnicalCorrectionIndicator?.CorrectedApprovedCumulativeAmountTotal;
                }
                else
                {
                    lastReportCorrectedCumulativeAmountMen = contractReportIndicator.LastReportCumulativeAmountMen;
                    lastReportCorrectedCumulativeAmountWomen = contractReportIndicator.LastReportCumulativeAmountWomen;
                    lastReportCorrectedCumulativeAmountTotal = contractReportIndicator.LastReportCumulativeAmountTotal;
                }
            }
            else
            {
                lastReportCorrectedCumulativeAmountMen = contractReportIndicator.LastReportCumulativeAmountMen;
                lastReportCorrectedCumulativeAmountWomen = contractReportIndicator.LastReportCumulativeAmountWomen;
                lastReportCorrectedCumulativeAmountTotal = contractReportIndicator.LastReportCumulativeAmountTotal;
            }

            var newContractReportTechnicalCorrectionIndicator = new ContractReportTechnicalCorrectionIndicator(
                contractReportTechnicalCorrectionId,
                contractReportIndicatorId,
                technicalCorrection.ContractReportTechnicalId,
                technicalCorrection.ContractReportId,
                technicalCorrection.ContractId,
                lastReportCorrectedCumulativeAmountMen,
                lastReportCorrectedCumulativeAmountWomen,
                lastReportCorrectedCumulativeAmountTotal);

            this.contractReportTechnicalCorrectionIndicatorsRepository.Add(newContractReportTechnicalCorrectionIndicator);

            this.unitOfWork.Save();

            return newContractReportTechnicalCorrectionIndicator;
        }

        public ContractReportTechnicalCorrectionIndicator UpdateContractReportTechnicalCorrectionIndicator(
            int contractReportTechnicalCorrectionIndicatorId,
            byte[] version,
            string notes,
            decimal? correctedApprovedPeriodAmountMen,
            decimal? correctedApprovedPeriodAmountWomen,
            decimal correctedApprovedPeriodAmountTotal,
            decimal? correctedApprovedCumulativeAmountMen,
            decimal? correctedApprovedCumulativeAmountWomen,
            decimal correctedApprovedCumulativeAmountTotal,
            decimal? correctedApprovedResidueAmountMen,
            decimal? correctedApprovedResidueAmountWomen,
            decimal correctedApprovedResidueAmountTotal)
        {
            var technicalCorrectionIndicator = this.contractReportTechnicalCorrectionIndicatorsRepository.FindForUpdate(contractReportTechnicalCorrectionIndicatorId, version);

            var technicalCorrection = this.contractReportTechnicalCorrectionsRepository.Find(technicalCorrectionIndicator.ContractReportTechnicalCorrectionId);

            this.AssertIsDraftContractReportTechnicalCorrection(technicalCorrection.Status);

            this.AssertIsDraftContractReportTechnicalCorrectionIndicator(technicalCorrectionIndicator.Status);

            var contractReportIndicator = this.contractReportIndicatorsRepository.Find(technicalCorrectionIndicator.ContractReportIndicatorId);

            var contractIndicator =
                this.contractsRepository.GetContractIndicator(contractReportIndicator.ContractId, contractReportIndicator.ContractIndicatorId);

            technicalCorrectionIndicator.UpdateAttributes(
                notes,
                correctedApprovedPeriodAmountMen,
                correctedApprovedPeriodAmountWomen,
                correctedApprovedPeriodAmountTotal,
                correctedApprovedCumulativeAmountMen,
                correctedApprovedCumulativeAmountWomen,
                correctedApprovedCumulativeAmountTotal,
                correctedApprovedResidueAmountMen,
                correctedApprovedResidueAmountWomen,
                correctedApprovedResidueAmountTotal,
                contractReportIndicator,
                contractIndicator);

            this.unitOfWork.Save();

            return technicalCorrectionIndicator;
        }

        public ContractReportTechnicalCorrectionIndicator DeleteContractReportTechnicalCorrectionIndicator(int contractReportTechnicalCorrectionIndicatorId, byte[] version)
        {
            var technicalCorrectionIndicator = this.contractReportTechnicalCorrectionIndicatorsRepository.FindForUpdate(contractReportTechnicalCorrectionIndicatorId, version);

            var technicalCorrection = this.contractReportTechnicalCorrectionsRepository.Find(technicalCorrectionIndicator.ContractReportTechnicalCorrectionId);

            this.AssertIsDraftContractReportTechnicalCorrection(technicalCorrection.Status);

            this.AssertIsDraftContractReportTechnicalCorrectionIndicator(technicalCorrectionIndicator.Status);

            this.contractReportTechnicalCorrectionIndicatorsRepository.Remove(technicalCorrectionIndicator);

            this.unitOfWork.Save();

            return technicalCorrectionIndicator;
        }

        public IList<string> CanChangeContractReportTechnicalCorrectionIndicatorStatusToEnded(int contractReportTechnicalCorrectionIndicatorId)
        {
            var errors = new List<string>();

            var technicalCorrectionIndicator = this.contractReportTechnicalCorrectionIndicatorsRepository.Find(contractReportTechnicalCorrectionIndicatorId);

            var contractReportIndicator = this.contractReportIndicatorsRepository.Find(technicalCorrectionIndicator.ContractReportIndicatorId);

            if (!technicalCorrectionIndicator.CorrectedApprovedPeriodAmountTotal.HasValue ||
                !technicalCorrectionIndicator.CorrectedApprovedCumulativeAmountTotal.HasValue ||
                !technicalCorrectionIndicator.CorrectedApprovedResidueAmountTotal.HasValue ||
                (contractReportIndicator.HasGenderDivision &&
                    (!technicalCorrectionIndicator.CorrectedApprovedPeriodAmountMen.HasValue ||
                    !technicalCorrectionIndicator.CorrectedApprovedPeriodAmountWomen.HasValue ||
                    !technicalCorrectionIndicator.CorrectedApprovedCumulativeAmountMen.HasValue ||
                    !technicalCorrectionIndicator.CorrectedApprovedCumulativeAmountWomen.HasValue ||
                    !technicalCorrectionIndicator.CorrectedApprovedResidueAmountMen.HasValue ||
                    !technicalCorrectionIndicator.CorrectedApprovedResidueAmountWomen.HasValue)))
            {
                errors.Add("Всички полета от секция 'Коригиран верифициран индикатор' трябва да са попълнени");
            }

            return errors;
        }

        public ContractReportTechnicalCorrectionIndicator ChangeContractReportTechnicalCorrectionIndicatorStatus(int contractReportTechnicalCorrectionIndicatorId, byte[] version, ContractReportTechnicalCorrectionIndicatorStatus status)
        {
            var technicalCorrectionIndicator = this.contractReportTechnicalCorrectionIndicatorsRepository.FindForUpdate(contractReportTechnicalCorrectionIndicatorId, version);

            var technicalCorrection = this.contractReportTechnicalCorrectionsRepository.Find(technicalCorrectionIndicator.ContractReportTechnicalCorrectionId);

            this.AssertIsDraftContractReportTechnicalCorrection(technicalCorrection.Status);

            switch (status)
            {
                case ContractReportTechnicalCorrectionIndicatorStatus.Draft:
                    if (technicalCorrectionIndicator.Status != ContractReportTechnicalCorrectionIndicatorStatus.Ended)
                    {
                        throw new DomainException("ContractReportTechnicalCorrectionIndicator status transition not allowed");
                    }

                    break;
                case ContractReportTechnicalCorrectionIndicatorStatus.Ended:
                    if (technicalCorrectionIndicator.Status != ContractReportTechnicalCorrectionIndicatorStatus.Draft)
                    {
                        throw new DomainException("ContractReportTechnicalCorrectionIndicator status transition not allowed");
                    }

                    break;
            }

            if (status == ContractReportTechnicalCorrectionIndicatorStatus.Ended)
            {
                technicalCorrectionIndicator.CheckedByUserId = this.accessContext.UserId;
                technicalCorrectionIndicator.CheckedDate = DateTime.Now;
            }
            else if (status == ContractReportTechnicalCorrectionIndicatorStatus.Draft)
            {
                technicalCorrectionIndicator.CheckedByUserId = null;
                technicalCorrectionIndicator.CheckedDate = null;
            }

            technicalCorrectionIndicator.Status = status;
            technicalCorrectionIndicator.ModifyDate = DateTime.Now;

            this.unitOfWork.Save();

            return technicalCorrectionIndicator;
        }

        private void AssertIsDraftContractReportTechnicalCorrection(ContractReportTechnicalCorrectionStatus status)
        {
            if (status != ContractReportTechnicalCorrectionStatus.Draft)
            {
                throw new DomainException("Cannot edit ContractReportTechnicalCorrection when not in 'Draft' status");
            }
        }

        private void AssertIsDraftContractReportTechnicalCorrectionIndicator(ContractReportTechnicalCorrectionIndicatorStatus status)
        {
            if (status != ContractReportTechnicalCorrectionIndicatorStatus.Draft)
            {
                throw new DomainException("Cannot edit ContractReportTechnicalCorrectionIndicator when not in 'Draft' status");
            }
        }
    }
}
