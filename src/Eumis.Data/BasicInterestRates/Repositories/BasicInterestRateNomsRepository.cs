using Eumis.Common.Db;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.BasicInterestRates;

namespace Eumis.Data.BasicInterestRates.Repositories
{
    internal class BasicInterestRateNomsRepository : EntityNomsRepository<BasicInterestRate, EntityNomVO>
    {
        public BasicInterestRateNomsRepository(IUnitOfWork unitOfWork)
            : base(
                unitOfWork,
                q => q.BasicInterestRateId,
                q => q.Name,
                q => new EntityNomVO
                {
                    NomValueId = q.BasicInterestRateId,
                    Name = q.Name,
                })
        {
        }
    }
}