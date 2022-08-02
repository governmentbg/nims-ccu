using Eumis.Common.Json;
using Newtonsoft.Json;
using System;

namespace Eumis.Domain.Contracts.ViewObjects
{
    public class ContractReportRevalidationCertAuthorityCorrectionVO
    {
        public int ContractReportRevalidationCertAuthorityCorrectionId { get; set; }

        public string ProgrammeName { get; set; }

        public string RegNumber { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ContractReportRevalidationCertAuthorityCorrectionStatus StatusDescr { get; set; }

        public ContractReportRevalidationCertAuthorityCorrectionStatus Status { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ContractReportRevalidationCertAuthorityCorrectionType Type { get; set; }

        public DateTime Date { get; set; }
    }
}
