using Eumis.Common.Db;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.NonAggregates;

namespace Eumis.Data.NonAggregates.Repositories.Noms
{
    internal class ErrandLegalActGidNomsRepository : EntityGidNomsRepository<ErrandLegalAct, EntityGidNomVO>
    {
        public ErrandLegalActGidNomsRepository(IUnitOfWork unitOfWork)
            : base(
                unitOfWork,
                t => t.ErrandLegalActId,
                t => t.Name,
                t => t.Gid,
                t => new EntityGidNomVO
                {
                    NomValueId = t.ErrandLegalActId,
                    Gid = t.Gid,
                    Name = t.Name,
                })
        {
        }
    }
}
