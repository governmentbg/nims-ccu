using System.Collections.Generic;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.NonAggregates.ViewObjects;
using Eumis.Domain.NonAggregates;

namespace Eumis.Data.NonAggregates.Repositories.Noms
{
    public interface ISettlementNomsRepository : IEntityCodeNomsRepository<Settlement, SettlementCodeNomVO>
    {
        IEnumerable<SettlementCodeNomVO> GetSettlementNoms(int municipalityId, string term, int offset = 0, int? limit = null);

        SettlementCodeNomVO GetSettlementNom(string ekatte);
    }
}
