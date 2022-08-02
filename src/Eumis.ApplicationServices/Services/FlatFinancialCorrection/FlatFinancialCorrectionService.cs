using Eumis.Common.Db;
using Eumis.Data.Contracts.Repositories;
using Eumis.Data.Core.Permissions;
using Eumis.Data.FlatFinancialCorrections.Repositories;
using Eumis.Domain;
using Eumis.Domain.Contracts;
using Eumis.Domain.MonitoringFinancialControl.FlatFinancialCorrections;
using Eumis.Domain.MonitoringFinancialControl.FlatFinancialCorrections.FlatFinancialCorrectionLevelItems;
using Eumis.Domain.Users.ProgrammePermissions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.ApplicationServices.Services.FlatFinancialCorrection
{
    public class FlatFinancialCorrectionService : IFlatFinancialCorrectionService
    {
        private IUnitOfWork unitOfWork;
        private IPermissionsRepository permissionsRepository;
        private IContractsRepository contractsRepository;
        private IFlatFinancialCorrectionsRepository flatFinancialCorrectionsRepository;

        public FlatFinancialCorrectionService(
            IUnitOfWork unitOfWork,
            IPermissionsRepository permissionsRepository,
            IContractsRepository contractsRepository,
            IFlatFinancialCorrectionsRepository flatFinancialCorrectionsRepository)
        {
            this.unitOfWork = unitOfWork;
            this.permissionsRepository = permissionsRepository;
            this.contractsRepository = contractsRepository;
            this.flatFinancialCorrectionsRepository = flatFinancialCorrectionsRepository;
        }

        public Eumis.Domain.MonitoringFinancialControl.FlatFinancialCorrections.FlatFinancialCorrection CreateFlatFinancialCorrection(
            int programmeId,
            string name,
            FlatFinancialCorrectionLevel level,
            FlatFinancialCorrectionType type,
            DateTime? impositionDate,
            string impositionNumber,
            string description,
            Guid? blobKey,
            int? contractId,
            int userId)
        {
            if (!this.CanCreate(userId, programmeId, contractId, level))
            {
                throw new DomainValidationException("Cannot create flat financial correction.");
            }

            var newFlatFinancialCorrection = new Eumis.Domain.MonitoringFinancialControl.FlatFinancialCorrections.FlatFinancialCorrection(
                programmeId,
                name,
                this.flatFinancialCorrectionsRepository.GetNextOrderNumber(),
                level,
                type,
                impositionDate,
                impositionNumber,
                description,
                blobKey,
                contractId);

            this.flatFinancialCorrectionsRepository.Add(newFlatFinancialCorrection);

            this.unitOfWork.Save();

            if (level == FlatFinancialCorrectionLevel.Programme)
            {
                this.CreateFlatFinancialCorrectionItem<FlatFinancialCorrectionProgrammeItem>(
                    newFlatFinancialCorrection.FlatFinancialCorrectionId,
                    newFlatFinancialCorrection.Version,
                    programmeId);
            }

            return newFlatFinancialCorrection;
        }

        private bool CanCreate(int userId, int programmeId, int? contractId, FlatFinancialCorrectionLevel level)
        {
            var programmeIds = this.permissionsRepository.GetProgrammeIdsByPermission(userId, MonitoringFinancialControlPermissions.CanWriteFinancial);
            var canCreate = programmeIds.Contains(programmeId);

            if (canCreate && level == FlatFinancialCorrectionLevel.ContractContract)
            {
                var contract = this.contractsRepository.Find(contractId.Value);

                canCreate = contract.ProgrammeId == programmeId && contract.ContractStatus == ContractStatus.Entered;
            }

            return canCreate;
        }

        public Eumis.Domain.MonitoringFinancialControl.FlatFinancialCorrections.FlatFinancialCorrection UpdateFlatFinancialCorrection(
            int flatFinancialCorrectionId,
            byte[] version,
            string name,
            FlatFinancialCorrectionType type,
            DateTime? impositionDate,
            string impositionNumber,
            string description,
            Guid? blobKey)
        {
            var flatFinancialCorrection = this.flatFinancialCorrectionsRepository.FindForUpdate(flatFinancialCorrectionId, version);

            this.AssertIsDraftFlatFinancialCorrection(flatFinancialCorrection.Status);

            flatFinancialCorrection.UpdateAttributes(
                name,
                type,
                impositionDate,
                impositionNumber,
                description,
                blobKey);

            this.unitOfWork.Save();

            return flatFinancialCorrection;
        }

        public IList<string> CanDeleteFlatFinancialCorrection(int flatFinancialCorrectionId)
        {
            var errors = new List<string>();

            var flatFinancialCorrection = this.flatFinancialCorrectionsRepository.Find(flatFinancialCorrectionId);

            if (flatFinancialCorrection.Level != FlatFinancialCorrectionLevel.Programme && flatFinancialCorrection.FlatFinancialCorrectionLevelItems.Any())
            {
                errors.Add("Не можете да изтриете финансовата корекция за системни пропуски, защото има елементи в обхвата");
            }

            return errors;
        }

        public Eumis.Domain.MonitoringFinancialControl.FlatFinancialCorrections.FlatFinancialCorrection DeleteFlatFinancialCorrection(int flatFinancialCorrectionId, byte[] version)
        {
            var flatFinancialCorrection = this.flatFinancialCorrectionsRepository.FindForUpdate(flatFinancialCorrectionId, version);

            this.AssertIsDraftFlatFinancialCorrection(flatFinancialCorrection.Status);

            if (this.CanDeleteFlatFinancialCorrection(flatFinancialCorrectionId).Any())
            {
                throw new DomainException("Cannot delete FlatFinancialCorrection");
            }

            if (flatFinancialCorrection.Level == FlatFinancialCorrectionLevel.Programme)
            {
                var programmeItem = flatFinancialCorrection.FlatFinancialCorrectionLevelItems.Single();
                this.DeleteFlatFinancialCorrectionItem<FlatFinancialCorrectionProgrammeItem>(flatFinancialCorrectionId, version, programmeItem.FlatFinancialCorrectionLevelItemId);
            }

            this.flatFinancialCorrectionsRepository.Remove(flatFinancialCorrection);

            this.unitOfWork.Save();

            return flatFinancialCorrection;
        }

        public IList<string> CanChangeFlatFinancialCorrectionStatusToActual(int flatFinancialCorrectionId)
        {
            var errors = new List<string>();

            var flatFinancialCorrection = this.flatFinancialCorrectionsRepository.Find(flatFinancialCorrectionId);

            if (!flatFinancialCorrection.FlatFinancialCorrectionLevelItems.Any())
            {
                errors.Add("Не можете да промените статуса на финансовата корекция за системни пропуски на 'Активна', защото няма нито един елемент в обхвата");
            }
            else if (flatFinancialCorrection.FlatFinancialCorrectionLevelItems.Where(t => !t.EuAmount.HasValue || !t.EuAmount.HasValue || !t.TotalAmount.HasValue).Any())
            {
                errors.Add("Не можете да промените статуса на финансовата корекция за системни пропуски на 'Активна', защото всички елементи в обхвата трябва да са с попълнени суми");
            }

            return errors;
        }

        public IList<string> CanChangeFlatFinancialCorrectionStatusToDraft(int flatFinancialCorrectionId)
        {
            IList<string> errors = this.flatFinancialCorrectionsRepository.CanChangeFlatFinancialCorrectionToDraft(flatFinancialCorrectionId);

            return errors;
        }

        public Eumis.Domain.MonitoringFinancialControl.FlatFinancialCorrections.FlatFinancialCorrection ChangeFlatFinancialCorrectionStatus(int flatFinancialCorrectionId, byte[] version, FlatFinancialCorrectionStatus status)
        {
            IList<string> errors = new List<string>();

            if (status == FlatFinancialCorrectionStatus.Actual)
            {
                errors = this.CanChangeFlatFinancialCorrectionStatusToActual(flatFinancialCorrectionId);
            }
            else if (status == FlatFinancialCorrectionStatus.Draft)
            {
                errors = this.CanChangeFlatFinancialCorrectionStatusToDraft(flatFinancialCorrectionId);
            }
            else
            {
                throw new NotImplementedException("Unknown flat financial correction status");
            }

            if (errors.Any())
            {
                throw new DomainException($"Cannot change FlatFinancialCorrection status to '{status.ToString()}'");
            }

            var flatFinancialCorrection = this.flatFinancialCorrectionsRepository.FindForUpdate(flatFinancialCorrectionId, version);

            flatFinancialCorrection.Status = status;
            flatFinancialCorrection.ModifyDate = DateTime.Now;

            this.unitOfWork.Save();

            return flatFinancialCorrection;
        }

        public TEntity CreateFlatFinancialCorrectionItem<TEntity>(int flatFinancialCorrectionId, byte[] version, int itemId)
            where TEntity : FlatFinancialCorrectionLevelItem, IFlatFinancialCorrectionItem, new()
        {
            var flatFinancialCorrection = this.flatFinancialCorrectionsRepository.FindForUpdate(flatFinancialCorrectionId, version);

            this.AssertIsDraftFlatFinancialCorrection(flatFinancialCorrection.Status);

            var newFlatFinancialCorrectionItem = new TEntity();

            newFlatFinancialCorrectionItem.ItemId = itemId;

            flatFinancialCorrection.FlatFinancialCorrectionLevelItems.Add(newFlatFinancialCorrectionItem);
            flatFinancialCorrection.ModifyDate = DateTime.Now;

            this.unitOfWork.Save();

            return newFlatFinancialCorrectionItem;
        }

        public TEntity UpdateFlatFinancialCorrectionItem<TEntity>(
            int flatFinancialCorrectionId,
            byte[] version,
            int flatFinancialCorrectionLevelItemId,
            decimal? percent,
            decimal? euAmount,
            decimal? bgAmount,
            decimal? totalAmount)
                where TEntity : FlatFinancialCorrectionLevelItem, new()
        {
            var flatFinancialCorrection = this.flatFinancialCorrectionsRepository.FindForUpdate(flatFinancialCorrectionId, version);

            this.AssertIsDraftFlatFinancialCorrection(flatFinancialCorrection.Status);

            var flatFinancialCorrectionItem = flatFinancialCorrection.FindFlatFinancialCorrectionItem<TEntity>(flatFinancialCorrectionLevelItemId);

            flatFinancialCorrectionItem.SetAttributes(percent, euAmount, bgAmount, totalAmount);
            flatFinancialCorrection.ModifyDate = DateTime.Now;

            this.unitOfWork.Save();

            return flatFinancialCorrectionItem;
        }

        public TEntity DeleteFlatFinancialCorrectionItem<TEntity>(int flatFinancialCorrectionId, byte[] version, int flatFinancialCorrectionLevelItemId)
            where TEntity : FlatFinancialCorrectionLevelItem, new()
        {
            var flatFinancialCorrection = this.flatFinancialCorrectionsRepository.FindForUpdate(flatFinancialCorrectionId, version);

            this.AssertIsDraftFlatFinancialCorrection(flatFinancialCorrection.Status);

            var flatFinancialCorrectionItem = flatFinancialCorrection.FindFlatFinancialCorrectionItem<TEntity>(flatFinancialCorrectionLevelItemId);

            flatFinancialCorrection.FlatFinancialCorrectionLevelItems.Remove(flatFinancialCorrectionItem);
            flatFinancialCorrection.ModifyDate = DateTime.Now;

            this.unitOfWork.Save();

            return flatFinancialCorrectionItem;
        }

        private void AssertIsDraftFlatFinancialCorrection(FlatFinancialCorrectionStatus status)
        {
            if (status != FlatFinancialCorrectionStatus.Draft)
            {
                throw new DomainException("Cannot edit FlatFinancialCorrection when not in 'Draft' status");
            }
        }
    }
}
