using System;
using System.Collections.Generic;
using Eumis.Data.EvalSessions.PortalViewObjects;
using Eumis.Data.EvalSessions.ViewObjects;
using Eumis.Data.Projects.ViewObjects;
using Eumis.Domain.EvalSessions;
using Eumis.Domain.Procedures;

namespace Eumis.Data.EvalSessions.Repositories
{
    public interface IEvalSessionsRepository : IAggregateRepository<EvalSession>
    {
        int GetProgrammeId(int evalSessionId);

        bool IsProjectApprovedOrReserveInEvalSession(int projectId);

        bool IsEvalSessionUser(int userId, int evalSessionId, EvalSessionUserType userType);

        bool IsEvalSessionProjectUserAdmin(int userId, int projectId);

        bool IsEvalSessionProjectAssessor(int userId, int projectId);

        bool IsEvalSessionProjectAssistantAssessor(int userId, int projectId);

        bool IsEvalSessionProjectObserver(int userId, int projectId);

        bool IsAssessorAssociatedWithEvalSessionSheet(int evalSessionSheetId, int userId);

        bool IsAssessorAssociatedWithEvalSessionStandpoint(int standpointId, int userId);

        EvalSessionStatus GetEvalSessionStatus(int evalSessionId);

        string GetEvalSessionNumber(int evalSessionId);

        IList<EvalSessionsVO> GetEvalSessions(int userId, int[] programmeIdsCanAdministrate, int[] programmeIdsCanRead, int? procedureId = null);

        IList<EvalSessionUsersVO> GetEvalSessionUsers(int evalSessionId);

        IList<EvalSessionProjectsVO> GetEvalSessionProjects(int evalSessionId);

        IList<EvalSessionSheetsVO> GetEvalSessionSheets(
            int evalSessionId,
            int? projectId = null,
            ProcedureEvalTableType? evalTableType = null,
            int? distributionId = null,
            int? assessorId = null,
            int? userId = null,
            EvalSessionSheetStatus[] statuses = null);

        IList<EvalSessionDistributionsVO> GetEvalSessionDistributions(int evalSessionId);

        IList<EvalSessionStandpointVO> GetEvalSessionStandpoints(
            int? evalSessionId,
            int? projectId = null,
            int? evalSessionUserId = null,
            int? userId = null,
            EvalSessionStandpointStatus[] statuses = null);

        IList<ProjectRegistrationsVO> GetProjectRegistrations(
            int[] programmeIds,
            int evalSessionId,
            DateTime? fromDate,
            DateTime? toDate,
            int? companySizeTypeId,
            int? companyKidCodeId,
            int? projectKidCodeId);

        EvalSession GetWithIncludedProjectStandingsAndEvaluations(int evalSessionId);

        IList<string> CanDeleteEvalSessionProject(int evalSessionId, int projectId);

        IList<string> CanDeleteEvalSessionUser(int evalSessionId, int evalSessionUserId);

        IList<EvalSessionDistributionUserDO> GetEvalSessionAsessors(int evalSessionId);

        IList<EvalSessionDistributionUserDO> GetEvalSessionNotDeactivatedAssessors(int evalSessionId);

        IList<EvalSessionDistributionProjectsVO> GetNewEvalSessionDistributionProjects(int evalSessionId);

        IList<EvalSessionDistributionProjectsVO> GetEvalSessionDistributionProjects(int evalSessionId, int distributionId);

        IList<EvalSessionProjectsVO> GetEvaluativeProjects(int evalSessionId, ProcedureEvalTableType evalTableType);

        IList<EvalSessionEvaluationVO> GetEvalSessionEvaluations(int evalSessionId, int projectId);

        bool CanRearrangeStanding(int evalSessionId, int standingId);

        IList<EvalSessionEvaluationVO> GetProjectEvalSessionEvaluations(int projectId);

        EvalSessionEvaluation GetProjectEvalSessionEvaluation(int projectId, int evaluationId);

        IList<EvalSessionSheetsVO> GetEvalSessionEvaluationSheets(int evalSessionId, int evaluationId, int projectId);

        IList<EvalSessionDocumentsVO> GetEvalSessionDocuments(int evalSessionId);

