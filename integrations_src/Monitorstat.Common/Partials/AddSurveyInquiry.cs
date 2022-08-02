using Monitorstat.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monitorstat.Common.MonitorstatService
{
    public partial class AddSurveyInquiry
    {
        public AddSurveyInquiry()
        {
        }

        public AddSurveyInquiry(ProcedureInquiryDO procedureInquiry)
        {
            this.ProcedureIdentifier = procedureInquiry.ProcedureCode;

            this.ExecutionDateFrom = procedureInquiry.ExecutionDateFrom;
            this.ExecutionDateTo = procedureInquiry.ExecutionDateTo;
            this.FinishDate = procedureInquiry.FinishDate;

            this.ReportInquiries = procedureInquiry.GetReportInquiries();
            this.ActivityInquiries = procedureInquiry.GetActivityInquiries();

            this.Type = SurveyInquiryType.Apply;
        }
    }
}
