using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.ContractReportFinancialCorrections.Repositories;
using Eumis.Data.ContractReports.Repositories;
using Eumis.Data.Contracts.Repositories;
using Eumis.Domain;
using Eumis.Domain.Contracts;
using Eumis.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.ApplicationServices.Services.ContractReportFinancialCorrection
{
    internal class ContractReportFinancialCorrectionService : IContractReportFinancialCorrectionService
    {
        private IUnitOfWork unitOfWork;
        private IAccessContext accessContext;
        private IContractsRepository contractsRepository;
        private IContractReportsRepository contractReportsRepository;
        private IContractReportFinancialsRepository contractReportFinancialsRepository;
        private IContractReportFinancialCorrectionsRepository contractReportFinancialCorrectionsRepository;
        private IContractReportFinancialCorrectionCSDsRepository contractReportFinancialCorrectionCSDsRepository;

        public ContractReportFinancialCorrectionService(
            IUnitOfWork unitOfWork,
            IAccessContext accessContext,
            IContractsRepository contractsRepository,
            IContractReportsRepository contractReportsRepository,
            IContractReportFinancialsRepository contractReportFinancialsRepository,
            IContractReportFinancialCorrectionsRepository contractReportFinancialCorrectionsRepository,
            IContractReportFinancialCorrectionCSDsRepository contractReportFinancialCorrectionCSDsRepository)
        {
            this.unitOfWork = unitOfWork;
            this.accessContext = accessContext;
            this.contractsRepository = contractsRepository;
            this.contractReportsRepository = contractReportsRepository;
            this.contractReportFinancialsRepository = contractReportFinancialsRepository;
            this.contractReportFinancialCorrectionsRepository = contractReportFinancialCorrectionsRepository;
            this.contractReportFinancialCorrectionCSDsRepository = contractReportFinancialCorrectionCSDsRepository;
        }

        public IList<string> CanCreateContractReportFinancialCorrection(string contractNum, string contractReportNum)
        {
            var errors = new List<string>();

            var contract = this.contractsRepository.FindByRegNumber(contractNum);
            var contractReport = this.contractReportsRepository.FindByNum(contract.ContractId, contractReportNum);

            var canCreateContractReport = this.contractReportFinancialCorrectionsRepository.CanCreate(contractReport.ContractReportId);
            if (!canCreateContractReport)
            {
                errors.Add("Не може да се създаде нова корекция на верифицирани суми, защото вече съществува такава със статус 'Чернова' към същия пакет отчетни документи.");
            }
            else if (contractReport.Status != ContractReportStatus.Accepted)
            {
                errors.Add("Не може да се създаде нова корекция на верифицирани суми, защото статуса на пакета отчетни документи не е 'Приет'.");
            }
            else
            {
                var actualFinancial = this.contractReportFinancialsRepository.GetActualContractReportFinancial(contractReport.ContractReportId);
                if (actualFinancial == null)
                {
                    errors.Add("Не може да се създаде нова корекция на верифицирани суми, защото в пакета отчетни документи няма финансов отчет.");
                }
            }

            return errors;
        }

        public Eumis.Domain.Contracts.ContractReportFinancialCorrection CreateContractReportFinancialCorrection(string contractNum, string contractReportNum)
        {
            if (this.CanCreateContractReportFinancialCorrection(contractNum, contractReportNum).Any())
            {
                throw new DomainException("Cannot create ContractReportFinancialCorrection");
            }

            var contract = this.contractsRepository.FindByRegNumber(contractNum);
            var contractReport = this.contractReportsRepository.FindByNum(contract.ContractId, contractReportNum);
            var actualFinancial = this.contractReportFinancialsRepository.GetActualContractReportFinancial(contractReport.ContractReportId);

            var newContractReportFinancialCorrection = new Eumis.Domain.Contracts.ContractReportFinancialCorrection(
                actualFinancial.ContractReportFinancialId,
                contractReport.ContractReportId,
                contractReport.ContractId,
                this.contractReportFinancialCorrectionsRepository.GetNextOrderNum(contractReport.ContractId));

            this.contractReportFinancialCorrectionsRepository.Add(newContractReportFinancialCorrection);

            this.unitOfWork.Save();

            return newContractReportFinancialCorrection;
        }

        public Eumis.Domain.Contracts.ContractReportFinancialCorrection UpdateContractReportFinancialCorrection(
            int contractReportFinancialCorrectionId,
            byte[] version,
            DateTime? correctionDate,
            Guid? blobKey,
            string notes)
        {
            var financialCorrection = this.contractReportFinancialCorrectionsRepository.FindForUpdate(contractReportFinancialCorrectionId, version);

            this.AssertIsDraftContractReportFinancialCorrection(financialCorrection.Status);

            financialCorrection.UpdateAttributes(
                correctionDate,
                blobKey,
                notes);

            this.unitOfWork.Save();

            return financialCorrection;
        }

        public IList<string> CanDeleteContractReportFinancialCorrection(int contractReportFinancialCorrectionId)
        {
            var errors = new List<string>();

            var financialCorrection = this.contractReportFinancialCorrectionsRepository.Find(contractReportFinancialCorrectionId);

            if (this.contractReportFinancialCorrectionCSDsRepository.HasContractReportFinancialCorrectionCSDs(contractReportFinancialCorrectionId))
            {
                errors.Add("Не можете да изтриете корекцията, защото има създадени коригирани верифицирани РОД");
            }

            return errors;
        }

        public Eumis.Domain.Contracts.ContractReportFinancialCorrection DeleteContractReportFinancialCorrection(int contractReportFinancialCorrectionId, byte[] version)
        {
            var financialCorrection = this.contractReportFinancialCorrectionsRepository.FindForUpdate(contractReportFinancialCorrectionId, version);

            if (financialCorrection.Status != ContractReportFinancialCorrectionStatus.Draft)
            {
                throw new DomainException("Cannot delete a ContractReportFinancialCorrection with status different from 'Draft'");
            }

            this.contractReportFinancialCorrectionsRepository.Remove(financialCorrection);

            this.unitOfWork.Save();

            return financialCorrection;
        }

        public IList<string> CanChangeContractReportFinancialCorrectionStatusToEnded(int contractReportFinancialCorrectionId)
        {
            var errors = new List<string>();

            var financialCorrection = this.contractReportFinancialCorrectionsRepository.Find(contractReportFinancialCorrectionId);

            if (!financialCorrection.BlobKey.HasValue)
            {
                errors.Add("Полето 'Файл' трябва да е попълнено");
            }

            if (!financialCorrection.CorrectionDate.HasValue)
            {
                errors.Add("Полето 'Дата на корекция' трябва да е попълнено");
            }

            if (!this.contractReportFinancialCorrectionCSDsRepository.HasContractReportFinancialCorrectionCSDs(contractReportFinancialCorrectionId))
            {
                errors.Add("Не можете да приключите корекцията, защото трябва да има поне един коригиран верифициран РОД");
            }

            if (this.contractReportFinancialCorrectionCSDsRepository.HasDraftContractReportFinancialCorrectionCSDs(contractReportFinancialCorrectionId))
            {
                errors.Add("Не можете да приключите корекцията, защото всички коригирани верифицирани РОД трябва да са със статус 'Приключен'");
            }

            return errors;
        }

        public IList<string> CanChangeContractReportFinancialCorrectionStatusToDraft(int contractReportFinancialCorrectionId)
        {
            var errors = new List<string>();

            if (this.contractReportFinancialCorrectionsRepository.IsIncludedInCertReport(contractReportFinancialCorrectionId))
            {
                errors.Add("Не можете да промените статуса на корекцията на 'Чернова', защото тя е включена в доклад по сертификация");
            }

            return errors;
        }

        public Eumis.Domain.Contracts.ContractReportFinancialCorrection ChangeContractReportFinancialCorrectionStatus(int contractReportFinancialCorrectionId, byte[] version, ContractReportFinancialCorrectionStatus status)
        {
            var financialCorrection = this.contractReportFinancialCorrectionsRepository.FindForUpdate(contractReportFinancialCorrectionId, version);

            if (status == ContractReportFinancialCorrectionStatus.Ended)
            {
                financialCorrection.CheckedByUserId = this.accessContext.UserId;
                financialCorrection.CheckedDate = DateTime.Now;
            }

            financialCorrection.Status = status;
            financialCorrection.ModifyDate = DateTime.Now;

            this.unitOfWork.Save();

            return financialCorrection;
        }

        private void AssertIsDraftContractReportFinancialCorrection(ContractReportFinancialCorrectionStatus status)
        {
            if (status != ContractReportFinancialCorrectionStatus.Draft)
            {
                throw new DomainException("Cannot edit ContractReportFinancialCorrection when not in 'Draft' status");
            }
        }

        public Eumis.Domain.Contracts.ContractReportFinancialCorrectionCSD CreateContractReportFinancialCorrectionCSD(
            int contractReportFinancialCorrectionId,
            int contractReportFinancialCSDBudgetItemId)
        {
            var financialCorrection = this.contractReportFinancialCorrectionsRepository.Find(contractReportFinancialCorrectionId);

            this.AssertIsDraftContractReportFinancialCorrection(financialCorrection.Status);

            var newContractReportFinancialCorrectionCSD = new Eumis.Domain.Contracts.ContractReportFinancialCorrectionCSD(
                contractReportFinancialCorrectionId,
                contractReportFinancialCSDBudgetItemId,
                financialCorrection.ContractReportFinancialId,
                financialCorrection.ContractReportId,
                financialCorrection.ContractId);

            this.contractReportFinancialCorrectionCSDsRepository.Add(newContractReportFinancialCorrectionCSD);

            this.unitOfWork.Save();

            return newContractReportFinancialCorrectionCSD;
        }

        public Eumis.Domain.Contracts.ContractReportFinancialCorrectionCSD UpdateContractReportFinancialCorrectionCSD(
            int contractReportFinancialCorrectionCSDId,
            byte[] version,
            Sign? sign,
            string notes,
            decimal? correctedUnapprovedEuAmount,
            decimal? correctedUnapprovedBgAmount,
            decimal? correctedUnapprovedBfpTotalAmount,
            decimal? correctedUnapprovedSelfAmount,
            decimal? correctedUnapprovedTotalAmount,
            decimal? correctedUnapprovedByCorrectionEuAmount,
            decimal? correctedUnapprovedByCorrectionBgAmount,
            decimal? correctedUnapprovedByCorrectionBfpTotalAmount,
            decimal? correctedUnapprovedByCorrectionSelfAmount,
            decimal? correctedUnapprovedByCorrectionTotalAmount,
            decimal? correctedApprovedEuAmount,
            decimal? correctedApprovedBgAmount,
            decimal? correctedApprovedBfpTotalAmount,
            decimal? correctedApprovedSelfAmount,
            decimal? correctedApprovedTotalAmount,
            CorrectionType? correctionType,
            int? financialCorrectionId,
            int? irregularityId)
        {
            var financialCorrectionCSD = this.contractReportFinancialCorrectionCSDsRepository.FindForUpdate(contractReportFinancialCorrectionCSDId, version);

            var financialCorrection = this.contractReportFinancialCorrectionsRepository.Find(financialCorrectionCSD.ContractReportFinancialCorrectionId);

            this.AssertIsDraftContractReportFinancialCorrection(financialCorrection.Status);

            this.AssertIsDraftContractReportFinancialCorrectionCSD(financialCorrectionCSD.Status);

            financialCorrectionCSD.UpdateAttributes(
                sign,
                notes,
                correctedUnapprovedEuAmount,
                correctedUnapprovedBgAmount,
                correctedUnapprovedBfpTotalAmount,
                correctedUnapprovedSelfAmount,
                correctedUnapprovedTotalAmount,
                correctedUnapprovedByCorrectionEuAmount,
                correctedUnapprovedByCorrectionBgAmount,
                correctedUnapprovedByCorrectionBfpTotalAmount,
                correctedUnapprovedByCorrectionSelfAmount,
                correctedUnapprovedByCorrectionTotalAmount,
                correctedApprovedEuAmount,
                correctedApprovedBgAmount,
                correctedApprovedBfpTotalAmount,
                correctedApprovedSelfAmount,
                correctedApprovedTotalAmount,
                correctionType,
                financialCorrectionId,
                irregularityId);

            this.unitOfWork.Save();

            return financialCorrectionCSD;
        }

        public Eumis.Domain.Contracts.ContractReportFinancialCorrectionCSD DeleteContractReportFinancialCorrectionCSD(int contractReportFinancialCorrectionCSDId, byte[] version)
        {
            var financialCorrectionCSD = this.contractReportFinancialCorrectionCSDsRepository.FindForUpdate(contractReportFinancialCorrectionCSDId, version);

            var financialCorrection = this.contractReportFinancialCorrectionsRepository.Find(financialCorrectionCSD.ContractReportFinancialCorrectionId);

            this.AssertIsDraftContractReportFinancialCorrection(financialCorrection.Status);

            this.AssertIsDraftContractReportFinancialCorrectionCSD(financialCorrectionCSD.Status);

            this.contractReportFinancialCorrectionCSDsRepository.Remove(financialCorrectionCSD);

            this.unitOfWork.Save();

            return financialCorrectionCSD;
        }

        public IList<string> CanChangeContractReportFinancialCorrectionCSDStatusToEnded(int contractReportFinancialCorrectionCSDId)
        {
            var errors = new List<string>();

            var financialCorrectionCSD = this.contractReportFinancialCorrectionCSDsRepository.Find(contractReportFinancialCorrectionCSDId);

            if (!financialCorrectionCSD.Sign.HasValue)
            {
                errors.Add("Полето 'Знак' трябва да е попълнено");
            }

            if (!financialCorrectionCSD.CorrectedUnapprovedBgAmount.HasValue ||
                !financialCorrectionCSD.CorrectedUnapprovedBfpTotalAmount.HasValue ||
                !financialCorrectionCSD.CorrectedUnapprovedTotalAmount.HasValue)
            {
                errors.Add("Сумата от секция 'Коригирана неверифицирана сума на разходооправдателния документ за конкретния бюджетен ред и дейност' трябва да е попълнена");
            }

            if (!financialCorrectionCSD.CorrectedUnapprovedByCorrectionBgAmount.HasValue ||
                !financialCorrectionCSD.CorrectedUnapprovedByCorrectionBfpTotalAmount.HasValue ||
                !financialCorrectionCSD.CorrectedUnapprovedByCorrectionTotalAmount.HasValue)
            {
                errors.Add("Сумата от секция 'Коригирана неверифицирана сума на разходооправдателен документ по наложена финансова корекция за конкретния бюджетен ред и дейност' трябва да е попълнена");
            }

            return errors;
        }

        public Eumis.Domain.Contracts.ContractReportFinancialCorrectionCSD ChangeContractReportFinancialCorrectionCSDStatus(int contractReportFinancialCorrectionCSDId, byte[] version, ContractReportFinancialCorrectionCSDStatus status)
        {
            var financialCorrectionCSD = this.contractReportFinancialCorrectionCSDsRepository.FindForUpdate(contractReportFinancialCorrectionCSDId, version);

            var financialCorrection = this.contractReportFinancialCorrectionsRepository.Find(financialCorrectionCSD.ContractReportFinancialCorrectionId);

            this.AssertIsDraftContractReportFinancialCorrection(financialCorrection.Status);

            switch (status)
            {
                case ContractReportFinancialCorrectionCSDStatus.Draft:
                    if (financialCorrectionCSD.Status != ContractReportFinancialCorrectionCSDStatus.Ended)
                    {
                        throw new DomainException("ContractReportFinancialCorrectionCSD status transition not allowed");
                    }

                    break;
                case ContractReportFinancialCorrectionCSDStatus.Ended:
                    if (financialCorrectionCSD.Status != ContractReportFinancialCorrectionCSDStatus.Draft)
                    {
                        throw new DomainException("ContractReportFinancialCorrectionCSD status transition not allowed");
                    }

                    break;
            }

            if (status == ContractReportFinancialCorrectionCSDStatus.Ended)
            {
                financialCorrectionCSD.CheckedByUserId = this.accessContext.UserId;
                financialCorrectionCSD.CheckedDate = DateTime.Now;
            }
            else if (status == ContractReportFinancialCorrectionCSDStatus.Draft)
            {
                financialCorrectionCSD.CheckedByUserId = null;
                financialCorrectionCSD.CheckedDate = null;
            }

            financialCorrectionCSD.Status = status;
            financialCorrectionCSD.ModifyDate = DateTime.Now;

            this.unitOfWork.Save();

            return financialCorrectionCSD;
        }

        private void AssertIsDraftContractReportFinancialCorrectionCSD(ContractReportFinancialCorrectionCSDStatus status)
        {
            if (status != ContractReportFinancialCorrectionCSDStatus.Draft)
            {
                throw new DomainException("Cannot edit ContractReportFinancialCorrectionCSD when not in 'Draft' status");
            }
        }
    }
}
