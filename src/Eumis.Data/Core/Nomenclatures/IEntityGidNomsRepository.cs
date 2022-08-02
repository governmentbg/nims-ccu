using System;

namespace Eumis.Data.Core.Nomenclatures
{
    public interface IEntityGidNomsRepository<TEntity, out TNomVO> : IEntityNomsRepository<TEntity, TNomVO>
    {
        int GetNomIdByGid(Guid gid);
    }
}
