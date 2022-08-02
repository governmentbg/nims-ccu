using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.Core.Permissions;
using Eumis.Domain.Procedures;
using Eumis.Domain.Users.ProgrammePermissions;
using System.Linq;

namespace Eumis.Data.EvalSessions.Repositories
{
    internal class EvalSessionProcedureNomsRepository : EntityNomsRepository<Procedure, EntityNomVO>, IEvalSessionProcedureNomsRepository
    {
        private IAccessContext accessContext;

        public EvalSessionProcedureNomsRepository(IUnitOfWork unitOfWork, IAccessContext accessContext)
            : base(
                unitOfWork,
                t => t.ProcedureId,
                t => t.Code + " " + t.Name,
                t => t.Code + " " + t.NameAlt,
                t => new EntityNomVO
                {
                    NomValueId = t.ProcedureId,
                    Name = t.Code + " " + t.Name,
                    NameAlt = t.Code + " " + t.NameAlt,
                })
        {
            this.accessContext = accessContext;
        }

        protected override System.Linq.IQueryable<Procedure> GetQuery()
        {
            var programmeIds = this.unitOfWork.CreateProgrammeIdsByPermissionQuery(this.accessContext.UserId, EvalSessionPermissions.CanAdministrate);

            return from p in this.unitOfWork.DbContext.Set<Procedure>()
                   join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on p.ProcedureId equals ps.ProcedureId
                   where ps.IsPrimary && programmeIds.Contains(ps.ProgrammeId) && Procedure.EvalSessionOrProjectCreationStatuses.Contains(p.ProcedureStatus) && p.ProcedureKind == ProcedureKind.Schema
                   select p;
        }
    }
}
