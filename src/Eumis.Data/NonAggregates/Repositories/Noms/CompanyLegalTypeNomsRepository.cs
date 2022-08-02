using Eumis.Common.Db;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.Linq;
using Eumis.Data.NonAggregates.ViewObjects;
using Eumis.Domain.NonAggregates;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Eumis.Data.NonAggregates.Repositories.Noms
{
    internal class CompanyLegalTypeNomsRepository : EntityGidNomsRepository<CompanyLegalType, CltCtQuery, CompanyLegalTypeGidNomVO>, ICompanyLegalTypeNomsRepository
    {
        public CompanyLegalTypeNomsRepository(IUnitOfWork unitOfWork)
            : base(
                unitOfWork,
                q => q.CompanyLegalType.CompanyLegalTypeId,
                q => q.CompanyLegalType.Name,
                q => q.CompanyLegalType.Gid,
                q => new CompanyLegalTypeGidNomVO
                {
                    CompanyTypeGid = q.CompanyType.Gid,
                    Gid = q.CompanyLegalType.Gid,
                    Name = q.CompanyLegalType.Name,
                    NomValueId = q.CompanyLegalType.CompanyLegalTypeId,
                    Alias = q.CompanyLegalType.Alias,
                },
                q => q.CompanyLegalType.Order)
        {
        }

        public IEnumerable<CompanyLegalTypeGidNomVO> GetCompanyLegalTypeNoms(int companyTypeId, string term, int offset = 0, int? limit = null)
        {
            return this.GetNameFilteredQuery(term)
                .Where(cltCt => cltCt.CompanyType.CompanyTypeId == companyTypeId)
                .OrderBy(t => t.CompanyLegalType.Order)
                .WithOffsetAndLimit(offset, limit)
                .Select(this.voSelector)
                .ToList();
        }

        protected override IQueryable<CltCtQuery> GetQuery()
        {
            return from clt in this.unitOfWork.DbContext.Set<CompanyLegalType>()
                   join ct in this.unitOfWork.DbContext.Set<CompanyType>() on clt.CompanyTypeId equals ct.CompanyTypeId
                   select new CltCtQuery
                   {
                       CompanyType = ct,
                       CompanyLegalType = clt,
                   };
        }
    }

    [SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "The class has no usage on its own.")]
    internal class CltCtQuery
    {
        public CompanyLegalType CompanyLegalType { get; set; }

        public CompanyType CompanyType { get; set; }
    }
}
