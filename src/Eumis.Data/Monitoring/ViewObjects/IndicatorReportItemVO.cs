using Eumis.Common.Json;
using Eumis.Domain.Contracts;
using Eumis.Domain.Indicators;
using Newtonsoft.Json;
using System;

namespace Eumis.Data.Monitoring.ViewObjects
{
    public class IndicatorReportItemVO
    {
        public string Programme { get; set; }

        public string Procedure { get; set; }

        public string ContractRegNum { get; set; }

        public string ContractName { get; set; }

        public string NutsFullPathName { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ContractExecutionStatus? ContractExecutionStatus { get; set; }

        public DateTime? ContractEndTerminationDate { get; set; }

        public string CompanyName { get; set; }

        public string CompanyUin { get; set; }

        public string CompanyType { get; set; }

        public string CompanyLegalType { get; set; }

        public string CompanySizeType { get; set; }

        public string Name { get; set; }

        public string Measure { get; set; }

        public decimal? BaseTotalValue { get; set; }

        public decimal? TargetTotalValue { get; set; }

        public decimal? ReportedTotalValue { get; set; }

        public decimal? ApprovedTotalValue { get; set; }
    }
}
