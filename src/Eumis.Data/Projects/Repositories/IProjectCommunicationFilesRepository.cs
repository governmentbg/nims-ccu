using Eumis.Domain.Projects;

namespace Eumis.Data.Projects.Repositories
{
    public interface IProjectCommunicationFilesRepository : IAggregateRepository<ProjectCommunicationFile>
    {
        ProjectCommunicationFile FindByProjectCommunicationAnswerId(int communicationAnswerId);
    }
}
