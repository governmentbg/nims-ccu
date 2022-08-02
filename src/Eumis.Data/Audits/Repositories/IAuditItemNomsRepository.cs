using System.Collections.Generic;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Audits;

namespace Eumis.Data.Audits.Repositories
{
    public interface IAuditItemNomsRepository : IEntityNomsRepository<AuditLevelItem, EntityNomVO>
    {
        IEnumerable<EntityNomVO> GetItemNoms(int[] ids, int auditId, string term, int offset = 0, int? limit = null);
    }
}