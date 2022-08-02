using Eumis.Common.Db;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Irregularities;

namespace Eumis.Data.Irregularities.Repositories
{
    internal class IrregularityCategoryNomsRepository : EntityCodeNomsRepository<IrregularityCategory, EntityCodeNomVO>
    {
        public IrregularityCategoryNomsRepository(IUnitOfWork unitOfWork)
            : base(
                unitOfWork,
                ic => ic.IrregularityCategoryId,
                ic => ic.Name,
                ic => ic.Code,
                ic => new EntityCodeNomVO
                {
                    NomValueId = ic.IrregularityCategoryId,
                    Name = ic.Name,
                    Code = ic.Code,
                })
        {
        }
    }
}
