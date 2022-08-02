using System.Collections.Generic;

namespace Eumis.Domain.CertReports.ViewObjects
{
    public class CertReportApprovedAmountsCorrectionsResultVO
    {
        public IList<ApprovedAmountsCorrectionsVO> Items { get; set; }

        public ApprovedAmountsCorrectionsVO Total { get; set; }

        public ApprovedAmountsCorrectionsVO TotalYEI { get; set; }
    }
}
