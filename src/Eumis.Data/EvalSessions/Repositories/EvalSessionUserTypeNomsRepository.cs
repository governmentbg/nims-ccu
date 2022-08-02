using Eumis.Common.Db;
using Eumis.Data.Core;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.Linq;
using Eumis.Domain.EvalSessions;
using Eumis.Domain.Users;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.Data.EvalSessions.Repositories
{
    internal class EvalSessionUserTypeNomsRepository : Repository, IEvalSessionUserTypeNomsRepository
    {
        public EvalSessionUserTypeNomsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public EntityNomVO GetNom(int nomValueId, int evalSessionId)
        {
            return (from e in this.unitOfWork.DbContext.Set<EvalSessionUser>()
                    join u in this.unitOfWork.DbContext.Set<User>() on e.UserId equals u.UserId
                    where e.EvalSessionId == evalSessionId && e.EvalSessionUserId == nomValueId
                    select new EntityNomVO
                    {
                        NomValueId = e.EvalSessionUserId,
                        Name = u.Fullname + "(" + u.Username + ")",
                    }).SingleOrDefault();
        }

        public IEnumerable<EntityNomVO> GetNoms(int evalSessionId, EvalSessionUserType userType, string term, int offset = 0, int? limit = null)
        {
            var predicate = PredicateBuilder.True<User>()
                .AndStringContains(p => p.Fullname + "(" + p.Username + ")", term);

            return (from e in this.unitOfWork.DbContext.Set<EvalSessionUser>()
                    join u in this.unitOfWork.DbContext.Set<User>().Where(predicate) on e.UserId equals u.UserId
                    where e.EvalSessionId == evalSessionId && e.Type == userType && e.Status == EvalSessionUserStatus.Activated
                    select new { e, u })
                .OrderBy(p => p.u.Fullname + "(" + p.u.Username + ")")
                .WithOffsetAndLimit(offset, limit)
                .Select(p => new EntityNomVO
                {
                    NomValueId = p.e.EvalSessionUserId,
                    Name = p.u.Fullname + "(" + p.u.Username + ")",
                })
                .ToList();
        }
    }
}
