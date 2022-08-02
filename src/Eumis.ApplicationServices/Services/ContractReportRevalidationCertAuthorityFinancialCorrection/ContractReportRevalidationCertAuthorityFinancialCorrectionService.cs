using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.ContractReportRevalidationCertAuthorityFinancialCorrections.Repositories;
using Eumis.Data.ContractReports.Repositories;
using Eumis.Data.Contracts.Repositories;
using Eumis.Domain;
using Eumis.Domain.Contracts;
using Eumis.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.ApplicationServices.Services.ContractReportRevalidationCertAuthorityFinancialCorrection
{
    internal class ContractReportRevalidationCertAuthorityFinancialCorrectionService : IContractReportRevalidationCertAuthorityFinancialCorrectionService
    {
        private IUnitOfWork unitOfWork;
        private IAccessContext accessContext;
        private IContractsRepository contractsRepository;
        private IContractReportsRepository contractReportsRepository;
        private IContractReportFinancialsRepository contractReportFinancialsRepository;
        private IContractReportRevalidationCertAuthorityFinancialCorrectionsRepository contractReportRevalidationCertAuthorityFinancialCorrectionsRepository;
        private IContractReportRevalidationCertAuthorityFinancialCorrectionCSDsRepository contractReportRevalidationCertAuthorityFinancialCorrectionCSDsRepository;

        public ContractReportRevalidationCertAuthorityFinancialCorrectionService(
            IUnitOfWork unitOfWork,
            IAccessContext accessContext,
            IContractsRepository contractsRepository,
            IContractReportsRepository contractReportsRepository,
            IContractReportFinancialsRepository contractReportFinancialsRepository,
            IContractReportRevalidationCertAuthorityFinancialCorrectionsRepository contractReportRevalidationCertAuthorityFinancialCorrectionsRepository,
            IContractReportRevalidationCertAuthorityFinancialCorrectionCSDsRepository contractReportRevalidationCertAuthorityFinancialCorrectionCSDsRepository)
        {
            this.unitOfWork = unitOfWork;
            this.accessContext = accessContext;
            this.contractsRepository = contractsRepository;
            this.contractReportsRepository = contractReportsRepository;
            this.contractReportFinancialsRepository = contractReportFinancialsRepository;
            this.contractReportRevalidationCertAuthorityFinancialCorrectionsRepository = contractReportRevalidationCertAuthorityFinancialCorrectionsRepository;
            this.contractReportRevalidationCertAuthorityFinancialCorrectionCSDsRepository = contractReportRevalidationCertAuthorityFinancialCorrectionCSDsRepository;
        }

        public IList<string> CanCreateContractReportRevalidationCertAuthorityFinancialCorrection(string contractNum, string contractReportNum)
        {
            var errors = new List<string>();

            var contract = this.contractsRepository.FindByRegNumber(contractNum);
            var contractReport = this.contractReportsRepository.FindByNum(contract.ContractId, contractReportNum);

            var canCreateContractReport = this.contractReportRevalidationCertAuthorityFinancialCorrectionsRepository.CanCreate(contractReport.ContractReportId);
            if (!canCreateContractReport)
            {
                errors.Add("Не може да се създаде нова корекция на сертифицирани препотвърдени суми, защото вече съществува такава със статус 'Чернова' към същия пакет отчетни документи.");
            }
            else if (contractReport.Status != ContractReportStatus.Accepted)
            {
                errors.Add("Не може да се създаде нова корекция на сертифицирани препотвърдени суми, защото статуса на пакета отчетни документи не е 'Приет'.");
            }
            else
            {
                var actualFinancial = this.contractReportFinancialsRepository.GetActualContractReportFinancial(contractReport.ContractReportId);
                if (actualFinancial == null)
                {
                    errors.Add("Не може да се създаде нова корекция на сертифицирани препотвърдени суми, защото в пакета отчетни документи няма финансов отчет.");
                }
            }

            return errors;
        }

        public Eumis.Domain.Contracts.ContractReportRevalidationCertAuthorityFinancialCorrection CreateContractReportRevalidationCertAuthorityFinancialCorrection(string contractNum, string contractReportNum)
        {
            if (this.CanCreateContractReportRevalidationCertAuthorityFinancialCorrection(contractNum, contractReportNum).Any())
            {
                throw new DomainException("Cannot create ContractReportRevalidationCertAuthorityFinancialCorrection");
            }

            var contract = this.contractsRepository.FindByRegNumber(contractNum);
            var contractReport = this.contractReportsRepository.FindByNum(contract.ContractId, contractReportNum);
            var actualFinancial = this.contractReportFinancialsRepository.GetActualContractReportFinancial(contractReport.ContractReportId);

            var newContractReportRevalidationCertAuthorityFinancialCorrection = new Eumis.Domain.Contracts.ContractReportRevalidationCertAuthorityFinancialCorrection(
                actualFinancial.ContractReportFinancialId,
                contractReport.ContractReportId,
                contractReport.ContractId,
                this.contractReportRevalidationCertAuthorityFinancialCorrectionsRepository.GetNextOrderNum(contractReport.ContractId));

            this.contractReportRevalidationCertAuthorityFinancialCorrectionsRepository.Add(newContractReportRevalidationCertAuthorityFinancialCorrection);

            this.unitOfWork.Save();

            return newContractReportRevalidationCertAuthorityFinancialCorrection;
        }

        public Eumis.Domain.Contracts.ContractReportRevalidationCertAuthorityFinancialCorrection UpdateContractReportRevalidationCertAuthorityFinancialCorrection(
            int contractReportRevalidationCertAuthorityFinancialCorrectionId,
            byte[] version,
            DateTime? certCorrectionDate,
            Guid? blobKey,
            string notes)
        {
            var certAuthorityFinancialCorrection = this.contractReportRevalidationCertAuthorityFinancialCorrectionsRepository.FindForUpdate(contractReportRevalidationCertAuthorityFinancialCorrectionId, version);

            this.AssertIsDraftContractReportRevalidationCertAuthorityFinancialCorrection(certAuthorityFinancialCorrection.Status);

            certAuthorityFinancialCorrection.UpdateAttributes(
                certCorrectionDate,
                blobKey,
                notes);

            this.unitOfWork.Save();

            return certAuthorityFinancialCorrection;
        }

        public IList<string> CanDeleteContractReportRevalidationCertAuthorityFinancialCorrection(int contractReportRevalidationCertAuthorityFinancialCorrectionId)
        {
            var errors = new List<string>();

            if (this.contractReportRevalidationCertAuthorityFinancialCorrectionCSDsRepository.HasContractReportRevalidationCertAuthorityFinancialCorrectionCSDs(contractReportRevalidationCertAuthorityFinancialCorrectionId))
            {
                errors.Add("Не можете да изтриете корекцията, защото има създадени коригирани сертифицирани препотвърдени РОД");
            }

            return errors;
        }

        public void DeleteContractReportRevalidationCertAuthorityFinancialCorrection(int contractReportRevalidationCertAuthorityFinancialCorrectionId, byte[] version)
        {
            var certAuthorityFinancialCorrection = this.contractReportRevalidationCertAuthorityFinancialCorrectionsRepository.FindForUpdate(contractReportRevalidationCertAuthorityFinancialCorrectionId, version);

            if (certAuthorityFinancialCorrection.Status != ContractReportRevalidationCertAuthorityFinancialCorrectionStatus.Draft)
            {
                throw new DomainException("Cannot delete a ContractReportRevalidationCertAuthorityFinancialCorrection with status different from 'Draft'");
            }

            this.contractReportRevalidationCertAuthorityFinancialCorrectionsRepository.Remove(certAuthorityFinancialCorrection);

            this.unitOfWork.Save();
        }

        public IList<string> CanChangeContractReportRevalidationCertAuthorityFinancialCorrectionStatusToEnded(int contractReportRevalidationCertAuthorityFinancialCorrectionId)
        {
            var errors = new List<string>();

            var certAuthorityFinancialCorrection = this.contractReportRevalidationCertAuthorityFinancialCorrectionsRepository.Find(contractReportRevalidationCertAuthorityFinancialCorrectionId);

            if (!certAuthorityFinancialCorrection.CertCorrectionDate.HasValue)
            {
                errors.Add("Полето 'Дата на корекция' трябва да е попълнено");
            }

            if (!this.contractReportRevalidationCertAuthorityFinancialCorrectionCSDsRepository.HasContractReportRevalidationCertAuthorityFinancialCorrectionCSDs(contractReportRevalidationCertAuthorityFinancialCorrectionId))
            {
                errors.Add("Не можете да приключите коригирането, защото трябва да има поне един коригиран верифициран РОД");
            }

            if (this.contractReportRevalidationCertAuthorityFinancialCorrectionCSDsRepository.HasDraftContractReportRevalidationCertAuthorityFinancialCorrectionCSDs(contractReportRevalidationCertAuthorityFinancialCorrectionId))
            {
                errors.Add("Не можете да приключите коригирането, защото всички коригирани сертифицирани РОД трябва да са със статус 'Приключен'");
            }

            return errors;
        }

        public IList<string> CanChangeContractReportRevalidationCertAuthorityFinancialCorrectionStatusToDraft(int contractReportRevalidationCertAuthorityFinancialCorrectionId)
        {
            var errors = new List<string>();

            if (this.contractReportRevalidationCertAuthorityFinancialCorrectionsRepository.IsIncludedInCertReport(contractReportRevalidationCertAuthorityFinancialCorrectionId))
            {
                errors.Add("Не можете да промените статуса на коригирането на 'Чернова', защото то е включено в доклад по сертификация");
            }

            return errors;
        }

        public Eumis.Domain.Contracts.ContractReportRevalidationCertAuthorityFinancialCorrection ChangeContractReportRevalidationCertAuthorityFinancialCorrectionStatus(int contractReportRevalidationCertAuthorityFinancialCorrectionId, byte[] version, ContractReportRevalidationCertAuthorityFinancialCorrectionStatus status)
        {
            var certAuthorityFinancialCorrection = this.contractReportRevalidationCertAuthorityFinancialCorrectionsRepository.FindForUpdate(contractReportRevalidationCertAuthorityFinancialCorrectionId, version);

            if (status == ContractReportRevalidationCertAuthorityFinancialCorrectionStatus.Ended)
            {
                certAuthorityFinancialCorrection.CheckedByUserId = this.accessContext.UserId;
                certAuthorityFinancialCorrection.CheckedDate = DateTime.Now;
            }

            certAuthorityFinancialCorrection.Status = status;
            certAuthorityFinancialCorrection.ModifyDate = DateTime.Now;

            this.unitOfWork.Save();

            return certAuthorityFinancialCorrection;
        }

        private void AssertIsDraftContractReportRevalidationCertAuthorityFinancialCorrection(ContractReportRevalidationCertAuthorityFinancialCorrectionStatus status)
        {
            if (status != ContractReportRevalidationCertAuthorityFinancialCorrectionStatus.Draft)
            {
                throw new DomainException("Cannot edit ContractReportRevalidationCertAuthorityFinancialCorrection when not in 'Draft' status");
            }
        }

        public Eumis.Domain.Contracts.ContractReportRevalidationCertAuthorityFinancialCorrectionCSD CreateContractReportRevalidationCertAuthorityFinancialCorrectionCSD(
            int contractReportRevalidationCertAuthorityFinancialCorrectionId,
            int contractReportFinancialCSDBudgetItemId)
        {
            var certAuthorityFinancialCorrection = this.contractReportRevalidationCertAuthorityFinancialCorrectionsRepository.Find(contractReportRevalidationCertAuthorityFinancialCorrectionId);

            this.AssertIsDraftContractReportRevalidationCertAuthorityFinancialCorrection(certAuthorityFinancialCorrection.Status);

            var newContractReportRevalidationCertAuthorityFinancialCorrectionCSD = new Eumis.Domain.Contracts.ContractReportRevalidationCertAuthorityFinancialCorrectionCSD(
                contractReportRevalidationCertAuthorityFinancialCorrectionId,
                contractReportFinancialCSDBudgetItemId,
                certAuthorityFinancialCorrection.ContractReportFinancialId,
                certAuthorityFinancialCorrection.ContractReportId,
                certAuthorityFinancialCorrection.ContractId);

            this.contractReportRevalidationCertAuthorityFinancialCorrectionCSDsRepository.Add(newContractReportRevalidationCertAuthorityFinancialCorrectionCSD);

            this.unitOfWork.Save();

            return newContractReportRevalidationCertAuthorityFinancialCorrectionCSD;
        }

        public Eumis.Domain.Contracts.ContractReportRevalidationCertAuthorityFinancialCorrectionCSD UpdateContractReportRevalidationCertAuthorityFinancialCorrectionCSD(
            int contractReportRevalidationCertAuthorityFinancialCorrectionCSDId,
            byte[] version,
            Sign? sign,
            string notes,
            decimal? revalidatedEuAmount,
            decimal? revalidatedBgAmount,
            decimal? revalidatedBfpTotalAmount,
            decimal? revalidatedSelfAmount,
            decimal? revalidatedTotalAmount)
        {
            var revalidationCertAuthorityFinancialCorrectionCSD = this.contractReportRevalidationCertAuthorityFinancialCorrectionCSDsRepository.FindForUpdate(contractReportRevalidationCertAuthorityFinancialCorrectionCSDId, version);

            var revalidationCertAuthorityFinancialCorrection = this.contractReportRevalidationCertAuthorityFinancialCorrectionsRepository.Find(revalidationCertAuthorityFinancialCorrectionCSD.ContractReportRevalidationCertAuthorityFinancialCorrectionId);

            this.AssertIsDraftContractReportRevalidationCertAuthorityFinancialCorrection(revalidationCertAuthorityFinancialCorrection.Status);

            this.AssertIsDraftContractReportRevalidationCertAuthorityFinancialCorrectionCSD(revalidationCertAuthorityFinancialCorrectionCSD.Status);

            revalidationCertAuthorityFinancialCorrectionCSD.UpdateAttributes(
                sign,
                notes,
                revalidatedEuAmount,
                revalidatedBgAmount,
                revalidatedBfpTotalAmount,
                revalidatedSelfAmount,
                revalidatedTotalAmount);

            this.unitOfWork.Save();

            return revalidationCertAuthorityFinancialCorrectionCSD;
        }

        public void DeleteContractReportRevalidationCertAuthorityFinancialCorrectionCSD(int contractReportRevalidationCertAuthorityFinancialCorrectionCSDId, byte[] version)
        {
            var certAuthorityFinancialCorrectionCSD = this.contractReportRevalidationCertAuthorityFinancialCorrectionCSDsRepository.FindForUpdate(contractReportRevalidationCertAuthorityFinancialCorrectionCSDId, version);

            var certAuthorityFinancialCorrection = this.contractReportRevalidationCertAuthorityFinancialCorrectionsRepository.Find(certAuthorityFinancialCorrectionCSD.ContractReportRevalidationCertAuthorityFinancialCorrectionId);

            this.AssertIsDraftContractReportRevalidationCertAuthorityFinancialCorrection(certAuthorityFinancialCorrection.Status);

            this.AssertIsDraftContractReportRevalidationCertAuthorityFinancialCorrectionCSD(certAuthorityFinancialCorrectionCSD.Status);

            this.contractReportRevalidationCertAuthorityFinancialCorrectionCSDsRepository.Remove(certAuthorityFinancialCorrectionCSD);

            this.unitOfWork.Save();
        }

        public IList<string> CanChangeContractReportRevalidationCertAuthorityFinancialCorrectionCSDStatusToEnded(int contractReportRevalidationCertAuthorityFinancialCorrectionCSDId)
        {
            var errors = new List<string>();

            var certAuthorityFinancialCorrectionCSD = this.contractReportRevalidationCertAuthorityFinancialCorrectionCSDsRepository.Find(contractReportRevalidationCertAuthorityFinancialCorrectionCSDId);

            if (!certAuthorityFinancialCorrectionCSD.Sign.HasValue)
            {
                errors.Add("Полето 'Знак' трябва да е попълнено");
            }

            if (!certAuthorityFinancialCorrectionCSD.RevalidatedBgAmount.HasValue ||
                !certAuthorityFinancialCorrectionCSD.RevalidatedEuAmount.HasValue ||
                !certAuthorityFinancialCorrectionCSD.RevalidatedBfpTotalAmount.HasValue ||
                !certAuthorityFinancialCorrectionCSD.RevalidatedSelfAmount.HasValue ||
                !certAuthorityFinancialCorrectionCSD.RevalidatedTotalAmount.HasValue)
            {
                errors.Add("Всички полета от секция 'Коригирана сертифицирана препотвърдена сума на разходооправдателния документ за конкретния бюджетен ред и дейност' трябва да са попълнени");
            }

            return errors;
        }

        public Eumis.Domain.Contracts.ContractReportRevalidationCertAuthorityFinancialCorrectionCSD ChangeContractReportRevalidationCertAuthorityFinancialCorrectionCSDStatus(int contractReportRevalidationCertAuthorityFinancialCorrectionCSDId, byte[] version, ContractReportRevalidationCertAuthorityFinancialCorrectionCSDStatus status)
        {
            var certAuthorityFinancialCorrectionCSD = this.contractReportRevalidationCertAuthorityFinancialCorrectionCSDsRepository.FindForUpdate(contractReportRevalidationCertAuthorityFinancialCorrectionCSDId, version);

            var certAuthorityFinancialCorrection = this.contractReportRevalidationCertAuthorityFinancialCorrectionsRepository.Find(certAuthorityFinancialCorrectionCSD.ContractReportRevalidationCertAuthorityFinancialCorrectionId);

            this.AssertIsDraftContractReportRevalidationCertAuthorityFinancialCorrection(certAuthorityFinancialCorrection.Status);

            switch (status)
            {
                case ContractReportRevalidationCertAuthorityFinancialCorrectionCSDStatus.Draft:
                    if (certAuthorityFinancialCorrectionCSD.Status != ContractReportRevalidationCertAuthorityFinancialCorrectionCSDStatus.Ended)
                    {
                        throw new DomainException("ContractReportRevalidationCertAuthorityFinancialCorrectionCSD status transition not allowed");
                    }

                    break;

                case ContractReportRevalidationCertAuthorityFinancialCorrectionCSDStatus.Ended:
                    if (certAuthorityFinancialCorrectionCSD.Status != ContractReportRevalidationCertAuthorityFinancialCorrectionCSDStatus.Draft)
                    {
                        throw new DomainException("ContractReportRevalidationCertAuthorityFinancialCorrectionCSD status transition not allowed");
                    }

                    break;
            }

            if (status == ContractReportRevalidationCertAuthorityFinancialCorrectionCSDStatus.Ended)
            {
                certAuthorityFinancialCorrectionCSD.CheckedByUserId = this.accessContext.UserId;
                certAuthorityFinancialCorrectionCSD.CheckedDate = DateTime.Now;
            }
            else if (status == ContractReportRevalidationCertAuthorityFinancialCorrectionCSDStatus.Draft)
            {
                certAuthorityFinancialCorrectionCSD.CheckedByUserId = null;
                certAuthorityFinancialCorrectionCSD.CheckedDate = null;
            }

            certAuthorityFinancialCorrectionCSD.Status = status;
            certAuthorityFinancialCorrectionCSD.ModifyDate = DateTime.Now;

            this.unitOfWork.Save();

            return certAuthorityFinancialCorrectionCSD;
        }

        private void AssertIsDraftContractReportRevalidationCertAuthorityFinancialCorrectionCSD(ContractReportRevalidationCertAuthorityFinancialCorrectionCSDStatus status)
        {
            if (status != ContractReportRevalidationCertAuthorityFinancialCorrectionCSDStatus.Draft)
            {
                throw new DomainException("Cannot edit ContractReportRevalidationCertAuthorityFinancialCorrectionCSD when not in 'Draft' status");
            }
        }
    }
}
