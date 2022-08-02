using Eumis.Common.Json;
using Eumis.Domain.Indicators;
using Newtonsoft.Json;
using System;

namespace Eumis.Domain.Contracts.DataObjects
{
    public class ContractReportIndicatorDO
    {
        public ContractReportIndicatorDO()
        {
        }

        public ContractReportIndicatorDO(
            ContractReportIndicator contractReportIndicator,
            ContractIndicator contractIndicator,
            Indicator indicator,
            string checkedByUser,
            ContractReportTechnicalCorrectionIndicator contractReportTechnicalCorrectionIndicator)
        {
            this.ContractReportIndicatorId = contractReportIndicator.ContractReportIndicatorId;
            this.ContractReportTechnicalId = contractReportIndicator.ContractReportTechnicalId;
            this.ContractReportId = contractReportIndicator.ContractReportId;
            this.ContractId = contractReportIndicator.ContractId;
            this.Gid = contractReportIndicator.Gid;

            this.Name = contractReportIndicator.Name;
            this.HasGenderDivision = contractReportIndicator.HasGenderDivision;
            this.MeasureName = contractReportIndicator.MeasureName;

            this.Status = contractReportIndicator.Status;
            this.Approval = contractReportIndicator.Approval;
            this.Notes = contractReportIndicator.Notes;
            this.CheckedByUser = checkedByUser;
            this.CheckedDate = contractReportIndicator.CheckedDate;

            this.PeriodAmountMen = contractReportIndicator.PeriodAmountMen;
            this.PeriodAmountWomen = contractReportIndicator.PeriodAmountWomen;
            this.PeriodAmountTotal = contractReportIndicator.PeriodAmountTotal;

            this.CumulativeAmountMen = contractReportIndicator.CumulativeAmountMen;
            this.CumulativeAmountWomen = contractReportIndicator.CumulativeAmountWomen;
            this.CumulativeAmountTotal = contractReportIndicator.CumulativeAmountTotal;

            this.ResidueAmountMen = contractReportIndicator.ResidueAmountMen;
            this.ResidueAmountWomen = contractReportIndicator.ResidueAmountWomen;
            this.ResidueAmountTotal = contractReportIndicator.ResidueAmountTotal;

            this.LastReportCumulativeAmountMen = contractReportIndicator.LastReportCumulativeAmountMen;
            this.LastReportCumulativeAmountWomen = contractReportIndicator.LastReportCumulativeAmountWomen;
            this.LastReportCumulativeAmountTotal = contractReportIndicator.LastReportCumulativeAmountTotal;

            this.Comment = contractReportIndicator.Comment;

            this.ApprovedPeriodAmountMen = contractReportIndicator.ApprovedPeriodAmountMen;
            this.ApprovedPeriodAmountWomen = contractReportIndicator.ApprovedPeriodAmountWomen;
            this.ApprovedPeriodAmountTotal = contractReportIndicator.ApprovedPeriodAmountTotal;

            this.ApprovedCumulativeAmountMen = contractReportIndicator.ApprovedCumulativeAmountMen;
            this.ApprovedCumulativeAmountWomen = contractReportIndicator.ApprovedCumulativeAmountWomen;
            this.ApprovedCumulativeAmountTotal = contractReportIndicator.ApprovedCumulativeAmountTotal;

            this.ApprovedResidueAmountMen = contractReportIndicator.ApprovedResidueAmountMen;
            this.ApprovedResidueAmountWomen = contractReportIndicator.ApprovedResidueAmountWomen;
            this.ApprovedResidueAmountTotal = contractReportIndicator.ApprovedResidueAmountTotal;

            this.HasCorrection = contractReportTechnicalCorrectionIndicator != null;

            if (contractReportTechnicalCorrectionIndicator != null)
            {
                this.CorrectedApprovedPeriodAmountMen = contractReportTechnicalCorrectionIndicator.CorrectedApprovedPeriodAmountMen;
                this.CorrectedApprovedPeriodAmountWomen = contractReportTechnicalCorrectionIndicator.CorrectedApprovedPeriodAmountWomen;
                this.CorrectedApprovedPeriodAmountTotal = contractReportTechnicalCorrectionIndicator.CorrectedApprovedPeriodAmountTotal;
                this.CorrectedApprovedCumulativeAmountMen = contractReportTechnicalCorrectionIndicator.CorrectedApprovedCumulativeAmountMen;
                this.CorrectedApprovedCumulativeAmountWomen = contractReportTechnicalCorrectionIndicator.CorrectedApprovedCumulativeAmountWomen;
                this.CorrectedApprovedCumulativeAmountTotal = contractReportTechnicalCorrectionIndicator.CorrectedApprovedCumulativeAmountTotal;
                this.CorrectedApprovedResidueAmountMen = contractReportTechnicalCorrectionIndicator.CorrectedApprovedResidueAmountMen;
                this.CorrectedApprovedResidueAmountWomen = contractReportTechnicalCorrectionIndicator.CorrectedApprovedResidueAmountWomen;
                this.CorrectedApprovedResidueAmountTotal = contractReportTechnicalCorrectionIndicator.CorrectedApprovedResidueAmountTotal;
            }

            this.ModifyDate = contractReportIndicator.ModifyDate;
            this.CreateDate = contractReportIndicator.CreateDate;
            this.Version = contractReportIndicator.Version;

            this.ContractIndicator = new ContractIndicatorDO(contractIndicator, indicator);
        }

        public ContractReportIndicatorDO(
            ContractReportIndicator contractReportIndicator,
            ContractIndicator contractIndicator,
            Indicator indicator,
            string checkedByUser,
            ContractReportTechnicalCorrectionIndicator contractReportTechnicalCorrectionIndicator,
            ContractReportTechnicalStatus contractReportTechnicalStatus)
        : this(
              contractReportIndicator,
              contractIndicator,
              indicator,
              checkedByUser,
              contractReportTechnicalCorrectionIndicator)
        {
            this.ContractReportTechnicalStatus = contractReportTechnicalStatus;
        }

        public int ContractReportIndicatorId { get; set; }

        public int ContractReportTechnicalId { get; set; }

        public int ContractReportId { get; set; }

        public int ContractId { get; set; }

        public Guid Gid { get; set; }

        public string Name { get; set; }

        public bool HasGenderDivision { get; set; }

        public string MeasureName { get; set; }

        public ContractReportIndicatorStatus Status { get; set; }

        public ContractReportTechnicalStatus ContractReportTechnicalStatus { get; set; }

        public ContractReportIndicatorApproval? Approval { get; set; }

        public string Notes { get; set; }

        public string CheckedByUser { get; set; }

        public DateTime? CheckedDate { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? PeriodAmountMen { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? PeriodAmountWomen { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal PeriodAmountTotal { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? CumulativeAmountMen { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? CumulativeAmountWomen { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal CumulativeAmountTotal { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? ResidueAmountMen { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? ResidueAmountWomen { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal ResidueAmountTotal { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? LastReportCumulativeAmountMen { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? LastReportCumulativeAmountWomen { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal LastReportCumulativeAmountTotal { get; set; }

        public string Comment { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? ApprovedPeriodAmountMen { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? ApprovedPeriodAmountWomen { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? ApprovedPeriodAmountTotal { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? ApprovedCumulativeAmountMen { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? ApprovedCumulativeAmountWomen { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? ApprovedCumulativeAmountTotal { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? ApprovedResidueAmountMen { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? ApprovedResidueAmountWomen { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? ApprovedResidueAmountTotal { get; set; }

        public bool HasCorrection { get; set; }

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

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }

        public ContractIndicatorDO ContractIndicator { get; set; }
    }
}