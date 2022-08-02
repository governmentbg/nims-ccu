using System;
using System.Collections.Generic;
using System.Linq;
using Eumis.Common.Db;
using Eumis.Data.Contracts.ViewObjects;
using Eumis.Data.Core;
using Eumis.Data.Linq;
using Eumis.Domain.Contracts;
using Eumis.Domain.Projects;

namespace Eumis.Data.Contracts.Repositories
{
    internal class ProjectDossierContractNomsRepository : Repository, IProjectDossierContractNomsRepository
    {
        public ProjectDossierContractNomsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public ProjectDossierContractNomVO GetNom(int nomValueId)
        {
            return (from c in this.unitOfWork.DbContext.Set<Contract>()
                    join p in this.unitOfWork.DbContext.Set<Project>() on c.ProjectId equals p.ProjectId
                    where c.ContractId == nomValueId
                    select new ProjectDossierContractNomVO
                    {
                        NomValueId = c.ContractId,
                        Name = c.RegNumber,
                        ProjectNum = p.RegNumber,
                    }).SingleOrDefault();
        }

        public IEnumerable<ProjectDossierContractNomVO> GetNoms(string term, int offset = 0, int? limit = null)
        {
            throw new NotSupportedException();
        }

        public IEnumerable<ProjectDossierContractNomVO> GetNoms(string projectNumber, string term, int offset = 0, int? limit = null, int[] programmeIds = null)
        {
            var predicate = PredicateBuilder.True<Contract>()
                .AndStringContains(c => c.RegNumber, term);

            if (programmeIds != null)
            {
                predicate = predicate.And(c => programmeIds.Contains(c.ProgrammeId));
            }

            var projPredicate = PredicateBuilder.True<Project>()
                .AndStringMatches(p => p.RegNumber, projectNumber, true);

            return (from c in this.unitOfWork.DbContext.Set<Contract>().Where(predicate)
                    join p in this.unitOfWork.DbContext.Set<Project>().Where(projPredicate) on c.ProjectId equals p.ProjectId
                    orderby c.CreateDate descending
                    select new ProjectDossierContractNomVO
                    {
                        NomValueId = c.ContractId,
                        Name = c.RegNumber,
                        ProjectNum = p.RegNumber,
                    }).WithOffsetAndLimit(offset, limit)
                      .ToList();
        }
    }
}
