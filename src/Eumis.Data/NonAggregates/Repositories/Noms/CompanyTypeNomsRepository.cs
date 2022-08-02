using Eumis.Common.Db;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.NonAggregates.ViewObjects;
using Eumis.Domain.NonAggregates;

namespace Eumis.Data.NonAggregates.Repositories.Noms
{
    internal class CompanyTypeNomsRepository : EntityGidNomsRepository<CompanyType, CompanyTypeGidNomVO>
    {
        public CompanyTypeNomsRepository(IUnitOfWork unitOfWork)
            : base(
                unitOfWork,
                t => t.CompanyTypeId,
                t => t.Name,
                t => t.Gid,
                t => new CompanyTypeGidNomVO
                {
                    NomValueId = t.CompanyTypeId,
                    Name = t.Name,
                    Alias = t.Alias,
                    Gid = t.Gid,
                },
                t => t.Order)
        {
        }
    }
}
