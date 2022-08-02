using System;
using System.Collections.Generic;
using System.Linq;
using Eumis.Domain.Companies;
using Eumis.Domain.Core;
using Eumis.Domain.Events;
using Eumis.Domain.Indicators;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.NotificationEvents;
using Eumis.Domain.Procedures.ProcedureContractReportDocuments;
using Eumis.Domain.Procedures.Validation;
using Eumis.Rio;
using Indicator = Eumis.Domain.Indicators.Indicator;

namespace Eumis.Domain.Procedures
{
    public partial class Procedure
    {
        private static readonly List<ValueTuple<ProcedureKind, ApplicationSectionType>> AvailableApplicableSections = new List<ValueTuple<ProcedureKind, ApplicationSectionType>>
        {
            (ProcedureKind.Schema, ApplicationSectionType.BasicData),
            (ProcedureKind.Schema, ApplicationSectionType.Beneficary),
            (ProcedureKind.Schema, ApplicationSectionType.Partners),
            (ProcedureKind.Schema, ApplicationSectionType.Directions),
            (ProcedureKind.Schema, ApplicationSectionType.Budget),
            (ProcedureKind.Schema, ApplicationSectionType.SummaryData),
            (ProcedureKind.Schema, ApplicationSectionType.Activities),
            (ProcedureKind.Schema, ApplicationSectionType.Indicators),
            (ProcedureKind.Schema, ApplicationSectionType.Team),
            (ProcedureKind.Schema, ApplicationSectionType.ProcurementPlan),
            (ProcedureKind.Schema, ApplicationSectionType.AdditionalInformation),
            (ProcedureKind.Schema, ApplicationSectionType.AttachedDocuments),
            (ProcedureKind.Schema, ApplicationSectionType.ElectronicDeclarations),

            (ProcedureKind.Budget, ApplicationSectionType.BasicData),
            (ProcedureKind.Budget, ApplicationSectionType.Programme),
            (ProcedureKind.Budget, ApplicationSectionType.ProgrammePriority),
            (ProcedureKind.Budget, ApplicationSectionType.Directions),
            (ProcedureKind.Budget, ApplicationSectionType.Budget),
            (ProcedureKind.Budget, ApplicationSectionType.SummaryData),
            (ProcedureKind.Budget, ApplicationSectionType.Activities),
            (ProcedureKind.Budget, ApplicationSectionType.Indicators),
            (ProcedureKind.Budget, ApplicationSectionType.ProcurementPlan),
            (ProcedureKind.Budget, ApplicationSectionType.AttachedDocuments),
        };

        #region Procedure

        public void SetAttributes(
            ProcedureKind procedureKind,
            string code,
            string name,
            string nameAlt,
            string description,
            string descriptionAlt,
            AllowedRegistrationType? allowedRegistrationType,
            decimal? projectMinAmount,
            string projectMinAmountInfo,
            string projectMinAmountInfoAlt,
            decimal? projectMaxAmount,
            string projectMaxAmountInfo,
            string projectMaxAmountInfoAlt,
            int? projectDuration,
            bool isCopy = false)
        {
            this.AssertIsDraftProcedure();

            if (!isCopy)
            {
                if (string.IsNullOrEmpty(description))
                {
                    throw new DomainValidationException("Description should not be null or empty.");
                }

                if (string.IsNullOrEmpty(descriptionAlt))
                {
                    throw new DomainValidationException("DescriptionAlt should not be null or empty.");
                }
            }

            if (this.ProcedureKind != procedureKind)
            {
                this.ProcedureApplicationSections.Clear();
            }

            this.ProcedureKind = procedureKind;
            this.Code = code;
            this.Name = name;
            this.NameAlt = nameAlt;
            this.Description = description;
            this.DescriptionAlt = descriptionAlt;
            this.AllowedRegistrationType = allowedRegistrationType ?? Procedures.AllowedRegistrationType.Digital;
            this.ProjectMinAmount = projectMinAmount;
            this.ProjectMinAmountInfo = projectMinAmountInfo;
            this.ProjectMinAmountInfoAlt = projectMinAmountInfoAlt;
            this.ProjectMaxAmount = projectMaxAmount;
            this.ProjectMaxAmountInfo = projectMaxAmountInfo;
            this.ProjectMaxAmountInfoAlt = projectMaxAmountInfoAlt;
            this.ProjectDuration = projectDuration;

            this.ModifyDate = DateTime.Now;
            this.SetProcedureApplicationSectionDefaults();
        }

        public void ChangeStatus(ProcedureStatus procedureStatus)
        {
            if (this.ProcedureStatus == procedureStatus)
            {
                throw new DomainValidationException("Cannot make a transition to the same Procedure status");
            }

            if (procedureStatus == ProcedureStatus.Draft)
            {
                ((IEventEmitter)this).Events.Add(new ProcedureSetToDraftEvent() { ProcedureId = this.ProcedureId });

                if (this.ProcedureStatus == ProcedureStatus.Active)
                {
                    ((INotificationEventEmitter)this).NotificationEvents.Add(new ProcedureNotificationEvent(this.ProcedureId, NotificationEventType.ProcedureStatusChangedToDraft));
                }
            }
            else if (procedureStatus == ProcedureStatus.Canceled)
            {
                ((IEventEmitter)this).Events.Add(new ProcedureCanceledEvent() { ProcedureId = this.ProcedureId });
            }
            else
            {
                Action<ProcedureStatus> validateStatus = (allowedStatus) =>
                {
                    if (procedureStatus != allowedStatus)
                    {
                        throw new DomainValidationException("Procedure status transition not allowed");
                    }
                };

                switch (this.ProcedureStatus)
                {
                    case ProcedureStatus.Draft:
                        validateStatus(ProcedureStatus.Entered);
                        break;
                    case ProcedureStatus.Entered:
                        validateStatus(ProcedureStatus.Checked);
                        break;
                    case ProcedureStatus.Checked:
                        validateStatus(ProcedureStatus.Active);
                        ((INotificationEventEmitter)this).NotificationEvents.Add(new ProcedureNotificationEvent(this.ProcedureId, NotificationEventType.ProcedureStatusChangedToActivated));
                        this.ActivateProcedure();
                        break;
                    case ProcedureStatus.Active:
                        this.ChangeStatusFromActive(procedureStatus);
                        break;
                    default:
                        throw new DomainValidationException("Procedure status transition not allowed");
                }
            }

            this.ProcedureStatus = procedureStatus;
            this.ModifyDate = DateTime.Now;
        }

        public void ChangeContractReportDocumentsSectionStatus(ProcedureContractReportDocumentsSectionStatus status)
        {
            if (this.ProcedureContractReportDocumentsSectionStatus == status)
            {
                throw new DomainValidationException("Cannot make a transition to the same ProcedureContractReportDocumentsSection status");
            }

            if (status == ProcedureContractReportDocumentsSectionStatus.Active)
            {
                foreach (var document in this.ProcedureContractReportDocuments)
                {
                    if (!document.IsActivated)
                    {
                        document.IsActivated = true;
                    }
                }
            }

            this.ProcedureContractReportDocumentsSectionStatus = status;
            this.ModifyDate = DateTime.Now;
        }

