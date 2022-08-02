using System;
using System.Collections.Generic;
using Eumis.Data.Guidances.ViewObjects;
using Eumis.Domain.Guidances;

namespace Eumis.Data.Guidances.Repositories
{
    public interface IGuidancesRepository : IAggregateRepository<Guidance>
    {
        IList<GuidanceDataVO> GetGuidances();

        IList<GuidanceVO> GetGuidances(GuidanceModule type);

        bool ExistsGuidanceWithKey(Guid fileKey);
    }
}
