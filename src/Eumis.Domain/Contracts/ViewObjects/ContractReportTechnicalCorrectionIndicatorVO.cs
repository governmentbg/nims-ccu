using Eumis.Common.Json;
using Eumis.Domain.Contracts.DataObjects;
using Eumis.Domain.Core;
using Eumis.Domain.NonAggregates;
using Newtonsoft.Json;
using System;

namespace Eumis.Domain.Contracts.ViewObjects
{
    public class ContractReportTechnicalCorrectionIndicatorVO
    {
        public int ContractReportTechnicalCorrectionIndicatorId { get; set; }

        public int ContractReportIndicatorId { get; set; }

        public int ContractReportTechnicalCorrectionId { get; set; }

        public int ContractReportTechnicalId { get; set; }

        public int ContractReportId { get; set; }

        public int ContractId { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ContractReportTechnicalCorrectionIndicatorStatus Status { get; set; }

        public string IndicatorName { get; set; }

        public decimal? ApprovedPeriodAmountMen { get; set; }

        public decimal? ApprovedPeriodAmountWomen { get; set; }

        public decimal? ApprovedPeriodAmountTotal { get; set; }

        public decimal? ApprovedCumulativeAmountMen { get; set; }

        public decimal? ApprovedCumulativeAmountWomen { get; set; }

        public decimal? ApprovedCumulativeAmountTotal { get; set; }

        public decimal? ApprovedResidueAmountMen { get; set; }

        public decimal? ApprovedResidueAmountWomen { get; set; }

        public decimal? ApprovedResidueAmountTotal { get; set; }

        public decimal? CorrectedApprovedPeriodAmountMen { get; set; }

        public decimal? CorrectedApprovedPeriodAmountWomen { get; set; }

        public decimal? CorrectedApprovedPeriodAmountTotal { get; set; }

        public decimal? CorrectedApprovedCumulativeAmountMen { get; set; }

        public decimal? CorrectedApprovedCumulativeAmountWomen { get; set; }

        public decimal? CorrectedApprovedCumulativeAmountTotal { get; set; }

        public decimal? CorrectedApprovedResidueAmountMen { get; set; }

        public decimal? CorrectedApprovedResidueAmountWomen { get; set; }

        public decimal? CorrectedApprovedResidueAmountTotal { get; set; }

        public byte[] Version { get; set; }
    }
}
