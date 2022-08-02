using System.Collections.Generic;
using System.Linq;
using Eumis.Common.Db;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.Linq;
using Eumis.Data.NonAggregates.ViewObjects;
using Eumis.Domain.NonAggregates;

namespace Eumis.Data.NonAggregates.Repositories.Noms
{
    internal class MunicipalityNomsRepository : EntityCodeNomsRepository<Municipality, NutsCodeNomVO>, IMunicipalityNomsRepository
    {
        public MunicipalityNomsRepository(IUnitOfWork unitOfWork)
            : base(
                unitOfWork,
                t => t.MunicipalityId,
                t => t.DisplayName,
                t => t.LauCode,
                t => new NutsCodeNomVO
                {
                    NomValueId = t.MunicipalityId,
                    ParentId = t.DistrictId,
                    Code = t.LauCode,
                    Name = t.DisplayName,
                    FullPath = t.FullPath,
                    FullPathName = t.FullPathName,
                })
        {
        }

        public IEnumerable<NutsCodeNomVO> GetMunicipalityNoms(int districtId, string term, int offset = 0, int? limit = null)
        {
            return this.GetNameFilteredQuery(term)
                .Where(m => m.DistrictId == districtId)
                .OrderBy(t => t.DisplayName)
                .WithOffsetAndLimit(offset, limit)
                .Select(this.voSelector)
                .ToList();
        }
    }
}
