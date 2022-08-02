using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Eumis.Domain.Debts;
using Eumis.Domain.NonAggregates;

namespace Eumis.Domain.Irregularities
{
    public partial class IrregularityVersion
    {
        #region IrregularityVersion

        [SuppressMessage("", "SA1515:SingleLineCommentMustBePrecededByBlankLine", Justification = "inline comments used for clarity")]
        [SuppressMessage("", "SA1114:ParameterListMustFollowDeclaration", Justification = "inline comments used for clarity")]
        public void UpdateVersionData(
            // basic data
            DateTime irregularityDateFrom,
            DateTime? irregularityDateTo,
            IrregularityClassification? irregularityClassification,
            int? irregularityCategoryId,
            int? irregularityTypeId,
            string endingActRegNum,
            DateTime? endingActDate,
            IrregularityCaseState? caseState,
            DateTime? irregularityEndDate,
            // report data
            DateTime createDate,
            Year reportYear,
            Quarter reportQuarter,
            IrregularityRapporteur? rapporteur,
            string rapporteurComments,
            // impaired regulations
            IrregularityImpairedRegulationAct? impairedRegulationAct,
            string impairedRegulationNum,
            int? impairedRegulationYear,
            string impairedRegulation,
            string impairedNationalRegulation,
            // euPercent,
            decimal? euCoFinancingPercent,
            // expenses
            decimal? expensesBfpEuAmountLv,
            decimal? expensesBfpBgAmountLv,
            decimal? expensesSelfAmountLv,
            decimal? expensesBfpEuAmountEuro,
            decimal? expensesBfpBgAmountEuro,
            decimal? expensesSelfAmountEuro,
            // irregular expenses
            decimal? irregularExpensesBfpEuAmountLv,
            decimal? irregularExpensesBfpBgAmountLv,
            decimal? irregularExpensesBfpEuAmountEuro,
            decimal? irregularExpensesBfpBgAmountEuro,
            // certified expenses
            decimal? certifiedExpensesBfpEuAmountLv,
            decimal? certifiedExpensesBfpBgAmountLv,
            decimal? certifiedExpensesBfpEuAmountEuro,
            decimal? certifiedExpensesBfpBgAmountEuro,
            // paid
            decimal? paidBfpEuAmountLv,
            decimal? paidBfpBgAmountLv,
            decimal? paidBfpEuAmountEuro,
            decimal? paidBfpBgAmountEuro,
            // decertification
            bool? shouldDecertifyIrregularExpenses,
            string decertificationComments,
            // sanctions
            IrregularitySanctionProcedureType sanctionProcedureType,
            IrregularitySanctionProcedureKind? sanctionProcedureKind,
            DateTime? sanctionProcedureStartDate,
            DateTime? sanctionProcedureExpectedEndDate,
            DateTime? sanctionProcedureEndDate,
            IrregularitySanctionProcedureStatus? sanctionProcedureStatus,
            int? sanctionCategoryId,
            int? sanctionTypeId,
            string fines,
            // general data
            ContractDebtExecutionStatus? contractDebtStatus,
            bool? isNewUnlawfulPractice,
            bool? shouldInformOther,
            IrregularityProcedureStatus? procedureStatus,
            int? financialStatusId,
            string appliedPractices,
            string beneficiaryData,
            string adminAscertainments,
            string irregularityDetectedBy,
            string adminProcedures,
            string penaltyProcedures,
            bool shouldReportToOlaf,
            IrregularityReasonNotReportingToOlaf? reasonNotReportingToOlaf,
            IrregularityCheckTime? checkTime)
        {
            this.IrregularityDateFrom = irregularityDateFrom;
            this.IrregularityDateTo = irregularityDateTo;
            this.IrregularityClassification = irregularityClassification;
            this.IrregularityCategoryId = irregularityCategoryId;
            this.IrregularityTypeId = irregularityTypeId;
            this.EndingActRegNum = endingActRegNum;
            this.EndingActDate = endingActDate;
            this.CaseState = caseState;
            this.IrregularityEndDate = caseState.HasValue && caseState == IrregularityCaseState.Ended ? irregularityEndDate.Value : (DateTime?)null;

            this.CreateDate = createDate;
            this.ReportYear = reportYear;
            this.ReportQuarter = reportQuarter;
            this.Rapporteur = rapporteur;
            this.RapporteurComments = rapporteurComments;

            this.EUCoFinancingPercent = euCoFinancingPercent;

            this.ImpairedRegulation.SetAttributes(
                impairedRegulationAct,
                impairedRegulationNum,
                impairedRegulationYear,
                impairedRegulation,
                impairedNationalRegulation);

            this.ExpensesLv.SetAttributes(
                expensesBfpEuAmountLv,
                expensesBfpBgAmountLv,
                expensesSelfAmountLv);

            this.ExpensesEuro.SetAttributes(
                expensesBfpEuAmountEuro,
                expensesBfpBgAmountEuro,
                expensesSelfAmountEuro);

            this.IrregularExpensesLv.SetAttributes(irregularExpensesBfpEuAmountLv, irregularExpensesBfpBgAmountLv);
            this.IrregularExpensesEuro.SetAttributes(irregularExpensesBfpEuAmountEuro, irregularExpensesBfpBgAmountEuro);

            this.CertifiedExpensesLv.SetAttributes(certifiedExpensesBfpEuAmountLv, certifiedExpensesBfpBgAmountLv);
            this.CertifiedExpensesEuro.SetAttributes(certifiedExpensesBfpEuAmountEuro, certifiedExpensesBfpBgAmountEuro);

            this.PaidLv.SetAttributes(paidBfpEuAmountLv, paidBfpBgAmountLv);
            this.PaidEuro.SetAttributes(paidBfpEuAmountEuro, paidBfpBgAmountEuro);

            this.ShouldDecertifyIrregularExpenses = shouldDecertifyIrregularExpenses;
            this.DecertificationComments = decertificationComments;

            this.Sanction.SetAttributes(
                sanctionProcedureType,
                sanctionProcedureKind,
                sanctionProcedureStartDate,
                sanctionProcedureExpectedEndDate,
                sanctionProcedureEndDate,
                sanctionProcedureStatus,
                sanctionCategoryId,
                sanctionTypeId,
                fines);

            this.ContractDebtStatus = contractDebtStatus;
            this.IsNewUnlawfulPractice = isNewUnlawfulPractice;
            this.ShouldInformOther = shouldInformOther;
            this.ProcedureStatus = procedureStatus;
            this.FinancialStatusId = financialStatusId;
            this.AppliedPractices = appliedPractices;
            this.BeneficiaryData = beneficiaryData;
            this.AdminAscertainments = adminAscertainments;
            this.IrregularityDetectedBy = irregularityDetectedBy;
            this.AdminProcedures = adminProcedures;
            this.PenaltyProcedures = penaltyProcedures;
            this.ShouldReportToOlaf = shouldReportToOlaf;
            this.ReasonNotReportingToOlaf = reasonNotReportingToOlaf;
            this.CheckTime = checkTime;

            this.ModifyDate = DateTime.Now;
        }

