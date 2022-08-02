using System.Collections.Generic;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.NonAggregates.ViewObjects;
using Eumis.Domain.NonAggregates;

namespace Eumis.Data.NonAggregates.Repositories.Noms
{
    public interface IDistrictNomsRepository : IEntityCodeNomsRepository<District, NutsCodeNomVO>
    {
        IEnumerable<NutsCodeNomVO> GetDistrictNoms(int nuts2Id, string term, int offset = 0, int? limit = null);
    }
}
