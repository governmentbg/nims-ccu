using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Projects;

namespace Eumis.Data.NonAggregates.Repositories.Noms
{
    public interface IProjectTypeNomsRepository : IEntityNomsRepository<ProjectType, EntityNomVO>
    {
        EntityNomVO GetNomByAlias(string alias);
    }
}
