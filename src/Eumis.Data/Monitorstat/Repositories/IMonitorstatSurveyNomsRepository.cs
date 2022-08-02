using System.Collections.Generic;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Monitorstat;

namespace Eumis.Data.Monitorstat.Repositories
{
    public interface IMonitorstatSurveyNomsRepository : IEntityNomsRepository<MonitorstatSurvey, EntityNomVO>
    {
        IList<EntityNomVO> GetNoms(MonitorstatYear? year, string term = null, int offset = 0, int? limit = null);
    }
}
