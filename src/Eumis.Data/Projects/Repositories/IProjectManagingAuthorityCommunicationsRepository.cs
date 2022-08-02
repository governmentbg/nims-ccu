using System;
using System.Collections.Generic;
using Eumis.Data.Projects.PortalViewObjects;
using Eumis.Data.Projects.ViewObjects;
using Eumis.Domain.Projects;

namespace Eumis.Data.Projects.Repositories
{
    public interface IProjectManagingAuthorityCommunicationsRepository : IAggregateRepository<ProjectManagingAuthorityCommunication>
    {
        ProjectCommunicationPVO GetProjectCommunications(Guid registeredGid, int offset, int? limit);

        IList<ProjectManagingAuthorityCommunicationVO> GetProjectManagingAuthorityCommunications(int projectId);

        IList<ProjectManagingAuthorityCommunicationVO> GetAllCommunications(
            int[] programmeIds,
            int? programmeId,
            int? programmePriorityId,
            int? procedureId,
            DateTime? fromDate,
            DateTime? toDate,
            ProjectManagingAuthorityCommunicationSource? source);

        int GetProjectPrimaryProgrammeId(int projectCommunicationId);

        int GetNextOrderNumber(int projectId);

        ProjectManagingAuthorityCommunication Find(Guid communicationGid);

        ProjectManagingAuthorityCommunication FindForUpdate(int registrationId, Guid communicationGid, byte[] version);

        ProjectManagingAuthorityCommunication FindForUpdate(Guid communicationGid, byte[] version);

        ProjectCommunicationSentPVO GetSentCommunicationInfo(Guid communicationGid);

        bool ProjectHasExistingCommunications(Guid registeredGid);

        bool RegistrationHasNewCommunications(int registrationId);

        IList<ProjectCommunicationAnswerVO> GetProjectManagingAuthorityCommunicationAnswers(int communicationId);

        ProjectCommunicationSentPVO GetSentAnswerInfo(Guid answerGid);
    }
}
