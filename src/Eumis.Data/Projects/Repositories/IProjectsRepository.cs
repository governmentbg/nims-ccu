using System;
using System.Collections.Generic;
using Eumis.Data.Projects.ViewObjects;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.Procedures;
using Eumis.Domain.Projects;

namespace Eumis.Data.Projects.Repositories
{
    public interface IProjectsRepository : IAggregateRepository<Project>
    {
        Project FindByRegNumber(string regNumber);

        IList<Project> FindAll(int[] projectIds);

        IList<ProjectRegistrationsVO> GetProjectRegistrations(
            int[] programmeIds,
            int? programmePriorityId,
            int? procedureId,
            DateTime? fromDate,
            DateTime? toDate,
            string projectNumber);

        int GetProcedureId(int projectId);

        int GetPrimaryProgrammeId(int projectId);

        ProjectRegistrationDataVO GetProjectRegistrationData(int projectId);

        bool HasAssociatedRegistration(int projectId);

        bool IsProjectNumExisting(string projectNum, int? procedureId);

        IList<string> CanWithdrawProject(int projectId);

        ProjectRegistrationStatus GetProjectRegistrationStatus(int projectId);

        ApplicationFormType GetProcedureApplicationFormType(int projectId);

        IList<Project> GetProjectsForSession(int evalSessionId);

        IList<Project> GetActiveProjectsForSession(int evalSessionId);

        IList<ProjectDossierDocumentVO> GetProjectDossierDocuments(
            int projectId,
            int? contractId,
            ProjectDossierDocumentType[] docTypes,
            string objDescription,
            string fileDescription);

        IList<ProjectRegistrationsVO> GetProjectRegistrationsForProjectDossier(
            int[] programmeIds,
            int? procedureId,
            string projectNumber);

        IList<string> IsProjectValidForProjectDossier(int projectId);

        bool IsProjectInFinishedEvalSession(int projectId);

        ProjectVersionXmlFile GetProjectVersionXmlFile(int projectId, int projectVersionXmlFileId);

        List<Project> GetProjectsByUin(int procedureId, string uin, UinType uinType);

        IList<ProjectMonitorstatRequestsVO> GetMonitorstatRequests(int projectId);

        IList<ProjectMonitorstatRequest> GetMonitorstatRequests(int procedureId, string uin);

        ProjectMonitorstatRequest GetMonitorstatRequest(Guid subjectRequest);

        IList<ProjectMonitorstatResponse> GetMonitorstatResponses(int projectMonitorstatRequestId);

        int? GetProjectId(string projectRegNumber);

        IList<EvalSessionProjectMonitorstatRequestsVO> GetMonitorstatRequestsForEvalSession(
            int evalSessionId,
            int? projectId,
            DateTime? dateFrom,
            DateTime? dateTo);

        IList<ProjectDirectionVO> GetProjectDirections(int[] projectIds);
    }
}
