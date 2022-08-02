using Eumis.Common.Db;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.NonAggregates;

namespace Eumis.Data.NonAggregates.Repositories.Noms
{
    internal class KidCodeNomsRepository : EntityCodeNomsRepository<KidCode, EntityCodeNomVO>
    {
        public KidCodeNomsRepository(IUnitOfWork unitOfWork)
            : base(
                unitOfWork,
                t => t.KidCodeId,
                t => t.Code + " " + t.Name,
                t => t.Code,
                t => new EntityCodeNomVO
                {
                    NomValueId = t.KidCodeId,
                    Code = t.Code,
                    Name = t.Code + " " + t.Name,
                    NameAlt = t.Code + " " + t.NameAlt,
                })
        {
        }
    }
}
