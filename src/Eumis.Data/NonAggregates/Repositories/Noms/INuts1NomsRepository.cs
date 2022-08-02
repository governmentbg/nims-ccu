using System.Collections.Generic;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.NonAggregates.ViewObjects;
using Eumis.Domain.NonAggregates;

namespace Eumis.Data.NonAggregates.Repositories.Noms
{
    public interface INuts1NomsRepository : IEntityCodeNomsRepository<Nuts1, NutsCodeNomVO>
    {
        IEnumerable<NutsCodeNomVO> GetNuts1Noms(int countryId, string term, int offset = 0, int? limit = null);
    }
}
