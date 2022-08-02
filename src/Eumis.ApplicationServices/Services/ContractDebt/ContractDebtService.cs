using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.Contracts.Repositories;
using Eumis.Data.Core.Permissions;
using Eumis.Data.Counters;
using Eumis.Data.Debts.Repositories;
using Eumis.Domain;
using Eumis.Domain.Contracts;
using Eumis.Domain.Debts;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.Users.ProgrammePermissions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.ApplicationServices.Services.ContractDebt
{
    internal class ContractDebtService : IContractDebtService
    {
        private IUnitOfWork unitOfWork;
        private IContractsRepository contractsRepository;
        private IContractDebtsRepository contractDebtsRepository;
        private IContractDebtVersionsRepository contractDebtVersionsRepository;
        private ICountersRepository countersRepository;
        private IAccessContext accessContext;
        private IPermissionsRepository permissionsRepository;

        public ContractDebtService(
            IUnitOfWork unitOfWork,
            IContractsRepository contractsRepository,
            IContractDebtsRepository contractDebtsRepository,
            IContractDebtVersionsRepository contractDebtVersionsRepository,
            ICountersRepository countersRepository,
            IAccessContext accessContext,
            IPermissionsRepository permissionsRepository)
        {
            this.unitOfWork = unitOfWork;
            this.contractsRepository = contractsRepository;
            this.contractDebtsRepository = contractDebtsRepository;
            this.contractDebtVersionsRepository = contractDebtVersionsRepository;
            this.countersRepository = countersRepository;
            this.accessContext = accessContext;
            this.permissionsRepository = permissionsRepository;
        }

        public Eumis.Domain.Debts.ContractDebt CreateContractDebt(
            int contractId,
            int programmePriorityId,
            DateTime regDate,
            DateTime debtStartDate,
            DateTime interestStartDate,
            int[] paymentIds)
        {
            var contract = this.contractsRepository.Find(contractId);
            if (this.CanCreate(contract).Any())
            {
                throw new DomainValidationException("Cannot create contract debt.");
            }

            var newContractDebt = new Eumis.Domain.Debts.ContractDebt(
                contractId,
                programmePriorityId,
                regDate,
                debtStartDate,
                interestStartDate,
                paymentIds);

            this.contractDebtsRepository.Add(newContractDebt);
            this.unitOfWork.Save();

            var newContractDebtVersion = new ContractDebtVersion(
                newContractDebt.ContractDebtId,
                this.accessContext.UserId);

            this.contractDebtVersionsRepository.Add(newContractDebtVersion);
            this.unitOfWork.Save();

            return newContractDebt;
        }

        public IList<string> CanCreate(string contractRegNumber)
        {
            var contract = this.contractsRepository.FindByRegNumber(contractRegNumber);

            return this.CanCreate(contract);
        }

        private IList<string> CanCreate(Domain.Contracts.Contract contract)
        {
            IList<string> errors = new List<string>();

            if (contract == null)
            {
                errors.Add("В системата няма въведен такъв договор.");
            }
            else
            {
                var programmeIds = this.permissionsRepository.GetProgrammeIdsByPermission(this.accessContext.UserId, MonitoringFinancialControlPermissions.CanWriteFinancial);
                if (!programmeIds.Contains(contract.ProgrammeId))
                {
                    errors.Add("Нямате право за писане за съответната програма.");
                }

                if (contract.ContractStatus != ContractStatus.Entered)
                {
                    errors.Add("Договорът трябва да е в статус въведен, за да може с него да се асоциира дълг.");
                }
            }

            return errors;
        }

        public void UpdateContractDebt(
            int contractDebtId,
            byte[] version,
            DateTime regDate,
            DateTime debtStartDate,
            DateTime interestStartDate,
            int? irregularityId,
            int? financialCorrectionId,
            string comment,
            int programmePriorityId,
            int[] paymentIds)
        {
            var contractDebt = this.contractDebtsRepository.FindForUpdate(contractDebtId, version);

            if (contractDebt.ContractDebtInterests.Any())
            {
                contractDebt.UpdateAttributes(
                    regDate,
                    debtStartDate,
                    irregularityId,
                    financialCorrectionId,
                    comment,
                    programmePriorityId,
                    paymentIds);
            }
            else
            {
                contractDebt.UpdateAttributes(
                    regDate,
                    debtStartDate,
                    irregularityId,
                    financialCorrectionId,
                    comment,
                    programmePriorityId,
                    paymentIds,
                    interestStartDate);
            }

            this.unitOfWork.Save();
        }

        public void DeleteContractDebt(int contractDebtId, byte[] version)
        {
            if (this.contractDebtVersionsRepository.HasNonDraftContractDebtVersions(contractDebtId))
            {
                throw new InvalidOperationException("Cannot delete contract debt");
            }

            this.contractDebtVersionsRepository.RemoveByContractDebtId(contractDebtId);
            this.unitOfWork.Save();

            var contractDebt = this.contractDebtsRepository.FindForUpdate(contractDebtId, version);
            this.contractDebtsRepository.Remove(contractDebt);
            this.unitOfWork.Save();
        }

        public IList<string> CanCreateContractDebtVersion(int contractDebtId)
        {
            var errors = new List<string>();

            var hasVersionsInProgress = this.contractDebtVersionsRepository.HasContractDebtVersionsInProgress(contractDebtId);
            if (hasVersionsInProgress)
            {
                errors.Add("Не може да се създаде нова версия на дълга, когато съществува версия, която не е в статус Актуална или Архивирана.");
            }

            return errors;
        }

        public ContractDebtVersion CreateContractDebtVersion(int contractDebtId)
        {
            if (this.CanCreateContractDebtVersion(contractDebtId).Count != 0)
            {
                throw new InvalidOperationException("Cannot create new ContractDebtVersion.");
            }

            var actualContractDebtVersion = this.contractDebtVersionsRepository.GetActualVersion(contractDebtId);

            var contractDebt = this.contractDebtsRepository.Find(actualContractDebtVersion.ContractDebtId);
            this.AssertIsNotRemovedContractDebt(contractDebt);

            if (actualContractDebtVersion.Status != ContractDebtVersionStatus.Actual)
            {
                throw new Exception("To create a new version the last ContractDebtVersion should be with status 'Actual'.");
            }

            var newContractDebtVersion = new ContractDebtVersion(actualContractDebtVersion, this.accessContext.UserId);

            this.contractDebtVersionsRepository.Add(newContractDebtVersion);

            this.unitOfWork.Save();

            return newContractDebtVersion;
        }

        public bool CanUpdateContractDebtVersion(ContractDebtVersion version)
        {
            var contarctDebtStatus = this.contractDebtsRepository.GetStatus(version.ContractDebtId);

            return contarctDebtStatus != ContractDebtStatus.Removed;
        }

        public bool CanDeleteContractDebtVersion(ContractDebtVersion version)
        {
            var contarctDebtStatus = this.contractDebtsRepository.GetStatus(version.ContractDebtId);

            return contarctDebtStatus != ContractDebtStatus.Removed;
        }

        public void ChangeContractDebtVersionStatusToActual(int contractDebtVersionId, byte[] version)
        {
            var contractDebtVersion = this.contractDebtVersionsRepository.FindForUpdate(contractDebtVersionId, version);
            if (contractDebtVersion.CanChangeStatusToActual().Any())
            {
                throw new DomainException("Requirements for activating the version are not met.");
            }

            var contractDebt = this.contractDebtsRepository.Find(contractDebtVersion.ContractDebtId);

            this.AssertIsNotRemovedContractDebt(contractDebt);
            this.AssertIsDraftContractDebtVersion(contractDebtVersion.Status);

            if (contractDebt.Status == ContractDebtStatus.New)
            {
                this.countersRepository.CreateContractDebtCounter(contractDebt.ContractId);
                var regNum = this.countersRepository.GetNextContractDebtNumber(contractDebt.ContractId);
                contractDebt.MakeEntered(regNum);
            }
            else
            {
                var lastContractDebtVersion = this.contractDebtVersionsRepository.GetActualVersion(contractDebtVersion.ContractDebtId);

                if (lastContractDebtVersion.Status != ContractDebtVersionStatus.Actual)
                {
                    throw new DomainException("Cannot change status of ContractDebtVersion to 'Archived', when the current status is not 'Actual'");
                }

                lastContractDebtVersion.Status = ContractDebtVersionStatus.Archived;
            }

            contractDebtVersion.Status = ContractDebtVersionStatus.Actual;
            contractDebtVersion.ActivationDate = contractDebtVersion.ModifyDate = DateTime.Now;

            contractDebt.ExecutionStatus = contractDebtVersion.ExecutionStatus;

            this.unitOfWork.Save();
        }

        public ContractDebtInterest CreateContractDebtInterest(
            int contractDebtId,
            byte[] version,
            int interestSchemeId,
            DateTime dateTo,
            decimal euInterestAmount,
            decimal bgInterestAmount,
            decimal totalInterestAmount,
            decimal euAmount,
            decimal bgAmount,
            decimal totalAmount)
        {
            var contractDebt = this.contractDebtsRepository.FindForUpdate(contractDebtId, version);

            this.AssertIsEnteredContractDebt(contractDebt);

            var orderNum = this.contractDebtsRepository.GetNextContractDebtInterestOrderNum(contractDebtId);
            DateTime dateFrom;

            if (orderNum == 1)
            {
                dateFrom = contractDebt.InterestStartDate;
            }
            else
            {
                dateFrom = contractDebt.ContractDebtInterests.OrderBy(t => t.OrderNum).Last().DateTo.AddDays(1);
            }

            var newContractDebtVersion = new ContractDebtInterest(
                contractDebtId,
                interestSchemeId,
                orderNum,
                dateFrom,
                dateTo,
                euInterestAmount,
                bgInterestAmount,
                totalInterestAmount,
                euAmount,
                bgAmount,
                totalAmount);

            contractDebt.ContractDebtInterests.Add(newContractDebtVersion);
            contractDebt.ModifyDate = DateTime.Now;

            this.unitOfWork.Save();

            return newContractDebtVersion;
        }

        public void UpdateContractDebtInterest(
            int contractDebtId,
            byte[] version,
            int contractDebtInterestId,
            int interestSchemeId,
            DateTime dateTo,
            decimal euInterestAmount,
            decimal bgInterestAmount,
            decimal totalInterestAmount,
            decimal euAmount,
            decimal bgAmount,
            decimal totalAmount)
        {
            var contractDebt = this.contractDebtsRepository.FindForUpdate(contractDebtId, version);

            this.AssertIsEnteredContractDebt(contractDebt);

            var contractDebtInterest = contractDebt.ContractDebtInterests.Where(t => t.ContractDebtInterestId == contractDebtInterestId).Single();

            contractDebtInterest.SetAttributes(
                interestSchemeId,
                dateTo,
                euInterestAmount,
                bgInterestAmount,
                totalInterestAmount,
                euAmount,
                bgAmount,
                totalAmount);
            contractDebt.ModifyDate = DateTime.Now;

            this.unitOfWork.Save();
        }

        public void DeleteContractDebtInterest(int contractDebtId, byte[] version, int contractDebtInterestId)
        {
            var contractDebt = this.contractDebtsRepository.FindForUpdate(contractDebtId, version);

            this.AssertIsEnteredContractDebt(contractDebt);

            var contractDebtInterest = contractDebt.ContractDebtInterests.Where(t => t.ContractDebtInterestId == contractDebtInterestId).Single();

            contractDebt.ContractDebtInterests.Remove(contractDebtInterest);
            contractDebt.ModifyDate = DateTime.Now;

            this.unitOfWork.Save();
        }

        private void AssertIsDraftContractDebtVersion(ContractDebtVersionStatus status)
        {
            if (status != ContractDebtVersionStatus.Draft)
            {
                throw new DomainException("Cannot edit ContractDebtVersion when not in 'Draft' status");
            }
        }

        private void AssertIsNotRemovedContractDebt(Domain.Debts.ContractDebt contractDebt)
        {
            if (contractDebt.Status == ContractDebtStatus.Removed)
            {
                throw new DomainException("Cannot edit ContractDebt or ContractDebtVersion when isDeleted is true");
            }
        }

        private void AssertIsEnteredContractDebt(Domain.Debts.ContractDebt contractDebt)
        {
            if (contractDebt.Status != ContractDebtStatus.Entered)
            {
                throw new DomainException("Cannot edit ContractDebt or ContractDebtVersion when isDeleted is true");
            }
        }
    }
}
