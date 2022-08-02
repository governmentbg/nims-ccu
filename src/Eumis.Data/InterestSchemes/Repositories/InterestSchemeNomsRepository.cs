using Eumis.Common.Db;
using Eumis.Data.Core;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.Linq;
using Eumis.Domain.InterestSchemes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.Data.NonAggregates.Repositories.Noms
{
    internal class InterestSchemeNomsRepository : Repository, IEntityNomsRepository<InterestScheme, EntityNomVO>
    {
        public InterestSchemeNomsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public EntityNomVO GetNom(int nomValueId)
        {
            return (from isc in this.unitOfWork.DbContext.Set<InterestScheme>()
                    where isc.InterestSchemeId == nomValueId
                    select new EntityNomVO
                    {
                        NomValueId = isc.InterestSchemeId,
                        Name = isc.Name + (isc.IsActive == false ? "(Анулирана)" : string.Empty),
                    })
                    .SingleOrDefault();
        }

        public IEnumerable<EntityNomVO> GetNoms(string term, int offset = 0, int? limit = null)
        {
            var predicate =
                PredicateBuilder.True<InterestScheme>()
                .AndStringContains(isc => isc.Name, term);

            return (from isc in this.unitOfWork.DbContext.Set<InterestScheme>().Where(predicate)
                    where isc.IsActive == true
                    select new EntityNomVO
                    {
                        NomValueId = isc.InterestSchemeId,
                        Name = isc.Name,
                    })
                    .OrderBy(p => p.Name)
                    .WithOffsetAndLimit(offset, limit)
                    .ToList();
        }
    }
}
