using Eumis.Common.Db;
using Eumis.Common.Json;
using Eumis.Data.Core;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.Linq;
using Eumis.Domain.Companies;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.Data.Companies.Repositories
{
    internal class CompanyNomsRepository : Repository, ICompanyNomsRepository
    {
        public CompanyNomsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public EntityNomVO GetNom(int nomValueId)
        {
            if (nomValueId == 0)
            {
                throw new ArgumentException("Filtering by the default value for nomValueId is not allowed.");
            }

            return this.unitOfWork.DbContext.Set<Company>()
                .Where(p => p.CompanyId == nomValueId)
                .ToList()
                .Select(p => new EntityNomVO
                {
                    NomValueId = p.CompanyId,
                    Name = EnumUtils.GetEnumDescription(p.UinType) + " " + p.Uin + " " + p.Name,
                })
                .SingleOrDefault();
        }

        public IEnumerable<EntityNomVO> GetNoms(string term, int offset = 0, int? limit = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EntityNomVO> GetLocalActionGroups(string term, int offset = 0, int? limit = null)
        {
            var predicate = PredicateBuilder.True<Company>()
                .AndStringContains(p => p.Uin + " " + p.Name, term);

            return this.unitOfWork.DbContext.Set<Company>()
                .Where(predicate)
                .OrderBy(p => p.Uin + " " + p.Name)
                .WithOffsetAndLimit(offset, limit)
                .ToList()
                .Select(p => new EntityNomVO
                {
                    NomValueId = p.CompanyId,
                    Name = EnumUtils.GetEnumDescription(p.UinType) + " " + p.Uin + " " + p.Name,
                })
                .ToList();
        }
    }
}
