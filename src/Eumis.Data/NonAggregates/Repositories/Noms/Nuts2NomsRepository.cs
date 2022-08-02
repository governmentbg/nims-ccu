using System.Collections.Generic;
using System.Linq;
using Eumis.Common.Db;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.Linq;
using Eumis.Data.NonAggregates.ViewObjects;
using Eumis.Domain.NonAggregates;

namespace Eumis.Data.NonAggregates.Repositories.Noms
{
    internal class Nuts2NomsRepository : EntityCodeNomsRepository<Nuts2, NutsCodeNomVO>, INuts2NomsRepository
    {
        public Nuts2NomsRepository(IUnitOfWork unitOfWork)
            : base(
                unitOfWork,
                t => t.Nuts2Id,
                t => t.Name,
                t => t.NutsCode,
                t => new NutsCodeNomVO
                {
                    NomValueId = t.Nuts2Id,
                    ParentId = t.Nuts1Id,
                    Code = t.NutsCode,
                    Name = t.Name,
                    FullPathName = t.FullPathName,
                    FullPath = t.FullPath,
                })
        {
        }

        public IEnumerable<NutsCodeNomVO> GetNuts2Noms(int nuts1Id, string term, int offset = 0, int? limit = null)
        {
            return this.GetNameFilteredQuery(term)
                .Where(m => m.Nuts1Id == nuts1Id)
                .OrderBy(t => t.Name)
                .WithOffsetAndLimit(offset, limit)
                .Select(this.voSelector)
                .ToList();
        }
    }
}