        public void ActivateVersion(string regNumberPattern)
        {
            this.AssertIsDraft();

            this.RegNumber = string.Format("{0}/{1:00}", regNumberPattern, this.OrderNum);
            this.Status = IrregularityVersionStatus.Active;
            this.ModifyDate = DateTime.Now;
        }

        public void ArchiveVersion()
        {
            if (this.Status != IrregularityVersionStatus.Active)
            {
                throw new DomainValidationException("Irregularity version status must be active!");
            }

            this.Status = IrregularityVersionStatus.Archived;
        }

        private void AssertIsDraft()
        {
            if (this.Status != IrregularityVersionStatus.Draft)
            {
                throw new DomainValidationException("Irregularity version status must be draft!");
            }
        }

        #endregion //IrregularityVersion

        #region IrregularityVersionInvolvedPerson

        public IrregularityVersionInvolvedPerson AddInvolvedPerson(
            string uin,
            UinType uinType,
            string undisclosureMotives,
            string firstName,
            string middleName,
            string lastName,
            int? countryId,
            int? settlementId,
            string postCode,
            string street,
            string address)
        {
            this.AssertIsDraft();

            var newPerson = new IrregularityVersionInvolvedPerson()
            {
                IrregularityVersionId = this.IrregularityVersionId,
                LegalType = InvolvedPersonLegalType.Person,
                Uin = uin,
                UinType = uinType,
                UndisclosureMotives = undisclosureMotives,
                CompanyName = null,
                TradeName = null,
                HoldingName = null,
                FirstName = firstName,
                MiddleName = middleName,
                LastName = lastName,
                CountryId = countryId,
                SettlementId = settlementId,
                PostCode = postCode,
                Street = street,
                Address = address,
            };

            this.InvolvedPersons.Add(newPerson);
            this.ModifyDate = DateTime.Now;

            return newPerson;
        }

