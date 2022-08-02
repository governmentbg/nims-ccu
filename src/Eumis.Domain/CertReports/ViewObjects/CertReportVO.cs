using Eumis.Common.Json;
using Newtonsoft.Json;
using System;

namespace Eumis.Domain.CertReports.ViewObjects
{
    public class CertReportVO
    {
        public int CertReportId { get; set; }

        public int? AttachedCertReportId { get; set; }

        public int ProgrammeId { get; set; }

        public string ProgrammeName { get; set; }

        public int OrderNum { get; set; }

        public int OrderVersionNum { get; set; }

        public DateTime RegDate { get; set; }

        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public CertReportStatus StatusDesc { get; set; }

        public CertReportStatus Status { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public CertReportType Type { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? ApprovalDate { get; set; }

        public int? CertReportOriginId { get; set; }

        public decimal? ApprovedBfpTotalAmount { get; set; }

        public decimal? ApprovedSelfAmount { get; set; }

        public decimal? CertifiedBfpTotalAmount { get; set; }

        public decimal? CertifiedSelfAmount { get; set; }

        public string CertReportNumber { get; set; }
    }
}
