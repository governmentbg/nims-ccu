using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.ContractReportFinancialCertCorrections.Repositories;
using Eumis.Data.ContractReports.Repositories;
using Eumis.Data.Contracts.Repositories;
using Eumis.Domain;
using Eumis.Domain.Contracts;
using Eumis.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.ApplicationServices.Services.ContractReportFinancialCertCorrection
{
    internal class ContractReportFinancialCertCorrectionService : IContractReportFinancialCertCorrectionService
    {
        private IUnitOfWork unitOfWork;
        private IAccessContext accessContext;
        private IContractsRepository contractsRepository;
        private IContractReportsRepository contractReportsRepository;
        private IContractReportFinancialsRepository contractReportFinancialsRepository;
        private IContractReportFinancialCertCorrectionsRepository contractReportFinancialCertCorrectionsRepository;
        private IContractReportFinancialCertCorrectionCSDsRepository contractReportFinancialCertCorrectionCSDsRepository;

        public ContractReportFinancialCertCorrectionService(
            IUnitOfWork unitOfWork,
            IAccessContext accessContext,
            IContractsRepository contractsRepository,
            IContractReportsRepository contractReportsRepository,
            IContractReportFinancialsRepository contractReportFinancialsRepository,
            IContractReportFinancialCertCorrectionsRepository contractReportFinancialCertCorrectionsRepository,
            IContractReportFinancialCertCorrectionCSDsRepository contractReportFinancialCertCorrectionCSDsRepository)
        {
            this.unitOfWork = unitOfWork;
            this.accessContext = accessContext;
            this.contractsRepository = contractsRepository;
            this.contractReportsRepository = contractReportsRepository;
            this.contractReportFinancialsRepository = contractReportFinancialsRepository;
            this.contractReportFinancialCertCorrectionsRepository = contractReportFinancialCertCorrectionsRepository;
            this.contractReportFinancialCertCorrectionCSDsRepository = contractReportFinancialCertCorrectionCSDsRepository;
        }

        public IList<string> CanCreateContractReportFinancialCertCorrection(string contractNum, string contractReportNum)
        {
            var errors = new List<string>();

            var contract = this.contractsRepository.FindByRegNumber(contractNum);
            var contractReport = this.contractReportsRepository.FindByNum(contract.ContractId, contractReportNum);

            var canCreateContractReport = this.contractReportFinancialCertCorrectionsRepository.CanCreate(contractReport.ContractReportId);
            if (!canCreateContractReport)
            {
                errors.Add("Не може да се създаде нова корекция на сертифицирани суми, защото вече съществува такава със статус 'Чернова' към същия пакет отчетни документи.");
            }
            else if (contractReport.Status != ContractReportStatus.Accepted)
            {
                errors.Add("Не може да се създаде нова корекция на сертифицирани суми, защото статуса на пакета отчетни документи не е 'Приет'.");
            }
            else
            {
                var actualFinancial = this.contractReportFinancialsRepository.GetActualContractReportFinancial(contractReport.ContractReportId);
                if (actualFinancial == null)
                {
                    errors.Add("Не може да се създаде нова корекция на сертифицирани суми, защото в пакета отчетни документи няма финансов отчет.");
                }
            }

            return errors;
        }

        public Eumis.Domain.Contracts.ContractReportFinancialCertCorrection CreateContractReportFinancialCertCorrection(string contractNum, string contractReportNum)
        {
            if (this.CanCreateContractReportFinancialCertCorrection(contractNum, contractReportNum).Any())
            {
                throw new DomainException("Cannot create ContractReportFinancialCertCorrection");
            }

            var contract = this.contractsRepository.FindByRegNumber(contractNum);
            var contractReport = this.contractReportsRepository.FindByNum(contract.ContractId, contractReportNum);
            var actualFinancial = this.contractReportFinancialsRepository.GetActualContractReportFinancial(contractReport.ContractReportId);

            var newContractReportFinancialCertCorrection = new Eumis.Domain.Contracts.ContractReportFinancialCertCorrection(
                actualFinancial.ContractReportFinancialId,
                contractReport.ContractReportId,
                contractReport.ContractId,
                this.contractReportFinancialCertCorrectionsRepository.GetNextOrderNum(contractReport.ContractId));

            this.contractReportFinancialCertCorrectionsRepository.Add(newContractReportFinancialCertCorrection);

            this.unitOfWork.Save();

            return newContractReportFinancialCertCorrection;
        }

        public Eumis.Domain.Contracts.ContractReportFinancialCertCorrection UpdateContractReportFinancialCertCorrection(
            int contractReportFinancialCertCorrectionId,
            byte[] version,
            DateTime? certCorrectionDate,
            Guid? blobKey,
            string notes)
        {
            var financialCertCorrection = this.contractReportFinancialCertCorrectionsRepository.FindForUpdate(contractReportFinancialCertCorrectionId, version);

            this.AssertIsDraftContractReportFinancialCertCorrection(financialCertCorrection.Status);

            financialCertCorrection.UpdateAttributes(
                certCorrectionDate,
                blobKey,
                notes);

            this.unitOfWork.Save();

            return financialCertCorrection;
        }

        public IList<string> CanDeleteContractReportFinancialCertCorrection(int contractReportFinancialCertCorrectionId)
        {
            var errors = new List<string>();

            var financialCertCorrection = this.contractReportFinancialCertCorrectionsRepository.Find(contractReportFinancialCertCorrectionId);

            if (this.contractReportFinancialCertCorrectionCSDsRepository.HasContractReportFinancialCertCorrectionCSDs(contractReportFinancialCertCorrectionId))
            {
                errors.Add("Не можете да изтриете корекцията, защото има създадени коригирани сертифицирани РОД");
            }

            return errors;
        }

        public Eumis.Domain.Contracts.ContractReportFinancialCertCorrection DeleteContractReportFinancialCertCorrection(int contractReportFinancialCertCorrectionId, byte[] version)
        {
            var financialCertCorrection = this.contractReportFinancialCertCorrectionsRepository.FindForUpdate(contractReportFinancialCertCorrectionId, version);

            if (financialCertCorrection.Status != ContractReportFinancialCertCorrectionStatus.Draft)
            {
                throw new DomainException("Cannot delete a ContractReportFinancialCertCorrection with status different from 'Draft'");
            }

            this.contractReportFinancialCertCorrectionsRepository.Remove(financialCertCorrection);

            this.unitOfWork.Save();

            return financialCertCorrection;
        }

        public IList<string> CanChangeContractReportFinancialCertCorrectionStatusToEnded(int contractReportFinancialCertCorrectionId)
        {
            var errors = new List<string>();

            var financialCertCorrection = this.contractReportFinancialCertCorrectionsRepository.Find(contractReportFinancialCertCorrectionId);

            if (!financialCertCorrection.BlobKey.HasValue)
            {
                errors.Add("Полето 'Файл' трябва да е попълнено");
            }

            if (!financialCertCorrection.CertCorrectionDate.HasValue)
            {
                errors.Add("Полето 'Дата на корекция' трябва да е попълнено");
            }

            if (!this.contractReportFinancialCertCorrectionCSDsRepository.HasContractReportFinancialCertCorrectionCSDs(contractReportFinancialCertCorrectionId))
            {
                errors.Add("Не можете да приключите изравняването, защото трябва да има поне един коригиран верифициран РОД");
            }

            if (this.contractReportFinancialCertCorrectionCSDsRepository.HasDraftContractReportFinancialCertCorrectionCSDs(contractReportFinancialCertCorrectionId))
            {
                errors.Add("Не можете да приключите изравняването, защото всички коригирани сертифицирани РОД трябва да са със статус 'Приключен'");
            }

            return errors;
        }

        public IList<string> CanChangeContractReportFinancialCertCorrectionStatusToDraft(int contractReportFinancialCertCorrectionId)
        {
            var errors = new List<string>();

            if (this.contractReportFinancialCertCorrectionsRepository.IsIncludedInCertReport(contractReportFinancialCertCorrectionId))
            {
                errors.Add("Не можете да промените статуса на изравняването на 'Чернова', защото то е включено в доклад по сертификация");
            }

            return errors;
        }

        public Eumis.Domain.Contracts.ContractReportFinancialCertCorrection ChangeContractReportFinancialCertCorrectionStatus(int contractReportFinancialCertCorrectionId, byte[] version, ContractReportFinancialCertCorrectionStatus status)
        {
            var financialCertCorrection = this.contractReportFinancialCertCorrectionsRepository.FindForUpdate(contractReportFinancialCertCorrectionId, version);

            if (status == ContractReportFinancialCertCorrectionStatus.Ended)
            {
                financialCertCorrection.CheckedByUserId = this.accessContext.UserId;
                financialCertCorrection.CheckedDate = DateTime.Now;
            }

            financialCertCorrection.Status = status;
            financialCertCorrection.ModifyDate = DateTime.Now;

            this.unitOfWork.Save();

            return financialCertCorrection;
        }

        private void AssertIsDraftContractReportFinancialCertCorrection(ContractReportFinancialCertCorrectionStatus status)
        {
            if (status != ContractReportFinancialCertCorrectionStatus.Draft)
            {
                throw new DomainException("Cannot edit ContractReportFinancialCertCorrection when not in 'Draft' status");
            }
        }

        public Eumis.Domain.Contracts.ContractReportFinancialCertCorrectionCSD CreateContractReportFinancialCertCorrectionCSD(
            int contractReportFinancialCertCorrectionId,
            int contractReportFinancialCSDBudgetItemId)
        {
            var financialCertCorrection = this.contractReportFinancialCertCorrectionsRepository.Find(contractReportFinancialCertCorrectionId);

            this.AssertIsDraftContractReportFinancialCertCorrection(financialCertCorrection.Status);

            var newContractReportFinancialCertCorrectionCSD = new Eumis.Domain.Contracts.ContractReportFinancialCertCorrectionCSD(
                contractReportFinancialCertCorrectionId,
                contractReportFinancialCSDBudgetItemId,
                financialCertCorrection.ContractReportFinancialId,
                financialCertCorrection.ContractReportId,
                financialCertCorrection.ContractId);

            this.contractReportFinancialCertCorrectionCSDsRepository.Add(newContractReportFinancialCertCorrectionCSD);

            this.unitOfWork.Save();

            return newContractReportFinancialCertCorrectionCSD;
        }

        public Eumis.Domain.Contracts.ContractReportFinancialCertCorrectionCSD UpdateContractReportFinancialCertCorrectionCSD(
            int contractReportFinancialCertCorrectionCSDId,
            byte[] version,
            Sign? sign,
            string notes,
            decimal? certifiedEuAmount,
            decimal? certifiedBgAmount,
            decimal? certifiedBfpTotalAmount,
            decimal? certifiedSelfAmount,
            decimal? certifiedTotalAmount)
        {
            var financialCertCorrectionCSD = this.contractReportFinancialCertCorrectionCSDsRepository.FindForUpdate(contractReportFinancialCertCorrectionCSDId, version);

            var financialCertCorrection = this.contractReportFinancialCertCorrectionsRepository.Find(financialCertCorrectionCSD.ContractReportFinancialCertCorrectionId);

            this.AssertIsDraftContractReportFinancialCertCorrection(financialCertCorrection.Status);

            this.AssertIsDraftContractReportFinancialCertCorrectionCSD(financialCertCorrectionCSD.Status);

            financialCertCorrectionCSD.UpdateAttributes(
                sign,
                notes,
                certifiedEuAmount,
                certifiedBgAmount,
                certifiedBfpTotalAmount,
                certifiedSelfAmount,
                certifiedTotalAmount);

            this.unitOfWork.Save();

            return financialCertCorrectionCSD;
        }

        public Eumis.Domain.Contracts.ContractReportFinancialCertCorrectionCSD DeleteContractReportFinancialCertCorrectionCSD(int contractReportFinancialCertCorrectionCSDId, byte[] version)
        {
            var financialCertCorrectionCSD = this.contractReportFinancialCertCorrectionCSDsRepository.FindForUpdate(contractReportFinancialCertCorrectionCSDId, version);

            var financialCertCorrection = this.contractReportFinancialCertCorrectionsRepository.Find(financialCertCorrectionCSD.ContractReportFinancialCertCorrectionId);

            this.AssertIsDraftContractReportFinancialCertCorrection(financialCertCorrection.Status);

            this.AssertIsDraftContractReportFinancialCertCorrectionCSD(financialCertCorrectionCSD.Status);

            this.contractReportFinancialCertCorrectionCSDsRepository.Remove(financialCertCorrectionCSD);

            this.unitOfWork.Save();

            return financialCertCorrectionCSD;
        }

        public IList<string> CanChangeContractReportFinancialCertCorrectionCSDStatusToEnded(int contractReportFinancialCertCorrectionCSDId)
        {
            var errors = new List<string>();

            var financialCertCorrectionCSD = this.contractReportFinancialCertCorrectionCSDsRepository.Find(contractReportFinancialCertCorrectionCSDId);

            if (!financialCertCorrectionCSD.Sign.HasValue)
            {
                errors.Add("Полето 'Знак' трябва да е попълнено");
            }

            if (!financialCertCorrectionCSD.CertifiedBgAmount.HasValue ||
                !financialCertCorrectionCSD.CertifiedEuAmount.HasValue ||
                !financialCertCorrectionCSD.CertifiedBfpTotalAmount.HasValue ||
                !financialCertCorrectionCSD.CertifiedSelfAmount.HasValue ||
                !financialCertCorrectionCSD.CertifiedTotalAmount.HasValue)
            {
                errors.Add("Всички полета от секция 'Изравнена сертифицирана сума на разходооправдателния документ за конкретния бюджетен ред и дейност' трябва да са попълнени");
            }

            return errors;
        }

        public Eumis.Domain.Contracts.ContractReportFinancialCertCorrectionCSD ChangeContractReportFinancialCertCorrectionCSDStatus(int contractReportFinancialCertCorrectionCSDId, byte[] version, ContractReportFinancialCertCorrectionCSDStatus status)
        {
            var financialCertCorrectionCSD = this.contractReportFinancialCertCorrectionCSDsRepository.FindForUpdate(contractReportFinancialCertCorrectionCSDId, version);

            var financialCertCorrection = this.contractReportFinancialCertCorrectionsRepository.Find(financialCertCorrectionCSD.ContractReportFinancialCertCorrectionId);

            this.AssertIsDraftContractReportFinancialCertCorrection(financialCertCorrection.Status);

            switch (status)
            {
                case ContractReportFinancialCertCorrectionCSDStatus.Draft:
                    if (financialCertCorrectionCSD.Status != ContractReportFinancialCertCorrectionCSDStatus.Ended)
                    {
                        throw new DomainException("ContractReportFinancialCertCorrectionCSD status transition not allowed");
                    }

                    break;
                case ContractReportFinancialCertCorrectionCSDStatus.Ended:
                    if (financialCertCorrectionCSD.Status != ContractReportFinancialCertCorrectionCSDStatus.Draft)
                    {
                        throw new DomainException("ContractReportFinancialCertCorrectionCSD status transition not allowed");
                    }

                    break;
            }

            if (status == ContractReportFinancialCertCorrectionCSDStatus.Ended)
            {
                financialCertCorrectionCSD.CheckedByUserId = this.accessContext.UserId;
                financialCertCorrectionCSD.CheckedDate = DateTime.Now;
            }
            else if (status == ContractReportFinancialCertCorrectionCSDStatus.Draft)
            {
                financialCertCorrectionCSD.CheckedByUserId = null;
                financialCertCorrectionCSD.CheckedDate = null;
            }

            financialCertCorrectionCSD.Status = status;
            financialCertCorrectionCSD.ModifyDate = DateTime.Now;

            this.unitOfWork.Save();

            return financialCertCorrectionCSD;
        }

        private void AssertIsDraftContractReportFinancialCertCorrectionCSD(ContractReportFinancialCertCorrectionCSDStatus status)
        {
            if (status != ContractReportFinancialCertCorrectionCSDStatus.Draft)
            {
                throw new DomainException("Cannot edit ContractReportFinancialCertCorrectionCSD when not in 'Draft' status");
            }
        }
    }
}
