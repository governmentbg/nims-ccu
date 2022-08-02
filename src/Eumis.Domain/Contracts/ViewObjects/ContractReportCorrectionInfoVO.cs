using Eumis.Common.Json;
using Newtonsoft.Json;

namespace Eumis.Domain.Contracts.ViewObjects
{
    public class ContractReportCorrectionInfoVO
    {
        public string ProgrammeCode { get; set; }

        public ContractReportCorrectionStatus Status { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ContractReportCorrectionStatus StatusDescr { get; set; }
    }
}
