using Eumis.Common.Db;
using Eumis.Data.Core;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.Linq;
using Eumis.Domain.EvalSessions;
using Eumis.Domain.Projects;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.Data.EvalSessions.Repositories
{
    internal class EvalSessionProjectNomsRepository : Repository, IEvalSessionProjectNomsRepository
    {
        public EvalSessionProjectNomsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public EntityNomVO GetNom(int nomValueId, int evalSessionId)
        {
            return (from e in this.unitOfWork.DbContext.Set<EvalSessionProject>()
                    join p in this.unitOfWork.DbContext.Set<Project>() on e.ProjectId equals p.ProjectId
                    where e.EvalSessionId == evalSessionId && e.ProjectId == nomValueId
                    select new EntityNomVO
                    {
                        NomValueId = e.ProjectId,
                        Name = p.RegNumber + " - " + p.Name,
                        NameAlt = p.RegNumber + " - " + p.NameAlt,
                    }).SingleOrDefault();
        }

        public IEnumerable<EntityNomVO> GetNoms(int evalSessionId, string term, int offset = 0, int? limit = null, bool? notDeletedOnly = true)
        {
            var predicate = PredicateBuilder.True<EvalSessionProject>();
            var projPredicate = PredicateBuilder.True<Project>();

            projPredicate = projPredicate
                .AndStringContains(p => p.RegNumber + " - " + p.Name, term);

            if (notDeletedOnly.Value)
            {
                predicate = predicate.And(p => !p.IsDeleted);
                projPredicate = projPredicate.And(p => p.RegistrationStatus != ProjectRegistrationStatus.Withdrawn);
            }

            return (from e in this.unitOfWork.DbContext.Set<EvalSessionProject>().Where(predicate)
                    join p in this.unitOfWork.DbContext.Set<Project>().Where(projPredicate) on e.ProjectId equals p.ProjectId
                    where e.EvalSessionId == evalSessionId
                    select p)
                    .OrderBy(p => p.RegNumber + " - " + p.Name)
                    .WithOffsetAndLimit(offset, limit)
                    .Select(p => new EntityNomVO
                    {
                        NomValueId = p.ProjectId,
                        Name = p.RegNumber + " - " + p.Name,
                        NameAlt = p.RegNumber + " - " + p.NameAlt,
                    })
                    .ToList();
        }
    }
}
