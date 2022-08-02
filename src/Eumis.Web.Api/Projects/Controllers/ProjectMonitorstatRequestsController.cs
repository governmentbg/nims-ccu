using Eumis.ApplicationServices.Services.Monitorstat;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.Core.Relations;
using Eumis.Data.EvalSessions.Repositories;
using Eumis.Data.Projects.Repositories;
using Eumis.Data.Projects.ViewObjects;
using Eumis.Domain.Projects;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.Core;
using Eumis.Web.Api.Projects.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Eumis.Web.Api.Projects.Controllers
{
    [RoutePrefix("api/projects/{projectId}/monitorstatRequests")]
    public class ProjectMonitorstatRequestsController : ApiController
    {
        private IAuthorizer authorizer;
        private IProjectsRepository projectsRepository;
        private IProjectVersionXmlsRepository projectVersionXmlsRepository;
        private IUnitOfWork unitOfWork;
        private IRelationsRepository relationsRepository;
        private IEvalSessionsRepository evalSessionsRepository;
        private IMonitorstatService monitorstatService;
        private IAccessContext accessContext;

        public ProjectMonitorstatRequestsController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IProjectsRepository projectsRepository,
            IRelationsRepository relationsRepository,
            IEvalSessionsRepository evalSessionsRepository,
            IMonitorstatService monitorstatService,
            IProjectVersionXmlsRepository projectVersionXmlsRepository,
            IAccessContext accessContext)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.projectsRepository = projectsRepository;
            this.relationsRepository = relationsRepository;
            this.monitorstatService = monitorstatService;
            this.projectVersionXmlsRepository = projectVersionXmlsRepository;
            this.evalSessionsRepository = evalSessionsRepository;
            this.accessContext = accessContext;
        }

        [Route("")]
        public IList<ProjectMonitorstatRequestsVO> GetProjectMonitorstatRequests(int projectId)
        {
            this.authorizer.AssertCanDo(ProjectActions.View, projectId);

            return this.projectsRepository.GetMonitorstatRequests(projectId);
        }

        [Route("~/api/evalSessionSheets/{sheetId:int}/monitorstatRequests")]
        public IList<ProjectMonitorstatRequestsVO> GetProjectMonitorstatRequestsForSheet(int sheetId)
        {
            var sheetData = this.evalSessionsRepository.GetSheetData(sheetId);

            if (sheetData.Status == Domain.EvalSessions.EvalSessionSheetStatus.Canceled)
            {
                throw new InvalidOperationException("Cannot view sheet project when sheet is 'Canceled'");
            }

            this.authorizer.AssertCanDo(ProjectActions.View, sheetData.ProjectId);

            return this.projectsRepository.GetMonitorstatRequests(sheetData.ProjectId);
        }

        [Route("~/api/evalSessionStandpoints/{standpointId:int}/monitorstatRequests")]
        public IList<ProjectMonitorstatRequestsVO> GetProjectMonitorstatRequestsForStandpoint(int standpointId)
        {
            var standpointData = this.evalSessionsRepository.GetStandpointData(standpointId);

            if (standpointData.Status == Domain.EvalSessions.EvalSessionStandpointStatus.Canceled)
            {
                throw new InvalidOperationException("Cannot view standpoint project when standpoint is 'Canceled'");
            }

            this.authorizer.AssertCanDo(ProjectActions.View, standpointData.ProjectId);

            return this.projectsRepository.GetMonitorstatRequests(standpointData.ProjectId);
        }

        [Route("~/api/evalSessions/{evalSessionId:int}/monitorstatRequests")]
        public IList<EvalSessionProjectMonitorstatRequestsVO> GetProjectMonitorstatRequestsForEvalSession(
            int evalSessionId,
            int? projectId = null,
            DateTime? dateFrom = null,
            DateTime? dateTo = null)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.ViewSessionData, evalSessionId);

            return this.projectsRepository.GetMonitorstatRequestsForEvalSession(evalSessionId, projectId, dateFrom, dateTo);
        }

        [HttpGet]
        [Route("new")]
        public ProjectMonitorstatRequestDO NewProjectMonitorstatRequest(int projectId)
        {
            this.authorizer.AssertCanDo(ProjectActions.View, projectId);
            var project = this.projectsRepository.FindWithoutIncludes(projectId);
            var actualProjectVersion = this.projectVersionXmlsRepository.GetActualProjectVersion(projectId);

            return new ProjectMonitorstatRequestDO(
                projectId,
                actualProjectVersion.ProjectVersionXmlId,
                project.Version,
                project.CompanyUin,
                project.CompanyUinType);
        }

        [HttpGet]
        [Route("newMonitorstatMassRequest")]
        public ProjectMonitorstatMassRequestDO NewProjectMonitorstatMassRequest(int projectId)
        {
            this.authorizer.AssertCanDo(ProjectActions.View, projectId);

            var project = this.projectsRepository.FindWithoutIncludes(projectId);
            var actualProjectVersion = this.projectVersionXmlsRepository.GetActualProjectVersion(projectId);

            return new ProjectMonitorstatMassRequestDO(
                project.ProjectId,
                project.Version,
                actualProjectVersion.ProjectVersionXmlId,
                actualProjectVersion.GetCompanies());
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Projects.MonitorstatRequests.Create), IdParam = "projectId")]
        public object CreateProjectMonitorstatRequest(int projectId, [FromBody] ProjectMonitorstatRequestDO request)
        {
            this.authorizer.AssertCanDo(ProjectActions.Edit, projectId);

            var projectMonitorstatRequest = this.monitorstatService.CreateProjectMonitorstatRequest(
                projectId,
                request.Version,
                request.ProcedureMonitorstatRequestId,
                request.ProjectVersionXmlId,
                request.ProgrammeDeclarationId,
                request.ProjectVersionXmlFileId);

            return new { projectMonitorstatRequest.ProjectMonitorstatRequestId };
        }

        [Route("{projectMonitorstatRequestId:int}")]
        public ProjectMonitorstatRequestDO GetProjectMonitorstatRequest(int projectMonitorstatRequestId, int projectId)
        {
            this.authorizer.AssertCanDo(ProjectActions.View, projectId);
            var project = this.projectsRepository.Find(projectId);

            var projectRequest = project
                .MonitorstatRequests
                .Where(x => x.ProjectMonitorstatRequestId == projectMonitorstatRequestId)
                .Single();

            var projectRequestResponses = this.projectsRepository.GetMonitorstatResponses(projectRequest.ProjectMonitorstatRequestId);

            var projectVersionXmlFileBlobKey = projectRequest.ProjectVersionXmlFileId.HasValue ?
                this.projectsRepository.GetProjectVersionXmlFile(projectId, projectRequest.ProjectVersionXmlFileId.Value).BlobKey : (Guid?)null;

            return new ProjectMonitorstatRequestDO(projectRequest, project.Version, projectRequestResponses, projectVersionXmlFileBlobKey);
        }

        [HttpPut]
        [Route("{projectMonitorstatRequestId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Projects.MonitorstatRequests.Edit), IdParam = "projectId", ChildIdParam = "projectMonitorstatRequestId")]
        public void UpdateProjectMonitorstatDocuments(int projectId, int projectMonitorstatRequestId, [FromBody] ProjectMonitorstatRequestDO request)
        {
            this.authorizer.AssertCanDo(ProjectActions.Edit, projectId);

            this.monitorstatService.UpdateProjectMonitorstatRequest(
                projectId,
                request.Version,
                projectMonitorstatRequestId,
                request.ProcedureMonitorstatRequestId,
                request.ProjectVersionXmlFileId,
                request.ProgrammeDeclarationId);
        }

        [HttpDelete]
        [Route("{projectMonitorstatRequestId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Projects.MonitorstatRequests.Delete), IdParam = "projectId", ChildIdParam = "projectMonitorstatRequestId")]
        public void DeleteProjectMonitorstatDocuments(int projectId, string version, int projectMonitorstatRequestId)
        {
            this.authorizer.AssertCanDo(ProjectActions.Edit, projectId);

            byte[] vers = Convert.FromBase64String(version);

            var project = this.projectsRepository.FindForUpdate(projectId, vers);

            project.RemoveMonitorstatRequest(projectMonitorstatRequestId);

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{projectMonitorstatRequestId:int}/canSendRequest")]
        public ErrorsDO CanSendMonitorstatRequest(int projectId, int projectMonitorstatRequestId)
        {
            this.authorizer.AssertCanDo(ProjectActions.Edit, projectId);

            IList<string> errors = this.monitorstatService.CanSendProjectMonitorstatRequest(projectId, projectMonitorstatRequestId);

            return new ErrorsDO(errors);
        }

        [HttpPost]
        [Route("{projectMonitorstatRequestId:int}/sendRequest")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Projects.MonitorstatRequests.Send), IdParam = "projectId", ChildIdParam = "projectMonitorstatRequestId")]
        public void SendMonitorstatRequest(int projectId, int projectMonitorstatRequestId, string version)
        {
            this.authorizer.AssertCanDo(ProjectActions.Edit, projectId);

            byte[] vers = Convert.FromBase64String(version);
            var userId = this.accessContext.UserId;

            this.monitorstatService.SendProjectMonitorstatRequest(projectId, projectMonitorstatRequestId, vers, userId);

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("canSendMassRequest")]
        public ErrorsDO CanSendMonitorstatMassRequest(int projectId, IList<ProjectMonitorstatRequestCompanyDO> chosenCompanies)
        {
            this.authorizer.AssertCanDo(ProjectActions.Edit, projectId);

            IList<string> errors = new List<string>();

            foreach (var company in chosenCompanies)
            {
                if (company.ProcedureMonitorstatRequestId == null)
                {
                    errors.Add(string.Format(WebApiTexts.ProjectMonitorstatMassRequests_CanSendErrors_Inquiry, company.CompanyName, company.Uin));
                }

                if (company.ProjectVersionXmlFileId == null && company.ProgrammeDeclarationId == null)
                {
                    errors.Add(string.Format(WebApiTexts.ProjectMonitorstatMassRequests_CanSendErrors_Declaration, company.CompanyName, company.Uin));
                }
            }

            return new ErrorsDO(errors);
        }

        [HttpPost]
        [Route("sendMassRequest")]
        [ActionLog(Action = typeof(ActionLogGroups.Projects.MonitorstatRequests.Send), IdParam = "projectId")]
        public void SendMonitorstatMassRequest(int projectId, string version, int projectVersionXmlId, IList<ProjectMonitorstatRequestCompanyDO> chosenCompanies)
        {
            this.authorizer.AssertCanDo(ProjectActions.Edit, projectId);

            var requests = new List<ProjectMonitorstatRequest>();
            var project = this.projectsRepository.FindForUpdate(projectId, Convert.FromBase64String(version));

            foreach (var company in chosenCompanies)
            {
                var request = new ProjectMonitorstatRequest(
                    company.ProcedureMonitorstatRequestId.Value,
                    projectVersionXmlId,
                    company.ProjectVersionXmlFileId,
                    null,
                    company.ProgrammeDeclarationId,
                    company.Uin,
                    company.UinType);

                project.MonitorstatRequests.Add(request);
                requests.Add(request);
            }

            this.unitOfWork.Save();

            byte[] vers = Convert.FromBase64String(version);
            var userId = this.accessContext.UserId;

            foreach (var request in requests)
            {
                this.monitorstatService.SendProjectMonitorstatRequest(projectId, request.ProjectMonitorstatRequestId, vers, userId);
            }

            this.unitOfWork.Save();
        }
    }
}
