using System.Collections.Generic;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.NonAggregates.ViewObjects;
using Eumis.Domain.NonAggregates;

namespace Eumis.Data.NonAggregates.Repositories.Noms
{
    public interface IProtectedZoneNomsRepository : IEntityCodeNomsRepository<ProtectedZone, NutsCodeNomVO>
    {
        IEnumerable<NutsCodeNomVO> GetProtectedZoneNoms(int countryId, string term, int offset = 0, int? limit = null);
    }
}