        public IrregularityVersionInvolvedPerson AddInvolvedLegalPerson(
            string uin,
            UinType uinType,
            string undisclosureMotives,
            string companyName,
            string tradeName,
            string holdingName,
            int? countryId,
            int? settlementId,
            string postCode,
            string street,
            string address)
        {
            this.AssertIsDraft();

            var newPerson = new IrregularityVersionInvolvedPerson()
            {
                IrregularityVersionId = this.IrregularityVersionId,
                LegalType = InvolvedPersonLegalType.LegalPerson,
                Uin = uin,
                UinType = uinType,
                UndisclosureMotives = undisclosureMotives,
                CompanyName = companyName,
                TradeName = tradeName,
                HoldingName = holdingName,
                FirstName = null,
                MiddleName = null,
                LastName = null,
                CountryId = countryId,
                SettlementId = settlementId,
                PostCode = postCode,
                Street = street,
                Address = address,
            };

            this.InvolvedPersons.Add(newPerson);
            this.ModifyDate = DateTime.Now;

            return newPerson;
        }

        public IrregularityVersionInvolvedPerson GetInvolvedPerson(int personId)
        {
            var person = this.InvolvedPersons.Single(ip => ip.IrregularityVersionInvolvedPersonId == personId);

            if (person == null)
            {
                throw new DomainObjectNotFoundException("Cannot find IrregularityVersionInvolvedPerson with id " + personId);
            }

            return person;
        }

        public void UpdateInvolvedPerson(
            int personId,
            string uin,
            UinType uinType,
            string undisclosureMotives,
            string firstName,
            string middleName,
            string lastName,
            int? countryId,
            int? settlementId,
            string postCode,
            string street,
            string address)
        {
            this.AssertIsDraft();

            var person = this.GetInvolvedPerson(personId);
            person.SetAttributes(
                InvolvedPersonLegalType.Person,
                uin,
                uinType,
                undisclosureMotives,
                null,
                null,
                null,
                firstName,
                middleName,
                lastName,
                countryId,
                settlementId,
                postCode,
                street,
                address);

            this.ModifyDate = DateTime.Now;
        }

        public void UpdateInvolvedLegalPerson(
            int personId,
            string uin,
            UinType uinType,
            string undisclosureMotives,
            string companyName,
            string tradeName,
            string holdingName,
            int? countryId,
            int? settlementId,
            string postCode,
            string street,
            string address)
        {
            this.AssertIsDraft();

            var person = this.GetInvolvedPerson(personId);
            person.SetAttributes(
                InvolvedPersonLegalType.LegalPerson,
                uin,
                uinType,
                undisclosureMotives,
                companyName,
                tradeName,
                holdingName,
                null,
                null,
                null,
                countryId,
                settlementId,
                postCode,
                street,
                address);

            this.ModifyDate = DateTime.Now;
        }

        public void RemoveInvolvedPerson(int personId)
        {
            this.AssertIsDraft();

            var person = this.GetInvolvedPerson(personId);
            this.InvolvedPersons.Remove(person);

            this.ModifyDate = DateTime.Now;
        }

        #endregion //IrregularityVersionInvolvedPerson

        #region IrregularityVersionDoc

        public void AddDocument(
            string description,
            string fileName,
            Guid fileKey)
        {
            this.AssertIsDraft();

            this.Documents.Add(new IrregularityVersionDoc()
            {
                IrregularityVersionId = this.IrregularityVersionId,
                FileName = fileName,
                Description = description,
                FileKey = fileKey,
            });

            this.ModifyDate = DateTime.Now;
        }

        public IrregularityVersionDoc GetDocument(int documentId)
        {
            var document = this.Documents.Single(d => d.IrregularityVersionDocId == documentId);

            if (document == null)
            {
                throw new DomainObjectNotFoundException("Cannot find IrregularityVersionDoc with id " + documentId);
            }

            return document;
        }

        public void UpdateDocument(
            int documentId,
            string description,
            string fileName,
            Guid fileKey)
        {
            this.AssertIsDraft();

            var document = this.GetDocument(documentId);
            document.SetAttributes(description, fileName, fileKey);

            this.ModifyDate = DateTime.Now;
        }

        public void RemoveDocument(int documentId)
        {
            this.AssertIsDraft();

            var document = this.GetDocument(documentId);
            this.Documents.Remove(document);

            this.ModifyDate = DateTime.Now;
        }

        #endregion //IrregularityVersionDoc
    }
}
