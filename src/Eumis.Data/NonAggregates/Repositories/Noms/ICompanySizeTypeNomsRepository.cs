using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.NonAggregates.ViewObjects;
using Eumis.Domain.NonAggregates;

namespace Eumis.Data.NonAggregates.Repositories.Noms
{
    public interface ICompanySizeTypeNomsRepository : IEntityGidNomsRepository<CompanySizeType, CompanySizeTypeGidNomVO>
    {
        CompanySizeTypeGidNomVO GetByAlias(string valueAlias);
    }
}
