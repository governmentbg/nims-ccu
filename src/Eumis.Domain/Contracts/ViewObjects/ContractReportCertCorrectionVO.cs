using Eumis.Common.Json;
using Newtonsoft.Json;
using System;

namespace Eumis.Domain.Contracts.ViewObjects
{
    public class ContractReportCertCorrectionVO
    {
        public int ContractReportCertCorrectionId { get; set; }

        public string ProgrammeName { get; set; }

        public string RegNumber { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ContractReportCertCorrectionStatus StatusDescr { get; set; }

        public ContractReportCertCorrectionStatus Status { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ContractReportCertCorrectionType Type { get; set; }

        public DateTime Date { get; set; }

        public decimal? BfpTotalAmount { get; set; }

        public decimal? SelfAmount { get; set; }
    }
}
