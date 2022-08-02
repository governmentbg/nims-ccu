using System.Collections.Generic;
using System.Linq;
using Eumis.Common.Db;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.Linq;
using Eumis.Data.NonAggregates.ViewObjects;
using Eumis.Domain.NonAggregates;

namespace Eumis.Data.NonAggregates.Repositories.Noms
{
    internal class Nuts1NomsRepository : EntityCodeNomsRepository<Nuts1, NutsCodeNomVO>, INuts1NomsRepository
    {
        public Nuts1NomsRepository(IUnitOfWork unitOfWork)
            : base(
                unitOfWork,
                t => t.Nuts1Id,
                t => t.Name,
                t => t.NutsCode,
                t => new NutsCodeNomVO
                {
                    NomValueId = t.Nuts1Id,
                    ParentId = t.CountryId,
                    Code = t.NutsCode,
                    Name = t.Name,
                    FullPathName = t.FullPathName,
                    FullPath = t.FullPath,
                })
        {
        }

        public IEnumerable<NutsCodeNomVO> GetNuts1Noms(int countryId, string term, int offset = 0, int? limit = null)
        {
            return this.GetNameFilteredQuery(term)
                .Where(m => m.CountryId == countryId)
                .OrderBy(t => t.Name)
                .WithOffsetAndLimit(offset, limit)
                .Select(this.voSelector)
                .ToList();
        }
    }
}