        public IList<string> Validate(IList<Indicator> procedureIndicators, bool useIndicators)
        {
            List<string> validationErrors = new List<string>();

            if (string.IsNullOrEmpty(this.Description))
            {
                validationErrors.Add("Полето 'Описание/Цели на предоставяната БФП' е задължително");
            }

            if (string.IsNullOrEmpty(this.DescriptionAlt))
            {
                validationErrors.Add("Полето 'Описание/Цели на предоставяната БФП на английски език' е задължително");
            }

            if (this.ApplicationFormType != ApplicationFormType.PreliminarySelection &&
                this.ApplicationFormType != ApplicationFormType.StandardSimplified &&
                useIndicators &&
                this.ProcedureProgrammes.Any(pp => !procedureIndicators.Any(pi => pi.ProgrammeId == pp.ProgrammeId)))
            {
                validationErrors.Add("Трябва да има присъединен поне един индикатор за всяка основна организация");
            }

            if (this.ProcedureProgrammes.Any(pp => pp.ProcedureBudgetLevel1.Any(lev1 => !lev1.ProcedureBudgetLevel2.Any())))
            {
                validationErrors.Add("В раздел 'Бюджет', за всеки въведен разход трябва да има въведен поне един подразход");
            }

            if (this.ProcedureEvalTables.Where(p => p.Status == ProcedureEvalTableStatus.Draft).Any())
            {
                validationErrors.Add("Всички оценителни таблици трябва да са със статус на въвеждане 'Приключена'");
            }

            var activeComplexEvalTablesCount = this.ProcedureEvalTables.Count(e => e.Type == ProcedureEvalTableType.Complex && e.IsActive);
            var activeaAminAdmissEvalTablesCount = this.ProcedureEvalTables.Count(e => e.Type == ProcedureEvalTableType.AdminAdmiss && e.IsActive);
            var activeTechFinanceEvalTablesCount = this.ProcedureEvalTables.Count(e => e.Type == ProcedureEvalTableType.TechFinance && e.IsActive);

            if (!this.HasCompleteSetEvalTables(activeaAminAdmissEvalTablesCount, activeTechFinanceEvalTablesCount, activeComplexEvalTablesCount))
            {
                validationErrors.Add("Трябва да има по една актива оценителна таблица от типовете \"Оценка на административното съответствие и допустимостта\" и \"Техническа и финансова оценка\" или една активна оценителна таблица от тип \"Комплексна оценка\"");
            }

            if (activeComplexEvalTablesCount != 0 &&
                (activeaAminAdmissEvalTablesCount != 0 || activeTechFinanceEvalTablesCount != 0))
            {
                validationErrors.Add("Не може да има активна оценителна таблица от тип \"Комплексна оценка\" и активна оценителна таблица от тип \"Оценка на административното съответствие и допустимостта\" или \"Техническа и финансова оценка\"");
            }

            if (activeaAminAdmissEvalTablesCount > 1)
            {
                validationErrors.Add("Може да има най-много една активна оценителна таблица от тип \"Оценка на административното съответствие и допустимостта\"");
            }

            if (activeTechFinanceEvalTablesCount > 1)
            {
                validationErrors.Add("Може да има най-много една активна оценителна таблица от тип \"Техническа и финансова оценка\"");
            }

            if (activeComplexEvalTablesCount > 1)
            {
                validationErrors.Add("Може да има най-много една активна оценителна таблица от тип \"Комплексна оценка\"");
            }

            if (this.ProcedureApplicationDocs.Count(ad => ad.IsActive) > 100)
            {
                validationErrors.Add("Може да има най-много 100 активни документа за подаване към един бюджет");
            }

            if (this.ProcedureSpecFields.Count(sf => sf.IsActive) > 20)
            {
                validationErrors.Add("Може да има най-много 20 активни допълнителни полета към един бюджет");
            }

            if (this.ProcedureIndicators.Count(i => i.IsActive) > 40)
            {
                validationErrors.Add("Може да има най-много 40 активни индикатора към един бюджет");
            }

            var isBudgetLevel2CountInvalid = this.ProcedureProgrammes
                .Any(p => p.ProcedureBudgetLevel1.SelectMany(bl1 => bl1.ProcedureBudgetLevel2).Count(bl2 => bl2.IsActive) > 200);
            var isBudgetLevel3CountInvalid = this.ProcedureProgrammes
                .Any(p => p.ProcedureBudgetLevel1.SelectMany(bl1 => bl1.ProcedureBudgetLevel2).SelectMany(bl2 => bl2.ProcedureBudgetLevel3).Count() > 200);
            if (isBudgetLevel2CountInvalid || isBudgetLevel3CountInvalid)
            {
                validationErrors.Add("Достигнали сте максимума от 200 реда в бюджета");
            }

            if (this.ProcedureLocations.Count == 0)
            {
                validationErrors.Add("Не е въведено местоположение за бюджета");
            }

            if (!this.ProcedureApplicationSections.Any())
            {
                validationErrors.Add("Не са избрани приложими секции за формуляра на бюджета");
            }

            if (this.ProcedureApplicationSections.Any(x => x.IsSelected && x.Section == ApplicationSectionType.AdditionalInformation) &&
                this.ProcedureSpecFields.Count == 0)
            {
                validationErrors.Add("Секцията \"Допълнителна информация\" е включена във формуляра, но липсват настройки за нея");
            }

            if (this.ProcedureApplicationSections.Any(x => x.IsSelected && x.Section == ApplicationSectionType.AttachedDocuments) &&
                this.ProcedureApplicationDocs.Count == 0)
            {
                validationErrors.Add("Секцията \"Прикачени документи\" е включена във формуляра, но липсват настройки за нея");
            }

            return validationErrors;
        }

        public IList<string> ValidateApplicationForm(IList<Indicator> procedureIndicators)
        {
            List<string> validationErrors = new List<string>();

            if (this.ApplicationFormType != ApplicationFormType.StandardSimplified &&
                this.ProcedureApplicationSections.Any(x => x.Section == ApplicationSectionType.Indicators && x.IsSelected) &&
                this.ProcedureProgrammes.Any(pp => !procedureIndicators.Any(pi => pi.ProgrammeId == pp.ProgrammeId)))
            {
                validationErrors.Add("Трябва да има присъединен поне един индикатор за всяка основна организация");
            }

            if (this.ApplicationFormType != ApplicationFormType.PreliminarySelection && this.ProcedureProgrammes.Any(pp => !pp.ProcedureBudgetLevel1.Any()))
            {
                validationErrors.Add("В раздел 'Бюджет', за всяка основна организация трябва да има въведен поне един разход");
            }

            if (this.ProcedureProgrammes.Any(pp => pp.ProcedureBudgetLevel1.Any(lev1 => !lev1.ProcedureBudgetLevel2.Any())))
            {
                validationErrors.Add("В раздел 'Бюджет', за всеки въведен разход трябва да има въведен поне един подразход");
            }

            if (!this.ProcedureTimeLimits.Any())
            {
                validationErrors.Add("Не е въведен срок на кандидатстване");
            }

            if (this.ProcedureApplicationDocs.Count(ad => ad.IsActive) > 100)
            {
                validationErrors.Add("Може да има най-много 100 активни документа за подаване към един бюджет");
            }

            if (this.ProcedureSpecFields.Count(sf => sf.IsActive) > 20)
            {
                validationErrors.Add("Може да има най-много 20 активни допълнителни полета към един бюджет");
            }

            if (this.ProcedureIndicators.Count(i => i.IsActive) > 40)
            {
                validationErrors.Add("Може да има най-много 40 активни индикатора към един бюджет");
            }

            var isBudgetLevel2CountInvalid = this.ProcedureProgrammes
                .Any(p => p.ProcedureBudgetLevel1.SelectMany(bl1 => bl1.ProcedureBudgetLevel2).Count(bl2 => bl2.IsActive) > 200);
            var isBudgetLevel3CountInvalid = this.ProcedureProgrammes
                .Any(p => p.ProcedureBudgetLevel1.SelectMany(bl1 => bl1.ProcedureBudgetLevel2).SelectMany(bl2 => bl2.ProcedureBudgetLevel3).Count() > 200);
            if (isBudgetLevel2CountInvalid || isBudgetLevel3CountInvalid)
            {
                validationErrors.Add("Достигнали сте максимума от 200 реда в бюджета");
            }

            return validationErrors;
        }

        private void ActivateProcedure()
        {
            foreach (var level0Item in this.ProcedureProgrammes)
            {
                foreach (var level1Item in level0Item.ProcedureBudgetLevel1)
                {
                    level1Item.IsActivated = true;

                    foreach (var level2Item in level1Item.ProcedureBudgetLevel2)
                    {
                        level2Item.IsActivated = true;
                    }
                }
            }

            foreach (var appDoc in this.ProcedureApplicationDocs)
            {
                appDoc.IsActivated = true;
            }

            foreach (var specField in this.ProcedureSpecFields)
            {
                specField.IsActivated = true;
            }

            foreach (var indicator in this.ProcedureIndicators)
            {
                indicator.IsActivated = true;
            }

            foreach (var evalTable in this.ProcedureEvalTables)
            {
                evalTable.IsActivated = true;
            }

            foreach (var question in this.ProcedureQuestions)
            {
                question.IsActivated = true;
            }

            foreach (var share in this.ProcedureShares)
            {
                share.IsActivated = true;
            }

            ((IEventEmitter)this).Events.Add(new ProcedureActivatedEvent() { ProcedureId = this.ProcedureId });
        }

        private void ChangeStatusFromActive(ProcedureStatus procedureStatus)
        {
            switch (procedureStatus)
            {
                case ProcedureStatus.Ended:
                    ((IEventEmitter)this).Events.Add(new ProcedureEndedEvent() { ProcedureId = this.ProcedureId });
                    break;
                case ProcedureStatus.Terminated:
                    ((IEventEmitter)this).Events.Add(new ProcedureTerminatedEvent() { ProcedureId = this.ProcedureId });
                    break;
                default:
                    throw new DomainValidationException("Procedure status transition not allowed");
            }
        }

        private void AssertIsDraftProcedure()
        {
            if (this.ProcedureStatus != ProcedureStatus.Draft)
            {
                throw new DomainValidationException("Cannot edit procedure that is not in Draft status");
            }
        }

        private void AssertIsDraftProcedureContractReportDocumentsSection()
        {
            if (this.ProcedureContractReportDocumentsSectionStatus != ProcedureContractReportDocumentsSectionStatus.Draft)
            {
                throw new DomainValidationException("Cannot edit contract report document if procedure contract report documents section is not in Draft status");
            }
        }

        private void AssertIsNotActivated(bool isActavated)
        {
            if (isActavated)
            {
                throw new DomainValidationException("Cannot edit object that is activated");
            }
        }

        private void AssertIsActivated(bool isActavated)
        {
            if (!isActavated)
            {
                throw new DomainValidationException("Cannot active/deactivate an object that is not activated");
            }
        }

