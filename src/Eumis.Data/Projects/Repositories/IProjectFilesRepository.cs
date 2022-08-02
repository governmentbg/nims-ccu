using Eumis.Domain.Projects;
using System.Collections.Generic;

namespace Eumis.Data.Projects.Repositories
{
    public interface IProjectFilesRepository : IAggregateRepository<ProjectFile>
    {
        ProjectFile FindByProjectVersionXmlId(int projectVersionXmlId);

        List<byte[]> GetFirstProjectFileSignatures(int projectId);
    }
}
