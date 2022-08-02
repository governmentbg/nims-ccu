using Eumis.Data.Projects.ViewObjects;
using Eumis.Domain.Projects;
using System.Collections.Generic;

namespace Eumis.Data.Projects.Repositories
{
    public interface IProjectMassManagingAuthorityCommunicationsRepository : IAggregateRepository<ProjectMassManagingAuthorityCommunication>
    {
        IList<ProjectMassManagingAuthorityCommunicationVO> GetProjectMassManagingAuthorityCommunications(int[] programmeIds);

        ProjectMassManagingAuthorityCommunicationInfoVO GetInfo(int communicationId);

        void DeleteProjectMassManagingAuthorityCommunication(int communicationId, byte[] vers);

        IList<ProjectMassManagingAuthorityCommunicationDocumentVO> GetCommunicationDocuments(int communicationId);

        IList<ProjectMassManagingAuthorityCommunicationRecipientVO> GetAttachedProjects(int communicationId);

        IList<ProjectMassManagingAuthorityCommunicationRecipientVO> GetUnattachedProjects(int communicationId, int procedureId);

        int GetPrimaryProcedureShareProgrammeId(int communicationId);

        int GetNextOrderNum(int programmeId);
    }
}
