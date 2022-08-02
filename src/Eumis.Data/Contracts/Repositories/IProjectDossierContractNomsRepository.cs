using Eumis.Data.Contracts.ViewObjects;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Contracts;
using System.Collections.Generic;

namespace Eumis.Data.Contracts.Repositories
{
    public interface IProjectDossierContractNomsRepository : IEntityNomsRepository<Contract, ProjectDossierContractNomVO>
    {
        IEnumerable<ProjectDossierContractNomVO> GetNoms(string projectNumber, string term, int offset = 0, int? limit = null, int[] programmeIds = null);
    }
}