        private bool HasCompleteSetEvalTables(int activeAminAdmissEvalTablesCount, int activeTechFinanceEvalTablesCount, int activeComplexEvalTablesCount)
        {
            if (this.ProcedureKind == ProcedureKind.Budget)
            {
                return true;
            }

            if (activeAminAdmissEvalTablesCount == 0 &&
                 activeTechFinanceEvalTablesCount == 0 &&
                 activeComplexEvalTablesCount == 0)
            {
                return false;
            }

            if (activeAminAdmissEvalTablesCount != activeTechFinanceEvalTablesCount)
            {
                return false;
            }

            return true;
        }

        #endregion // Procedure

        #region ProcedureIndicator

        public ProcedureIndicator FindProcedureIndicator(int indicatorId)
        {
            var pi = this.ProcedureIndicators.Where(i => i.IndicatorId == indicatorId).SingleOrDefault();

            if (pi == null)
            {
                throw new DomainObjectNotFoundException("Cannot find ProcedureIndicator with id " + indicatorId);
            }

            return pi;
        }

        public int[] GetProcedureIndicatorIds()
        {
            return this.ProcedureIndicators.Select(e => e.IndicatorId).ToArray();
        }

        public void UpdateProcedureIndicator(
            int indicatorId,
            decimal? baseTotalValue,
            decimal? baseMenValue,
            decimal? baseWomenValue,
            int? baseYear,
            decimal? targetTotalValue,
            decimal? targetMenValue,
            decimal? targetWomenValue,
            decimal? milestoneTargetTotalValue,
            decimal? milestoneTargetMenValue,
            decimal? milestoneTargetWomenValue,
            string dataSource,
            string description)
        {
            this.AssertIsDraftProcedure();

            var pi = this.FindProcedureIndicator(indicatorId);

            this.AssertIsNotActivated(pi.IsActivated);

            pi.SetAttributes(
                baseTotalValue,
                baseMenValue,
                baseWomenValue,
                baseYear,
                targetTotalValue,
                targetMenValue,
                targetWomenValue,
                milestoneTargetTotalValue,
                milestoneTargetMenValue,
                milestoneTargetWomenValue,
                dataSource,
                description);

            this.ModifyDate = DateTime.Now;
        }

        public void AddProcedureIndicator(
            int indicatorId,
            decimal? baseTotalValue,
            decimal? baseMenValue,
            decimal? baseWomenValue,
            int? baseYear,
            decimal? targetTotalValue,
            decimal? targetMenValue,
            decimal? targetWomenValue,
            decimal? milestoneTargetTotalValue,
            decimal? milestoneTargetMenValue,
            decimal? milestoneTargetWomenValue,
            string dataSource,
            string description,
            int sourceMapNodeId)
        {
            this.AssertIsDraftProcedure();

            this.ProcedureIndicators.Add(new ProcedureIndicator()
            {
                ProcedureId = this.ProcedureId,
                IndicatorId = indicatorId,
                BaseTotalValue = baseTotalValue,
                BaseMenValue = baseMenValue,
                BaseWomenValue = baseWomenValue,
                BaseYear = baseYear,
                TargetTotalValue = targetTotalValue,
                TargetMenValue = targetMenValue,
                TargetWomenValue = targetWomenValue,
                MilestoneTargetTotalValue = milestoneTargetTotalValue,
                MilestoneTargetMenValue = milestoneTargetMenValue,
                MilestoneTargetWomenValue = milestoneTargetWomenValue,
                DataSource = dataSource,
                Description = description,
                SourceMapNodeId = sourceMapNodeId,
            });

            this.ModifyDate = DateTime.Now;
        }

        public void RemoveProcedureIndicator(int indicatorId)
        {
            this.AssertIsDraftProcedure();

            var pi = this.FindProcedureIndicator(indicatorId);

            this.AssertIsNotActivated(pi.IsActivated);

            this.ProcedureIndicators.Remove(pi);

            this.ModifyDate = DateTime.Now;
        }

        public void DeactivateProcedureIndicator(int indicatorId)
        {
            this.AssertIsDraftProcedure();

            var pi = this.FindProcedureIndicator(indicatorId);

            this.AssertIsActivated(pi.IsActivated);

            pi.IsActive = false;

            this.ModifyDate = DateTime.Now;
        }

        public void ActivateProcedureIndicator(int indicatorId)
        {
            this.AssertIsDraftProcedure();

            var pi = this.FindProcedureIndicator(indicatorId);

            this.AssertIsActivated(pi.IsActivated);

            pi.IsActive = true;

            this.ModifyDate = DateTime.Now;
        }

        #endregion // ProcedureIndicator

        #region ProcedureShare

        private ProcedureShare FindProcedureShareByPPriorityAndFinSource(int programmeId, int programmePriorityId)
        {
            var procedureShare = this.ProcedureShares.SingleOrDefault(e =>
                e.ProgrammeId == programmeId &&
                e.ProgrammePriorityId == programmePriorityId);

            if (procedureShare == null)
            {
                throw new DomainObjectNotFoundException(string.Format("Cannot find ProcedureShare with programmePriorityId {0} ", programmePriorityId));
            }

            return procedureShare;
        }

        public ProcedureShare FindProcedureShare(int procedureShareId)
        {
            var procedureShare = this.ProcedureShares.Where(e => e.ProcedureShareId == procedureShareId).SingleOrDefault();

            if (procedureShare == null)
            {
                throw new DomainObjectNotFoundException("Cannot find ProcedureShare with id " + procedureShareId);
            }

            return procedureShare;
        }

        public ProcedureShare FindPrimaryProcedureShare()
        {
            return this.ProcedureShares.SingleOrDefault(e => e.IsPrimary);
        }

        public List<ProcedureShare> GetProcedureSharesByProgramme(int programmeId)
        {
            return this.ProcedureShares.Where(e => e.ProgrammeId == programmeId).ToList();
        }

        public void AddProcedureShare(
            int programmeId,
            int programmePriorityId,
            decimal bgAmount,
            bool isPrimary)
        {
            this.AssertIsDraftProcedure();

            ProcedureShare procedureShare = new ProcedureShare()
            {
                ProgrammeId = programmeId,
                ProgrammePriorityId = programmePriorityId,
                BgAmount = bgAmount,
                IsPrimary = isPrimary,
            };

            this.ProcedureShares.Add(procedureShare);

            if (!this.ProcedureProgrammes.Any(e => e.ProgrammeId == programmeId))
            {
                this.ProcedureProgrammes.Add(new ProcedureProgramme()
                {
                    ProcedureId = this.ProcedureId,
                    ProgrammeId = programmeId,
                });
            }

            this.ModifyDate = DateTime.Now;
        }

        public void UpdateProcedureShare(int procedureShareId, decimal bgAmount)
        {
            this.AssertIsDraftProcedure();

            var procedureShare = this.FindProcedureShare(procedureShareId);

            procedureShare.SetAttributes(bgAmount);

            this.ModifyDate = DateTime.Now;
        }

        public void RemoveProcedureShare(int procedureShareId)
        {
            this.AssertIsDraftProcedure();

            var procedureShare = this.FindProcedureShare(procedureShareId);

            this.AssertIsNotActivated(procedureShare.IsActivated);

            if (procedureShare.IsPrimary)
            {
                throw new Exception("Unable to delete primary procedureShare.");
            }
            else if (procedureShare.ProcedureBudgetLevel2.Count > 0)
            {
                throw new Exception("Unable to delete procedureShare. There are linked expense budgets.");
            }

            this.ProcedureShares.Remove(procedureShare);

            if (!this.ProcedureShares.Any(e => e.ProgrammeId == procedureShare.ProgrammeId))
            {
                var procedureProgramme = this.FindProcedureProgrammeByProgramme(procedureShare.ProgrammeId);

                this.ProcedureProgrammes.Remove(procedureProgramme);
            }

            this.ModifyDate = DateTime.Now;
        }

        #endregion //ProcedureShare

        #region ProcedureProgrammes

        private ProcedureProgramme FindProcedureProgrammeByProgramme(int programmeId)
        {
            var procedureProgrammes = this.ProcedureProgrammes.SingleOrDefault(e => e.ProgrammeId == programmeId);

            if (procedureProgrammes == null)
            {
                throw new DomainObjectNotFoundException("Cannot find ProcedureProgrammes with programmeId " + programmeId);
            }

            return procedureProgrammes;
        }

        #endregion //ProcedureProgrammes

        #region ProcedureTimeLimit

        public ProcedureTimeLimit FindProcedureTimeLimit(int procedureTimeLimitId)
        {
            var procedureTimeLimit = this.ProcedureTimeLimits.Where(e => e.ProcedureTimeLimitId == procedureTimeLimitId).SingleOrDefault();

            if (procedureTimeLimit == null)
            {
                throw new DomainObjectNotFoundException("Cannot find ProcedureTimeLimit with id " + procedureTimeLimitId);
            }

            return procedureTimeLimit;
        }

