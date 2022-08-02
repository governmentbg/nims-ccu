using System.Collections.Generic;
using System.Linq;
using Eumis.Common.Db;
using Eumis.Common.Json;
using Eumis.Data.Monitorstat.Contracts;
using Eumis.Data.Procedures.ViewObjects;
using Eumis.Domain.Monitorstat;
using Eumis.Domain.Procedures;

namespace Eumis.Data.Procedures.Repositories
{
    internal class ProcedureMonitorstatEconomicActivitiesRepository : AggregateRepository<ProcedureMonitorstatEconomicActivity>, IProcedureMonitorstatEconomicActivitiesRepository
    {
        public ProcedureMonitorstatEconomicActivitiesRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public IList<ProcedureMonitorstatEconomicActivityVO> GetProcedureMonitorstatEconomicActivities(int procedureId)
        {
            var activities =
                (from ea in this.unitOfWork.DbContext.Set<ProcedureMonitorstatEconomicActivity>().Where(x => x.ProcedureId == procedureId)
                 select new ProcedureMonitorstatEconomicActivityVO
                 {
                     ProcedureMonitorstatEconomicActivityId = ea.ProcedureMonitorstatEconomicActivityId,
                     Status = ea.Status,
                     Type = ea.Type,
                     Year = ea.Year,
                     CreateDate = ea.CreateDate,
                     Version = ea.Version,
                 })
                 .OrderByDescending(x => x.CreateDate)
                 .ToList();

            return activities;
        }

        public IList<ActivityDO> GetProcedureInquiryActivities(int procedureId)
        {
            var activities =
                (from ea in this.unitOfWork.DbContext.Set<ProcedureMonitorstatEconomicActivity>().Where(x => x.ProcedureId == procedureId)
                 select new ActivityDO
                 {
                     Type = (int)ea.Type,
                     Year = (int)ea.Year,
                 }).ToList();

            return activities;
        }

        public IList<string> CanCreateProcedureMonitorstatEconomicActivity(int procedureId, MonitorstatYear year)
        {
            var errors = new List<string>();

            var activityExists = this.unitOfWork.DbContext.Set<ProcedureMonitorstatEconomicActivity>()
                .Where(a => a.ProcedureId == procedureId && a.Year == year)
                .Any();

            if (activityExists)
            {
                errors.Add($"Вече има добавена дейност за {year.GetEnumDescription()} година към тази процедура.");
            }

            return errors;
        }
    }
}
