using System.Collections.Generic;
using System.Linq;
using Eumis.Common.Db;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.Linq;
using Eumis.Data.NonAggregates.ViewObjects;
using Eumis.Domain.NonAggregates;

namespace Eumis.Data.NonAggregates.Repositories.Noms
{
    internal class DistrictNomsRepository : EntityCodeNomsRepository<District, NutsCodeNomVO>, IDistrictNomsRepository
    {
        public DistrictNomsRepository(IUnitOfWork unitOfWork)
            : base(
                unitOfWork,
                t => t.DistrictId,
                t => t.Name,
                t => t.NutsCode,
                t => new NutsCodeNomVO
                {
                    NomValueId = t.DistrictId,
                    ParentId = t.Nuts2Id,
                    Code = t.NutsCode,
                    Name = t.Name,
                    FullPath = t.FullPath,
                    FullPathName = t.FullPathName,
                })
        {
        }

        public IEnumerable<NutsCodeNomVO> GetDistrictNoms(int nuts2Id, string term, int offset = 0, int? limit = null)
        {
            return this.GetNameFilteredQuery(term)
                .Where(m => m.Nuts2Id == nuts2Id)
                .OrderBy(t => t.Name)
                .WithOffsetAndLimit(offset, limit)
                .Select(this.voSelector)
                .ToList();
        }
    }
}
