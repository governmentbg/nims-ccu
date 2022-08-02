using System.Collections.Generic;

namespace Eumis.Domain.CertReports.ViewObjects
{
    public class CertReportReaffirmedCostsByAdministrativeAuthorityResultVO
    {
        public IList<ReaffirmedCostsByAdministrativeAuthorityVO> Items { get; set; }

        public ReaffirmedCostsByAdministrativeAuthorityVO Total { get; set; }

        public ReaffirmedCostsByAdministrativeAuthorityVO TotalYEI { get; set; }
    }
}
