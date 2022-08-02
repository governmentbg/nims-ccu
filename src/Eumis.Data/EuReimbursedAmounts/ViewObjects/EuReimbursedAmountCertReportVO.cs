using System;
using Eumis.Common.Json;
using Eumis.Domain.CertReports;
using Newtonsoft.Json;

namespace Eumis.Data.EuReimbursedAmounts.ViewObjects
{
    public class EuReimbursedAmountCertReportVO
    {
        public int? EuReimbursedAmountCertReportId { get; set; }

        public int CertReportId { get; set; }

        public int OrderNum { get; set; }

        public DateTime RegDate { get; set; }

        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public CertReportType Type { get; set; }
    }
}
