using System;
using Eumis.Common.Json;
using Eumis.Domain.Contracts;
using Newtonsoft.Json;

namespace Eumis.Data.ContractReportIndicators.ViewObjects
{
    public class ContractPhysicalExecutionIndicatorVO
    {
        public int ContractVersionId { get; set; }

        public int ContractId { get; set; }

        public string ContractRegNum { get; set; }

        public string IndicatorName { get; set; }

        public string MeasureName { get; set; }

        public decimal BaseTotal { get; set; }

        public decimal TargetTotal { get; set; }

        public decimal CumulativeAmount { get; set; }

        public decimal? ApprovedCumulativeAmount { get; set; }

        public decimal? CorrectedApprovedCumulativeAmountTotal { get; set; }
    }
}
