using Eumis.Common.Db;
using Eumis.Data.Core;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.NonAggregates;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.Data.NonAggregates.Repositories.Noms
{
    internal class NutsLevelEnumNomsRepository : Repository, IEnumNomsRepository<NutsLevel>
    {
        public NutsLevelEnumNomsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public EnumNomVO<NutsLevel> GetNom(NutsLevel n)
        {
            return new EnumNomVO<NutsLevel>(n);
        }

        public IList<EnumNomVO<NutsLevel>> GetNoms(string term)
        {
            return NutsLevel.GetValues(typeof(NutsLevel))
                .Cast<NutsLevel>()
                .Where(e => string.IsNullOrEmpty(term) || Enum.GetName(typeof(NutsLevel), e).Contains(term))
                .Select(e => new EnumNomVO<NutsLevel>(e))
                .ToList();
        }
    }
}
