using System.Collections.Generic;

namespace Eumis.Domain.CertReports.ViewObjects
{
    public class CertReportStateAidPaidAdvancePaymentsResultVO
    {
        public IList<StateAidPaidAdvancePaymentsVO> Items { get; set; }

        public StateAidPaidAdvancePaymentsVO Total { get; set; }

        public StateAidPaidAdvancePaymentsVO TotalYEI { get; set; }
    }
}
