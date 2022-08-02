using Eumis.Common.Json;
using Newtonsoft.Json;

namespace Eumis.Domain.Contracts.ViewObjects
{
    public class ContractReportRevalidationInfoVO
    {
        public string ProgrammeCode { get; set; }

        public ContractReportRevalidationStatus Status { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ContractReportRevalidationStatus StatusDescr { get; set; }
    }
}
