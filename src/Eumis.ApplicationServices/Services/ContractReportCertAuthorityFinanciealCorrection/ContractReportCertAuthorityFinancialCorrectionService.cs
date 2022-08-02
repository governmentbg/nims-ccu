using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.ContractReportCertAuthorityFinancialCorrections.Repositories;
using Eumis.Data.ContractReports.Repositories;
using Eumis.Data.Contracts.Repositories;
using Eumis.Domain;
using Eumis.Domain.Contracts;
using Eumis.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.ApplicationServices.Services.ContractReportCertAuthorityFinancialCorrectionService
{
    internal class ContractReportCertAuthorityFinancialCorrectionService : IContractReportCertAuthorityFinancialCorrectionService
    {
        private IUnitOfWork unitOfWork;
        private IAccessContext accessContext;
        private IContractsRepository contractsRepository;
        private IContractReportsRepository contractReportsRepository;
        private IContractReportFinancialsRepository contractReportFinancialsRepository;
        private IContractReportCertAuthorityFinancialCorrectionsRepository contractReportCertAuthorityFinancialCorrectionsRepository;
        private IContractReportCertAuthorityFinancialCorrectionCSDsRepository contractReportCertAuthorityFinancialCorrectionCSDsRepository;

        public ContractReportCertAuthorityFinancialCorrectionService(
            IUnitOfWork unitOfWork,
            IAccessContext accessContext,
            IContractsRepository contractsRepository,
            IContractReportsRepository contractReportsRepository,
            IContractReportFinancialsRepository contractReportFinancialsRepository,
            IContractReportCertAuthorityFinancialCorrectionsRepository contractReportCertAuthorityFinancialCorrectionsRepository,
            IContractReportCertAuthorityFinancialCorrectionCSDsRepository contractReportCertAuthorityFinancialCorrectionCSDsRepository)
        {
            this.unitOfWork = unitOfWork;
            this.accessContext = accessContext;
            this.contractsRepository = contractsRepository;
            this.contractReportsRepository = contractReportsRepository;
            this.contractReportFinancialsRepository = contractReportFinancialsRepository;
            this.contractReportCertAuthorityFinancialCorrectionsRepository = contractReportCertAuthorityFinancialCorrectionsRepository;
            this.contractReportCertAuthorityFinancialCorrectionCSDsRepository = contractReportCertAuthorityFinancialCorrectionCSDsRepository;
        }

        public IList<string> CanCreateContractReportCertAuthorityFinancialCorrection(string contractNum, string contractReportNum)
        {
            var errors = new List<string>();

            var contract = this.contractsRepository.FindByRegNumber(contractNum);
            var contractReport = this.contractReportsRepository.FindByNum(contract.ContractId, contractReportNum);

            var canCreateContractReport = this.contractReportCertAuthorityFinancialCorrectionsRepository.CanCreate(contractReport.ContractReportId);
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

        public Eumis.Domain.Contracts.ContractReportCertAuthorityFinancialCorrection CreateContractReportCertAuthorityFinancialCorrection(string contractNum, string contractReportNum)
        {
            if (this.CanCreateContractReportCertAuthorityFinancialCorrection(contractNum, contractReportNum).Any())
            {
                throw new DomainException("Cannot create ContractReportCertAuthorityFinancialCorrection");
            }

            var contract = this.contractsRepository.FindByRegNumber(contractNum);
            var contractReport = this.contractReportsRepository.FindByNum(contract.ContractId, contractReportNum);
            var actualFinancial = this.contractReportFinancialsRepository.GetActualContractReportFinancial(contractReport.ContractReportId);

            var newContractReportCertAuthorityFinancialCorrection = new Eumis.Domain.Contracts.ContractReportCertAuthorityFinancialCorrection(
                actualFinancial.ContractReportFinancialId,
                contractReport.ContractReportId,
                contractReport.ContractId,
                this.contractReportCertAuthorityFinancialCorrectionsRepository.GetNextOrderNum(contractReport.ContractId));

            this.contractReportCertAuthorityFinancialCorrectionsRepository.Add(newContractReportCertAuthorityFinancialCorrection);

            this.unitOfWork.Save();

            return newContractReportCertAuthorityFinancialCorrection;
        }

        public Eumis.Domain.Contracts.ContractReportCertAuthorityFinancialCorrection UpdateContractReportCertAuthorityFinancialCorrection(
            int contractReportCertAuthorityFinancialCorrectionId,
            byte[] version,
            DateTime? certCorrectionDate,
            Guid? blobKey,
            string notes)
        {
            var certAuthorityFinancialCorrection = this.contractReportCertAuthorityFinancialCorrectionsRepository.FindForUpdate(contractReportCertAuthorityFinancialCorrectionId, version);

            this.AssertIsDraftContractReportCertAuthorityFinancialCorrection(certAuthorityFinancialCorrection.Status);

            certAuthorityFinancialCorrection.UpdateAttributes(
                certCorrectionDate,
                blobKey,
                notes);

            this.unitOfWork.Save();

            return certAuthorityFinancialCorrection;
        }

        public IList<string> CanDeleteContractReportCertAuthorityFinancialCorrection(int contractReportCertAuthorityFinancialCorrectionId)
        {
            var errors = new List<string>();

            var certAuthorityFinancialCorrection = this.contractReportCertAuthorityFinancialCorrectionsRepository.Find(contractReportCertAuthorityFinancialCorrectionId);

            if (this.contractReportCertAuthorityFinancialCorrectionCSDsRepository.HasContractReportCertAuthorityFinancialCorrectionCSDs(contractReportCertAuthorityFinancialCorrectionId))
            {
                errors.Add("Не можете да изтриете корекцията, защото има създадени коригирани сертифицирани РОД");
            }

            return errors;
        }

        public Eumis.Domain.Contracts.ContractReportCertAuthorityFinancialCorrection DeleteContractReportCertAuthorityFinancialCorrection(int contractReportCertAuthorityFinancialCorrectionId, byte[] version)
        {
            var certAuthorityFinancialCorrection = this.contractReportCertAuthorityFinancialCorrectionsRepository.FindForUpdate(contractReportCertAuthorityFinancialCorrectionId, version);

            if (certAuthorityFinancialCorrection.Status != ContractReportCertAuthorityFinancialCorrectionStatus.Draft)
            {
                throw new DomainException("Cannot delete a ContractReportCertAuthorityFinancialCorrection with status different from 'Draft'");
            }

            this.contractReportCertAuthorityFinancialCorrectionsRepository.Remove(certAuthorityFinancialCorrection);

            this.unitOfWork.Save();

            return certAuthorityFinancialCorrection;
        }

        public IList<string> CanChangeContractReportCertAuthorityFinancialCorrectionStatusToEnded(int contractReportCertAuthorityFinancialCorrectionId)
        {
            var errors = new List<string>();

            var certAuthorityFinancialCorrection = this.contractReportCertAuthorityFinancialCorrectionsRepository.Find(contractReportCertAuthorityFinancialCorrectionId);

            if (!certAuthorityFinancialCorrection.CertCorrectionDate.HasValue)
            {
                errors.Add("Полето 'Дата на корекция' трябва да е попълнено");
            }

            if (!this.contractReportCertAuthorityFinancialCorrectionCSDsRepository.HasContractReportCertAuthorityFinancialCorrectionCSDs(contractReportCertAuthorityFinancialCorrectionId))
            {
                errors.Add("Не можете да приключите коригирането, защото трябва да има поне един коригиран верифициран РОД");
            }

            if (this.contractReportCertAuthorityFinancialCorrectionCSDsRepository.HasDraftContractReportCertAuthorityFinancialCorrectionCSDs(contractReportCertAuthorityFinancialCorrectionId))
            {
                errors.Add("Не можете да приключите коригирането, защото всички коригирани сертифицирани РОД трябва да са със статус 'Приключен'");
            }

            return errors;
        }

        public IList<string> CanChangeContractReportCertAuthorityFinancialCorrectionStatusToDraft(int contractReportCertAuthorityFinancialCorrectionId)
        {
            var errors = new List<string>();

            if (this.contractReportCertAuthorityFinancialCorrectionsRepository.IsIncludedInCertReport(contractReportCertAuthorityFinancialCorrectionId))
            {
                errors.Add("Не можете да промените статуса на коригирането на 'Чернова', защото то е включено в доклад по сертификация");
            }

            return errors;
        }

        public Eumis.Domain.Contracts.ContractReportCertAuthorityFinancialCorrection ChangeContractReportCertAuthorityFinancialCorrectionStatus(int contractReportCertAuthorityFinancialCorrectionId, byte[] version, ContractReportCertAuthorityFinancialCorrectionStatus status)
        {
            var certAuthorityFinancialCorrection = this.contractReportCertAuthorityFinancialCorrectionsRepository.FindForUpdate(contractReportCertAuthorityFinancialCorrectionId, version);

            if (status == ContractReportCertAuthorityFinancialCorrectionStatus.Ended)
            {
                certAuthorityFinancialCorrection.CheckedByUserId = this.accessContext.UserId;
                certAuthorityFinancialCorrection.CheckedDate = DateTime.Now;
            }

            certAuthorityFinancialCorrection.Status = status;
            certAuthorityFinancialCorrection.ModifyDate = DateTime.Now;

            this.unitOfWork.Save();

            return certAuthorityFinancialCorrection;
        }

        private void AssertIsDraftContractReportCertAuthorityFinancialCorrection(ContractReportCertAuthorityFinancialCorrectionStatus status)
        {
            if (status != ContractReportCertAuthorityFinancialCorrectionStatus.Draft)
            {
                throw new DomainException("Cannot edit ContractReportCertAuthorityFinancialCorrection when not in 'Draft' status");
            }
        }

        public Eumis.Domain.Contracts.ContractReportCertAuthorityFinancialCorrectionCSD CreateContractReportCertAuthorityFinancialCorrectionCSD(
            int contractReportCertAuthorityFinancialCorrectionId,
            int contractReportFinancialCSDBudgetItemId)
        {
            var certAuthorityFinancialCorrection = this.contractReportCertAuthorityFinancialCorrectionsRepository.Find(contractReportCertAuthorityFinancialCorrectionId);

            this.AssertIsDraftContractReportCertAuthorityFinancialCorrection(certAuthorityFinancialCorrection.Status);

            var newContractReportCertAuthorityFinancialCorrectionCSD = new Eumis.Domain.Contracts.ContractReportCertAuthorityFinancialCorrectionCSD(
                contractReportCertAuthorityFinancialCorrectionId,
                contractReportFinancialCSDBudgetItemId,
                certAuthorityFinancialCorrection.ContractReportFinancialId,
                certAuthorityFinancialCorrection.ContractReportId,
                certAuthorityFinancialCorrection.ContractId);

            this.contractReportCertAuthorityFinancialCorrectionCSDsRepository.Add(newContractReportCertAuthorityFinancialCorrectionCSD);

            this.unitOfWork.Save();

            return newContractReportCertAuthorityFinancialCorrectionCSD;
        }

        public Eumis.Domain.Contracts.ContractReportCertAuthorityFinancialCorrectionCSD UpdateContractReportCertAuthorityFinancialCorrectionCSD(
            int contractReportCertAuthorityFinancialCorrectionCSDId,
            byte[] version,
            Sign? sign,
            string notes,
            decimal? certifiedEuAmount,
            decimal? certifiedBgAmount,
            decimal? certifiedBfpTotalAmount,
            decimal? certifiedSelfAmount,
            decimal? certifiedTotalAmount)
        {
            var certAuthorityFinancialCorrectionCSD = this.contractReportCertAuthorityFinancialCorrectionCSDsRepository.FindForUpdate(contractReportCertAuthorityFinancialCorrectionCSDId, version);

            var certAuthorityFinancialCorrection = this.contractReportCertAuthorityFinancialCorrectionsRepository.Find(certAuthorityFinancialCorrectionCSD.ContractReportCertAuthorityFinancialCorrectionId);

            this.AssertIsDraftContractReportCertAuthorityFinancialCorrection(certAuthorityFinancialCorrection.Status);

            this.AssertIsDraftContractReportCertAuthorityFinancialCorrectionCSD(certAuthorityFinancialCorrectionCSD.Status);

            certAuthorityFinancialCorrectionCSD.UpdateAttributes(
                sign,
                notes,
                certifiedEuAmount,
                certifiedBgAmount,
                certifiedBfpTotalAmount,
                certifiedSelfAmount,
                certifiedTotalAmount);

            this.unitOfWork.Save();

            return certAuthorityFinancialCorrectionCSD;
        }

        public Eumis.Domain.Contracts.ContractReportCertAuthorityFinancialCorrectionCSD DeleteContractReportCertAuthorityFinancialCorrectionCSD(int contractReportCertAuthorityFinancialCorrectionCSDId, byte[] version)
        {
            var certAuthorityFinancialCorrectionCSD = this.contractReportCertAuthorityFinancialCorrectionCSDsRepository.FindForUpdate(contractReportCertAuthorityFinancialCorrectionCSDId, version);

            var certAuthorityFinancialCorrection = this.contractReportCertAuthorityFinancialCorrectionsRepository.Find(certAuthorityFinancialCorrectionCSD.ContractReportCertAuthorityFinancialCorrectionId);

            this.AssertIsDraftContractReportCertAuthorityFinancialCorrection(certAuthorityFinancialCorrection.Status);

            this.AssertIsDraftContractReportCertAuthorityFinancialCorrectionCSD(certAuthorityFinancialCorrectionCSD.Status);

            this.contractReportCertAuthorityFinancialCorrectionCSDsRepository.Remove(certAuthorityFinancialCorrectionCSD);

            this.unitOfWork.Save();

            return certAuthorityFinancialCorrectionCSD;
        }

        public IList<string> CanChangeContractReportCertAuthorityFinancialCorrectionCSDStatusToEnded(int contractReportCertAuthorityFinancialCorrectionCSDId)
        {
            var errors = new List<string>();

            var certAuthorityFinancialCorrectionCSD = this.contractReportCertAuthorityFinancialCorrectionCSDsRepository.Find(contractReportCertAuthorityFinancialCorrectionCSDId);

            if (!certAuthorityFinancialCorrectionCSD.Sign.HasValue)
            {
                errors.Add("Полето 'Знак' трябва да е попълнено");
            }

            if (!certAuthorityFinancialCorrectionCSD.CertifiedBgAmount.HasValue ||
                !certAuthorityFinancialCorrectionCSD.CertifiedEuAmount.HasValue ||
                !certAuthorityFinancialCorrectionCSD.CertifiedBfpTotalAmount.HasValue ||
                !certAuthorityFinancialCorrectionCSD.CertifiedSelfAmount.HasValue ||
                !certAuthorityFinancialCorrectionCSD.CertifiedTotalAmount.HasValue)
            {
                errors.Add("Всички полета от секция 'Коригирана сертифицирана сума на разходооправдателния документ за конкретния бюджетен ред и дейност' трябва да са попълнени");
            }

            return errors;
        }

        public Eumis.Domain.Contracts.ContractReportCertAuthorityFinancialCorrectionCSD ChangeContractReportCertAuthorityFinancialCorrectionCSDStatus(int contractReportCertAuthorityFinancialCorrectionCSDId, byte[] version, ContractReportCertAuthorityFinancialCorrectionCSDStatus status)
        {
            var certAuthorityFinancialCorrectionCSD = this.contractReportCertAuthorityFinancialCorrectionCSDsRepository.FindForUpdate(contractReportCertAuthorityFinancialCorrectionCSDId, version);

            var certAuthorityFinancialCorrection = this.contractReportCertAuthorityFinancialCorrectionsRepository.Find(certAuthorityFinancialCorrectionCSD.ContractReportCertAuthorityFinancialCorrectionId);

            this.AssertIsDraftContractReportCertAuthorityFinancialCorrection(certAuthorityFinancialCorrection.Status);

            switch (status)
            {
                case ContractReportCertAuthorityFinancialCorrectionCSDStatus.Draft:
                    if (certAuthorityFinancialCorrectionCSD.Status != ContractReportCertAuthorityFinancialCorrectionCSDStatus.Ended)
                    {
                        throw new DomainException("ContractReportCertAuthorityFinancialCorrectionCSD status transition not allowed");
                    }

                    break;
                case ContractReportCertAuthorityFinancialCorrectionCSDStatus.Ended:
                    if (certAuthorityFinancialCorrectionCSD.Status != ContractReportCertAuthorityFinancialCorrectionCSDStatus.Draft)
                    {
                        throw new DomainException("ContractReportCertAuthorityFinancialCorrectionCSD status transition not allowed");
                    }

                    break;
            }

            if (status == ContractReportCertAuthorityFinancialCorrectionCSDStatus.Ended)
            {
                certAuthorityFinancialCorrectionCSD.CheckedByUserId = this.accessContext.UserId;
                certAuthorityFinancialCorrectionCSD.CheckedDate = DateTime.Now;
            }
            else if (status == ContractReportCertAuthorityFinancialCorrectionCSDStatus.Draft)
            {
                certAuthorityFinancialCorrectionCSD.CheckedByUserId = null;
                certAuthorityFinancialCorrectionCSD.CheckedDate = null;
            }

            certAuthorityFinancialCorrectionCSD.Status = status;
            certAuthorityFinancialCorrectionCSD.ModifyDate = DateTime.Now;

            this.unitOfWork.Save();

            return certAuthorityFinancialCorrectionCSD;
        }

        private void AssertIsDraftContractReportCertAuthorityFinancialCorrectionCSD(ContractReportCertAuthorityFinancialCorrectionCSDStatus status)
        {
            if (status != ContractReportCertAuthorityFinancialCorrectionCSDStatus.Draft)
            {
                throw new DomainException("Cannot edit ContractReportCertAuthorityFinancialCorrectionCSD when not in 'Draft' status");
            }
        }
    }
}
