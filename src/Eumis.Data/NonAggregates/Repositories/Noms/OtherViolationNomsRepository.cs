using Eumis.Common.Db;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.Linq;
using Eumis.Domain.MonitoringFinancialControl;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.Data.NonAggregates.Repositories.Noms
{
    internal class OtherViolationNomsRepository : EntityNomsRepository<OtherViolation, EntityNomVO>, IOtherViolationNomsRepository
    {
        public OtherViolationNomsRepository(IUnitOfWork unitOfWork)
            : base(
                unitOfWork,
                t => t.OtherViolationId,
                t => t.Name,
                t => new EntityNomVO
                {
                    NomValueId = t.OtherViolationId,
                    Name = t.Name,
                })
        {
        }

        public IList<EntityNomVO> GetNoms(int[] ids, string term = null, int offset = 0, int? limit = null)
        {
            var predicate =
                PredicateBuilder.True<OtherViolation>()
                .AndStringContains(this.nameSelector, term);

            if (ids.Length != 0)
            {
                predicate = predicate.And(t => ids.Contains(t.OtherViolationId));
            }

            return this.GetQuery()
                .OrderBy(this.nameSelector)
                .Where(predicate)
                .WithOffsetAndLimit(offset, limit)
                .Select(this.voSelector)
                .ToList();
        }
    }
}
