using Eumis.Common.Json;
using Eumis.Domain.MonitoringFinancialControl.CompensationDocuments;
using Newtonsoft.Json;

namespace Eumis.Data.CompensationDocuments.ViewObjects
{
    public class CompensationDocumentInfoVO
    {
        public string ProgrammeCode { get; set; }

        public CompensationDocumentStatus Status { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public CompensationDocumentStatus StatusDescr { get; set; }
    }
}
