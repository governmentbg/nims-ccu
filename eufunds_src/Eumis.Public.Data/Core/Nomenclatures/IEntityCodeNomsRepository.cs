using System;

namespace Eumis.Public.Data.Core.Nomenclatures
{
    public interface IEntityCodeNomsRepository<TEntity, TCodeNomVO> : IEntityNomsRepository<TEntity, TCodeNomVO>
    {
        Guid GetNomIdByCode(string code);

        bool HasCode(string code);
    }
}
