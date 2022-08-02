using Eumis.Common.Json;
using Eumis.Domain.Contracts;
using Eumis.Domain.Contracts.DataObjects;
using Eumis.Domain.Core;
using Newtonsoft.Json;
using System;

namespace Eumis.Domain.CertReports.ViewObjects
{
    public class CertReportCertCorrectionVO
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

        public DateTime? CertCheckedDate { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public Sign? Sign { get; set; }

        public decimal? CertifiedBfpTotalAmount { get; set; }

        public decimal? CertifiedSelfAmount { get; set; }

        public ContractReportCertCorrectionDO ContractReportCertCorrection { get; set; }
    }
}
