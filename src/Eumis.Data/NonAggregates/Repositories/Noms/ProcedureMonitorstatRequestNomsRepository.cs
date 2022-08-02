using System.Collections.Generic;
using System.Linq;
using Eumis.Common.Db;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.Linq;
using Eumis.Domain.Procedures;
using Eumis.Domain.Projects;

namespace Eumis.Data.NonAggregates.Repositories.Noms
{
    internal class ProcedureMonitorstatRequestNomsRepository : EntityNomsRepository<ProcedureMonitorstatRequest, EntityNomVO>, IProcedureMonitorstatRequestNomsRepository
    {
        public ProcedureMonitorstatRequestNomsRepository(IUnitOfWork unitOfWork)
            : base(
                unitOfWork,
                t => t.ProcedureMonitorstatRequestId,
                t => t.Name,
                t => new EntityNomVO
                {
                    NomValueId = t.ProcedureMonitorstatRequestId,
                    Name = t.Name,
                })
        {
        }

        public IList<EntityNomVO> GetNomsForProcedure(
            int projectId,
            string term,
            int offset = 0,
            int? limit = null)
        {
            return (from p in this.unitOfWork.DbContext.Set<Project>().Where(x => x.ProjectId == projectId)
                    join pmr in this.unitOfWork.DbContext.Set<ProcedureMonitorstatRequest>() on p.ProcedureId equals pmr.ProcedureId
                    where pmr.Status == ProcedureMonitorstatRequestStatus.Sent
                    select pmr)
                .OrderBy(this.nameSelector)
                .WithOffsetAndLimit(offset, limit)
                .Select(this.voSelector)
                .ToList();
        }

        public IList<EntityNomVO> GetNSIDeclarationNomsForProcedure(
            int procedureId,
            string term = null,
            int offset = 0,
            int? limit = null)
        {
            return this.unitOfWork.DbContext.Set<ProcedureMonitorstatRequest>()
                .Where(pmr => pmr.ProcedureId == procedureId && pmr.Status == ProcedureMonitorstatRequestStatus.Sent)
                .OrderBy(this.nameSelector)
                .WithOffsetAndLimit(offset, limit)
                .Select(this.voSelector)
                .ToList();
        }
    }
}
