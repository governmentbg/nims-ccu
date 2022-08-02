using System;
using Eumis.Common.Json;
using Eumis.Domain.MonitoringFinancialControl.CompensationDocuments;
using Newtonsoft.Json;

namespace Eumis.Data.CompensationDocuments.ViewObjects
{
    public class CompensationDocumentVO
    {
        public int CompensationDocumentId { get; set; }

        public string ProgrammeName { get; set; }

        public string RegNumber { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public CompensationDocumentStatus StatusDescr { get; set; }

        public CompensationDocumentStatus Status { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public CompensationDocumentType Type { get; set; }

        public DateTime CompensationDocDate { get; set; }
    }
}
