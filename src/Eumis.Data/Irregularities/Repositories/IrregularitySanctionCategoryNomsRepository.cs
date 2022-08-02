using Eumis.Common.Db;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Irregularities;

namespace Eumis.Data.Irregularities.Repositories
{
    internal class IrregularitySanctionCategoryNomsRepository : EntityCodeNomsRepository<IrregularitySanctionCategory, EntityCodeNomVO>
    {
        public IrregularitySanctionCategoryNomsRepository(IUnitOfWork unitOfWork)
            : base(
                unitOfWork,
                ic => ic.IrregularitySanctionCategoryId,
                ic => ic.Name,
                ic => ic.Code,
                ic => new EntityCodeNomVO
                {
                    NomValueId = ic.IrregularitySanctionCategoryId,
                    Name = ic.Name,
                    Code = ic.Code,
                })
        {
        }
    }
}