        public void AddProcedureTimeLimit(DateTime endDate, string notes)
        {
            this.AssertIsDraftProcedure();

            ProcedureTimeLimit procedureTimeLimit = new ProcedureTimeLimit()
            {
                EndDate = endDate,
                Notes = notes,
            };

            this.ProcedureTimeLimits.Add(procedureTimeLimit);

            this.ModifyDate = DateTime.Now;
        }

        public void UpdateProcedureTimeLimit(int procedureTimeLimitId, DateTime endDate, string notes)
        {
            this.AssertIsDraftProcedure();

            var procedureTimeLimit = this.FindProcedureTimeLimit(procedureTimeLimitId);

            if (this.ProcedureTimeLimits.OrderBy(t => t.EndDate).Last().ProcedureTimeLimitId != procedureTimeLimitId)
            {
                throw new DomainException("Cannot update ProcedureTimeLimit other than the last");
            }

            procedureTimeLimit.SetAttributes(endDate, notes);

            this.ModifyDate = DateTime.Now;
        }

        public void RemoveProcedureTimeLimit(int procedureTimeLimitId)
        {
            this.AssertIsDraftProcedure();

            var procedureTimeLimit = this.FindProcedureTimeLimit(procedureTimeLimitId);

            this.ProcedureTimeLimits.Remove(procedureTimeLimit);

            this.ModifyDate = DateTime.Now;
        }

        public void CopyProcedureTimeLimits(ICollection<ProcedureTimeLimit> timeLimits)
        {
            foreach (var timeLimit in timeLimits)
            {
                this.ProcedureTimeLimits.Add(new ProcedureTimeLimit()
                {
                    EndDate = timeLimit.EndDate,
                    Notes = timeLimit.Notes,
                });
            }
        }

        #endregion //ProcedureTimeLimit

        #region ProcedureExpenseBudget

        private void AssertIsValidExpression(ProcedureProgramme procedureProgramme, string expression)
        {
            if (!string.IsNullOrWhiteSpace(expression) && ProcedureValidationEngine.Instance.ValidateExpression(expression, procedureProgramme) != null)
            {
                throw new DomainValidationException("Invalid expression: " + expression);
            }
        }

        public ProcedureBudgetLevel1 FindProcedureBudgetLevel1(int procedureBudgetLevel1Id)
        {
            foreach (var level0Item in this.ProcedureProgrammes)
            {
                foreach (var level1Item in level0Item.ProcedureBudgetLevel1)
                {
                    if (level1Item.ProcedureBudgetLevel1Id == procedureBudgetLevel1Id)
                    {
                        return level1Item;
                    }
                }
            }

            throw new DomainObjectNotFoundException("Cannot find ProcedureBudgetLevel1 with id " + procedureBudgetLevel1Id);
        }

        public ProcedureBudgetLevel2 FindProcedureBudgetLevel2(int procedureBudgetLevel2Id)
        {
            foreach (var level0Item in this.ProcedureProgrammes)
            {
                foreach (var level1Item in level0Item.ProcedureBudgetLevel1)
                {
                    foreach (var level2Item in level1Item.ProcedureBudgetLevel2)
                    {
                        if (level2Item.ProcedureBudgetLevel2Id == procedureBudgetLevel2Id)
                        {
                            return level2Item;
                        }
                    }
                }
            }

            throw new DomainObjectNotFoundException("Cannot find ProcedureBudgetLevel2 with id " + procedureBudgetLevel2Id);
        }

        public ProcedureBudgetLevel3 FindProcedureBudgetLevel3(int procedureBudgetLevel3Id)
        {
            foreach (var level0Item in this.ProcedureProgrammes)
            {
                foreach (var level1Item in level0Item.ProcedureBudgetLevel1)
                {
                    foreach (var level2Item in level1Item.ProcedureBudgetLevel2)
                    {
                        foreach (var level3Item in level2Item.ProcedureBudgetLevel3)
                        {
                            if (level3Item.ProcedureBudgetLevel3Id == procedureBudgetLevel3Id)
                            {
                                return level3Item;
                            }
                        }
                    }
                }
            }

            throw new DomainObjectNotFoundException("Cannot find ProcedureBudgetLevel3 with id " + procedureBudgetLevel3Id);
        }

        public ProcedureBudgetValidationRule FindProcedureBudgetValidationRule(int procedureBudgetValidationRuleId)
        {
            foreach (var level0Item in this.ProcedureProgrammes)
            {
                foreach (var validationRule in level0Item.ProcedureBudgetValidationRules)
                {
                    if (validationRule.ProcedureBudgetValidationRuleId == procedureBudgetValidationRuleId)
                    {
                        return validationRule;
                    }
                }
            }

            throw new DomainObjectNotFoundException("Cannot find ProcedureBudgetValidationRule with id " + procedureBudgetValidationRuleId);
        }

        public void AddProcedureBudgetLevel1(int programmeId, int expenseTypeId)
        {
            this.AssertIsDraftProcedure();

            ProcedureProgramme procedureProgramme = this.FindProcedureProgrammeByProgramme(programmeId);

            int orderNum = procedureProgramme.ProcedureBudgetLevel1.Any() ? (procedureProgramme.ProcedureBudgetLevel1.Max(e => e.OrderNum) + 1) : 1;

            ProcedureBudgetLevel1 newProcedureBudgetLevel1 = new ProcedureBudgetLevel1(programmeId, expenseTypeId, orderNum);

            procedureProgramme.ProcedureBudgetLevel1.Add(newProcedureBudgetLevel1);

            this.ModifyDate = DateTime.Now;
        }

        public void RemoveProcedureBudgetLevel1(int procedureBudgetLevel1Id)
        {
            this.AssertIsDraftProcedure();

            foreach (var prp in this.ProcedureProgrammes)
            {
                var level1Items = prp.ProcedureBudgetLevel1.ToList();

                foreach (var level1Item in level1Items)
                {
                    if (level1Item.ProcedureBudgetLevel1Id == procedureBudgetLevel1Id)
                    {
                        this.AssertIsNotActivated(level1Item.IsActivated);
                        var level2Items = level1Item.ProcedureBudgetLevel2.ToList();

                        foreach (var level2Item in level2Items)
                        {
                            var level3Items = level2Item.ProcedureBudgetLevel3.ToList();

                            foreach (var level3Item in level3Items)
                            {
                                level2Item.ProcedureBudgetLevel3.Remove(level3Item);
                            }

                            level1Item.ProcedureBudgetLevel2.Remove(level2Item);
                        }

                        prp.ProcedureBudgetLevel1.Remove(level1Item);

                        this.ModifyDate = DateTime.Now;

                        return;
                    }
                }
            }
        }

        public void DeactivateProcedureBudgetLevel1(int procedureBudgetLevel1Id)
        {
            this.AssertIsDraftProcedure();

            var pblvl1 = this.FindProcedureBudgetLevel1(procedureBudgetLevel1Id);

            this.AssertIsActivated(pblvl1.IsActivated);

            pblvl1.IsActive = false;

            this.ModifyDate = DateTime.Now;
        }

        public void ActivateProcedureBudgetLevel1(int procedureBudgetLevel1Id)
        {
            this.AssertIsDraftProcedure();

            var pblvl1 = this.FindProcedureBudgetLevel1(procedureBudgetLevel1Id);

            this.AssertIsActivated(pblvl1.IsActivated);

            pblvl1.IsActive = true;

            this.ModifyDate = DateTime.Now;
        }

        public List<string> CanDeleteProcedureBudgetLevel1(int procedureBudgetLevel1Id)
        {
            List<string> errors = new List<string>();

            bool hasLinkedValidations = false;

            var pblvl1 = this.FindProcedureBudgetLevel1(procedureBudgetLevel1Id);

            foreach (var pblvl2 in pblvl1.ProcedureBudgetLevel2)
            {
                if (this.CanDeleteProcedureBudgetLevel2(pblvl2.ProcedureBudgetLevel2Id).Count > 0)
                {
                    hasLinkedValidations = true;
                    break;
                }
            }

            if (!hasLinkedValidations)
            {
                foreach (var validationRule in this.ProcedureProgrammes.Single(e => e.ProgrammeId == pblvl1.ProgrammeId).ProcedureBudgetValidationRules)
                {
                    if (validationRule.HasConditionSerializableGuid(pblvl1.Gid) || validationRule.HasRuleSerializableGuid(pblvl1.Gid))
                    {
                        hasLinkedValidations = true;
                        break;
                    }
                }
            }

            if (hasLinkedValidations)
            {
                errors.Add("Разходът не може да бъде изтрит, защото той или някой от подчинените му разходи участват във валидационните формули");
            }

            return errors;
        }

