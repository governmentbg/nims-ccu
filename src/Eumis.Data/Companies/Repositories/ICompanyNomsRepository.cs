using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Companies;
using System.Collections.Generic;

namespace Eumis.Data.Companies.Repositories
{
    public interface ICompanyNomsRepository : IEntityNomsRepository<Company, EntityNomVO>
    {
        IEnumerable<EntityNomVO> GetLocalActionGroups(string term, int offset = 0, int? limit = null);
    }
}
