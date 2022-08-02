using System;
using System.Collections.Generic;
using System.Linq;
using Eumis.Data.Core;
using Eumis.Data.Projects.PortalViewObjects;
using Eumis.Data.Projects.ViewObjects;
using Eumis.Domain.Projects;

namespace Eumis.Data.Projects.Repositories
{
    public interface IProjectCommunicationsRepository : IAggregateRepository<ProjectCommunication>
    {
        IList<CommunicationVO> GetProjectCommunications(int projectId, bool sentOnly = false);

        IList<EvalSessionCommunicationVO> GetProjectCommunicationsForEvalSession(
            int evalSessionId,
            int? projectId = null,
            ProjectCommunicationStatus? statusId = null,
            DateTime? questionDateFrom = null,
            DateTime? questionDateTo = null);

        CommunicationRegVO GetCommunicationRegData(int projectCommunicationId);

        ProjectCommunication Find(Guid gid);

        ProjectCommunication Find(int registrationId, Guid gid);

        ProjectCommunication FindForUpdate(Guid gid, byte[] version);

        ProjectCommunication FindForUpdate(int registrationId, Guid gid, byte[] version);

        bool HasProjectCommunicationInProgress(int evalSessionId, int? projectId = null);

        PagePVO<RegMessagePVO> GetAllForRegistration(
            int registrationId,
            int offset = 0,
            int? limit = null);

        RegMessageCountPVO GetCountForRegistration(int registrationId);

        int GetNextOrderNumber(int projectId);

        int GetProjectId(int communicationId);

        int GetCommunicationId(Guid gid);

        IEnumerable<ProjectCommunication> GetProjectCommunicationsToExpire();

        IList<ProjectCommunicationAnswerVO> GetProjectCommunicationAnswers(int communicationId);

        ProjectCommunicationAnswer FindAnswer(int registrationId, string answerHash);

        ProjectCommunication GetActiveProjectCommunication(int projectId);

        IQueryable<ProjectQuestionExpirationEmailVO> GetCurrentExpiringQuestions();
    }
}
