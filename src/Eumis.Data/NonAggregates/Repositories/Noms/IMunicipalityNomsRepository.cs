using System.Collections.Generic;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.NonAggregates.ViewObjects;
using Eumis.Domain.NonAggregates;

namespace Eumis.Data.NonAggregates.Repositories.Noms
{
    public interface IMunicipalityNomsRepository : IEntityCodeNomsRepository<Municipality, NutsCodeNomVO>
    {
        IEnumerable<NutsCodeNomVO> GetMunicipalityNoms(int districtId, string term, int offset = 0, int? limit = null);
    }
}
