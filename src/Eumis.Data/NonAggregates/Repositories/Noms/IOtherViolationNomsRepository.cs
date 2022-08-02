using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.MonitoringFinancialControl;
using System.Collections.Generic;

namespace Eumis.Data.NonAggregates.Repositories.Noms
{
    public interface IOtherViolationNomsRepository : IEntityNomsRepository<OtherViolation, EntityNomVO>
    {
        IList<EntityNomVO> GetNoms(int[] ids, string term = null, int offset = 0, int? limit = null);
    }
}
