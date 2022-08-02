using Eumis.Common.Db;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Irregularities;

namespace Eumis.Data.Irregularities.Repositories
{
    internal class IrregularityFinancialStatusNomsRepository : EntityCodeNomsRepository<IrregularityFinancialStatus, EntityCodeNomVO>
    {
        public IrregularityFinancialStatusNomsRepository(IUnitOfWork unitOfWork)
            : base(
                unitOfWork,
                ifs => ifs.IrregularityFinancialStatusId,
                ifs => ifs.Name,
                ifs => ifs.Code,
                ifs => new EntityCodeNomVO
                {
                    NomValueId = ifs.IrregularityFinancialStatusId,
                    Name = ifs.Name,
                    Code = ifs.Code,
                })
        {
        }
    }
}
