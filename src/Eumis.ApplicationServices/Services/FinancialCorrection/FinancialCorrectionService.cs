using System;
using System.Collections.Generic;
using System.Linq;
using Eumis.Common.Db;
using Eumis.Data.Contracts.Repositories;
using Eumis.Data.Core.Permissions;
using Eumis.Data.FinancialCorrections.Repositories;
using Eumis.Domain;
using Eumis.Domain.Contracts;
using Eumis.Domain.MonitoringFinancialControl.FinancialCorrections;
using Eumis.Domain.Users.ProgrammePermissions;

namespace Eumis.ApplicationServices.Services.FinancialCorrection
{
    public class FinancialCorrectionService : IFinancialCorrectionService
    {
        private IUnitOfWork unitOfWork;
        private IPermissionsRepository permissionsRepository;
        private IFinancialCorrectionsRepository financialCorrectionsRepository;
        private IFinancialCorrectionVersionsRepository financialCorrectionVersionsRepository;
        private IContractVersionsRepository contractVersionsRepository;
        private IContractsRepository contractsRepository;

        public FinancialCorrectionService(
            IUnitOfWork unitOfWork,
            IPermissionsRepository permissionsRepository,
            IFinancialCorrectionsRepository financialCorrectionsRepository,
            IFinancialCorrectionVersionsRepository financialCorrectionVersionsRepository,
            IContractVersionsRepository contractVersionsRepository,
            IContractsRepository contractsRepository)
        {
            this.unitOfWork = unitOfWork;
            this.permissionsRepository = permissionsRepository;
            this.financialCorrectionsRepository = financialCorrectionsRepository;
            this.financialCorrectionVersionsRepository = financialCorrectionVersionsRepository;
            this.contractVersionsRepository = contractVersionsRepository;
            this.contractsRepository = contractsRepository;
        }

        public Domain.MonitoringFinancialControl.FinancialCorrections.FinancialCorrection CreateFinancialCorrection(
            DateTime impositionDate,
            int contractId,
            int? contractContractId,
            int? contractBudgetLevel3AmountId,
            int userId)
        {
            var contract = this.contractsRepository.Find(contractId);
            if (this.CanCreate(contract, userId).Any())
            {
                throw new DomainValidationException("Cannot create financial correction.");
            }

            var newFinancialCorrection = new Domain.MonitoringFinancialControl.FinancialCorrections.FinancialCorrection(
                this.financialCorrectionsRepository.GetNextOrderNumber(contractId),
                impositionDate,
                contractId,
                contractContractId,
                contractBudgetLevel3AmountId);

            this.financialCorrectionsRepository.Add(newFinancialCorrection);

            this.unitOfWork.Save();

            var newFinancialCorrectionVersion = new FinancialCorrectionVersion(newFinancialCorrection.FinancialCorrectionId);

            this.financialCorrectionVersionsRepository.Add(newFinancialCorrectionVersion);

            this.unitOfWork.Save();

            return newFinancialCorrection;
        }

        public IList<string> CanCreate(string contractRegNumber, int userId)
        {
            var contract = this.contractsRepository.FindByRegNumber(contractRegNumber);

            return this.CanCreate(contract, userId);
        }

        private IList<string> CanCreate(Domain.Contracts.Contract contract, int userId)
        {
            IList<string> errors = new List<string>();

            if (contract == null)
            {
                errors.Add("В системата няма въведен такъв договор.");
            }
            else
            {
                var isExternalVerifier = this.contractsRepository.IsUserAssociatedWithContract(contract.ContractId, userId);

                var programmeIds = this.permissionsRepository.GetProgrammeIdsByPermission(userId, MonitoringFinancialControlPermissions.CanWriteFinancial);

                if (!programmeIds.Contains(contract.ProgrammeId) && !isExternalVerifier)
                {
                    errors.Add("Нямате право за писане за съответната програма.");
                }

                if (contract.ContractStatus != ContractStatus.Entered)
                {
                    errors.Add("Договорът трябва да е в статус въведен, за да може с него да се асоциира финансова корекция.");
                }
            }

            return errors;
        }

        public void DeleteFinancialCorrection(int financialCorrectionId, byte[] version)
        {
            if (this.financialCorrectionVersionsRepository.HasNonDraftFinancialCorrectionVersions(financialCorrectionId))
            {
                throw new InvalidOperationException("Cannot delete financial correction");
            }

            this.financialCorrectionVersionsRepository.RemoveByFinancialCorrectionId(financialCorrectionId);
            this.unitOfWork.Save();

            var financialCorrection = this.financialCorrectionsRepository.FindForUpdate(financialCorrectionId, version);
            this.financialCorrectionsRepository.Remove(financialCorrection);
            this.unitOfWork.Save();
        }

