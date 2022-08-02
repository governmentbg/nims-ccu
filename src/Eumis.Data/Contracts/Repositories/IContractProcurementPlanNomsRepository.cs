using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Contracts;
using System.Collections.Generic;

namespace Eumis.Data.Contracts.Repositories
{
    public interface IContractProcurementPlanNomsRepository : IEntityNomsRepository<ContractProcurementPlan, EntityNomVO>
    {
        IEnumerable<EntityNomVO> GetContractProcurementPlans(int contractId, string term, int offset = 0, int? limit = null);
    }
}
