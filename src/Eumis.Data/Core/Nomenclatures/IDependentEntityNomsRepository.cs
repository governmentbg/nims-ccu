using System.Collections.Generic;

namespace Eumis.Data.Core.Nomenclatures
{
    public interface IDependentEntityNomsRepository<TEntity, out TNomVO> : IEntityNomsRepository<TEntity, TNomVO>
    {
        new IEnumerable<TNomVO> GetNoms(string term, int offset = 0, int? limit = null);

        IEnumerable<TNomVO> GetNoms(int parentId, string term, int offset = 0, int? limit = null);
    }
}
