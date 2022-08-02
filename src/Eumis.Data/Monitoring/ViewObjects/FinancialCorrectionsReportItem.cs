using Eumis.Common.Json;
using Eumis.Domain.MonitoringFinancialControl;
using Eumis.Domain.MonitoringFinancialControl.FinancialCorrections;
using Newtonsoft.Json;
using System;

namespace Eumis.Data.Monitoring.ViewObjects
{
    public class FinancialCorrectionsReportItem
    {
        public string Programme { get; set; }

        public string ProgrammePriority { get; set; }

        public string Procedure { get; set; }

        public string ContractRegNum { get; set; }

        public string CompanyUin { get; set; }

        public string CompanyName { get; set; }

        public string CompanyType { get; set; }

        public string CompanyLegalType { get; set; }

        public DateTime CorrectionDate { get; set; }

        public int CorrectionNum { get; set; }

        public string ContractContractName { get; set; }

        public string ContractContractCompanyName { get; set; }

        public string ContractContractUin { get; set; }

        public decimal? InitialContractContractPercent { get; set; }

        public decimal? InitialAmountTotal { get; set; }

        public decimal? InitialAmountBfp { get; set; }

        public decimal? InitialAmountEu { get; set; }

        public decimal? InitialAmountBg { get; set; }

        public decimal? InitialAmountSelf { get; set; }

        public string InitialReason { get; set; }

        public string InitialViolations { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public FinancialCorrectionVersionViolationFoundBy? InitialViolationFoundBy { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public CorrectionBearer? InitialBearer { get; set; }

        public decimal? CurrentContractContractPercent { get; set; }

        public decimal? CurrentAmountTotal { get; set; }

        public decimal? CurrentAmountBfp { get; set; }

        public decimal? CurrentAmountEu { get; set; }

        public decimal? CurrentAmountBg { get; set; }

        public decimal? CurrentAmountSelf { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public AmendmentReason? AmendmentReason { get; set; }

        public string CurrentReason { get; set; }

        public string CurrentViolations { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public FinancialCorrectionVersionViolationFoundBy? CurrentViolationFoundBy { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public CorrectionBearer? CurrentBearer { get; set; }

        public string Irregularity { get; set; }

        public string ReasonBase { get; set; }

        public decimal? CorretionAmountTotal { get; set; }

        public decimal? CorretionAmountBfp { get; set; }

        public decimal? CorretionAmountEu { get; set; }

        public decimal? CorretionAmountBg { get; set; }

        public decimal? CorretionAmountSelf { get; set; }

        public string ContractReportPayments { get; set; }

        public string CertReports { get; set; }
    }
}
