using System;
using Eumis.Common.Json;
using Eumis.Domain.Debts;
using Eumis.Domain.Irregularities;
using Eumis.Domain.NonAggregates;
using Newtonsoft.Json;

namespace Eumis.Web.Api.Irregularities.DataObjects
{
    public class IrregularityVersionDO
    {
        public IrregularityVersionDO()
        {
        }

        public IrregularityVersionDO(IrregularityVersion version)
        {
            this.VersionId = version.IrregularityVersionId;
            this.IrregularityId = version.IrregularityId;
            this.OrderNum = version.OrderNum;
            this.RegNumber = version.RegNumber;
            this.Status = version.Status;
            this.CreateDate = version.CreateDate;
            this.ModifyDate = version.ModifyDate;

            this.Language = "BG";
            this.Currency = "EUR";

            this.IrregularityDateFrom = version.IrregularityDateFrom;
            this.IrregularityDateTo = version.IrregularityDateTo;
            this.IrregularityClassification = version.IrregularityClassification;
            this.IrregularityCategoryId = version.IrregularityCategoryId;
            this.IrregularityTypeId = version.IrregularityTypeId;
            this.EndingActRegNum = version.EndingActRegNum;
            this.EndingActDate = version.EndingActDate;
            this.CaseState = version.CaseState;
            this.IrregularityEndDate = version.IrregularityEndDate;

            this.ReportYear = version.ReportYear;
            this.ReportQuarter = version.ReportQuarter;
            this.Rapporteur = version.Rapporteur;
            this.RapporteurComments = version.RapporteurComments;

            this.EUCoFinancingPercent = version.EUCoFinancingPercent;

            this.ImpairedRegulationAct = version.ImpairedRegulation.ImpairedRegulationAct;
            this.ImpairedRegulationNum = version.ImpairedRegulation.ImpairedRegulationNum;
            this.ImpairedRegulationYear = version.ImpairedRegulation.ImpairedRegulationYear;
            this.ImpairedRegulation = version.ImpairedRegulation.ImpairedRegulation;
            this.ImpairedNationalRegulation = version.ImpairedRegulation.ImpairedNationalRegulation;

            this.ExpensesBfpEuAmountLv = version.ExpensesLv.BfpEuAmount;
            this.ExpensesBfpBgAmountLv = version.ExpensesLv.BfpBgAmount;
            this.ExpensesBfpTotalAmountLv = version.ExpensesLv.BfpTotalAmount;
            this.ExpensesSelfAmountLv = version.ExpensesLv.SelfAmount;
            this.ExpensesTotalAmountLv = version.ExpensesLv.TotalAmount;

            this.ExpensesBfpEuAmountEuro = version.ExpensesEuro.BfpEuAmount;
            this.ExpensesBfpBgAmountEuro = version.ExpensesEuro.BfpBgAmount;
            this.ExpensesBfpTotalAmountEuro = version.ExpensesEuro.BfpTotalAmount;
            this.ExpensesSelfAmountEuro = version.ExpensesEuro.SelfAmount;
            this.ExpensesTotalAmountEuro = version.ExpensesEuro.TotalAmount;

            this.IrregularExpensesBfpEuAmountLv = version.IrregularExpensesLv.EuAmount;
            this.IrregularExpensesBfpBgAmountLv = version.IrregularExpensesLv.BgAmount;
            this.IrregularExpensesBfpTotalAmountLv = version.IrregularExpensesLv.TotalAmount;

            this.IrregularExpensesBfpEuAmountEuro = version.IrregularExpensesEuro.EuAmount;
            this.IrregularExpensesBfpBgAmountEuro = version.IrregularExpensesEuro.BgAmount;
            this.IrregularExpensesBfpTotalAmountEuro = version.IrregularExpensesEuro.TotalAmount;

            this.CertifiedExpensesBfpEuAmountLv = version.CertifiedExpensesLv.EuAmount;
            this.CertifiedExpensesBfpBgAmountLv = version.CertifiedExpensesLv.BgAmount;
            this.CertifiedExpensesBfpTotalAmountLv = version.CertifiedExpensesLv.TotalAmount;

            this.CertifiedExpensesBfpEuAmountEuro = version.CertifiedExpensesEuro.EuAmount;
            this.CertifiedExpensesBfpBgAmountEuro = version.CertifiedExpensesEuro.BgAmount;
            this.CertifiedExpensesBfpTotalAmountEuro = version.CertifiedExpensesEuro.TotalAmount;

            this.PaidBfpEuAmountLv = version.PaidLv.EuAmount;
            this.PaidBfpBgAmountLv = version.PaidLv.BgAmount;
            this.PaidBfpTotalAmountLv = version.PaidLv.TotalAmount;

            this.PaidBfpEuAmountEuro = version.PaidEuro.EuAmount;
            this.PaidBfpBgAmountEuro = version.PaidEuro.BgAmount;
            this.PaidBfpTotalAmountEuro = version.PaidEuro.TotalAmount;

            this.ContractDebtStatus = version.ContractDebtStatus;
            this.ShouldDecertifyIrregularExpenses = version.ShouldDecertifyIrregularExpenses;
            this.DecertificationComments = version.DecertificationComments;

            this.SanctionProcedureType = version.Sanction.ProcedureType;
            this.SanctionProcedureKind = version.Sanction.ProcedureKind;
            this.SanctionProcedureStartDate = version.Sanction.ProcedureStartDate;
            this.SanctionProcedureExpectedEndDate = version.Sanction.ProcedureExpectedEndDate;
            this.SanctionProcedureEndDate = version.Sanction.ProcedureEndDate;
            this.SanctionProcedureStatus = version.Sanction.ProcedureStatus;
            this.SanctionCategoryId = version.Sanction.SanctionCategoryId;
            this.SanctionTypeId = version.Sanction.SanctionTypeId;
            this.Fines = version.Sanction.Fines;

            this.IsNewUnlawfulPractice = version.IsNewUnlawfulPractice;
            this.ShouldInformOther = version.ShouldInformOther;
            this.ProcedureStatus = version.ProcedureStatus;
            this.FinancialStatusId = version.FinancialStatusId;
            this.AppliedPractices = version.AppliedPractices;
            this.BeneficiaryData = version.BeneficiaryData;
            this.AdminAscertainments = version.AdminAscertainments;
            this.IrregularityDetectedBy = version.IrregularityDetectedBy;
            this.AdminProcedures = version.AdminProcedures;
            this.PenaltyProcedures = version.PenaltyProcedures;
            this.ShouldReportToOlaf = version.ShouldReportToOlaf;
            this.ReasonNotReportingToOlaf = version.ReasonNotReportingToOlaf;
            this.CheckTime = version.CheckTime;

            this.Version = version.Version;
        }