        public void AddProcedureBudgetLevel2(
            int procedureBudgetLevel1Id,
            int programmeId,
            int programmePriorityId,
            string name,
            string nameAlt,
            ProcedureBudgetLevel2AidMode aidMode)
        {
            this.AssertIsDraftProcedure();

            ProcedureShare procedureShare = this.FindProcedureShareByPPriorityAndFinSource(programmeId, programmePriorityId);

            ProcedureBudgetLevel1 procedureBudgetLevel1 = this.FindProcedureBudgetLevel1(procedureBudgetLevel1Id);

            int orderNum = procedureBudgetLevel1.ProcedureBudgetLevel2.Any() ? (procedureBudgetLevel1.ProcedureBudgetLevel2.Max(e => e.OrderNum) + 1) : 1;

            ProcedureBudgetLevel2 newProcedureBudgetLevel2 =
                new ProcedureBudgetLevel2(
                    procedureShare.ProcedureShareId,
                    procedureBudgetLevel1Id,
                    name,
                    nameAlt,
                    orderNum,
                    aidMode);

            procedureBudgetLevel1.ProcedureBudgetLevel2.Add(newProcedureBudgetLevel2);

            this.ModifyDate = DateTime.Now;
        }

        public void EditProcedureBudgetLevel2(
            int procedureBudgetLevel2Id,
            string name,
            string nameAlt,
            ProcedureBudgetLevel2AidMode aidMode)
        {
            this.AssertIsDraftProcedure();

            ProcedureBudgetLevel2 procedureBudgetLevel2 = this.FindProcedureBudgetLevel2(procedureBudgetLevel2Id);
            this.AssertIsNotActivated(procedureBudgetLevel2.IsActivated);

            procedureBudgetLevel2.Name = name;
            procedureBudgetLevel2.NameAlt = nameAlt;
            procedureBudgetLevel2.AidMode = aidMode;

            this.ModifyDate = DateTime.Now;
        }

        public void RemoveProcedureBudgetLevel2(int procedureBudgetLevel2Id)
        {
            this.AssertIsDraftProcedure();

            foreach (var prp in this.ProcedureProgrammes)
            {
                var level1Items = prp.ProcedureBudgetLevel1.ToList();

                foreach (var level1Item in level1Items)
                {
                    var level2Items = level1Item.ProcedureBudgetLevel2.ToList();

                    foreach (var level2Item in level2Items)
                    {
                        if (level2Item.ProcedureBudgetLevel2Id == procedureBudgetLevel2Id)
                        {
                            this.AssertIsNotActivated(level2Item.IsActivated);
                            var level3Items = level2Item.ProcedureBudgetLevel3.ToList();

                            foreach (var level3Item in level3Items)
                            {
                                level2Item.ProcedureBudgetLevel3.Remove(level3Item);
                            }

                            level1Item.ProcedureBudgetLevel2.Remove(level2Item);

                            this.ModifyDate = DateTime.Now;

                            return;
                        }
                    }
                }
            }
        }

        public void DeactivateProcedureBudgetLevel2(int procedureBudgetLevel2Id)
        {
            this.AssertIsDraftProcedure();

            var pblvl2 = this.FindProcedureBudgetLevel2(procedureBudgetLevel2Id);

            this.AssertIsActivated(pblvl2.IsActivated);

            pblvl2.IsActive = false;

            this.ModifyDate = DateTime.Now;
        }

        public void ActivateProcedureBudgetLevel2(int procedureBudgetLevel2Id)
        {
            this.AssertIsDraftProcedure();

            var pblvl2 = this.FindProcedureBudgetLevel2(procedureBudgetLevel2Id);

            this.AssertIsActivated(pblvl2.IsActivated);

            pblvl2.IsActive = true;

            this.ModifyDate = DateTime.Now;
        }

        public List<string> CanDeleteProcedureBudgetLevel2(int procedureBudgetLevel2Id)
        {
            List<string> errors = new List<string>();

            var hasLinkedValidation = false;

            var pblvl2 = this.FindProcedureBudgetLevel2(procedureBudgetLevel2Id);

            foreach (var validationRule in this.ProcedureProgrammes.Single(e => e.ProgrammeId == pblvl2.ProcedureBudgetLevel1.ProgrammeId).ProcedureBudgetValidationRules)
            {
                if (validationRule.HasConditionSerializableGuid(pblvl2.Gid) || validationRule.HasRuleSerializableGuid(pblvl2.Gid))
                {
                    hasLinkedValidation = true;
                    break;
                }
            }

            if (hasLinkedValidation)
            {
                errors.Add("Разходът не може да бъде изтрит, защото участва във валидационните формули");
            }

            return errors;
        }

        public void AddProcedureBudgetLevel3(int procedureBudgetLevel2Id, string note)
        {
            this.AssertIsDraftProcedure();

            ProcedureBudgetLevel2 procedureBudgetLevel2 = this.FindProcedureBudgetLevel2(procedureBudgetLevel2Id);

            int orderNum = procedureBudgetLevel2.ProcedureBudgetLevel3.Any() ? (procedureBudgetLevel2.ProcedureBudgetLevel3.Max(e => e.OrderNum) + 1) : 1;

            procedureBudgetLevel2.ProcedureBudgetLevel3.Add(new ProcedureBudgetLevel3(note, orderNum));

            this.ModifyDate = DateTime.Now;
        }

        public void EditProcedureBudgetLevel3(
            int procedureBudgetLevel3Id,
            string note)
        {
            this.AssertIsDraftProcedure();

            ProcedureBudgetLevel3 procedureBudgetLevel3 = this.FindProcedureBudgetLevel3(procedureBudgetLevel3Id);

            procedureBudgetLevel3.Note = note;

            this.ModifyDate = DateTime.Now;
        }

        public void RemoveProcedureBudgetLevel3(int procedureBudgetLevel3Id)
        {
            this.AssertIsDraftProcedure();

            foreach (var prp in this.ProcedureProgrammes)
            {
                var level1Items = prp.ProcedureBudgetLevel1.ToList();

                foreach (var level1Item in level1Items)
                {
                    var level2Items = level1Item.ProcedureBudgetLevel2.ToList();

                    foreach (var level2Item in level2Items)
                    {
                        var level3Items = level2Item.ProcedureBudgetLevel3.ToList();

                        foreach (var level3Item in level3Items)
                        {
                            if (level3Item.ProcedureBudgetLevel3Id == procedureBudgetLevel3Id)
                            {
                                level2Item.ProcedureBudgetLevel3.Remove(level3Item);

                                this.ModifyDate = DateTime.Now;

                                return;
                            }
                        }
                    }
                }
            }
        }

        public void AddProcedureBudgetValidationRule(
            int programmeId,
            string message,
            string condition,
            string rule)
        {
            this.AssertIsDraftProcedure();

            ProcedureProgramme procedureProgramme = this.FindProcedureProgrammeByProgramme(programmeId);

            this.AssertIsValidExpression(procedureProgramme, condition);
            this.AssertIsValidExpression(procedureProgramme, rule);

            ProcedureBudgetValidationRule newProcedureBudgetValidationRule = new ProcedureBudgetValidationRule(procedureProgramme, message, condition, rule);

            procedureProgramme.ProcedureBudgetValidationRules.Add(newProcedureBudgetValidationRule);

            this.ModifyDate = DateTime.Now;
        }

        public void EditProcedureBudgetValidationRule(
            int procedureBudgetValidationRuleId,
            string message,
            string condition,
            string rule)
        {
            this.AssertIsDraftProcedure();

            ProcedureBudgetValidationRule procedureBudgetValidationRule = this.FindProcedureBudgetValidationRule(procedureBudgetValidationRuleId);

            this.AssertIsValidExpression(procedureBudgetValidationRule.ProcedureProgramme, condition);
            this.AssertIsValidExpression(procedureBudgetValidationRule.ProcedureProgramme, rule);

            procedureBudgetValidationRule.Message = message;
            procedureBudgetValidationRule.Condition = condition;
            procedureBudgetValidationRule.Rule = rule;

            this.ModifyDate = DateTime.Now;
        }

        public void RemoveProcedureBudgetValidationRule(int procedureBudgetValidationRuleId)
        {
            this.AssertIsDraftProcedure();

            foreach (var prp in this.ProcedureProgrammes)
            {
                var validationRules = prp.ProcedureBudgetValidationRules.ToList();

                foreach (var validationRule in validationRules)
                {
                    if (validationRule.ProcedureBudgetValidationRuleId == procedureBudgetValidationRuleId)
                    {
                        prp.ProcedureBudgetValidationRules.Remove(validationRule);

                        this.ModifyDate = DateTime.Now;

                        return;
                    }
                }
            }
        }

        public string ValidateExpression(int programmeId, string expression)
        {
            ProcedureProgramme procedureProgramme = this.FindProcedureProgrammeByProgramme(programmeId);

            return ProcedureValidationEngine.Instance.ValidateExpression(expression, procedureProgramme);
        }

