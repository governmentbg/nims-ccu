using Eumis.Common.Db;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.NonAggregates;

namespace Eumis.Data.NonAggregates.Repositories.Noms
{
    internal class ErrandAreaNomsRepository : EntityCodeNomsRepository<ErrandArea, EntityCodeNomVO>
    {
        public ErrandAreaNomsRepository(IUnitOfWork unitOfWork)
            : base(
                unitOfWork,
                t => t.ErrandAreaId,
                t => t.Name,
                t => t.Code,
                t => new EntityCodeNomVO
                {
                    NomValueId = t.ErrandAreaId,
                    Name = t.Name,
                    Code = t.Code,
                })
        {
        }
    }
}
