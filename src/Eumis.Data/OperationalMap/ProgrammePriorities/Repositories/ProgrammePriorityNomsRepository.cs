using System.Collections.Generic;
using System.Linq;
using Eumis.Common.Db;
using Eumis.Data.Core;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.Linq;
using Eumis.Domain.Contracts;
using Eumis.Domain.OperationalMap.ProgrammePriorities;
using Eumis.Domain.Procedures;

namespace Eumis.Data.OperationalMap.ProgrammePriorities.Repositories
{
    internal class ProgrammePriorityNomsRepository : Repository, IProgrammePriorityNomsRepository
    {
        public ProgrammePriorityNomsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        private EntityNomVO Selector(int id, string code, string name, string nameAlt)
        {
            return new EntityNomVO
            {
                NomValueId = id,
                Name = code + " " + name,
                NameAlt = !string.IsNullOrWhiteSpace(nameAlt) ?
                    code + " " + nameAlt
                    : null, // the JsonConverter will default to the Name if NameAlt is null
            };
        }

        public EntityNomVO GetNom(int nomValueId)
        {
            return (from pp in this.unitOfWork.DbContext.Set<ProgrammePriority>()
                    where pp.MapNodeId == nomValueId
                    select new
                    {
                        pp.MapNodeId,
                        pp.Code,
                        pp.Name,
                        pp.NameAlt,
                    })
                    .ToList() // enumerate to enable complex expressions
                    .Select(pp => this.Selector(pp.MapNodeId, pp.Code, pp.Name, pp.NameAlt))
                    .SingleOrDefault();
        }

        public IEnumerable<EntityNomVO> GetNoms(string term, int offset = 0, int? limit = null)
        {
            return this.GetProgrammePriorityNoms(term, offset, limit);
        }

        public IEnumerable<EntityNomVO> GetProgrammePriorityNoms(string term, int offset = 0, int? limit = null, int? programmeId = null)
        {
            var predicate =
                PredicateBuilder.True<ProgrammePriority>()
                .AndAnyStringContains(pp => pp.Code, pp => pp.Name, pp => pp.NameAlt, term);

            var programmePriorities = from pp in this.unitOfWork.DbContext.Set<ProgrammePriority>().Where(predicate)
                                      select pp;

            if (programmeId.HasValue)
            {
                programmePriorities = from pp in programmePriorities
                                      where pp.MapNodeRelation.ProgrammeId == programmeId.Value
                                      select pp;
            }

            return (from pp in programmePriorities
                    select new
                    {
                        pp.MapNodeId,
                        pp.Code,
                        pp.Name,
                        pp.NameAlt,
                    })
                    .ToList()
                    .Select(pp => this.Selector(pp.MapNodeId, pp.Code, pp.Name, pp.NameAlt));
        }

        public IEnumerable<EntityNomVO> GetProcedureProgrammePriorityNoms(int procedureId, int programmeId, string term, int offset = 0, int? limit = null)
        {
            var procedureProgrammePriorities = from ps in this.unitOfWork.DbContext.Set<ProcedureShare>()
                                               join pp in this.unitOfWork.DbContext.Set<ProgrammePriority>() on ps.ProgrammePriorityId equals pp.MapNodeId
                                               where ps.ProcedureId == procedureId && pp.MapNodeRelation.ProgrammeId == programmeId
                                               select ps.ProgrammePriorityId;

            return this.GetProgrammePrioritiesWithIds(procedureProgrammePriorities, term, offset, limit);
        }

        public IEnumerable<EntityNomVO> GetContractProgrammePriorityNoms(int contractId, string term, int offset = 0, int? limit = null)
        {
            var contractProgrammePriorities = from cba in this.unitOfWork.DbContext.Set<ContractBudgetLevel3Amount>()
                                              join pb in this.unitOfWork.DbContext.Set<ProcedureBudgetLevel2>() on cba.ProcedureBudgetLevel2Id equals pb.ProcedureBudgetLevel2Id
                                              join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on pb.ProcedureShareId equals ps.ProcedureShareId
                                              where cba.ContractId == contractId && cba.IsActive
                                              select ps.ProgrammePriorityId;

            return this.GetProgrammePrioritiesWithIds(contractProgrammePriorities, term, offset, limit);
        }

        private IEnumerable<EntityNomVO> GetProgrammePrioritiesWithIds(IQueryable<int> ids, string term, int offset, int? limit)
        {
            var predicate = PredicateBuilder.True<ProgrammePriority>()
                .AndAnyStringContains(pp => pp.Code, pp => pp.Name, pp => pp.NameAlt, term);

            return (from pp in this.unitOfWork.DbContext.Set<ProgrammePriority>().Where(predicate)
                    where ids.Contains(pp.MapNodeId)
                    select new
                    {
                        pp.MapNodeId,
                        pp.Code,
                        pp.Name,
                        pp.NameAlt,
                    })
                .OrderBy(t => t.Name)
                .WithOffsetAndLimit(offset, limit)
                .ToList()
                .Select(pp => this.Selector(pp.MapNodeId, pp.Code, pp.Name, pp.NameAlt));
        }
    }
}
