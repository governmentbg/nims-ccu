using System.Collections.Generic;
using System.Linq;
using Eumis.Common.Db;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.Linq;
using Eumis.Data.NonAggregates.ViewObjects;
using Eumis.Domain.NonAggregates;

namespace Eumis.Data.NonAggregates.Repositories.Noms
{
    internal class ProtectedZoneNomsRepository : EntityCodeNomsRepository<ProtectedZone, NutsCodeNomVO>, IProtectedZoneNomsRepository
    {
        public ProtectedZoneNomsRepository(IUnitOfWork unitOfWork)
            : base(
                unitOfWork,
                t => t.ProtectedZoneId,
                t => t.Name,
                t => t.NutsCode,
                t => new NutsCodeNomVO
                {
                    NomValueId = t.ProtectedZoneId,
                    ParentId = t.CountryId,
                    Code = t.NutsCode,
                    Name = t.Name,
                    FullPath = t.FullPath,
                    FullPathName = t.FullPathName,
                })
        {
        }

        public IEnumerable<NutsCodeNomVO> GetProtectedZoneNoms(int countryId, string term, int offset = 0, int? limit = null)
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
