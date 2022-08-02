using System.Collections.Generic;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.Irregularities.ViewObjects;
using Eumis.Domain.Irregularities;

namespace Eumis.Data.Irregularities.Repositories
{
    public interface IIrregularitySanctionTypeNomsRepository : IEntityCodeNomsRepository<IrregularitySanctionType, IrregularitySanctionTypeVO>
    {
        IEnumerable<IrregularitySanctionTypeVO> GetSanctionTypeNoms(int sanctionCategoryId, string term, int offset = 0, int? limit = null);
    }
}
