using Eumis.Common.Db;
using Eumis.Data.Core;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.Linq;
using Eumis.Domain.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.Data.Contracts.Repositories
{
    internal class ContractBudgetLevel3NomsRepository : Repository, IContractBudgetLevel3NomsRepository
    {
        public ContractBudgetLevel3NomsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public EntityNomVO GetNom(int nomValueId)
        {
            return (from cbla in this.unitOfWork.DbContext.Set<ContractBudgetLevel3Amount>()
                    where cbla.ContractBudgetLevel3AmountId == nomValueId
                    select new EntityNomVO
                    {
                        NomValueId = cbla.ContractBudgetLevel3AmountId,
                        Name = cbla.Code + " " + cbla.Name,
                    })
                    .SingleOrDefault();
        }

        public IEnumerable<EntityNomVO> GetNoms(string term, int offset = 0, int? limit = null)
        {
            return this.GetContractContractBudgetLevel3s(term: term, offset: offset, limit: limit);
        }

        public IEnumerable<EntityNomVO> GetContractContractBudgetLevel3s(int? contractContractId = null, string term = null, int offset = 0, int? limit = null)
        {
            var predicate = PredicateBuilder.True<ContractBudgetLevel3Amount>()
                .AndStringContains(c => c.Code + " " + c.Name, term);

            var contractContractActivityPredicate = PredicateBuilder.True<ContractContractActivity>()
                .AndEquals(cca => cca.ContractContractId, contractContractId);

            return (from cbla in this.unitOfWork.DbContext.Set<ContractBudgetLevel3Amount>().Where(predicate)
                    join cca in this.unitOfWork.DbContext.Set<ContractContractActivity>().Where(contractContractActivityPredicate) on cbla.ContractBudgetLevel3AmountId equals cca.ContractBudgetLevel3AmountId
                    select new EntityNomVO
                    {
                        NomValueId = cbla.ContractBudgetLevel3AmountId,
                        Name = cbla.Code + " " + cbla.Name,
                    })
                    .OrderBy(p => p.Name)
                    .WithOffsetAndLimit(offset, limit)
                    .ToList();
        }
    }
}
