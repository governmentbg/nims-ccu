using Eumis.ApplicationServices.Services.ProjectVersionXml;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.EvalSessions.Repositories;
using Eumis.Data.Projects.Repositories;
using Eumis.Data.Projects.ViewObjects;
using Eumis.Domain;
using Eumis.Domain.Projects;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.Core;
using Eumis.Web.Api.Projects.DataObjects;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.Projects.Controllers
{
    [RoutePrefix("api/projects/{projectId:int}/versions")]
    public class ProjectVersionsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IAccessContext accessContext;
        private IProjectVersionXmlsRepository projectVersionXmlsRepository;
        private IProjectVersionXmlService projectVersionXmlService;
        private IEvalSessionsRepository evalSessionsRepository;
        private IProjectFilesRepository projectFilesRepository;

        public ProjectVersionsController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IAccessContext accessContext,
            IProjectVersionXmlsRepository projectVersionXmlsRepository,
            IProjectVersionXmlService projectVersionXmlService,
            IEvalSessionsRepository evalSessionsRepository,
            IProjectFilesRepository projectFilesRepository)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.accessContext = accessContext;
            this.projectVersionXmlsRepository = projectVersionXmlsRepository;
            this.projectVersionXmlService = projectVersionXmlService;
            this.evalSessionsRepository = evalSessionsRepository;
            this.projectFilesRepository = projectFilesRepository;
        }

        [Route("")]
        public IList<ProjectVersionVO> GetProjectVersions(int projectId)
        {
            this.authorizer.AssertCanDoAny(
                Tuple.Create<Enum, int?>(ProjectActions.SearchVersions, projectId),
                Tuple.Create<Enum, int?>(ProjectDossierActions.View, projectId));

            return this.projectVersionXmlsRepository.GetProjectVersions(projectId);
        }

        [Route("~/api/evalSessionSheets/{sheetId:int}/projectVersions")]
        public IList<ProjectVersionVO> GetProjectVersionsForSheet(int sheetId)
        {
            var sheetData = this.evalSessionsRepository.GetSheetData(sheetId);

            if (sheetData.Status == Domain.EvalSessions.EvalSessionSheetStatus.Canceled)
            {
                throw new DomainException("Cannot view sheet project when sheet is 'Canceled'");
            }

            this.authorizer.AssertCanDo(ProjectActions.SearchVersions, sheetData.ProjectId);

            return this.projectVersionXmlsRepository.GetProjectVersions(sheetData.ProjectId, true);
        }

        [Route("~/api/evalSessionStandpoints/{standpointId:int}/projectVersions")]
        public IList<ProjectVersionVO> GetProjectVersionsForStandpoint(int standpointId)
        {
            var standpointData = this.evalSessionsRepository.GetStandpointData(standpointId);

            if (standpointData.Status == Domain.EvalSessions.EvalSessionStandpointStatus.Canceled)
            {
                throw new DomainException("Cannot view standpoint project when sheet is 'Canceled'");
            }

            this.authorizer.AssertCanDo(ProjectActions.SearchVersions, standpointData.ProjectId);

            return this.projectVersionXmlsRepository.GetProjectVersions(standpointData.ProjectId, true);
        }

        [Route("{versionId:int}")]
        public ProjectVersionDO GetProjectVersion(int projectId, int versionId)
        {
            this.authorizer.AssertCanDoAny(
                Tuple.Create<Enum, int?>(ProjectVersionActions.View, versionId),
                Tuple.Create<Enum, int?>(ProjectDossierActions.View, projectId));

            var projectVersion = this.projectVersionXmlsRepository.Find(versionId);

            var projectVersionFile = this.projectFilesRepository.FindByProjectVersionXmlId(versionId);

            return new ProjectVersionDO(projectVersion, projectVersionFile);
        }

        [HttpPut]
        [Route("{versionId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.EvalSessions.Edit.Projects.Versions.UpdateNote), IdParam = "projectId", ChildIdParam = "versionId")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public void UpdateProjectVersion(int projectId, int versionId, int evalSessionId, ProjectVersionDO projectVersionDO)
        {
            this.authorizer.AssertCanDo(ProjectVersionActions.Edit, versionId);

            if (!this.projectVersionXmlService.CanUpdateProjectVersionData(versionId, evalSessionId))
            {
                throw new DomainValidationException("Cannot update project data.");
            }

            ProjectVersionXml projectVersion = this.projectVersionXmlsRepository.FindForUpdate(versionId, projectVersionDO.Version);

            projectVersion.SetAttributes(projectVersionDO.CreateNote);

            this.unitOfWork.Save();
        }

        [HttpGet]
        [Route("new")]
        public ProjectVersionDO NewProjectVersion(int projectId)
        {
            this.authorizer.AssertCanDo(ProjectActions.CreateVersion, projectId);

            return new ProjectVersionDO();
        }

        [HttpPost]
        [Route("createFromRegData")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.EvalSessions.Edit.Projects.Versions.CreateFromRegData), IdParam = "projectId")]
        public ProjectVersionDO CreateProjectVersionFromRegData(int projectId, int evalSessionId)
        {
            this.authorizer.AssertCanDo(ProjectActions.CreateVersion, projectId);

            if (!this.projectVersionXmlService.CanCreateProjectVersionFromRegData(projectId, evalSessionId))
            {
                throw new InvalidOperationException("Cannot create project version from reg data.");
            }

            var newProjectVersopn = this.projectVersionXmlService.CreateProjectVersionFromProjectData(projectId, this.accessContext.UserId);

            this.unitOfWork.Save();

            return new ProjectVersionDO(newProjectVersopn);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.EvalSessions.Edit.Projects.Versions.Create), IdParam = "projectId")]
        public ProjectVersionDO CreateProjectVersion(int projectId, int evalSessionId, ProjectVersionDO projectVersion)
        {
            this.authorizer.AssertCanDo(ProjectActions.CreateVersion, projectId);

            if (this.projectVersionXmlService.CanCreateProjectVersion(projectId, evalSessionId).Count != 0)
            {
                throw new InvalidOperationException("Cannot create project version.");
            }

            var newProjectVersion =
                this.projectVersionXmlService.CreateProjectVersion(
                    projectId,
                    this.accessContext.UserId,
                    projectVersion.CreateNote,
                    projectVersion.CreateNoteAlt);

            return new ProjectVersionDO(newProjectVersion);
        }

        [HttpDelete]
        [Route("{versionId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.EvalSessions.Edit.Projects.Versions.Delete), IdParam = "projectId", ChildIdParam = "versionId")]
        public void DeleteProjectVersion(int projectId, int versionId, int evalSessionId, string version)
        {
            this.authorizer.AssertCanDo(ProjectVersionActions.Delete, versionId);

            if (!this.projectVersionXmlService.CanDeleteProjectVersion(versionId, evalSessionId))
            {
                throw new DomainValidationException("Cannot delete project version.");
            }

            byte[] vers = System.Convert.FromBase64String(version);
            ProjectVersionXml projectVersion = this.projectVersionXmlsRepository.FindForUpdate(versionId, vers);

            this.projectVersionXmlsRepository.Remove(projectVersion);
            this.unitOfWork.Save();

            var lastArchived = this.projectVersionXmlsRepository.GetLastArchivedProjectVersion(projectId);
            if (lastArchived != null)
            {
                lastArchived.ActivateProjectVersion();
                this.unitOfWork.Save();
            }
        }

        [HttpPost]
        [Route("canCreate")]
        public ErrorsDO CanCreateProjectVersion(int projectId, int evalSessionId)
        {
            this.authorizer.AssertCanDo(ProjectActions.CreateVersion, projectId);

            var errorList = this.projectVersionXmlService.CanCreateProjectVersion(projectId, evalSessionId);

            return new ErrorsDO(errorList);
        }
    }
}
