using System.Collections.Generic;
using System.Linq;
using Eumis.Common.Db;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.Linq;
using Eumis.Domain.Procedures;

namespace Eumis.Data.Procedures.Repositories
{
    internal class ProcedureNomsRepository : EntityNomsRepository<Procedure, EntityNomVO>, IProcedureNomsRepository
    {
        public ProcedureNomsRepository(IUnitOfWork unitOfWork)
            : base(
                unitOfWork,
                q => q.ProcedureId,
                q => q.Code + " " + q.Name,
                q => q.Code + " " + q.NameAlt,
                q => new EntityNomVO
                {
                    NomValueId = q.ProcedureId,
                    Name = q.Code + " " + q.Name,
                    NameAlt = q.Code + " " + q.NameAlt,
                })
        {
        }

        public IList<EntityNomVO> GetProcedureNoms(int programmeId, string term, int offset = 0, int? limit = null)
        {
            var predicate = PredicateBuilder.True<Procedure>()
                .AndAnyStringContains(p => p.Code + " " + p.Name, p => p.Code + " " + p.NameAlt, term);

            return (from p in this.unitOfWork.DbContext.Set<Procedure>().Where(predicate)
                    join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on p.ProcedureId equals ps.ProcedureId
                    where ps.ProgrammeId == programmeId
                    select new EntityNomVO
                    {
                        NomValueId = p.ProcedureId,
                        Name = p.Code + " " + p.Name,
                        NameAlt = p.Code + " " + p.NameAlt,
                    })
                    .Distinct()
                    .OrderBy(t => t.Name)
                    .WithOffsetAndLimit(offset, limit)
                    .ToList();
        }

        public IList<EntityNomVO> GetProcedureNoms(int[] programmeIds, string term, int offset = 0, int? limit = null)
        {
            var predicate = PredicateBuilder.True<Procedure>()
                .AndAnyStringContains(p => p.Code + " " + p.Name, p => p.Code + " " + p.NameAlt, term);

            return (from p in this.unitOfWork.DbContext.Set<Procedure>().Where(predicate)
                    join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on p.ProcedureId equals ps.ProcedureId
                    where programmeIds.Contains(ps.ProgrammeId)
                    select new EntityNomVO
                    {
                        NomValueId = p.ProcedureId,
                        Name = p.Code + " " + p.Name,
                        NameAlt = p.Code + " " + p.NameAlt,
                    })
                    .Distinct()
                    .OrderBy(t => t.Name)
                    .WithOffsetAndLimit(offset, limit)
                    .ToList();
        }

        public IList<EntityNomVO> GetProcedureNoms(string term, int offset = 0, int? limit = null)
        {
            var predicate = PredicateBuilder.True<Procedure>()
                .AndAnyStringContains(p => p.Code + " " + p.Name, p => p.Code + " " + p.NameAlt, term);

            return (from p in this.unitOfWork.DbContext.Set<Procedure>().Where(predicate)
                    where p.ProcedureStatus != ProcedureStatus.Canceled
                    select new EntityNomVO
                    {
                        NomValueId = p.ProcedureId,
                        Name = p.Code + " " + p.Name,
                        NameAlt = p.Code + " " + p.NameAlt,
                    })
                    .Distinct()
                    .OrderBy(t => t.Name)
                    .WithOffsetAndLimit(offset, limit)
                    .ToList();
        }

        public IList<EntityNomVO> GetProcedureNomsByProgrammeAndProgrammePriority(int programmeId, int programmePriorityId, string term, int offset = 0, int? limit = null)
        {
            var predicate = PredicateBuilder.True<Procedure>()
                .AndAnyStringContains(p => p.Code + " " + p.Name, p => p.Code + " " + p.NameAlt, term);

            var groupedProcedures = from p in this.unitOfWork.DbContext.Set<Procedure>().Where(predicate)
                                    join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on p.ProcedureId equals ps.ProcedureId
                                    group p by new { p.ProcedureId, p.Code, p.Name, p.NameAlt } into g
                                    where g.Count() == 1
                                    select g;

            return (from g in groupedProcedures
                    join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on g.Key.ProcedureId equals ps.ProcedureId
                    where ps.ProgrammeId == programmeId && ps.ProgrammePriorityId == programmePriorityId
                    select new EntityNomVO
                    {
                        NomValueId = g.Key.ProcedureId,
                        Name = g.Key.Code + " " + g.Key.Name,
                        NameAlt = g.Key.Code + " " + g.Key.NameAlt,
                    })
                .Distinct()
                .OrderBy(t => t.Name)
                .WithOffsetAndLimit(offset, limit)
                .ToList();
        }

        public IList<EntityNomVO> GetProcedureNomsByProgramme(int programmeId, string term, int offset = 0, int? limit = null)
        {
            var predicate = PredicateBuilder.True<Procedure>()
                .AndAnyStringContains(p => p.Code + " " + p.Name, p => p.Code + " " + p.NameAlt, term);

            return (from p in this.unitOfWork.DbContext.Set<Procedure>().Where(predicate)
                    join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on p.ProcedureId equals ps.ProcedureId
                    where ps.ProgrammeId == programmeId
                    select new EntityNomVO
                    {
                        NomValueId = p.ProcedureId,
                        Name = p.Code + " " + p.Name,
                        NameAlt = p.Code + " " + p.NameAlt,
                    })
                .Distinct()
                .OrderBy(t => t.Name)
                .WithOffsetAndLimit(offset, limit)
                .ToList();
        }

        public IList<EntityNomVO> GetProcedureNomsByProgrammePriority(int programmePriorityId, string term, int offset = 0, int? limit = null)
        {
            var predicate = PredicateBuilder.True<Procedure>()
                .AndAnyStringContains(p => p.Code + " " + p.Name, p => p.Code + " " + p.NameAlt, term);

            return (from p in this.unitOfWork.DbContext.Set<Procedure>().Where(predicate)
                    join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on p.ProcedureId equals ps.ProcedureId
                    where ps.ProgrammePriorityId == programmePriorityId
                    select new EntityNomVO
                    {
                        NomValueId = p.ProcedureId,
                        Name = p.Code + " " + p.Name,
                        NameAlt = p.Code + " " + p.NameAlt,
                    })
                .Distinct()
                .OrderBy(t => t.Name)
                .WithOffsetAndLimit(offset, limit)
                .ToList();
        }

        public IList<EntityNomVO> GetActiveProcedureNoms(int[] programmeIds, string term, int offset = 0, int? limit = null)
        {
            var predicate = PredicateBuilder.True<Procedure>()
                .And(p => p.ProcedureStatus == ProcedureStatus.Active || p.ProcedureStatus == ProcedureStatus.Ended)
                .AndAnyStringContains(p => p.Code + " " + p.Name, p => p.Code + " " + p.NameAlt, term);

            return (from p in this.unitOfWork.DbContext.Set<Procedure>().Where(predicate)
                    join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on p.ProcedureId equals ps.ProcedureId
                    where programmeIds.Contains(ps.ProgrammeId)
                    select new EntityNomVO
                    {
                        NomValueId = p.ProcedureId,
                        Name = p.Code + " " + p.Name,
                        NameAlt = p.Code + " " + p.NameAlt,
                    })
                    .Distinct()
                    .OrderBy(t => t.Name)
                    .WithOffsetAndLimit(offset, limit)
                    .ToList();
        }
    }
}
