using Eumis.Data.Monitorstat.ViewObjects;
using Eumis.Data.Procedures.ViewObjects;
using Eumis.Domain.Monitorstat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Data.Monitorstat.Repositories
{
    public interface IMonitorstatSurveysRepository : IAggregateRepository<MonitorstatSurvey>
    {
        IList<MonitorstatSurvey> GetSurveysByYear(MonitorstatYear year);

        IList<MonitorstatSurveyVO> GetSurveys();

        MonitorstatSurveyVO GetSurvey(int id);
    }
}
