using Eumis.Common.Db;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.NonAggregates;

namespace Eumis.Data.NonAggregates.Repositories.Noms
{
    internal class InstitutionTypeNomsRepository : EntityNomsRepository<InstitutionType, EntityNomVO>
    {
        public InstitutionTypeNomsRepository(IUnitOfWork unitOfWork)
            : base(
                unitOfWork,
                t => t.InstitutionTypeId,
                t => t.Name,
                t => new EntityNomVO
                {
                    NomValueId = t.InstitutionTypeId,
                    Name = t.Name,
                })
        {
        }
    }
}
