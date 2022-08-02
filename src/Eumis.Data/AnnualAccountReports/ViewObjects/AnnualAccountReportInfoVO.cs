using Eumis.Common.Json;
using Eumis.Domain.AnnualAccountReports;
using Eumis.Domain.CertAuthorityChecks;
using Newtonsoft.Json;

namespace Eumis.Data.AnnualAccountReports.ViewObjects
{
    public class AnnualAccountReportInfoVO
    {
        public int OrderNum { get; set; }

        public int OrderVersionNum { get; set; }

        public string ProgrammeShortName { get; set; }

        public AnnualAccountReportStatus Status { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public AnnualAccountReportStatus StatusDescription { get; set; }

        public AnnualAccountReportPeriod AccountPeriod { get; set; }

        public byte[] Version { get; set; }
    }
}
