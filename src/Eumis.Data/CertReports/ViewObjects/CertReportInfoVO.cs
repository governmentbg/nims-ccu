using Eumis.Common.Json;
using Eumis.Domain.CertReports;
using Newtonsoft.Json;
using System;

namespace Eumis.Data.CertReports.ViewObjects
{
    public class CertReportInfoVO
    {
        public int OrderNum { get; set; }

        public int OrderVersionNum { get; set; }

        public string ProgrammeShortName { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public CertReportStatus StatusDescription { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public CertReportType TypeDescription { get; set; }

        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }

        public CertReportStatus Status { get; set; }

        public CertReportType Type { get; set; }

        public byte[] Version { get; set; }
    }
}
