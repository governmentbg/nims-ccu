using Eumis.Common.Json;
using Eumis.Domain.Contracts;
using Eumis.Domain.Contracts.DataObjects;
using Eumis.Domain.Core;
using Newtonsoft.Json;
using System;

namespace Eumis.Domain.CertReports.ViewObjects
{
    public class CertReportRevalidationVO
    {
        public int ContractReportRevalidationId { get; set; }

        public string ProgrammeName { get; set; }

        public string RegNumber { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ContractReportRevalidationStatus StatusDescr { get; set; }

        public ContractReportRevalidationStatus Status { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ContractReportRevalidationType Type { get; set; }

        public DateTime Date { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ContractReportRevalidationCertStatus? CertStatus { get; set; }

        public DateTime? CertCheckedDate { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public Sign? Sign { get; set; }

        public decimal? ApprovedBfpTotalAmount { get; set; }

        public decimal? ApprovedSelfAmount { get; set; }

        public decimal? CertifiedBfpTotalAmount { get; set; }

        public decimal? CertifiedSelfAmount { get; set; }

        public ContractReportRevalidationDO ContractReportRevalidation { get; set; }
    }
}
