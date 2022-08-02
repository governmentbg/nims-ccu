using System;
using System.Collections.Generic;
using Eumis.Domain.Projects;

namespace Eumis.ApplicationServices.Services.ProjectManagingAuthorityCommunication
{
    public interface IProjectManagingAuthorityCommunicationService
    {
        #region ProjectManagingAuthorityCommunication

        IList<string> CanCreate(int projectId);

        bool CanActivateQuestion(Guid communicationGid);

        bool CanDelete(int communicationId);

        Domain.Projects.ProjectManagingAuthorityCommunication CreateProjectCommunication(int projectId, ProjectManagingAuthorityCommunicationSource source);

        void AssertIsFromBeneficiary(Domain.Projects.ProjectManagingAuthorityCommunication projectCommunication);

        void AssertIsFromManagingAuthority(Domain.Projects.ProjectManagingAuthorityCommunication projectCommunication);

        #endregion

        #region ProjectCommunicationAnswer

        IList<string> CanCreateProjectCommunicationAnswer(int communicationId);

        Domain.Projects.ProjectCommunicationAnswer CreateProjectCommunicationAnswer(
            Domain.Projects.ProjectManagingAuthorityCommunication communication,
            int projectId,
            int orderNum);

        void AssertIsBeneficiaryAnswer(Domain.Projects.ProjectCommunicationAnswer answer);

        void AssertIsManagingAuthorityAnswer(Domain.Projects.ProjectCommunicationAnswer answer);

        void AssertProjectCommunicationAnswerPreconditions(int projectCommunicationId);

        #endregion

        #region ProjectMassManagingAuthorityCommunicationRecipients

        IList<int> ParseRecipientsExcelFile(Guid blobKey, out IList<string> errors);

        #endregion ProjectMassManagingAuthorityCommunicationRecipients

        #region ProjectMassManagingAuthorityCommunication

        void SendProjectMassManagingAuthorityCommunication(int projectMassManagingAuthorityCommunicationId, byte[] version);

        #endregion ProjectMassManagingAuthorityCommunication
    }
}
