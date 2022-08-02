using Eumis.Common.Db;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.NonAggregates.ViewObjects;
using Eumis.Domain.NonAggregates;
using System.Linq;

namespace Eumis.Data.NonAggregates.Repositories.Noms
{
    internal class CompanySizeTypeNomsRepository : EntityGidNomsRepository<CompanySizeType, CompanySizeTypeGidNomVO>, ICompanySizeTypeNomsRepository
    {
        public CompanySizeTypeNomsRepository(IUnitOfWork unitOfWork)
            : base(
                unitOfWork,
                t => t.CompanySizeTypeId,
                t => t.Name,
                t => t.Gid,
                t => new CompanySizeTypeGidNomVO
                {
                    NomValueId = t.CompanySizeTypeId,
                    Name = t.Name,
                    Gid = t.Gid,
                    Alias = t.Alias,
                    NameAlt = t.NameAlt,
                },
                t => t.Order)
        {
        }

        public CompanySizeTypeGidNomVO GetByAlias(string valueAlias)
        {
            return this.GetQuery()
                .Where(t => t.Alias == valueAlias)
                .Select(this.voSelector)
                .SingleOrDefault();
        }
    }
}
