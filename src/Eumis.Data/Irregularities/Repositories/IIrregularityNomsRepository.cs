using System.Collections.Generic;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Irregularities;

namespace Eumis.Data.Irregularities.Repositories
{
    public interface IIrregularityNomsRepository : IEntityNomsRepository<Irregularity, EntityNomVO>
    {
        IEnumerable<EntityNomVO> GetNoms(int contractId, string term, int offset = 0, int? limit = null);
    }
}