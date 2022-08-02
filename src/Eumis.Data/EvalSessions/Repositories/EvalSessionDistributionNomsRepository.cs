using Eumis.Common.Db;
using Eumis.Data.Core;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.Linq;
using Eumis.Domain.EvalSessions;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.Data.EvalSessions.Repositories
{
    internal class EvalSessionDistributionNomsRepository : Repository, IEvalSessionDistributionNomsRepository
    {
        public EvalSessionDistributionNomsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public EntityNomVO GetNom(int nomValueId, int evalSessionId)
        {
            return (from e in this.unitOfWork.DbContext.Set<EvalSessionDistribution>()
                    where e.EvalSessionId == evalSessionId && e.EvalSessionDistributionId == nomValueId
                    select new EntityNomVO
                    {
                        NomValueId = e.EvalSessionDistributionId,
                        Name = e.Code,
                    }).SingleOrDefault();
        }

        public IEnumerable<EntityNomVO> GetNoms(int evalSessionId, string term, int offset = 0, int? limit = null)
        {
            var predicate = PredicateBuilder.True<EvalSessionDistribution>()
                .AndStringContains(p => p.Code, term);

            return (from e in this.unitOfWork.DbContext.Set<EvalSessionDistribution>().Where(predicate)
                    where e.EvalSessionId == evalSessionId
                    select e)
                .OrderBy(p => p.Code)
                .WithOffsetAndLimit(offset, limit)
                .Select(e => new EntityNomVO
                {
                    NomValueId = e.EvalSessionDistributionId,
                    Name = e.Code,
                })
                .ToList();
        }
    }
}
