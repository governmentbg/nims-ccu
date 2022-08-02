using System;
using Eumis.Domain.Irregularities;
using Eumis.Domain.NonAggregates;

namespace Eumis.Data.Irregularities.ViewObjects
{
    public class IrrReportItemVO
    {
        // irregularityData
        public int IrregularityId { get; set; }

        public int IrregularityVersionId { get; set; }

        public string RegNumber { get; set; }

        public string ProgrammeCode { get; set; }

        public string ProgrammeName { get; set; }

        public string ProjectRegNumber { get; set; }

        public string ProjectName { get; set; }

        public string SignalNumber { get; set; }

        public DateTime? SignalRegDate { get; set; }

        public string SignalSource { get; set; }

        public string SignalActRegNum { get; set; }

        public DateTime? SignalActRegDate { get; set; }

        // basic data
        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public IrregularityRapporteur? Rapporteur { get; set; }

        public Year ReportYear { get; set; }

        public Quarter ReportQuarter { get; set; }

        public bool ShouldReportToOlaf { get; set; }

        public IrregularityReasonNotReportingToOlaf? ReasonNotReportingToOlaf { get; set; }

        public bool? IsNewUnlawfulPractice { get; set; }

        public bool? ShouldInformOther { get; set; }

        public IrregularityProcedureStatus? ProcedureStatus { get; set; }

        public string FinancialStatus { get; set; }

        public IrregularityCaseState? CaseState { get; set; }

        public DateTime? IrregularityEndDate { get; set; }

        public string EndingActRegNum { get; set; }

        public DateTime? EndingActDate { get; set; }

        // impaired regulation
        public IrregularityImpairedRegulationAct? ImpairedRegulationAct { get; set; }

        public string ImpairedRegulationNum { get; set; }

        public int? ImpairedRegulationYear { get; set; }

        public string ImpairedRegulation { get; set; }

        public string ImpairedNationalRegulation { get; set; }

        // common data
        public DateTime IrregularityDateFrom { get; set; }

        public DateTime? IrregularityDateTo { get; set; }

        public IrregularityClassification? IrregularityClassification { get; set; }

        public string IrregularityCategory { get; set; }

        public string IrregularityType { get; set; }

        public string AppliedPractices { get; set; }

        public string BeneficiaryData { get; set; }

        public string AdminAscertainments { get; set; }

        public string IrregularityDetectedBy { get; set; }

        public string AdminProcedures { get; set; }

        public string PenaltyProcedures { get; set; }

        public IrregularityCheckTime? CheckTime { get; set; }

        // amounts
        public decimal? EUCoFinancingPercent { get; set; }

        public decimal? ExpensesLvBfpEuAmount { get; set; }

        public decimal? ExpensesLvBfpBgAmount { get; set; }

        public decimal? ExpensesLvBfpTotalAmount { get; set; }

        public decimal? ExpensesLvSelfAmount { get; set; }

        public decimal? ExpensesLvTotalAmount { get; set; }

        public decimal? IrregularExpensesLvEuAmount { get; set; }

        public decimal? IrregularExpensesLvBgAmount { get; set; }

        public decimal? IrregularExpensesLvTotalAmount { get; set; }

        public decimal? CertifiedExpensesLvEuAmount { get; set; }

        public decimal? CertifiedExpensesLvBgAmount { get; set; }

        public decimal? CertifiedExpensesLvTotalAmount { get; set; }

        public bool? ShouldDecertifyIrregularExpenses { get; set; }

        public string DecertificationComments { get; set; }

        // sanction procedure
        public IrregularitySanctionProcedureType SanctionProcedureType { get; set; }

        public IrregularitySanctionProcedureKind? SanctionProcedureKind { get; set; }

        public DateTime? SanctionProcedureStartDate { get; set; }

        public DateTime? SanctionProcedureExpectedEndDate { get; set; }

        public DateTime? SanctionProcedureEndDate { get; set; }

        public IrregularitySanctionProcedureStatus? SanctionProcedureStatus { get; set; }

        public string SanctionCategory { get; set; }

        public string SanctionType { get; set; }

        public string Fines { get; set; }

        // comments
        public string RapporteurComments { get; set; }
    }
}
