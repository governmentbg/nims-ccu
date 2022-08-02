using Eumis.Common.Db;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.NonAggregates;

namespace Eumis.Data.NonAggregates.Repositories.Noms
{
    internal class ErrandTypeNomsCodeRepository : EntityCodeNomsRepository<ErrandType, EntityCodeNomVO>
    {
        public ErrandTypeNomsCodeRepository(IUnitOfWork unitOfWork)
            : base(
                unitOfWork,
                t => t.ErrandTypeId,
                t => t.Name,
                t => t.Code,
                t => new EntityCodeNomVO
                {
                    NomValueId = t.ErrandTypeId,
                    Name = t.Name,
                    Code = t.Code,
                })
        {
        }
    }
}