        public void CopyProcedureProgrammes(ICollection<ProcedureProgramme> procedureProgrammes)
        {
            var oldProcedureProgramme = procedureProgrammes.Single();
            var newProcedureProgramme = this.ProcedureProgrammes.Single();

            var newProcedureShareId = this.ProcedureShares.Single().ProcedureShareId;

            foreach (var oldBudgetLevel1 in oldProcedureProgramme.ProcedureBudgetLevel1)
            {
                var newBudgetLevel1 = new ProcedureBudgetLevel1(
                    oldBudgetLevel1.ProgrammeId,
                    oldBudgetLevel1.ExpenseTypeId,
                    oldBudgetLevel1.OrderNum);

                foreach (var oldBudgetLevel2 in oldBudgetLevel1.ProcedureBudgetLevel2)
                {
                    var newBudgetLevel2 = new ProcedureBudgetLevel2(
                        newProcedureShareId,
                        newBudgetLevel1.ProcedureBudgetLevel1Id,
                        oldBudgetLevel2.Name,
                        oldBudgetLevel2.NameAlt,
                        oldBudgetLevel2.OrderNum,
                        oldBudgetLevel2.AidMode);

                    foreach (var oldBudgetLevel3 in oldBudgetLevel2.ProcedureBudgetLevel3)
                    {
                        newBudgetLevel2.ProcedureBudgetLevel3.Add(new ProcedureBudgetLevel3()
                        {
                            Note = oldBudgetLevel3.Note,
                            OrderNum = oldBudgetLevel3.OrderNum,
                        });
                    }

                    newBudgetLevel1.ProcedureBudgetLevel2.Add(newBudgetLevel2);
                }

                newProcedureProgramme.ProcedureBudgetLevel1.Add(newBudgetLevel1);
            }

            foreach (var oldValidationRule in oldProcedureProgramme.ProcedureBudgetValidationRules)
            {
                newProcedureProgramme.ProcedureBudgetValidationRules.Add(
                    new ProcedureBudgetValidationRule(
                        newProcedureProgramme,
                        oldValidationRule.Message,
                        oldValidationRule.Condition,
                        oldValidationRule.Rule));
            }
        }

        #endregion //ProcedureExpenseBudget

        #region ProcedureApplicationGuideline

        public ProcedureApplicationGuideline FindProcedureApplicationGuideline(int appGuidelineId)
        {
            var pag = this.ProcedureApplicationGuidelines.Where(i => i.ProcedureApplicationGuidelineId == appGuidelineId).SingleOrDefault();

            if (pag == null)
            {
                throw new DomainObjectNotFoundException("Cannot find ProcedureApplicationGuideline with id " + appGuidelineId);
            }

            return pag;
        }

        public void UpdateProcedureApplicationGuideline(
            int appGuidelineId,
            string name,
            string description,
            Guid blobKey)
        {
            this.AssertIsDraftProcedure();

            var pi = this.FindProcedureApplicationGuideline(appGuidelineId);
            pi.SetAttributes(
                name,
                description,
                blobKey);

            this.ModifyDate = DateTime.Now;
        }

        public void AddProcedureApplicationGuideline(
            string name,
            string description,
            Guid blobKey)
        {
            this.AssertIsDraftProcedure();

            this.ProcedureApplicationGuidelines.Add(new ProcedureApplicationGuideline(name, description, blobKey));
            this.ModifyDate = DateTime.Now;
        }

        public void RemoveProcedureApplicationGuideline(int appGuidelineId)
        {
            this.AssertIsDraftProcedure();

            var pag = this.FindProcedureApplicationGuideline(appGuidelineId);
            this.ProcedureApplicationGuidelines.Remove(pag);

            this.ModifyDate = DateTime.Now;
        }

        #endregion //ProcedureApplicationGuideline

        #region ProcedureSpecField

        public ProcedureSpecField FindProcedureSpecField(int specFieldId)
        {
            var f = this.ProcedureSpecFields.Where(i => i.ProcedureSpecFieldId == specFieldId).SingleOrDefault();

            if (f == null)
            {
                throw new DomainObjectNotFoundException("Cannot find ProcedureSpecField with id " + specFieldId);
            }

            return f;
        }

        public void UpdateProcedureSpecField(int specFieldId, string title, string titleAlt, string description, string descriptionAlt, bool isRequired, ProcedureSpecFieldMaxLength maxLength)
        {
            this.AssertIsDraftProcedure();

            var f = this.FindProcedureSpecField(specFieldId);

            this.AssertIsNotActivated(f.IsActivated);

            f.SetAttributes(title, titleAlt, description, descriptionAlt, isRequired, maxLength);

            this.ModifyDate = DateTime.Now;
        }

        public void AddProcedureSpecField(string title, string titleAlt, string description, string descriptionAlt, bool isRequired, ProcedureSpecFieldMaxLength maxLength)
        {
            this.AssertIsDraftProcedure();

            this.ProcedureSpecFields.Add(new ProcedureSpecField(title, titleAlt, description, descriptionAlt, isRequired, maxLength));

            this.ModifyDate = DateTime.Now;
        }

        public void RemoveProcedureSpecField(int specFieldId)
        {
            this.AssertIsDraftProcedure();

            var f = this.FindProcedureSpecField(specFieldId);
            this.AssertIsNotActivated(f.IsActivated);
            this.ProcedureSpecFields.Remove(f);

            this.ModifyDate = DateTime.Now;
        }

        public void DeactivateProcedureSpecField(int specFieldId)
        {
            this.AssertIsDraftProcedure();

            var f = this.FindProcedureSpecField(specFieldId);

            this.AssertIsActivated(f.IsActivated);

            f.IsActive = false;

            this.ModifyDate = DateTime.Now;
        }

        public void ActivateProcedureSpecField(int specFieldId)
        {
            this.AssertIsDraftProcedure();

            var f = this.FindProcedureSpecField(specFieldId);

            this.AssertIsActivated(f.IsActivated);

            f.IsActive = true;

            this.ModifyDate = DateTime.Now;
        }

        public void CopyProcedureSpecFields(ICollection<ProcedureSpecField> specFields)
        {
            foreach (var specField in specFields)
            {
                this.ProcedureSpecFields.Add(new ProcedureSpecField(
                    specField.Title,
                    specField.TitleAlt,
                    specField.Description,
                    specField.DescriptionAlt,
                    specField.IsRequired,
                    specField.MaxLength));
            }
        }

        #endregion //ProcedureSpecField

        #region ProcedureDocument

        public ProcedureDocument FindProcedureDocument(int documentId)
        {
            var pd = this.ProcedureDocuments.Where(d => d.ProcedureDocumentId == documentId).SingleOrDefault();

            if (pd == null)
            {
                throw new DomainObjectNotFoundException("Cannot find ProcedureDocument with id " + documentId);
            }

            return pd;
        }

        public void UpdateProcedureDocument(
            int documentId,
            string name,
            string description,
            Guid? blobKey)
        {
            this.AssertIsDraftProcedure();

            var pd = this.FindProcedureDocument(documentId);
            pd.SetAttributes(
                name,
                description,
                blobKey);

            this.ModifyDate = DateTime.Now;
        }

        public void AddProcedureDocument(
            string name,
            string description,
            Guid? blobKey)
        {
            this.ProcedureDocuments.Add(new ProcedureDocument()
            {
                ProcedureId = this.ProcedureId,
                Name = name,
                Description = description,
                BlobKey = blobKey,
            });

            this.ModifyDate = DateTime.Now;
        }

        public void RemoveProcedureDocument(int documentId)
        {
            this.AssertIsDraftProcedure();

            var pd = this.FindProcedureDocument(documentId);
            this.ProcedureDocuments.Remove(pd);

            this.ModifyDate = DateTime.Now;
        }

        #endregion // ProcedureDocument

        #region ProcedureApplicationDoc

        private void AssertAppDocumentIsEditable(bool isActivated, bool isActive)
        {
            if (isActivated && isActive)
            {
                throw new DomainValidationException("Cannot edit document that is in active status");
            }
        }

        public ProcedureApplicationDoc FindProcedureApplicationDoc(int applicationDocId)
        {
            var pad = this.ProcedureApplicationDocs.Where(d => d.ProcedureApplicationDocId == applicationDocId).SingleOrDefault();

            if (pad == null)
            {
                throw new DomainObjectNotFoundException("Cannot find ProcedureApplicationDoc with id " + applicationDocId);
            }

            return pad;
        }

        public void UpdateProcedureApplicationDoc(
            int applicationDocId,
            int? programmeApplicationDocumentId,
            string name,
            string extension,
            bool isRequired,
            bool isSignatureRequired)
        {
            this.AssertIsDraftProcedure();

            var appDocument = this.FindProcedureApplicationDoc(applicationDocId);
            this.AssertAppDocumentIsEditable(appDocument.IsActivated, appDocument.IsActive);

            appDocument.SetAttributes(
                programmeApplicationDocumentId,
                name,
                extension,
                isRequired,
                isSignatureRequired);

            this.ModifyDate = DateTime.Now;
        }

        public void AddProcedureApplicationDoc(
            int? programmeApplicationDocumentId,
            string name,
            bool isRequired,
            bool isSignatureRequired)
        {
            this.AssertIsDraftProcedure();

            this.ProcedureApplicationDocs.Add(new ProcedureApplicationDoc(programmeApplicationDocumentId, name, isRequired, isSignatureRequired));

            this.ModifyDate = DateTime.Now;
        }

        public void RemoveProcedureApplicationDoc(int applicationDocId)
        {
            this.AssertIsDraftProcedure();

            var pad = this.FindProcedureApplicationDoc(applicationDocId);

            this.AssertIsNotActivated(pad.IsActivated);

            this.ProcedureApplicationDocs.Remove(pad);

            this.ModifyDate = DateTime.Now;
        }

