using System.Collections.Generic;
using System.Linq;
using Eumis.Common.Db;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.Linq;
using Eumis.Domain.Monitorstat;

namespace Eumis.Data.Monitorstat.Repositories
{
    internal class MonitorstatSurveyNomsRepository : EntityNomsRepository<MonitorstatSurvey, EntityNomVO>, IMonitorstatSurveyNomsRepository
    {
        public MonitorstatSurveyNomsRepository(IUnitOfWork unitOfWork)
            : base(
                unitOfWork,
                t => t.MonitorstatSurveyId,
                t => t.Name,
                t => t.Name,
                t => new EntityNomVO
                {
                    NomValueId = t.MonitorstatSurveyId,
                    Name = t.Name,
                    NameAlt = t.Name,
                })
        {
        }

        public IList<EntityNomVO> GetNoms(MonitorstatYear? year, string term = null, int offset = 0, int? limit = null)
        {
            return this.unitOfWork.DbContext.Set<MonitorstatSurvey>()
                .Where(x => x.Year == year)
                .Select(t => new EntityNomVO { NomValueId = t.MonitorstatSurveyId, Name = t.Name })
                .OrderBy(x => x.Name)
                .ToList();
        }

        protected override System.Linq.IQueryable<MonitorstatSurvey> GetQuery()
        {
            return this.unitOfWork.DbContext.Set<MonitorstatSurvey>();
        }
    }
}
