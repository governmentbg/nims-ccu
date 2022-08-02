using Eumis.Data.Monitorstat.Contracts;
using Eumis.Data.Procedures.ViewObjects;
using Eumis.Domain.Monitorstat;
using Eumis.Domain.Procedures;
using System.Collections.Generic;

namespace Eumis.Data.Procedures.Repositories
{
    public interface IProcedureMonitorstatEconomicActivitiesRepository : IAggregateRepository<ProcedureMonitorstatEconomicActivity>
    {
        IList<ProcedureMonitorstatEconomicActivityVO> GetProcedureMonitorstatEconomicActivities(int procedureId);

        IList<ActivityDO> GetProcedureInquiryActivities(int procedureId);

        IList<string> CanCreateProcedureMonitorstatEconomicActivity(int procedureId, MonitorstatYear year);
    }
}
