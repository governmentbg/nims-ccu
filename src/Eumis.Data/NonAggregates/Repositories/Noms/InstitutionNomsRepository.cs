using Eumis.Common.Db;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.Linq;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.OperationalMap.MapNodes;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.Data.NonAggregates.Repositories.Noms
{
    internal class InstitutionNomsRepository : EntityNomsRepository<Institution, EntityNomVO>, IInstitutionNomsRepository
    {
        public InstitutionNomsRepository(IUnitOfWork unitOfWork)
            : base(
                unitOfWork,
                t => t.InstitutionId,
                t => t.Name,
                t => new EntityNomVO
                {
                    NomValueId = t.InstitutionId,
                    Name = t.Name,
                })
        {
        }

        public IList<EntityNomVO> GetInstitutionNoms(int programmeId, string term, int offset = 0, int? limit = null)
        {
            var predicate =
                PredicateBuilder.True<Institution>()
                .AndStringContains(this.nameSelector, term);

            var institutions = this.unitOfWork.DbContext.Set<Institution>().Where(predicate);

            IQueryable<Institution> used;
            used = from i in institutions
                   select i;

            institutions = institutions.Except(used);

            return institutions
                .OrderBy(this.nameSelector)
                .WithOffsetAndLimit(offset, limit)
                .Select(this.voSelector)
                .ToList();
        }
    }
}
