using System.Collections.Generic;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.NonAggregates;

namespace Eumis.Data.NonAggregates.Repositories.Noms
{
    public interface IInstitutionNomsRepository : IEntityNomsRepository<Institution, EntityNomVO>
    {
        IList<EntityNomVO> GetInstitutionNoms(int programmeId, string term, int offset = 0, int? limit = null);
    }
}
