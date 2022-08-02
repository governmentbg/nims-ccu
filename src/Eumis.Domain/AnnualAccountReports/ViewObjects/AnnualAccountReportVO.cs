using Eumis.Common.Json;
using Newtonsoft.Json;
using System;

namespace Eumis.Domain.AnnualAccountReports.ViewObjects
{
    public class AnnualAccountReportVO
    {
        public int AnnualAccountReportId { get; set; }

        public int ProgrammeId { get; set; }

        public string ProgrammeName { get; set; }

        public int OrderNum { get; set; }

        public int OrderVersionNum { get; set; }

        public DateTime RegDate { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public AnnualAccountReportPeriod AccountPeriod { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public AnnualAccountReportStatus StatusDesc { get; set; }

        public AnnualAccountReportStatus Status { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? ApprovalDate { get; set; }

        public int? CertReportOriginId { get; set; }

        public decimal? CertifiedBfpTotalAmount { get; set; }

        public decimal? CertifiedSelfAmount { get; set; }
    }
}
