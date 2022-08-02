namespace Eumis.Data.Core.Nomenclatures
{
    public interface IEntityCodeNomsRepository<TEntity, TCodeNomVO> : IEntityNomsRepository<TEntity, TCodeNomVO>
    {
        int GetNomIdByCode(string code);
    }
}
