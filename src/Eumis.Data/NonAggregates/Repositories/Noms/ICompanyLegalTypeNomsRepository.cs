using System.Collections.Generic;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.NonAggregates.ViewObjects;
using Eumis.Domain.NonAggregates;

namespace Eumis.Data.NonAggregates.Repositories.Noms
{
    public interface ICompanyLegalTypeNomsRepository : IEntityGidNomsRepository<CompanyLegalType, CompanyLegalTypeGidNomVO>
    {
        IEnumerable<CompanyLegalTypeGidNomVO> GetCompanyLegalTypeNoms(int companyTypeId, string term, int offset = 0, int? limit = null);
    }
}
