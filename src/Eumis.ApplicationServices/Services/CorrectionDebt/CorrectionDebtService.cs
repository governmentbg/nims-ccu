using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.Core.Permissions;
using Eumis.Data.Counters;
using Eumis.Data.Debts.Repositories;
using Eumis.Data.FlatFinancialCorrections.Repositories;
using Eumis.Domain;
using Eumis.Domain.Debts;
using Eumis.Domain.MonitoringFinancialControl.FlatFinancialCorrections;
using Eumis.Domain.Users.ProgrammePermissions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.ApplicationServices.Services.CorrectionDebt
{
    internal class CorrectionDebtService : ICorrectionDebtService
    {
        private IUnitOfWork unitOfWork;
        private ICorrectionDebtsRepository correctionDebtsRepository;
        private ICorrectionDebtVersionsRepository correctionDebtVersionsRepository;
        private IFlatFinancialCorrectionsRepository flatFinancialCorrectionsRepository;
        private ICountersRepository countersRepository;
        private IAccessContext accessContext;
        private IPermissionsRepository permissionsRepository;

        public CorrectionDebtService(
            IUnitOfWork unitOfWork,
            ICorrectionDebtsRepository correctionDebtsRepository,
            ICorrectionDebtVersionsRepository correctionDebtVersionsRepository,
            IFlatFinancialCorrectionsRepository flatFinancialCorrectionsRepository,
            ICountersRepository countersRepository,
            IAccessContext accessContext,
            IPermissionsRepository permissionsRepository)
        {
            this.unitOfWork = unitOfWork;
            this.correctionDebtsRepository = correctionDebtsRepository;
            this.correctionDebtVersionsRepository = correctionDebtVersionsRepository;
            this.flatFinancialCorrectionsRepository = flatFinancialCorrectionsRepository;
            this.countersRepository = countersRepository;
            this.accessContext = accessContext;
            this.permissionsRepository = permissionsRepository;
        }

        public Eumis.Domain.Debts.CorrectionDebt CreateContractDebt(int flatFinancialCorrectionId, DateTime regDate)
        {
            var flatFinancialCorrection = this.flatFinancialCorrectionsRepository.Find(flatFinancialCorrectionId);
            if (this.CanCreate(flatFinancialCorrection).Any())
            {
                throw new DomainValidationException("Cannot create correction debt.");
            }

            var newCorrectionDebt = new Eumis.Domain.Debts.CorrectionDebt(
                flatFinancialCorrectionId,
                regDate);

            this.correctionDebtsRepository.Add(newCorrectionDebt);
            this.unitOfWork.Save();

            var newCorrectionDebtVersion = new CorrectionDebtVersion(
                newCorrectionDebt.CorrectionDebtId,
                this.accessContext.UserId);

            this.correctionDebtVersionsRepository.Add(newCorrectionDebtVersion);
            this.unitOfWork.Save();

            return newCorrectionDebt;
        }

        private IList<string> CanCreate(Eumis.Domain.MonitoringFinancialControl.FlatFinancialCorrections.FlatFinancialCorrection flatFinancialCorrection)
        {
            IList<string> errors = new List<string>();

            var programmeIds = this.permissionsRepository.GetProgrammeIdsByPermission(this.accessContext.UserId, MonitoringFinancialControlPermissions.CanWriteFinancial);
            if (!programmeIds.Contains(flatFinancialCorrection.ProgrammeId))
            {
                errors.Add("Нямате право за писане за съответната програма.");
            }

            if (flatFinancialCorrection.Status != FlatFinancialCorrectionStatus.Actual)
            {
                errors.Add("Плоската финансова корекция трябва да е в статус 'Актуална'.");
            }

            return errors;
        }

        public void DeleteDebt(int correctionDebtId, byte[] version)
        {
            if (this.correctionDebtVersionsRepository.HasNonDraftCorrectionDebtVersions(correctionDebtId))
            {
                throw new InvalidOperationException("Cannot delete correction debt");
            }

            this.correctionDebtVersionsRepository.RemoveByDebtId(correctionDebtId);
            this.unitOfWork.Save();

            var correctionDebt = this.correctionDebtsRepository.FindForUpdate(correctionDebtId, version);
            this.correctionDebtsRepository.Remove(correctionDebt);
            this.unitOfWork.Save();
        }

        public IList<string> CanCreateCorrectionDebtVersion(int correctionDebtId)
        {
            var errors = new List<string>();

            var hasVersionsInProgress = this.correctionDebtVersionsRepository.HasCorrectionDebtVersionsInProgress(correctionDebtId);
            if (hasVersionsInProgress)
            {
                errors.Add("Не може да се създаде нова версия на дълга, когато съществува версия, която не е в статус Актуална или Архивирана.");
            }

            return errors;
        }

        public CorrectionDebtVersion CreateCorrectionDebtVersion(int correctionDebtId)
        {
            if (this.CanCreateCorrectionDebtVersion(correctionDebtId).Count != 0)
            {
                throw new InvalidOperationException("Cannot create new CorrectionDebtVersion.");
            }

            var actualCorrectionDebtVersion = this.correctionDebtVersionsRepository.GetActualVersion(correctionDebtId);

            var correctionDebt = this.correctionDebtsRepository.Find(actualCorrectionDebtVersion.CorrectionDebtId);
            correctionDebt.AssertIsNotRemoved();

            if (actualCorrectionDebtVersion.Status != CorrectionDebtVersionStatus.Actual)
            {
                throw new Exception("To create a new version the last CorrectionDebtVersion should be with status 'Actual'.");
            }

            var newCorrectionDebtVersion = new CorrectionDebtVersion(actualCorrectionDebtVersion, this.accessContext.UserId);

            this.correctionDebtVersionsRepository.Add(newCorrectionDebtVersion);

            this.unitOfWork.Save();

            return newCorrectionDebtVersion;
        }

        public void UpdateCorrectionDebtVersion(
            int correctionDebtVersionId,
            byte[] version,
            decimal? debtEuAmount,
            decimal? debtBgAmount,
            decimal? certEuAmount,
            decimal? certBgAmount,
            decimal? reimbursedEuAmount,
            decimal? reimbursedBgAmount,
            string createNotes)
        {
            var correctionDebtVersion = this.correctionDebtVersionsRepository.FindForUpdate(correctionDebtVersionId, version);

            var correctionDebt = this.correctionDebtsRepository.Find(correctionDebtVersion.CorrectionDebtId);
            correctionDebt.AssertIsNotRemoved();
            this.AssertIsDraftCorrectionDebtVersion(correctionDebtVersion.Status);

            correctionDebtVersion.UpdateAttributes(
                debtEuAmount,
                debtBgAmount,
                certEuAmount,
                certBgAmount,
                reimbursedEuAmount,
                reimbursedBgAmount,
                createNotes);

            this.unitOfWork.Save();
        }

        public void DeleteCorrectionDebtVersion(int correctionDebtVersionId, byte[] version)
        {
            var correctionDebtVersion = this.correctionDebtVersionsRepository.FindForUpdate(correctionDebtVersionId, version);

            var correctionDebt = this.correctionDebtsRepository.Find(correctionDebtVersion.CorrectionDebtId);
            correctionDebt.AssertIsNotRemoved();

            this.AssertIsDraftCorrectionDebtVersion(correctionDebtVersion.Status);

            this.correctionDebtVersionsRepository.Remove(correctionDebtVersion);

            this.unitOfWork.Save();
        }

        public void ChangeCorrectionDebtVersionStatusToActual(int correctionDebtVersionId, byte[] version)
        {
            var correctionDebtVersion = this.correctionDebtVersionsRepository.FindForUpdate(correctionDebtVersionId, version);
            if (correctionDebtVersion.CanChangeStatusToActual().Any())
            {
                throw new DomainException("Requirements for activating the version are not met.");
            }

            var correctionDebt = this.correctionDebtsRepository.Find(correctionDebtVersion.CorrectionDebtId);

            correctionDebt.AssertIsNotRemoved();
            this.AssertIsDraftCorrectionDebtVersion(correctionDebtVersion.Status);

            if (correctionDebt.Status == CorrectionDebtStatus.New)
            {
                this.countersRepository.CreateCorrectionDebtCounter(correctionDebt.FlatFinancialCorrectionId);
                var regNum = this.countersRepository.GetNextCorrectionDebtNumber(correctionDebt.FlatFinancialCorrectionId);
                correctionDebt.MakeEntered(regNum);
            }
            else
            {
                var lastCorrectionDebtVersion = this.correctionDebtVersionsRepository.GetActualVersion(correctionDebt.CorrectionDebtId);

                if (lastCorrectionDebtVersion.Status != CorrectionDebtVersionStatus.Actual)
                {
                    throw new DomainException("Cannot change status of CorrectionDebtVersion to 'Archived', when the current status is not 'Actual'");
                }

                lastCorrectionDebtVersion.Status = CorrectionDebtVersionStatus.Archived;
            }

            correctionDebtVersion.Status = CorrectionDebtVersionStatus.Actual;
            correctionDebtVersion.ModifyDate = DateTime.Now;

            this.unitOfWork.Save();
        }

        private void AssertIsDraftCorrectionDebtVersion(CorrectionDebtVersionStatus status)
        {
            if (status != CorrectionDebtVersionStatus.Draft)
            {
                throw new DomainException("Cannot edit CorrectionDebtVersion when not in 'Draft' status");
            }
        }
    }
}