        IList<string> CanCancelEvalSessionProject(int evalSessionId, int projectId);

        IList<string> CanRestoreEvalSessionProject(int projectId);

        IList<string> CanCancelEvalSessionSheet(int evalSessionId, int sheetId);

        IList<string> CanContinueEvalSessionSheet(int evalSessionId, int sheetId);

        IList<string> CanRefuseEvalSessionDistribution(int evalSessionId, int distributionId);

        IList<EvalSession> GetNonCanceledEvalSessionsByProcedure(int procedureId);

        IList<EvalSessionProjectStandingVO> GetEvalSessionProjectStandings(int evalSessionId, int projectId);

        IList<EvalSessionProjectStandingVO> GetProjectEvalSessionProjectStandings(int projectId);

        EvalSessionProjectStanding GetProjectEvalSessionProjectStanding(int projectId, int projectStandingId);

        bool IsOrderNumUnique(int evalSessionId, bool isPreliminary, int orderNum);

        IList<string> CanDeleteEvalSessionEvaluation(int evaluationId, int projectId);

        IList<EvalSessionEvaluationVO> GetEvalSessionProjectStandingEvaluations(int evalSessionId, int projectStandingId, int projectId);

        IList<EvalSessionStandingsVO> GetEvalSessionStandings(int evalSessionId);

        IList<EvalSessionStandingProjectDO> GetEvalSessionStandingProjects(int evalSessionId, int standingId);

        IList<EvalSessionStandingProjectDO> GetEvalSessionRearrangedStandingProjects(int evalSessionId, int standingId);

        IList<EvalSessionStandingProjectDO> GetNewEvalSessionStandingProjects(int evalSessionId);

        int[] GetProcedurePreviousEvalSessionStandingProjects(int procedureId, int evalSessionId);

        int GetEvalSessionStandpointProjectId(int evalSessionStandpointId);

        EvalSessionSheetData GetSheetData(int evalSessionSheetId);

        EvalSessionStandpointVO GetStandpointData(int evalSessionStandpointId);

        IList<string> CanAddEvalSessionUser(int evalSessionId, int userId, EvalSessionUserType userType);

        IList<EvalSessionProjectsVO> GetProjectEvalSessionProjects(int projectId);

        IList<string> CanChangeStatusToDraft(int evalSessionId);

        IList<string> GetEvalSessionProjectsWithContracts(int standingId);

        EvalSessionResultsVO GetEvalSessionResults(int evalSessionId);

        IList<EvalSessionAdminAdmissProjectsVO> GetEvalSessionResultAdminAdmissProjects(int evalSessionId, int resultId);

        IList<EvalSessionPreliminaryProjectsVO> GetEvalSessionResultPreliminaryProjects(int evalSessionId, int resultId);

        IList<EvalSessionStandingProjectsVO> GetEvalSessionResultStandingProjects(int evalSessionId, int resultId);

        IList<string> EvalSessionHasUnevaluatedProjects(int evalSessionId);

        IList<string> ProcedureHasEvalTable(int evalSessionId, ProcedureEvalTableType type);

        IList<string> GetPublishedProjectEmails(int evalSessionId);

        void AddEvaluatedProjects(int evalSessionId, EvalSessionResult evalSessionResult);

        void AddPreliminaryStandingProjects(int evalSessionId, EvalSessionResult evalSessionResult);

        void AddStandingProjects(int evalSessionId, EvalSessionResult evalSessionResult);

        IList<string> CanEvaluateProject(int evalSessionId, ProcedureEvalTableType evalTableType, int projectId);

        NewEvalSessionStandingType GetEvalSessionStandingType(int evalSessionStandingId);

        EvalSessionActionsVO GetEvalSessionAvailableActions(int procedureId, int evalSessionId);

        bool EvalSessionHasStandingType(int evalSesionId, ProcedureEvalTableType procedureEvalTable);

        IList<ProjectRegistrationsVO> GetProjectsForAutomaticProjectMonitorstatRequests(int evalSessionId);

        IList<int> GetEvalSessionProjectIds(int evalSessionId);

        Procedure GetEvalSessionProcedure(int evalSessionId);
    }
}
