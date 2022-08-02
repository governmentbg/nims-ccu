using System.Collections.Generic;

namespace Eumis.Domain.CertReports.ViewObjects
{
    public class CertReportEligibleProgrammePriorityExpensesResultVO
    {
        public IList<EligibleProgrammePriorityExpensesVO> Items { get; set; }

        public EligibleProgrammePriorityExpensesVO Total { get; set; }

        public EligibleProgrammePriorityExpensesVO TotalYEI { get; set; }
    }
}
