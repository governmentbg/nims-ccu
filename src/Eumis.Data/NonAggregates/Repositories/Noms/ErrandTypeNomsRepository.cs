using Eumis.Common.Db;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.NonAggregates;

namespace Eumis.Data.NonAggregates.Repositories.Noms
{
    internal class ErrandTypeNomsRepository : DependentEntityNomsRepository<ErrandType, EntityNomVO>
    {
        public ErrandTypeNomsRepository(IUnitOfWork unitOfWork)
            : base(
                unitOfWork,
                t => t.ErrandTypeId,
                t => t.ErrandLegalActId,
                t => t.Name,
                t => new EntityNomVO
                {
                    NomValueId = t.ErrandTypeId,
                    Name = t.Name,
                })
        {
        }
    }
}
