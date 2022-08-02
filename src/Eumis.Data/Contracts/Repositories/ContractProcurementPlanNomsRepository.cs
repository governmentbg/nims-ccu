using System;
using System.Collections.Generic;
using System.Linq;
using Eumis.Common.Db;
using Eumis.Data.Core;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.Linq;
using Eumis.Domain.Contracts;

namespace Eumis.Data.Contracts.Repositories
{
    internal class ContractProcurementPlanNomsRepository : Repository, IContractProcurementPlanNomsRepository
    {
        public ContractProcurementPlanNomsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public EntityNomVO GetNom(int nomValueId)
        {
            return (from c in this.unitOfWork.DbContext.Set<ContractProcurementPlan>()
                    where c.ContractId == nomValueId
                    select new EntityNomVO
                    {
                        NomValueId = c.ContractProcurementPlanId,
                        Name = c.Name,
                    }).SingleOrDefault();
        }

        public IEnumerable<EntityNomVO> GetNoms(string term, int offset = 0, int? limit = null)
        {
            throw new NotSupportedException();
        }

        public IEnumerable<EntityNomVO> GetContractProcurementPlans(int contractId, string term, int offset = 0, int? limit = null)
        {
            var predicate = PredicateBuilder.True<ContractProcurementPlan>()
                .AndEquals(c => c.ContractId, contractId)
                .AndStringContains(c => c.Name, term);

            return (from c in this.unitOfWork.DbContext.Set<ContractProcurementPlan>().Where(predicate)
                    select new EntityNomVO
                    {
                        NomValueId = c.ContractProcurementPlanId,
                        Name = c.Name,
                    })
                    .WithOffsetAndLimit(offset, limit)
                    .ToList();
        }
    }
}
