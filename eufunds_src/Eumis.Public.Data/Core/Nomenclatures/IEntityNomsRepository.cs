using System;
using System.Collections.Generic;

namespace Eumis.Public.Data.Core.Nomenclatures
{
    public interface IEntityNomsRepository<TEntity, out TNomVO> : IRepository
    {
        TNomVO GetNom(Guid nomValueId);

        IEnumerable<TNomVO> GetNoms(string term, int offset = 0, int? limit = null);
    }
}
