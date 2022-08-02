using System.Collections.Generic;
using System.Linq;
using Eumis.Common.Db;
using Eumis.Data.Contracts.ViewObjects;
using Eumis.Data.Core;
using Eumis.Data.Linq;
using Eumis.Domain.Contracts;
using Eumis.Domain.Debts;

namespace Eumis.Data.Debts.Repositories
{
    internal class ContractDebtNomsRepository : Repository, IContractDebtNomsRepository
    {
        public ContractDebtNomsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public ContractDebtNomVO GetNom(int nomValueId)
        {
            return (from c in this.unitOfWork.DbContext.Set<ContractDebt>()
                    where c.ContractDebtId == nomValueId
                    select new ContractDebtNomVO
                    {
                        NomValueId = c.ContractDebtId,
                        Name = c.RegNumber,
                        ContractId = c.ContractId,
                    }).SingleOrDefault();
        }

        public IEnumerable<ContractDebtNomVO> GetNoms(string term, int offset = 0, int? limit = null)
        {
            return this.GetDebts(term, offset, limit);
        }

        public IEnumerable<ContractDebtNomVO> GetDebts(string term, int offset = 0, int? limit = null, int? contractId = null, int[] programmeIds = null)
        {
            var debtPredicate = PredicateBuilder.True<ContractDebt>()
                .AndEquals(cd => cd.ContractId, contractId)
                .And(cd => cd.Status == ContractDebtStatus.Entered)
                .AndStringContains(c => c.RegNumber, term);

            var contractPredicate = PredicateBuilder.True<Contract>();

            if (programmeIds != null)
            {
                contractPredicate = contractPredicate.And(c => programmeIds.Contains(c.ProgrammeId));
            }

            return (from cd in this.unitOfWork.DbContext.Set<ContractDebt>().Where(debtPredicate)
                    join c in this.unitOfWork.DbContext.Set<Contract>().Where(contractPredicate) on cd.ContractId equals c.ContractId
                    orderby cd.CreateDate descending
                    select new ContractDebtNomVO
                    {
                        NomValueId = cd.ContractDebtId,
                        Name = cd.RegNumber,
                        ContractId = cd.ContractId,
                    }).WithOffsetAndLimit(offset, limit)
                      .ToList();
        }
    }
}
