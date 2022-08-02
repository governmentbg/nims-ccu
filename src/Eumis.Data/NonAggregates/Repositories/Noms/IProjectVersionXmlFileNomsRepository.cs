using System.Collections.Generic;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.Procedures;
using Eumis.Domain.Projects;

namespace Eumis.Data.NonAggregates.Repositories.Noms
{
    public interface IProjectVersionXmlFileNomsRepository : IEntityNomsRepository<ProjectVersionXmlFile, EntityNomVO>
    {
        IList<EntityNomVO> GetNomsForProjectVersion(
            int projectVersionXmlId,
            string term,
            int offset = 0,
            int? limit = null);
    }
}
