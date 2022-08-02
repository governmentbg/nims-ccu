using Eumis.Common.Db;
using Eumis.Common.Json;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.Linq;
using Eumis.Domain.Companies;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.Data.Companies.Repositories
{
    internal class ProgrammeCompanyNomsRepository : EntityNomsRepository<Company, EntityNomVO>
    {
        public ProgrammeCompanyNomsRepository(IUnitOfWork unitOfWork)
            : base(
                unitOfWork,
                q => q.CompanyId,
                q => q.Uin + " " + q.Name,
                q => new EntityNomVO
                {
                    NomValueId = q.CompanyId,
                    Name = q.Uin + " " + q.Name,
                })
        {
        }

        public override EntityNomVO GetNom(int nomValueId)
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

        public override IEnumerable<EntityNomVO> GetNoms(string term, int offset = 0, int? limit = null)
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