        public void DeactivateProcedureApplicationDoc(int applicationDocId)
        {
            this.AssertIsDraftProcedure();

            var pad = this.FindProcedureApplicationDoc(applicationDocId);

            this.AssertIsActivated(pad.IsActivated);

            pad.IsActive = false;

            this.ModifyDate = DateTime.Now;
        }

        public void ActivateProcedureApplicationDoc(int applicationDocId)
        {
            this.AssertIsDraftProcedure();

            var pad = this.FindProcedureApplicationDoc(applicationDocId);

            this.AssertIsActivated(pad.IsActivated);

            pad.IsActive = true;

            this.ModifyDate = DateTime.Now;
        }

        #endregion // ProcedureApplicationDoc

        #region ProcedureEvalTable

        public ProcedureEvalTable FindProcedureEvalTable(int evalTableId)
        {
            var prt = this.ProcedureEvalTables.Where(d => d.ProcedureEvalTableId == evalTableId).SingleOrDefault();

            if (prt == null)
            {
                throw new DomainObjectNotFoundException("Cannot find ProcedureEvalTable with id " + evalTableId);
            }

            return prt;
        }

        public void UpdateProcedureEvalTable(
            int evalTableId,
            string name,
            ProcedureEvalTableType type)
        {
            this.AssertIsDraftProcedure();

            var pd = this.FindProcedureEvalTable(evalTableId);
            this.AssertIsNotActivated(pd.IsActivated);
            pd.SetAttributes(name, type);

            this.ModifyDate = DateTime.Now;
        }

        public ProcedureEvalTable AddProcedureEvalTable(
            string name,
            ProcedureEvalTableType type,
            ProcedureEvalType evalType)
        {
            this.AssertIsDraftProcedure();

            var procedureEvalTable = new ProcedureEvalTable()
            {
                ProcedureId = this.ProcedureId,
                Name = name,
                Type = type,
                EvalType = evalType,
                Status = ProcedureEvalTableStatus.Draft,
            };

            this.ProcedureEvalTables.Add(procedureEvalTable);

            this.ModifyDate = DateTime.Now;

            return procedureEvalTable;
        }

        public void RemoveProcedureEvalTable(int evalTableId)
        {
            this.AssertIsDraftProcedure();

            var prt = this.FindProcedureEvalTable(evalTableId);
            this.AssertIsNotActivated(prt.IsActivated);
            this.ProcedureEvalTables.Remove(prt);

            this.ModifyDate = DateTime.Now;
        }

        public void DeactivateProcedureEvalTable(int evalTableId)
        {
            this.AssertIsDraftProcedure();

            var prt = this.FindProcedureEvalTable(evalTableId);

            this.AssertIsActivated(prt.IsActivated);

            prt.IsActive = false;

            this.ModifyDate = DateTime.Now;
        }

        public void ActivateProcedureEvalTable(int evalTableId)
        {
            this.AssertIsDraftProcedure();

            var prt = this.FindProcedureEvalTable(evalTableId);

            this.AssertIsActivated(prt.IsActivated);

            prt.IsActive = true;

            this.ModifyDate = DateTime.Now;
        }

        public void EndProcedureEvalTable(int evalTableId)
        {
            this.AssertIsDraftProcedure();

            var pet = this.FindProcedureEvalTable(evalTableId);

            pet.Status = ProcedureEvalTableStatus.Ended;

            this.ModifyDate = DateTime.Now;
        }

        public IList<Tuple<ProcedureEvalTable, string>> CopyProcedureEvalTables(ICollection<ProcedureEvalTable> evalTables, IList<ProcedureEvalTableXml> evalTableXmls)
        {
            var evalTablesWithXml = new List<Tuple<ProcedureEvalTable, string>>();

            foreach (var evalTable in evalTables)
            {
                var newEvalTable = new ProcedureEvalTable()
                {
                    Name = evalTable.Name,
                    Type = evalTable.Type,
                    EvalType = evalTable.EvalType,
                    Status = ProcedureEvalTableStatus.Draft,
                };

                this.ProcedureEvalTables.Add(newEvalTable);

                var xml = evalTableXmls.Single(et => et.ProcedureEvalTableId == evalTable.ProcedureEvalTableId).Xml;

                evalTablesWithXml.Add(new Tuple<ProcedureEvalTable, string>(newEvalTable, xml));
            }

            return evalTablesWithXml;
        }

        #endregion // ProcedureEvalTable

        #region ProcedureQuestion

        public ProcedureQuestion FindProcedureQuestion(int questionId)
        {
            var pq = this.ProcedureQuestions.Where(d => d.ProcedureQuestionId == questionId).SingleOrDefault();

            if (pq == null)
            {
                throw new DomainObjectNotFoundException("Cannot find ProcedureQuestion with id " + questionId);
            }

            return pq;
        }

        public void AddProcedureQuestion(Guid blobKey, int currentUserId)
        {
            this.AssertCanAddProcedureQuestion();

            var procedureQuestion = new ProcedureQuestion()
            {
                ProcedureId = this.ProcedureId,
                CreatedByUserId = currentUserId,
                CreateDate = DateTime.Now,
                BlobKey = blobKey,
            };

            if (this.ProcedureStatus != Procedures.ProcedureStatus.Active)
            {
                procedureQuestion.IsActivated = false;
            }
            else
            {
                procedureQuestion.IsActivated = true;
                ((IEventEmitter)this).Events.Add(new ProcedureQaActivatedEvent() { ProcedureId = this.ProcedureId });
            }

            this.ProcedureQuestions.Add(procedureQuestion);

            this.ModifyDate = DateTime.Now;
        }

        public void RemoveProcedureQuestion(int questionId)
        {
            this.AssertIsDraftProcedure();

            var pq = this.FindProcedureQuestion(questionId);

            this.AssertIsNotActivated(pq.IsActivated);

            if (this.ProcedureQuestions.OrderBy(t => t.CreateDate).Last().ProcedureQuestionId != questionId)
            {
                throw new DomainException("Cannot delete ProcedureQuestion other than the last");
            }

            this.ProcedureQuestions.Remove(pq);

            this.ModifyDate = DateTime.Now;
        }

        public void AssertCanAddProcedureQuestion()
        {
            if (this.ProcedureStatus != ProcedureStatus.Draft && this.ProcedureStatus != ProcedureStatus.Active)
            {
                throw new DomainValidationException("Cannot add ProcedureQuestion to procedure that is not in Draft or Active status");
            }

            if (!this.ActivationDate.HasValue)
            {
                throw new DomainValidationException("Cannot add ProcedureQuestion to procedure that is not announced");
            }
        }

        #endregion // ProcedureQuestion

        #region ProcedureContractReportDocument

        public ProcedureContractReportDocument FindProcedureContractReportDocument(int procedureContractReportDocumentId)
        {
            var procedureContractReportDocument = this.ProcedureContractReportDocuments.Where(d => d.ProcedureContractReportDocumentId == procedureContractReportDocumentId).SingleOrDefault();

            if (procedureContractReportDocument == null)
            {
                throw new DomainObjectNotFoundException("Cannot find ProcedureContractReportDocument with id " + procedureContractReportDocumentId);
            }

            return procedureContractReportDocument;
        }

        public void UpdateProcedureContractReportDocument(
            int procedureContractReportDocumentId,
            string name,
            string extension,
            bool isRequired)
        {
            this.AssertIsDraftProcedureContractReportDocumentsSection();

            var procedureContractReportDocument = this.FindProcedureContractReportDocument(procedureContractReportDocumentId);
            procedureContractReportDocument.SetAttributes(
                name,
                extension,
                isRequired);

            this.ModifyDate = DateTime.Now;
        }

        public void AddProcedureContractReportDocument(
            string name,
            string extension,
            bool isRequired,
            ProcedureContractReportDocumentType type)
        {
            this.AssertIsDraftProcedureContractReportDocumentsSection();

            var documentType = ProcedureContractReportDocument.ProcedureReportDocuments[type];

            var newDocument = (ProcedureContractReportDocument)Activator.CreateInstance(documentType, name, extension, isRequired);

            this.ProcedureContractReportDocuments.Add(newDocument);

            this.ModifyDate = DateTime.Now;
        }

        public void RemoveProcedureContractReportDocument(int procedureContractReportDocumentId)
        {
            this.AssertIsDraftProcedureContractReportDocumentsSection();

            var procedureContractReportDocument = this.FindProcedureContractReportDocument(procedureContractReportDocumentId);

            this.AssertIsNotActivated(procedureContractReportDocument.IsActivated);

            this.ProcedureContractReportDocuments.Remove(procedureContractReportDocument);

            this.ModifyDate = DateTime.Now;
        }

        public void DeactivateProcedureContractReportDocument(int procedureContractReportDocumentId)
        {
            this.AssertIsDraftProcedureContractReportDocumentsSection();

            var procedureContractReportDocument = this.FindProcedureContractReportDocument(procedureContractReportDocumentId);

            this.AssertIsActivated(procedureContractReportDocument.IsActivated);

            procedureContractReportDocument.IsActive = false;

            this.ModifyDate = DateTime.Now;
        }

