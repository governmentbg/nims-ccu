using Eumis.Common.Json;
using Eumis.Domain.Contracts.ContractReportMicros;
using Newtonsoft.Json;
using System;

namespace Eumis.Domain.Contracts.ViewObjects
{
    public class ContractReportMicroVO
    {
        public int ContractReportMicroId { get; set; }

        public int ContractReportId { get; set; }

        public int ContractId { get; set; }

        public int VersionNum { get; set; }

        public int VersionSubNum { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public Source Source { get; set; }

        public Source SourceName { get; set; }

        public ContractReportMicroType Type { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ContractReportMicroStatus Status { get; set; }

        public ContractReportMicroStatus StatusName { get; set; }

        public string StatusNote { get; set; }

        public bool IsFromExternalSystem { get; set; }

        public DateTime CreateDate { get; set; }

        public string ContractRegistrationEmail { get; set; }
    }
}
