using System.Collections.Generic;
using System.Linq;
using Eumis.Common.Db;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.Linq;
using Eumis.Data.NonAggregates.ViewObjects;
using Eumis.Domain.NonAggregates;

namespace Eumis.Data.NonAggregates.Repositories.Noms
{
    internal class SettlementNomsRepository : EntityCodeNomsRepository<Settlement, SettlementCodeNomVO>, ISettlementNomsRepository
    {
        public SettlementNomsRepository(IUnitOfWork unitOfWork)
            : base(
                unitOfWork,
                t => t.SettlementId,
                t => t.DisplayName,
                t => t.LauCode,
                t => new SettlementCodeNomVO
                {
                    NomValueId = t.SettlementId,
                    ParentId = t.MunicipalityId,
                    Code = t.LauCode,
                    Name = t.DisplayName,
                    FullPath = t.FullPath,
                    FullPathName = t.FullPathName,
                },
                t => t.Order)
        {
        }

        public IEnumerable<SettlementCodeNomVO> GetSettlementNoms(int municipalityId, string term, int offset = 0, int? limit = null)
        {
            return this.GetNameFilteredQuery(term)
            .Where(m => m.MunicipalityId == municipalityId)
            .OrderBy(t => t.DisplayName)
            .WithOffsetAndLimit(offset, limit)
            .Select(this.voSelector)
            .ToList();
        }

        public SettlementCodeNomVO GetSettlementNom(string ekatte)
        {
            if (string.IsNullOrEmpty(ekatte))
            {
                return null;
            }

            return this.GetNoms(string.Empty)
                .Where(x => x.Code == ekatte.ToLower())
                .FirstOrDefault();
        }
    }
}