        public void ActivateProcedureContractReportDocument(int procedureContractReportDocumentId)
        {
            this.AssertIsDraftProcedureContractReportDocumentsSection();

            var procedureContractReportDocument = this.FindProcedureContractReportDocument(procedureContractReportDocumentId);

            this.AssertIsActivated(procedureContractReportDocument.IsActivated);

            procedureContractReportDocument.IsActive = true;

            this.ModifyDate = DateTime.Now;
        }

        public void CopyProcedureContractReportDocuments(ICollection<ProcedureContractReportDocument> procedureContractReportDocuments)
        {
            foreach (var document in procedureContractReportDocuments)
            {
                var documentType = ProcedureContractReportDocument.ProcedureReportDocuments[document.Type];

                var newDocument = (ProcedureContractReportDocument)Activator.CreateInstance(documentType, document.Name, document.Extension, document.IsRequired);

                this.ProcedureContractReportDocuments.Add(newDocument);
                this.ModifyDate = DateTime.Now;
            }
        }

        #endregion // ProcedureContractReportDocument

        #region ProcedureLocation

        public ProcedureLocation FindProcedureLocation(int locationId)
        {
            var location = this.ProcedureLocations.Where(x => x.ProcedureLocationId == locationId).FirstOrDefault();

            if (location == null)
            {
                throw new DomainObjectNotFoundException($"Procedure with id {this.ProcedureId} doesn't contain location with id {locationId}");
            }

            return location;
        }

        public void AddProcedureLocation(NutsLevel nutsLevel, int? countryId, int? nuts1Id, int? nuts2Id, int? districtId, int? municipalityId, int? settlementId, int? protectedZoneId)
        {
            this.AssertIsDraftProcedure();
            var location = new ProcedureLocation(nutsLevel, countryId, nuts1Id, nuts2Id, districtId, municipalityId, settlementId, protectedZoneId);

            this.ProcedureLocations.Add(location);
            this.ModifyDate = DateTime.Now;
        }

        public void UpdateProcedureLocation(int procedureLocationId, NutsLevel nutsLevel, int? countryId, int? nuts1Id, int? nuts2Id, int? districtId, int? municipalityId, int? settlementId, int? protectedZoneId)
        {
            this.AssertIsDraftProcedure();
            var location = this.ProcedureLocations.Where(x => x.ProcedureLocationId == procedureLocationId).Single();

            location.NutsLevel = nutsLevel;
            location.CountryId = countryId;
            location.Nuts1Id = nuts1Id;
            location.Nuts2Id = nuts2Id;
            location.DistrictId = districtId;
            location.MunicipalityId = municipalityId;
            location.SettlementId = settlementId;
            location.ProtectedZoneId = protectedZoneId;

            this.ModifyDate = DateTime.Now;
        }

        public void RemoveProcedureLocation(int locationId)
        {
            this.AssertIsDraftProcedure();
            var location = this.FindProcedureLocation(locationId);

            this.ProcedureLocations.Remove(location);
            this.ModifyDate = DateTime.Now;
        }

        public void CopyProcedureLocations(ICollection<ProcedureLocation> locations)
        {
            foreach (var location in locations)
            {
                this.ProcedureLocations.Add(new ProcedureLocation(
                    location.NutsLevel,
                    location.CountryId,
                    location.Nuts1Id,
                    location.Nuts2Id,
                    location.DistrictId,
                    location.MunicipalityId,
                    location.SettlementId,
                    location.ProtectedZoneId));
            }
        }

        public void RemoveProcedureLAGMunicipalities(IList<int> municipalityIds)
        {
            this.AssertIsDraftProcedure();

            foreach (var municipalityId in municipalityIds)
            {
                var location = this.ProcedureLocations.FirstOrDefault(l => l.NutsLevel == NutsLevel.Municipality && l.MunicipalityId == municipalityId);

                if (location != null)
                {
                    this.ProcedureLocations.Remove(location);
                }
            }
        }

        public void AddProcedureLAGMunicipalities(IEnumerable<int> municipalityIds)
        {
            this.AssertIsDraftProcedure();

            foreach (var municipalityId in municipalityIds)
            {
                this.ProcedureLocations.Add(new ProcedureLocation(municipalityId));
            }
        }

        #endregion //ProcedureLocation

        #region ApplicableSections

        public IList<ApplicationSectionType> GetApplicableSections()
        {
            return this.GetApplicableSections(this.ProcedureKind);
        }

        public IList<ApplicationSectionType> GetApplicableSections(ProcedureKind procedureKind)
        {
            return Procedure.AvailableApplicableSections
                .Where(t => t.Item1 == procedureKind).Select(t => t.Item2)
                .ToList();
        }

        public void InsertOrUpdateApplicationSections(IList<ValueTuple<ApplicationSectionType, bool, int>> sections)
        {
            this.ProcedureApplicationSections.Clear();
            foreach (var section in sections)
            {
                var newSection = new ProcedureApplicationSection(
                    section.Item1,
                    section.Item2,
                    section.Item3);

                this.ProcedureApplicationSections.Add(newSection);
                this.ModifyDate = DateTime.Now;
            }
        }

        public void InsertOrUpdateApplicationSectionAdditionalSettings(IList<ValueTuple<string, bool>> settings)
        {
            if (this.ProcedureApplicationSectionAdditionalSetting == null)
            {
                this.ProcedureApplicationSectionAdditionalSetting = new ProcedureApplicationSectionAdditionalSetting(this.ProcedureId);
            }

            foreach (var setting in settings)
            {
                var prop = this.ProcedureApplicationSectionAdditionalSetting.GetType().GetProperty(setting.Item1);
                prop.SetValue(this.ProcedureApplicationSectionAdditionalSetting, setting.Item2, null);
            }

            this.ModifyDate = DateTime.Now;
        }

        private void SetProcedureApplicationSectionDefaults()
        {
            if (this.ProcedureKind == ProcedureKind.Schema)
            {
                return;
            }

            var budgetSections = this.GetApplicableSections()
                .Select((x, t) => (x, x == ApplicationSectionType.Indicators && Procedure.HideIndicators ? false : true, t + 1))
                .ToList();

            this.InsertOrUpdateApplicationSections(budgetSections);
        }

        public void CopyApplicationSections(ICollection<ProcedureApplicationSection> oldProcedureApplicationSections)
        {
            if (this.ProcedureKind == ProcedureKind.Budget)
            {
                return;
            }

            foreach (var section in oldProcedureApplicationSections)
            {
                var newSection = new ProcedureApplicationSection(section.Section, section.IsSelected, section.OrderNum);
                this.ProcedureApplicationSections.Add(newSection);
            }
        }

        #endregion

        #region Directions

        public ProcedureDirection FindProcedureDirection(int procedureDirectionId)
        {
            var direction = this.ProcedureDirections
                .Where(x => x.ProcedureDirectionId == procedureDirectionId)
                .FirstOrDefault();

            if (direction == null)
            {
                throw new DomainObjectNotFoundException("Cannot find ProcedureDirection with id " + procedureDirectionId);
            }

            return direction;
        }

        public void AddProcedureDirection(int mapNodeId, int directionId, int? subDirectionId)
        {
            if (this.ProcedureDirections
                .Any(x => x.DirectionId == directionId && x.SubDirectionId == subDirectionId && x.ProgrammePriorityId == mapNodeId))
            {
                throw new DomainValidationException($"Procedure with id: {this.ProcedureId} already has direction with id {directionId} and sub direction {subDirectionId}");
            }

            this.AddProcedureDirectionInternal(mapNodeId, directionId, subDirectionId);
        }

        public void UpdateProcedureDirection(int procedureDirectionId, decimal? amount)
        {
            var procedureDirection = this.FindProcedureDirection(procedureDirectionId);

            procedureDirection.Amount = amount;
            this.ModifyDate = DateTime.Now;
        }

        public void RemoveProcedureDirection(int procedureDirectionId)
        {
            var procedureDirection = this.FindProcedureDirection(procedureDirectionId);

            this.ProcedureDirections.Remove(procedureDirection);

            this.ModifyDate = DateTime.Now;
        }

        public void CopyProcedureDirections(ICollection<ProcedureDirection> oldProcedureDirections)
        {
            foreach (var oldDirection in oldProcedureDirections)
            {
                this.AddProcedureDirectionInternal(
                    oldDirection.ProgrammePriorityId,
                    oldDirection.DirectionId,
                    oldDirection.SubDirectionId,
                    oldDirection.Amount);
            }
        }

        private void AddProcedureDirectionInternal(int programmePrirityId, int directionId, int? subDirectionId, decimal? amount = null)
        {
            var procedureDirection = new ProcedureDirection(programmePrirityId, directionId, subDirectionId, amount);

            this.ProcedureDirections.Add(procedureDirection);

            this.ModifyDate = DateTime.Now;
        }
        #endregion
    }
}
