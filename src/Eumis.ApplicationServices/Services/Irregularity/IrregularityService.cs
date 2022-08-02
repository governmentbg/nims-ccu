using System;
using System.Collections.Generic;
using System.Linq;
using Eumis.Common.Db;
using Eumis.Data.Core.Permissions;
using Eumis.Data.FinancialCorrections.Repositories;
using Eumis.Data.Irregularities.Repositories;
using Eumis.Domain;
using Eumis.Domain.Irregularities;
using Eumis.Domain.MonitoringFinancialControl.FinancialCorrections;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.Users.ProgrammePermissions;

namespace Eumis.ApplicationServices.Services.Irregularity
{
    public class IrregularityService : IIrregularityService
    {
        private IUnitOfWork unitOfWork;
        private IPermissionsRepository permissionsRepository;
        private IIrregularitySignalsRepository irregularitySignalsRepository;
        private IIrregularityVersionsRepository irregularityVersionsRepository;
        private IIrregularitiesRepository irregularitiesRepository;
        private IFinancialCorrectionsRepository financialCorrectionsRepository;

        public IrregularityService(
            IUnitOfWork unitOfWork,
            IPermissionsRepository permissionsRepository,
            IIrregularitySignalsRepository irregularitySignalsRepository,
            IIrregularityVersionsRepository irregularityVersionsRepository,
            IIrregularitiesRepository irregularitiesRepository,
            IFinancialCorrectionsRepository financialCorrectionsRepository)
        {
            this.unitOfWork = unitOfWork;
            this.permissionsRepository = permissionsRepository;
            this.irregularitySignalsRepository = irregularitySignalsRepository;
            this.irregularityVersionsRepository = irregularityVersionsRepository;
            this.irregularitiesRepository = irregularitiesRepository;
            this.financialCorrectionsRepository = financialCorrectionsRepository;
        }

        public Domain.Irregularities.Irregularity CreateIrregularity(
            int userId,
            int irregularitySignalId,
            DateTime irregularityDateFrom,
            DateTime? irregularityDateTo,
            Year reportYear,
            Quarter reportQuarter,
            bool shouldReportToOlaf,
            IrregularityReasonNotReportingToOlaf? reasonNotReportingToOlaf,
            IrregularitySanctionProcedureType sanctionProcedureType,
            IrregularitySanctionProcedureKind? sanctionProcedureKind,
            DateTime? sanctionProcedureStartDate,
            DateTime? sanctionProcedureExpectedEndDate,
            DateTime? sanctionProcedureEndDate,
            IrregularitySanctionProcedureStatus? sanctionProcedureStatus,
            int? sanctionCategoryId,
            int? sanctionTypeId,
            string fines)
        {
            var currentDate = DateTime.Now;

            var signal = this.irregularitySignalsRepository.Find(irregularitySignalId);
            var irregularity = new Domain.Irregularities.Irregularity(currentDate, signal);
            this.irregularitiesRepository.Add(irregularity);
            this.unitOfWork.Save();

            var firstVersion = new Domain.Irregularities.IrregularityVersion(
                irregularity.IrregularityId,
                irregularityDateFrom,
                irregularityDateTo,
                reportYear,
                reportQuarter,
                shouldReportToOlaf,
                reasonNotReportingToOlaf,
                sanctionProcedureType,
                sanctionProcedureKind,
                sanctionProcedureStartDate,
                sanctionProcedureExpectedEndDate,
                sanctionProcedureEndDate,
                sanctionProcedureStatus,
                sanctionCategoryId,
                sanctionTypeId,
                fines,
                currentDate);
            this.irregularityVersionsRepository.Add(firstVersion);

            foreach (var signalInvolvedPerson in signal.InvolvedPersons)
            {
                switch (signalInvolvedPerson.LegalType)
                {
                    case InvolvedPersonLegalType.Person:
                        firstVersion.AddInvolvedPerson(
                            signalInvolvedPerson.Uin,
                            signalInvolvedPerson.UinType,
                            null,
                            signalInvolvedPerson.FirstName,
                            signalInvolvedPerson.MiddleName,
                            signalInvolvedPerson.LastName,
                            signalInvolvedPerson.CountryId,
                            signalInvolvedPerson.SettlementId,
                            signalInvolvedPerson.PostCode,
                            signalInvolvedPerson.Street,
                            signalInvolvedPerson.Address);
                        break;
                    case InvolvedPersonLegalType.LegalPerson:
                        firstVersion.AddInvolvedLegalPerson(
                            signalInvolvedPerson.Uin,
                            signalInvolvedPerson.UinType,
                            null,
                            signalInvolvedPerson.CompanyName,
                            signalInvolvedPerson.TradeName,
                            signalInvolvedPerson.HoldingName,
                            signalInvolvedPerson.CountryId,
                            signalInvolvedPerson.SettlementId,
                            signalInvolvedPerson.PostCode,
                            signalInvolvedPerson.Street,
                            signalInvolvedPerson.Address);
                        break;
                }
            }

            foreach (var signalDoc in signal.Documents)
            {
                firstVersion.AddDocument(signalDoc.Description, signalDoc.FileName, signalDoc.FileKey);
            }

            this.unitOfWork.Save();

            return irregularity;
        }

        public IList<string> CanUpdatePartial(int programmeId, int irregularityId, string regNumber)
        {
            IList<string> errors = new List<string>();

            if (this.irregularitiesRepository.HasNonRemovedIrregularityWithTheSameNumber(programmeId, irregularityId, regNumber))
            {
                errors.Add("В системата вече съществува активен или приключен сигнал за нередност с този номер.");
            }

            if (!this.irregularitiesRepository.HasRemovedRemovedIrregularityWithTheSameNumber(programmeId, irregularityId, regNumber))
            {
                errors.Add("В системата не съществува анулиран сигнал с този номер");
            }

            return errors;
        }

        public void DeleteIrregularity(int irregularityId, byte[] version)
        {
            if (this.CanDeleteIrregularity(irregularityId).Any())
            {
                throw new InvalidOperationException("Cannot delete irregularity");
            }

            this.irregularityVersionsRepository.RemoveByIrregularityId(irregularityId);
            this.unitOfWork.Save();

            var irregularity = this.irregularitiesRepository.FindForUpdate(irregularityId, version);
            this.irregularitiesRepository.Remove(irregularity);
            this.unitOfWork.Save();
        }

        public IList<string> CanDeleteIrregularity(int irregularityId)
        {
            IList<string> errors = new List<string>();

            if (this.irregularityVersionsRepository.HasNonDraftVersions(irregularityId))
            {
                errors.Add("Не може да се изтрие нередност, която има версии със статус различен от чернова.");
            }

            return errors;
        }

        public void AddFinancialCorrections(Domain.Irregularities.Irregularity irregularity, int userId, int[] financialCorrectionIds)
        {
            foreach (var financialCorrectionId in financialCorrectionIds)
            {
                var finCorrectionInfo = this.financialCorrectionsRepository.GetInfo(financialCorrectionId);

                if (finCorrectionInfo.ContractId != irregularity.ContractId || finCorrectionInfo.Status != FinancialCorrectionStatus.Entered)
                {
                    throw new InvalidOperationException(string.Format("Cannot create irregularity item with id {0}", financialCorrectionId));
                }

                irregularity.AddFinancialCorrection(financialCorrectionId);
            }

            this.unitOfWork.Save();
        }
    }
}
