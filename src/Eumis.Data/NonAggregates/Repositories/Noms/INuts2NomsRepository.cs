using System.Collections.Generic;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.NonAggregates.ViewObjects;
using Eumis.Domain.NonAggregates;

namespace Eumis.Data.NonAggregates.Repositories.Noms
{
    public interface INuts2NomsRepository : IEntityCodeNomsRepository<Nuts2, NutsCodeNomVO>
    {
        IEnumerable<NutsCodeNomVO> GetNuts2Noms(int nuts1Id, string term, int offset = 0, int? limit = null);
    }
}
