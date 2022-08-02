using Eumis.Common.Json;
using Newtonsoft.Json;
using System;

namespace Eumis.Domain.Contracts.ViewObjects
{
    public class ContractReportCertAuthorityCorrectionVO
    {
        public int ContractReportCertAuthorityCorrectionId { get; set; }

        public int ContractReportId { get; set; }

        public int ContractId { get; set; }

        public string ProgrammeName { get; set; }

        public string RegNumber { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ContractReportCertAuthorityCorrectionStatus StatusDescr { get; set; }

        public ContractReportCertAuthorityCorrectionStatus Status { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ContractReportCertAuthorityCorrectionType Type { get; set; }

        public DateTime Date { get; set; }

        public string ContractRegNum { get; set; }

        public int ReportOrderNum { get; set; }

        public decimal? CertifiedBfpTotalAmount { get; set; }

        public decimal? CertifiedSelfAmount { get; set; }

        public int AnnualAccountReportOrderNum { get; set; }

        public string Description { get; set; }
    }
}
