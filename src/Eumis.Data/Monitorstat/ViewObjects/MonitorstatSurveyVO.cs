using Eumis.Domain.Monitorstat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Data.Monitorstat.ViewObjects
{
    public class MonitorstatSurveyVO
    {
        public MonitorstatSurveyVO()
        {
            this.Reports = new List<MonitorstatReportVO>();
        }

        public MonitorstatSurveyVO(MonitorstatSurvey survey)
            : this()
        {
            this.MonitorstatSurveyId = survey.MonitorstatSurveyId;
            this.Name = survey.Name;
            this.Year = (int)survey.Year;
            this.CreateDate = survey.CreateDate;

            survey.Reports.ToList().ForEach(t => this.Reports.Add(new MonitorstatReportVO { Code = t.Code, Name = t.Name }));
        }

        public int MonitorstatSurveyId { get; set; }

        public string Name { get; set; }

        public int Year { get; set; }

        public DateTime CreateDate { get; set; }

        public IList<MonitorstatReportVO> Reports { get; set; }
    }
}
