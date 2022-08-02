using Eumis.Common.Json;
using Eumis.Domain.Contracts;
using Newtonsoft.Json;
using System;

namespace Eumis.Data.ContractReportIndicators.ViewObjects
{
    public class ContractReportIndicatorsVO
    {
        public int ContractReportIndicatorId { get; set; }

        public int ContractReportTechnicalId { get; set; }

        public int ContractReportId { get; set; }

        public int ContractId { get; set; }

        public Guid Gid { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ContractReportIndicatorStatus Status { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ContractReportIndicatorApproval? Approval { get; set; }

        public string Notes { get; set; }

        public int? CheckedByUserId { get; set; }

        public DateTime? CheckedDate { get; set; }

        public decimal PeriodAmount { get; set; }

        public decimal CumulativeAmount { get; set; }

        public decimal ResidueAmount { get; set; }

        public decimal LastReportCumulativeAmount { get; set; }

        public string Comment { get; set; }

        public decimal? ApprovedPeriodAmount { get; set; }

        public decimal? ApprovedCumulativeAmount { get; set; }

        public decimal? ApprovedResidueAmount { get; set; }

        public string IndicatorName { get; set; }
    }
}
