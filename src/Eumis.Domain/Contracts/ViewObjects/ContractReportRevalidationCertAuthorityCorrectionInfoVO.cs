using Eumis.Common.Json;
using Newtonsoft.Json;

namespace Eumis.Domain.Contracts.ViewObjects
{
    public class ContractReportRevalidationCertAuthorityCorrectionInfoVO
    {
        public string ProgrammeCode { get; set; }

        public ContractReportRevalidationCertAuthorityCorrectionStatus Status { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ContractReportRevalidationCertAuthorityCorrectionStatus StatusDescr { get; set; }
    }
}
