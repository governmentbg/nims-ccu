using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.ContractReportFinancialRevalidations.Repositories;
using Eumis.Data.ContractReports.Repositories;
using Eumis.Data.Contracts.Repositories;
using Eumis.Domain;
using Eumis.Domain.Contracts;
using Eumis.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.ApplicationServices.Services.ContractReportFinancialRevalidation
{
    internal class ContractReportFinancialRevalidationService : IContractReportFinancialRevalidationService
    {
        private IUnitOfWork unitOfWork;
        private IAccessContext accessContext;
        private IContractsRepository contractsRepository;
        private IContractReportsRepository contractReportsRepository;
        private IContractReportFinancialsRepository contractReportFinancialsRepository;
        private IContractReportFinancialRevalidationsRepository contractReportFinancialRevalidationsRepository;
        private IContractReportFinancialRevalidationCSDsRepository contractReportFinancialRevalidationCSDsRepository;

        public ContractReportFinancialRevalidationService(
            IUnitOfWork unitOfWork,
            IAccessContext accessContext,
            IContractsRepository contractsRepository,
            IContractReportsRepository contractReportsRepository,
            IContractReportFinancialsRepository contractReportFinancialsRepository,
            IContractReportFinancialRevalidationsRepository contractReportFinancialRevalidationsRepository,
            IContractReportFinancialRevalidationCSDsRepository contractReportFinancialRevalidationCSDsRepository)
        {
            this.unitOfWork = unitOfWork;
            this.accessContext = accessContext;
            this.contractsRepository = contractsRepository;
            this.contractReportsRepository = contractReportsRepository;
            this.contractReportFinancialsRepository = contractReportFinancialsRepository;
            this.contractReportFinancialRevalidationsRepository = contractReportFinancialRevalidationsRepository;
            this.contractReportFinancialRevalidationCSDsRepository = contractReportFinancialRevalidationCSDsRepository;
        }

        public IList<string> CanCreateContractReportFinancialRevalidation(string contractNum, string contractReportNum)
        {
            var errors = new List<string>();

            var contract = this.contractsRepository.FindByRegNumber(contractNum);
            var contractReport = this.contractReportsRepository.FindByNum(contract.ContractId, contractReportNum);

            var canCreateContractReport = this.contractReportFinancialRevalidationsRepository.CanCreate(contractReport.ContractReportId);
            if (!canCreateContractReport)
            {
                errors.Add("Не може да се създаде ново препотвърждаване на верифицирани суми, защото вече съществува такава със статус 'Чернова' към същия пакет отчетни документи.");
            }
            else if (contractReport.Status != ContractReportStatus.Accepted)
            {
                errors.Add("Не може да се създаде ново препотвърждаване на верифицирани суми, защото статуса на пакета отчетни документи не е 'Приет'.");
            }
            else
            {
                var actualFinancial = this.contractReportFinancialsRepository.GetActualContractReportFinancial(contractReport.ContractReportId);
                if (actualFinancial == null)
                {
                    errors.Add("Не може да се създаде ново препотвърждаване на верифицирани суми, защото в пакета отчетни документи няма финансов отчет.");
                }
            }

            return errors;
        }

        public Eumis.Domain.Contracts.ContractReportFinancialRevalidation CreateContractReportFinancialRevalidation(string contractNum, string contractReportNum)
        {
            if (this.CanCreateContractReportFinancialRevalidation(contractNum, contractReportNum).Any())
            {
                throw new DomainException("Cannot create ContractReportFinancialRevalidation");
            }

            var contract = this.contractsRepository.FindByRegNumber(contractNum);
            var contractReport = this.contractReportsRepository.FindByNum(contract.ContractId, contractReportNum);
            var actualFinancial = this.contractReportFinancialsRepository.GetActualContractReportFinancial(contractReport.ContractReportId);

            var newContractReportFinancialRevalidation = new Eumis.Domain.Contracts.ContractReportFinancialRevalidation(
                actualFinancial.ContractReportFinancialId,
                contractReport.ContractReportId,
                contractReport.ContractId,
                this.contractReportFinancialRevalidationsRepository.GetNextOrderNum(contractReport.ContractId));

            this.contractReportFinancialRevalidationsRepository.Add(newContractReportFinancialRevalidation);

            this.unitOfWork.Save();

            return newContractReportFinancialRevalidation;
        }

        public Eumis.Domain.Contracts.ContractReportFinancialRevalidation UpdateContractReportFinancialRevalidation(
            int contractReportFinancialRevalidationId,
            byte[] version,
            DateTime? revalidationDate,
            Guid? blobKey,
            string notes)
        {
            var financialRevalidation = this.contractReportFinancialRevalidationsRepository.FindForUpdate(contractReportFinancialRevalidationId, version);

            this.AssertIsDraftContractReportFinancialRevalidation(financialRevalidation.Status);

            financialRevalidation.UpdateAttributes(
                revalidationDate,
                blobKey,
                notes);

            this.unitOfWork.Save();

            return financialRevalidation;
        }

        public IList<string> CanDeleteContractReportFinancialRevalidation(int contractReportFinancialRevalidationId)
        {
            var errors = new List<string>();

            var financialRevalidation = this.contractReportFinancialRevalidationsRepository.Find(contractReportFinancialRevalidationId);

            if (this.contractReportFinancialRevalidationCSDsRepository.HasContractReportFinancialRevalidationCSDs(contractReportFinancialRevalidationId))
            {
                errors.Add("Не можете да изтриете препотвърждаването, защото има създадени препотвърдени верифицирани РОД");
            }

            return errors;
        }

        public Eumis.Domain.Contracts.ContractReportFinancialRevalidation DeleteContractReportFinancialRevalidation(int contractReportFinancialRevalidationId, byte[] version)
        {
            var financialRevalidation = this.contractReportFinancialRevalidationsRepository.FindForUpdate(contractReportFinancialRevalidationId, version);

            if (financialRevalidation.Status != ContractReportFinancialRevalidationStatus.Draft)
            {
                throw new DomainException("Cannot delete a ContractReportFinancialRevalidation with status different from 'Draft'");
            }

            this.contractReportFinancialRevalidationsRepository.Remove(financialRevalidation);

            this.unitOfWork.Save();

            return financialRevalidation;
        }

        public IList<string> CanChangeContractReportFinancialRevalidationStatusToEnded(int contractReportFinancialRevalidationId)
        {
            var errors = new List<string>();

            var financialRevalidation = this.contractReportFinancialRevalidationsRepository.Find(contractReportFinancialRevalidationId);

            if (!financialRevalidation.BlobKey.HasValue)
            {
                errors.Add("Полето 'Файл' трябва да е попълнено");
            }

            if (!financialRevalidation.RevalidationDate.HasValue)
            {
                errors.Add("Полето 'Дата на препотвърждаване' трябва да е попълнено");
            }

            if (!this.contractReportFinancialRevalidationCSDsRepository.HasContractReportFinancialRevalidationCSDs(contractReportFinancialRevalidationId))
            {
                errors.Add("Не можете да приключите препотвърждаването, защото трябва да има поне един препотвърден верифициран РОД");
            }

            if (this.contractReportFinancialRevalidationCSDsRepository.HasDraftContractReportFinancialRevalidationCSDs(contractReportFinancialRevalidationId))
            {
                errors.Add("Не можете да приключите препотвърждаването, защото всички препотвърдени верифицирани РОД трябва да са със статус 'Приключен'");
            }

            return errors;
        }

        public IList<string> CanChangeContractReportFinancialRevalidationStatusToDraft(int contractReportFinancialRevalidationId)
        {
            var errors = new List<string>();

            if (this.contractReportFinancialRevalidationsRepository.IsIncludedInCertReport(contractReportFinancialRevalidationId))
            {
                errors.Add("Не можете да промените статуса на препотвърждаването на 'Чернова', защото то е включено в доклад по сертификация");
            }

            return errors;
        }

        public Eumis.Domain.Contracts.ContractReportFinancialRevalidation ChangeContractReportFinancialRevalidationStatus(int contractReportFinancialRevalidationId, byte[] version, ContractReportFinancialRevalidationStatus status)
        {
            var financialRevalidation = this.contractReportFinancialRevalidationsRepository.FindForUpdate(contractReportFinancialRevalidationId, version);

            if (status == ContractReportFinancialRevalidationStatus.Ended)
            {
                financialRevalidation.CheckedByUserId = this.accessContext.UserId;
                financialRevalidation.CheckedDate = DateTime.Now;
            }

            financialRevalidation.Status = status;
            financialRevalidation.ModifyDate = DateTime.Now;

            this.unitOfWork.Save();

            return financialRevalidation;
        }

        private void AssertIsDraftContractReportFinancialRevalidation(ContractReportFinancialRevalidationStatus status)
        {
            if (status != ContractReportFinancialRevalidationStatus.Draft)
            {
                throw new DomainException("Cannot edit ContractReportFinancialRevalidation when not in 'Draft' status");
            }
        }

        public Eumis.Domain.Contracts.ContractReportFinancialRevalidationCSD CreateContractReportFinancialRevalidationCSD(
            int contractReportFinancialRevalidationId,
            int contractReportFinancialCSDBudgetItemId)
        {
            var financialRevalidation = this.contractReportFinancialRevalidationsRepository.Find(contractReportFinancialRevalidationId);

            this.AssertIsDraftContractReportFinancialRevalidation(financialRevalidation.Status);

            var newContractReportFinancialRevalidationCSD = new Eumis.Domain.Contracts.ContractReportFinancialRevalidationCSD(
                contractReportFinancialRevalidationId,
                contractReportFinancialCSDBudgetItemId,
                financialRevalidation.ContractReportFinancialId,
                financialRevalidation.ContractReportId,
                financialRevalidation.ContractId);

            this.contractReportFinancialRevalidationCSDsRepository.Add(newContractReportFinancialRevalidationCSD);

            this.unitOfWork.Save();

            return newContractReportFinancialRevalidationCSD;
        }

        public Eumis.Domain.Contracts.ContractReportFinancialRevalidationCSD UpdateContractReportFinancialRevalidationCSD(
            int contractReportFinancialRevalidationCSDId,
            byte[] version,
            string notes,
            decimal? revalidatedEuAmount,
            decimal? revalidatedBgAmount,
            decimal? revalidatedBfpTotalAmount,
            decimal? revalidatedSelfAmount,
            decimal? revalidatedTotalAmount)
        {
            var financialRevalidationCSD = this.contractReportFinancialRevalidationCSDsRepository.FindForUpdate(contractReportFinancialRevalidationCSDId, version);

            var financialRevalidation = this.contractReportFinancialRevalidationsRepository.Find(financialRevalidationCSD.ContractReportFinancialRevalidationId);

            this.AssertIsDraftContractReportFinancialRevalidation(financialRevalidation.Status);

            this.AssertIsDraftContractReportFinancialRevalidationCSD(financialRevalidationCSD.Status);

            financialRevalidationCSD.UpdateAttributes(
                notes,
                revalidatedEuAmount,
                revalidatedBgAmount,
                revalidatedBfpTotalAmount,
                revalidatedSelfAmount,
                revalidatedTotalAmount);

            this.unitOfWork.Save();

            return financialRevalidationCSD;
        }

        public Eumis.Domain.Contracts.ContractReportFinancialRevalidationCSD DeleteContractReportFinancialRevalidationCSD(int contractReportFinancialRevalidationCSDId, byte[] version)
        {
            var financialRevalidationCSD = this.contractReportFinancialRevalidationCSDsRepository.FindForUpdate(contractReportFinancialRevalidationCSDId, version);

            var financialRevalidation = this.contractReportFinancialRevalidationsRepository.Find(financialRevalidationCSD.ContractReportFinancialRevalidationId);

            this.AssertIsDraftContractReportFinancialRevalidation(financialRevalidation.Status);

            this.AssertIsDraftContractReportFinancialRevalidationCSD(financialRevalidationCSD.Status);

            this.contractReportFinancialRevalidationCSDsRepository.Remove(financialRevalidationCSD);

            this.unitOfWork.Save();

            return financialRevalidationCSD;
        }

        public IList<string> CanChangeContractReportFinancialRevalidationCSDStatusToEnded(int contractReportFinancialRevalidationCSDId)
        {
            var errors = new List<string>();

            var financialRevalidationCSD = this.contractReportFinancialRevalidationCSDsRepository.Find(contractReportFinancialRevalidationCSDId);

            if (!financialRevalidationCSD.Sign.HasValue)
            {
                errors.Add("Полето 'Знак' трябва да е попълнено");
            }

            if (!financialRevalidationCSD.RevalidatedBgAmount.HasValue ||
                !financialRevalidationCSD.RevalidatedEuAmount.HasValue ||
                !financialRevalidationCSD.RevalidatedBfpTotalAmount.HasValue ||
                !financialRevalidationCSD.RevalidatedSelfAmount.HasValue ||
                !financialRevalidationCSD.RevalidatedTotalAmount.HasValue)
            {
                errors.Add("Всички полета от секция 'Препотвърдена верифицирана сума на разходооправдателния документ за конкретния бюджетен ред и дейност' трябва да са попълнени");
            }

            return errors;
        }

        public Eumis.Domain.Contracts.ContractReportFinancialRevalidationCSD ChangeContractReportFinancialRevalidationCSDStatus(int contractReportFinancialRevalidationCSDId, byte[] version, ContractReportFinancialRevalidationCSDStatus status)
        {
            var financialRevalidationCSD = this.contractReportFinancialRevalidationCSDsRepository.FindForUpdate(contractReportFinancialRevalidationCSDId, version);

            var financialRevalidation = this.contractReportFinancialRevalidationsRepository.Find(financialRevalidationCSD.ContractReportFinancialRevalidationId);

            this.AssertIsDraftContractReportFinancialRevalidation(financialRevalidation.Status);

            switch (status)
            {
                case ContractReportFinancialRevalidationCSDStatus.Draft:
                    if (financialRevalidationCSD.Status != ContractReportFinancialRevalidationCSDStatus.Ended)
                    {
                        throw new DomainException("ContractReportFinancialRevalidationCSD status transition not allowed");
                    }

                    break;
                case ContractReportFinancialRevalidationCSDStatus.Ended:
                    if (financialRevalidationCSD.Status != ContractReportFinancialRevalidationCSDStatus.Draft)
                    {
                        throw new DomainException("ContractReportFinancialRevalidationCSD status transition not allowed");
                    }

                    break;
            }

            if (status == ContractReportFinancialRevalidationCSDStatus.Ended)
            {
                financialRevalidationCSD.CheckedByUserId = this.accessContext.UserId;
                financialRevalidationCSD.CheckedDate = DateTime.Now;
            }
            else if (status == ContractReportFinancialRevalidationCSDStatus.Draft)
            {
                financialRevalidationCSD.CheckedByUserId = null;
                financialRevalidationCSD.CheckedDate = null;
            }

            financialRevalidationCSD.Status = status;
            financialRevalidationCSD.ModifyDate = DateTime.Now;

            this.unitOfWork.Save();

            return financialRevalidationCSD;
        }

        private void AssertIsDraftContractReportFinancialRevalidationCSD(ContractReportFinancialRevalidationCSDStatus status)
        {
            if (status != ContractReportFinancialRevalidationCSDStatus.Draft)
            {
                throw new DomainException("Cannot edit ContractReportFinancialRevalidationCSD when not in 'Draft' status");
            }
        }
    }
}
