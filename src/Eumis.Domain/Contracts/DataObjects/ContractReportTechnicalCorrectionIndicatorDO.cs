using System;
using Eumis.Common.Json;
using Eumis.Domain.Core;
using Eumis.Domain.Indicators;
using Newtonsoft.Json;

namespace Eumis.Domain.Contracts.DataObjects
{
    public class ContractReportTechnicalCorrectionIndicatorDO
    {
        public ContractReportTechnicalCorrectionIndicatorDO()
        {
        }

        public ContractReportTechnicalCorrectionIndicatorDO(
            ContractReportTechnicalCorrectionIndicator contractReportTechnicalCorrectionIndicator,
            ContractReportIndicator contractReportIndicator,
            ContractIndicator contractIndicator,
            Indicator indicator,
            string checkedByUser,
            string indicatorCheckedByUser,
            bool existsCorrectionForPreviousContractReport)
        {
            this.ContractReportTechnicalCorrectionIndicatorId = contractReportTechnicalCorrectionIndicator.ContractReportTechnicalCorrectionIndicatorId;
            this.ContractReportTechnicalCorrectionId = contractReportTechnicalCorrectionIndicator.ContractReportTechnicalCorrectionId;
            this.ContractReportIndicatorId = contractReportTechnicalCorrectionIndicator.ContractReportIndicatorId;
            this.ContractReportTechnicalId = contractReportTechnicalCorrectionIndicator.ContractReportTechnicalId;
            this.ContractReportId = contractReportTechnicalCorrectionIndicator.ContractReportId;
            this.ContractId = contractReportTechnicalCorrectionIndicator.ContractId;
            this.Gid = contractReportTechnicalCorrectionIndicator.Gid;

            this.Status = contractReportTechnicalCorrectionIndicator.Status;
            this.Notes = contractReportTechnicalCorrectionIndicator.Notes;
            this.CheckedByUser = checkedByUser;
            this.CheckedDate = contractReportTechnicalCorrectionIndicator.CheckedDate;

            this.ExistsCorrectionForPreviousContractReport = existsCorrectionForPreviousContractReport;

            this.CorrectedApprovedPeriodAmountMen = contractReportTechnicalCorrectionIndicator.CorrectedApprovedPeriodAmountMen;
            this.CorrectedApprovedPeriodAmountWomen = contractReportTechnicalCorrectionIndicator.CorrectedApprovedPeriodAmountWomen;
            this.CorrectedApprovedPeriodAmountTotal = contractReportTechnicalCorrectionIndicator.CorrectedApprovedPeriodAmountTotal;

            this.CorrectedApprovedCumulativeAmountMen = contractReportTechnicalCorrectionIndicator.CorrectedApprovedCumulativeAmountMen;
            this.CorrectedApprovedCumulativeAmountWomen = contractReportTechnicalCorrectionIndicator.CorrectedApprovedCumulativeAmountWomen;
            this.CorrectedApprovedCumulativeAmountTotal = contractReportTechnicalCorrectionIndicator.CorrectedApprovedCumulativeAmountTotal;

            this.CorrectedApprovedResidueAmountMen = contractReportTechnicalCorrectionIndicator.CorrectedApprovedResidueAmountMen;
            this.CorrectedApprovedResidueAmountWomen = contractReportTechnicalCorrectionIndicator.CorrectedApprovedResidueAmountWomen;
            this.CorrectedApprovedResidueAmountTotal = contractReportTechnicalCorrectionIndicator.CorrectedApprovedResidueAmountTotal;

            this.LastReportCorrectedCumulativeAmountMen = contractReportTechnicalCorrectionIndicator.LastReportCorrectedCumulativeAmountMen;
            this.LastReportCorrectedCumulativeAmountWomen = contractReportTechnicalCorrectionIndicator.LastReportCorrectedCumulativeAmountWomen;
            this.LastReportCorrectedCumulativeAmountTotal = contractReportTechnicalCorrectionIndicator.LastReportCorrectedCumulativeAmountTotal;

            this.CreateDate = contractReportTechnicalCorrectionIndicator.CreateDate;
            this.ModifyDate = contractReportTechnicalCorrectionIndicator.ModifyDate;
            this.Version = contractReportTechnicalCorrectionIndicator.Version;

            this.ContractReportIndicator = new ContractReportIndicatorDO(
                contractReportIndicator,
                contractIndicator,
                indicator,
                indicatorCheckedByUser,
                null);
        }

        public int ContractReportTechnicalCorrectionIndicatorId { get; set; }

        public int ContractReportTechnicalCorrectionId { get; set; }

        public int ContractReportIndicatorId { get; set; }

        public int ContractReportTechnicalId { get; set; }

        public int ContractReportId { get; set; }

        public int ContractId { get; set; }

        public Guid Gid { get; set; }

        public ContractReportTechnicalCorrectionIndicatorStatus Status { get; set; }

        public string Notes { get; set; }

        public string CheckedByUser { get; set; }

        public DateTime? CheckedDate { get; set; }

        public bool? ExistsCorrectionForPreviousContractReport { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? CorrectedApprovedPeriodAmountMen { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? CorrectedApprovedPeriodAmountWomen { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? CorrectedApprovedPeriodAmountTotal { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? CorrectedApprovedCumulativeAmountMen { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? CorrectedApprovedCumulativeAmountWomen { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? CorrectedApprovedCumulativeAmountTotal { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? CorrectedApprovedResidueAmountMen { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? CorrectedApprovedResidueAmountWomen { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? CorrectedApprovedResidueAmountTotal { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? LastReportCorrectedCumulativeAmountMen { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? LastReportCorrectedCumulativeAmountWomen { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal LastReportCorrectedCumulativeAmountTotal { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }

        public ContractReportIndicatorDO ContractReportIndicator { get; set; }
    }
}
