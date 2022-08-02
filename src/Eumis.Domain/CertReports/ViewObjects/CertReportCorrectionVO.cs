using Eumis.Common.Json;
using Eumis.Domain.Contracts;
using Eumis.Domain.Contracts.DataObjects;
using Eumis.Domain.Core;
using Newtonsoft.Json;
using System;

namespace Eumis.Domain.CertReports.ViewObjects
{
    public class CertReportCorrectionVO
    {
        public int ContractReportCorrectionId { get; set; }

        public string ProgrammeName { get; set; }

        public string RegNumber { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ContractReportCorrectionStatus StatusDescr { get; set; }

        public ContractReportCorrectionStatus Status { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ContractReportCorrectionType Type { get; set; }

        public string ElementNumber { get; set; }

        public DateTime Date { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ContractReportCorrectionCertStatus? CertStatus { get; set; }

        public DateTime? CertCheckedDate { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public Sign? Sign { get; set; }

        public decimal? ApprovedBfpTotalAmount { get; set; }

        public decimal? ApprovedSelfAmount { get; set; }

        public decimal? CertifiedBfpTotalAmount { get; set; }

        public decimal? CertifiedSelfAmount { get; set; }

        public ContractReportCorrectionDO ContractReportCorrection { get; set; }
    }
}
