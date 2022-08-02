using Eumis.Common.Db;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.Linq;
using Eumis.Domain.EvalSessions;
using Eumis.Domain.Procedures;
using Eumis.Domain.Users;
using Eumis.Domain.Users.ProgrammePermissions;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.Data.EvalSessions.Repositories
{
    internal class EvalSessionUserNomsRepository : EntityNomsRepository<User, EntityNomVO>, IEvalSessionUserNomsRepository
    {
        private IEvalSessionsRepository evalSessionsRepository;

        public EvalSessionUserNomsRepository(IUnitOfWork unitOfWork, IEvalSessionsRepository evalSessionsRepository)
            : base(
                unitOfWork,
                t => t.UserId,
                t => t.Fullname + t.Username,
                t => new EntityNomVO
                {
                    NomValueId = t.UserId,
                    Name = t.Fullname + "(" + t.Username + ")",
                })
        {
            this.evalSessionsRepository = evalSessionsRepository;
        }

        public IList<EntityNomVO> GetSessionUserNoms(int evalSessionId, string term, int offset = 0, int? limit = null)
        {
            var programmeId = this.evalSessionsRepository.GetProgrammeId(evalSessionId);
            var permission = EvalSessionPermissions.CanEvaluate.ToString();

            var predicate =
                PredicateBuilder.True<User>()
                .AndStringContains(this.nameSelector, term);

            var users = from u in this.unitOfWork.DbContext.Set<User>()
                        join pp in this.unitOfWork.DbContext.Set<ProgrammePermission>() on u.UserId equals pp.UserId
                        join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on pp.ProgrammeId equals ps.ProgrammeId
                        join es in this.unitOfWork.DbContext.Set<EvalSession>() on ps.ProcedureId equals es.ProcedureId
                        where ps.IsPrimary && pp.PermissionString == permission && u.IsActive && !u.IsDeleted && !u.IsLocked && es.EvalSessionId == evalSessionId && ps.ProgrammeId == programmeId
                        select u;

            return users
                .Where(predicate)
                .OrderBy(this.nameSelector)
                .WithOffsetAndLimit(offset, limit)
                .Select(this.voSelector)
                .ToList();
        }
    }
}
