using Eumis.Common.Db;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.NonAggregates;

namespace Eumis.Data.NonAggregates.Repositories.Noms
{
    internal class CountryNomsRepository : EntityCodeNomsRepository<Country, EntityCodeNomVO>
    {
        public CountryNomsRepository(IUnitOfWork unitOfWork)
            : base(
                unitOfWork,
                t => t.CountryId,
                t => t.Name,
                t => t.NutsCode,
                t => new EntityCodeNomVO
                {
                    NomValueId = t.CountryId,
                    Name = t.Name,
                    Code = t.NutsCode,
                })
        {
        }
    }
}
