using System.Collections.Generic;
using System.Linq;
using Eumis.Common.Db;
using Eumis.Data.Core;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.Linq;
using Eumis.Domain.Contracts;
using Eumis.Domain.Procedures;

namespace Eumis.Data.Contracts.Repositories
{
    internal class ContractNomsRepository : Repository, IContractNomsRepository
    {
        public ContractNomsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public EntityNomVO GetNom(int nomValueId)
        {
            return (from c in this.unitOfWork.DbContext.Set<Contract>()
                    where c.ContractId == nomValueId
                    select new EntityNomVO
                    {
                        NomValueId = c.ContractId,
                        Name = c.RegNumber,
                    }).SingleOrDefault();
        }

        public IEnumerable<EntityNomVO> GetNoms(string term, int offset = 0, int? limit = null)
        {
            return this.GetContracts(term, offset, limit);
        }

        public IEnumerable<EntityNomVO> GetContracts(string term, int offset = 0, int? limit = null, int[] programmeIds = null, int? userId = null)
        {
            var predicate = PredicateBuilder.True<Contract>()
                .And(c => c.ContractStatus == ContractStatus.Entered)
                .AndStringContains(c => c.RegNumber, term);

            var externalUserContracts = from cu in this.unitOfWork.DbContext.Set<ContractUser>().Where(x => x.UserId == userId)
                                        join c in this.unitOfWork.DbContext.Set<Contract>().Where(predicate) on cu.ContractId equals c.ContractId
                                        select c;

            var programmePredicate = predicate;
            if (programmeIds != null)
            {
                programmePredicate = predicate.And(c => programmeIds.Contains(c.ProgrammeId));
            }

            return (from c in this.unitOfWork.DbContext.Set<Contract>().Where(programmePredicate).Union(externalUserContracts)
                    orderby c.CreateDate descending
                    select new EntityNomVO
                    {
                        NomValueId = c.ContractId,
                        Name = c.RegNumber,
                    })
                    .WithOffsetAndLimit(offset, limit)
                    .Distinct()
                    .ToList();
        }

        public IEnumerable<EntityNomVO> GetContracts(int procedureId, string term, int offset = 0, int? limit = null)
        {
            return (from c in this.unitOfWork.DbContext.Set<Contract>()
                    join p in this.unitOfWork.DbContext.Set<Procedure>() on c.ProcedureId equals p.ProcedureId
                    where p.ProcedureId == procedureId
                    orderby c.CreateDate descending
                    select new EntityNomVO
                    {
                        NomValueId = c.ContractId,
                        Name = c.RegNumber,
                    }).WithOffsetAndLimit(offset, limit)
                      .ToList();
        }
    }
}
