using System.Collections.Generic;

namespace Eumis.Data.Core.Nomenclatures
{
    public interface IEntityNomsRepository<TEntity, out TNomVO> : IRepository
    {
        TNomVO GetNom(int nomValueId);

        IEnumerable<TNomVO> GetNoms(string term, int offset = 0, int? limit = null);
    }
}
