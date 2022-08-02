using System.Collections.Generic;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Contracts;

namespace Eumis.Data.FlatFinancialCorrections.Repositories
{
    public interface IFlatFinancialCorrectionContractNomsRepository : IEntityNomsRepository<Contract, EntityNomVO>
    {
        IList<EntityNomVO> GetContractNoms(
            int[] programmeIds,
            string term,
            int offset = 0,
            int? limit = null);
    }
}