        public int VersionId { get; set; }

        public int IrregularityId { get; set; }

        public int OrderNum { get; set; }

        public string RegNumber { get; set; }

        public IrregularityVersionStatus Status { get; set; }

        public string Language { get; set; }

        public string Currency { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public DateTime? IrregularityDateFrom { get; set; }

        public DateTime? IrregularityDateTo { get; set; }

        public IrregularityClassification? IrregularityClassification { get; set; }

        public int? IrregularityCategoryId { get; set; }

        public int? IrregularityTypeId { get; set; }

        public string EndingActRegNum { get; set; }

        public DateTime? EndingActDate { get; set; }

        public IrregularityCaseState? CaseState { get; set; }

        public DateTime? IrregularityEndDate { get; set; }

        public Year? ReportYear { get; set; }

        public Quarter? ReportQuarter { get; set; }

        public IrregularityRapporteur? Rapporteur { get; set; }

        public string RapporteurComments { get; set; }

        public IrregularityImpairedRegulationAct? ImpairedRegulationAct { get; set; }

        public string ImpairedRegulationNum { get; set; }

        public int? ImpairedRegulationYear { get; set; }

        public string ImpairedRegulation { get; set; }

        public string ImpairedNationalRegulation { get; set; }

        public decimal? EUCoFinancingPercent { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? ExpensesBfpEuAmountLv { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? ExpensesBfpBgAmountLv { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? ExpensesBfpTotalAmountLv { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? ExpensesSelfAmountLv { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? ExpensesTotalAmountLv { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? ExpensesBfpEuAmountEuro { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? ExpensesBfpBgAmountEuro { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? ExpensesBfpTotalAmountEuro { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? ExpensesSelfAmountEuro { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? ExpensesTotalAmountEuro { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? IrregularExpensesBfpEuAmountLv { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? IrregularExpensesBfpBgAmountLv { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? IrregularExpensesBfpTotalAmountLv { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? IrregularExpensesBfpEuAmountEuro { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? IrregularExpensesBfpBgAmountEuro { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? IrregularExpensesBfpTotalAmountEuro { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? CertifiedExpensesBfpEuAmountLv { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? CertifiedExpensesBfpBgAmountLv { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? CertifiedExpensesBfpTotalAmountLv { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? CertifiedExpensesBfpEuAmountEuro { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? CertifiedExpensesBfpBgAmountEuro { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? CertifiedExpensesBfpTotalAmountEuro { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? PaidBfpEuAmountLv { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? PaidBfpBgAmountLv { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? PaidBfpTotalAmountLv { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? PaidBfpEuAmountEuro { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? PaidBfpBgAmountEuro { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? PaidBfpTotalAmountEuro { get; set; }

        public ContractDebtExecutionStatus? ContractDebtStatus { get; set; }

        public bool? ShouldDecertifyIrregularExpenses { get; set; }

        public string DecertificationComments { get; set; }

        public IrregularitySanctionProcedureType? SanctionProcedureType { get; set; }

        public IrregularitySanctionProcedureKind? SanctionProcedureKind { get; set; }

        public DateTime? SanctionProcedureStartDate { get; set; }

        public DateTime? SanctionProcedureExpectedEndDate { get; set; }

        public DateTime? SanctionProcedureEndDate { get; set; }

        public IrregularitySanctionProcedureStatus? SanctionProcedureStatus { get; set; }

        public int? SanctionCategoryId { get; set; }

        public int? SanctionTypeId { get; set; }

        public string Fines { get; set; }

        public bool? IsNewUnlawfulPractice { get; set; }

        public bool? ShouldInformOther { get; set; }

        public IrregularityProcedureStatus? ProcedureStatus { get; set; }

        public int? FinancialStatusId { get; set; }

        public string AppliedPractices { get; set; }

        public string BeneficiaryData { get; set; }

        public string AdminAscertainments { get; set; }

        public string IrregularityDetectedBy { get; set; }

        public string AdminProcedures { get; set; }

        public string PenaltyProcedures { get; set; }

        public bool? ShouldReportToOlaf { get; set; }

        public IrregularityReasonNotReportingToOlaf? ReasonNotReportingToOlaf { get; set; }

        public IrregularityCheckTime? CheckTime { get; set; }

        public byte[] Version { get; set; }
    }
}
