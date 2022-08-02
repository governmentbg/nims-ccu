using System.Collections.Generic;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.SpotChecks;

namespace Eumis.Data.SpotChecks.Repositories
{
    public interface ISpotCheckItemNomsRepository : IEntityNomsRepository<SpotCheckItem, EntityNomVO>
    {
        IEnumerable<EntityNomVO> GetItemNoms(int[] ids, int spotCheckId, string term, int offset = 0, int? limit = null);
    }
}