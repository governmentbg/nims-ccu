using System;
using System.Collections.Generic;

namespace Eumis.ApplicationServices.Services.ProjectCommunication
{
    public interface IProjectCommunicationService
    {
        IList<string> CanCreate(int projectId, int evalSessionId);

        IList<string> CanRegisterEvalSessionProjectAnswer(int communicationId, int answerId, string regAnswerHash, DateTime regDate);

        bool CanDelete(int communicationId);

        bool CanCancel(int communicationId);

        bool IsCommunicationEvalSessionStatusActive(int communicationId);

        bool CanUpdateQuestion(Guid communicationGid);

        bool CanActivateQuestion(Guid communicationGid);

        Domain.Projects.ProjectCommunication CreateCommunication(int projectId, int evalSessionId);

        byte[] GetProjectCommunicationAttachedDocumentsZip(int communicationId, Guid answerGid, bool isQuestion);

        byte[] GetCommunicationAttachedDocumentsZip(int communicationId);

        byte[] GetCommunicationAnswerAttachedDocumentsZip(int communicationId, Guid answerGid);

        void DeleteProjectCommunicationAnswerMessageFiles(int projectCommunicationAnswerId);

        void ApplyAnsweredProjectCommunication(int projectId);
    }
}
