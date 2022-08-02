using System;
using Eumis.Common.Json;
using Eumis.Domain.Debts;
using Eumis.Domain.Irregularities;
using Eumis.Domain.NonAggregates;
using Newtonsoft.Json;

namespace Eumis.Data.Irregularities.ViewObjects
{
    public class IrrRegisterItemVO
    {
        public int IrregularityId { get; set; }

        public int VersionId { get; set; }

        public string RegNumber { get; set; }

        public string SignalNumber { get; set; }

        public string SignalActRegNum { get; set; }

        public DateTime? SignalActRegDate { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public Year FirstReportYear { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public Quarter FirstReportQuarter { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public Year ReportYear { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public Quarter ReportQuarter { get; set; }

        public string ProgrammeCode { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public IrregularityRapporteur? Rapporteur { get; set; }

        public string ProjectName { get; set; }

        public string ProjectNumber { get; set; }

        public string ProjectOtherNumber { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public IrregularityClassification? IrregularityClassification { get; set; }

        public string AppliedPractices { get; set; }

        public decimal? ExpensesLvEuAmount { get; set; }

        public decimal? ExpensesLvBgAmount { get; set; }

        public decimal? ExpensesLvBfpAmount { get; set; }

        public decimal? ExpensesLvSelfAmount { get; set; }

        public decimal? ExpensesLvTotalAmount { get; set; }

        public decimal? IrregularExpensesLvEuAmount { get; set; }

        public decimal? IrregularExpensesLvBgAmount { get; set; }

        public decimal? IrregularExpensesLvBfpAmount { get; set; }

        public decimal? PaidIrregularExpensesLvEuAmount { get; set; }

        public decimal? PaidIrregularExpensesLvBgAmount { get; set; }

        public decimal? PaidIrregularExpensesLvBfpAmount { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ContractDebtExecutionStatus? ContractDebtStatus { get; set; }

        public string AdminProcedures { get; set; }

        public string PenaltyProcedures { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public IrregularityCaseState? CaseState { get; set; }

        public string EndingActRegNum { get; set; }

        public DateTime? EndingActDate { get; set; }

        public string RapporteurComments { get; set; }
    }
}
