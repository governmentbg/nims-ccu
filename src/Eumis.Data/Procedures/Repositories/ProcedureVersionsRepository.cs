using Eumis.Common.Db;
using Eumis.Data.Procedures.PortalViewObjects;
using Eumis.Domain.Procedures;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.Data.Procedures.Repositories
{
    internal class ProcedureVersionsRepository : AggregateRepository<ProcedureVersion>, IProcedureVersionsRepository
    {
        public ProcedureVersionsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public ProcedureVersion GetLastVersion(int procedureId)
        {
            return this.Set()
                .Where(pv => pv.ProcedureId == procedureId)
                .OrderByDescending(pv => pv.ProcedureVersionId)
                .FirstOrDefault();
        }

        public ProcedureInfoPVO GetPortalProcedureInfo(Guid procedureGid)
        {
            var procedureVersion = this.unitOfWork.DbContext.Set<ProcedureVersion>()
                .Where(pv => pv.ProcedureGid == procedureGid)
                .OrderByDescending(pv => pv.ProcedureVersionId)
                .FirstOrDefault();

            if (procedureVersion == null)
            {
                throw new DataObjectNotFoundException("Procedures", procedureGid);
            }

            var procedure = (from p in this.unitOfWork.DbContext.Set<Procedure>()

                             where p.Gid == procedureGid
                             select new
                             {
                                 Status = p.ProcedureStatus,
                                 ProcedureId = p.ProcedureId,
                             })
                             .FirstOrDefault();

            var timeLimits = (from l in this.unitOfWork.DbContext.Set<ProcedureTimeLimit>()
                              where l.ProcedureId == procedure.ProcedureId
                              orderby l.EndDate
                              select new { l.ProcedureTimeLimitId, l.EndDate, l.Notes }).ToList();

            var actualTimeLimits = timeLimits.Where(t => t.EndDate >= DateTime.Now).DefaultIfEmpty(timeLimits.Last()).First();

            return new ProcedureInfoPVO(procedureVersion.ProcedureVersionJson, procedureVersion.IsActive, procedureGid, procedure.Status, actualTimeLimits.EndDate);
        }

        public ProcedureVersion GetPortalProcedureVersion(Guid procedureGid)
        {
            var oldTimeout = this.unitOfWork.DbContext.Database.CommandTimeout;
            this.unitOfWork.DbContext.Database.CommandTimeout = 10 * 60;

            var procedureVersion = this.unitOfWork.DbContext.Set<ProcedureVersion>()
                .Where(pv => pv.ProcedureGid == procedureGid)
                .OrderByDescending(pv => pv.ProcedureVersionId)
                .FirstOrDefault();

            this.unitOfWork.DbContext.Database.CommandTimeout = oldTimeout;

            if (procedureVersion == null)
            {
                throw new DataObjectNotFoundException("Procedures", procedureGid);
            }

            return procedureVersion;
        }
    }
}
