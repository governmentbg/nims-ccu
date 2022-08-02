using Eumis.Common.Json;
using Newtonsoft.Json;

namespace Eumis.Domain.Contracts.ViewObjects
{
    public class ContractReportCertCorrectionInfoVO
    {
        public string ProgrammeCode { get; set; }

        public ContractReportCertCorrectionStatus Status { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ContractReportCertCorrectionStatus StatusDescr { get; set; }
    }
}