        public System.Collections.Generic.IList<string> CanCreateFinancialCorrectionVersion(int financialCorrectionId)
        {
            var errors = new List<string>();

            var hasVersionsInProgress = this.financialCorrectionVersionsRepository.HasFinancialCorrectionVersionsInProgress(financialCorrectionId);
            if (hasVersionsInProgress)
            {
                errors.Add("Не може да се създаде нова версия на финансова корекция, когато съществува версия, която не е в статус Актуална или Архивирана.");
            }

            return errors;
        }

        public FinancialCorrectionVersion CreateFinancialCorrectionVersion(int financialCorrectionId)
        {
            if (this.CanCreateFinancialCorrectionVersion(financialCorrectionId).Count != 0)
            {
                throw new InvalidOperationException("Cannot create new FinancialCorrectionVersion.");
            }

            var actualFinancialCorrectionVersion = this.financialCorrectionVersionsRepository.GetActualVersion(financialCorrectionId);

            var financialCorrection = this.financialCorrectionsRepository.Find(actualFinancialCorrectionVersion.FinancialCorrectionId);
            this.AssertIsNotDeletedFinancialCorrection(financialCorrection);

            if (actualFinancialCorrectionVersion.Status != FinancialCorrectionVersionStatus.Actual)
            {
                throw new Exception("To create a new version the last FinancialCorrectionVersion should be with status 'Actual'.");
            }

            var newFinancialCorrectionVersion = new FinancialCorrectionVersion(actualFinancialCorrectionVersion);

            this.financialCorrectionVersionsRepository.Add(newFinancialCorrectionVersion);

            this.unitOfWork.Save();

            return newFinancialCorrectionVersion;
        }

        public bool CanModifyFinancialCorrectionVersion(int financialCorrectionVersionId)
        {
            var correctionStatus = this.financialCorrectionVersionsRepository.GetFinancialCorrectionStatus(financialCorrectionVersionId);

            return correctionStatus != FinancialCorrectionStatus.Removed;
        }

        public FinancialCorrectionVersion ChangeFinancialCorrectionVersionStatusToActual(int flatFinancialCorrectionVersionId, byte[] version)
        {
            var financialCorrectionVersion = this.financialCorrectionVersionsRepository.FindForUpdate(flatFinancialCorrectionVersionId, version);
            var financialCorrection = this.financialCorrectionsRepository.Find(financialCorrectionVersion.FinancialCorrectionId);

            this.AssertIsNotDeletedFinancialCorrection(financialCorrection);
            this.AssertIsDraftFinancialCorrectionVersion(financialCorrectionVersion.Status);

            if (financialCorrection.Status == FinancialCorrectionStatus.New)
            {
                financialCorrection.MakeEntered();
            }
            else
            {
                var lastFinancialCorrectionVersion = this.financialCorrectionVersionsRepository.GetActualVersion(financialCorrectionVersion.FinancialCorrectionId);

                if (lastFinancialCorrectionVersion.Status != FinancialCorrectionVersionStatus.Actual)
                {
                    throw new DomainException("Cannot change status of FinancialCorrectionVersion to 'Archived', when the current status is not 'Actual'");
                }

                lastFinancialCorrectionVersion.Status = FinancialCorrectionVersionStatus.Archived;
            }

            financialCorrectionVersion.Status = FinancialCorrectionVersionStatus.Actual;
            financialCorrectionVersion.ModifyDate = DateTime.Now;

            this.unitOfWork.Save();

            return financialCorrectionVersion;
        }

        private void AssertIsDraftFinancialCorrectionVersion(FinancialCorrectionVersionStatus status)
        {
            if (status != FinancialCorrectionVersionStatus.Draft)
            {
                throw new DomainException("Cannot edit FinancialCorrectionVersion when not in 'Draft' status");
            }
        }

        private void AssertIsNotDeletedFinancialCorrection(Domain.MonitoringFinancialControl.FinancialCorrections.FinancialCorrection financialCorrection)
        {
            if (financialCorrection.Status == FinancialCorrectionStatus.Removed)
            {
                throw new DomainException("Cannot edit FinancialCorrection or FinancialCorrectionVersion when isDeleted is true");
            }
        }
    }
}
