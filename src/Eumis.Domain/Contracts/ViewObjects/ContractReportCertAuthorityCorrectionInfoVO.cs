using Eumis.Common.Json;
using Newtonsoft.Json;

namespace Eumis.Domain.Contracts.ViewObjects
{
    public class ContractReportCertAuthorityCorrectionInfoVO
    {
        public string ProgrammeCode { get; set; }

        public ContractReportCertAuthorityCorrectionStatus Status { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ContractReportCertAuthorityCorrectionStatus StatusDescr { get; set; }
    }
}
