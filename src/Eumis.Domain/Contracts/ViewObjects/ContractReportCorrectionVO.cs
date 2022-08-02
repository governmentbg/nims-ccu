using Eumis.Common.Json;
using Newtonsoft.Json;
using System;

namespace Eumis.Domain.Contracts.ViewObjects
{
    public class ContractReportCorrectionVO
    {
        public int ContractReportCorrectionId { get; set; }

        public string ProgrammeName { get; set; }

        public string RegNumber { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ContractReportCorrectionStatus StatusDescr { get; set; }

        public ContractReportCorrectionStatus Status { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ContractReportCorrectionType Type { get; set; }

        public DateTime Date { get; set; }

        public decimal? CorrectedApprovedBfpTotalAmount { get; set; }

        public decimal? CorrectedApprovedSelfAmount { get; set; }
    }
}
