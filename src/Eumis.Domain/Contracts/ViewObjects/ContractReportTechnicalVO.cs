using Eumis.Common.Json;
using Newtonsoft.Json;
using System;

namespace Eumis.Domain.Contracts.ViewObjects
{
    public class ContractReportTechnicalVO
    {
        public int ContractReportTechnicalId { get; set; }

        public int ContractReportId { get; set; }

        public int ContractId { get; set; }

        public int VersionNum { get; set; }

        public int VersionSubNum { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ContractReportTechnicalStatus Status { get; set; }

        public ContractReportTechnicalStatus StatusName { get; set; }

        public string StatusNote { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ContractReportTechnicalType? Type { get; set; }

        public DateTime? RegDate { get; set; }

        public DateTime? SubmitDate { get; set; }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? ReturnDate { get; set; }

        public int? ContractRegistrationId { get; set; }

        public string ContractRegistrationEmail { get; set; }
    }
}
