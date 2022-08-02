using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Projects;
using System.Collections.Generic;

namespace Eumis.Data.NonAggregates.Repositories.Noms
{
    public interface IProjectDossierDocumentTypeEnumNomsRepository : IEnumNomsRepository<ProjectDossierDocumentType>
    {
        IList<EnumNomVO<ProjectDossierDocumentType>> GetNoms(ProjectDossierDocumentType[] ids, string term);
    }
}
