using Monitorstat.Common.MonitorstatService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monitorstat.Common.Contracts
{
    public class ProcedureInquiryDO
    {
        public ProcedureInquiryDO()
        {
            this.Reports = new List<ReportDO>();
            this.Activities = new List<ActivityDO>();
        }

        public string ProcedureCode { get; set; }

        public List<ReportDO> Reports { get; set; }

        public List<ActivityDO> Activities { get; set; }

        public DateTime FinishDate { get; set; }

        public DateTime ExecutionDateTo { get; set; }

        public DateTime ExecutionDateFrom { get; set; }

        public ReportInquiry[] GetReportInquiries()
        {
            return this.Reports
                .GroupBy(x => new
                {
                    x.SurveyCode,
                    x.Year,
                })
                .Select(t => new ReportInquiry(t.Select(x => x.ReportCode).ToList())
                {
                    Year = t.Key.Year,
                    SurveyCode = t.Key.SurveyCode,
                })
                .ToArray();
        }

        public ActivityInquiry[] GetActivityInquiries()
        {
            return this.Activities
                .Select(a => new ActivityInquiry()
                {
                    Year = a.Year,
                    Type = (ActivityType)a.Type,
                })
                .ToArray();
        }
    }
}
