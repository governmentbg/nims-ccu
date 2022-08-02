using Eumis.ApplicationServices.Services.ProjectVersionXml;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Db;
using Eumis.Data.Projects.Repositories;
using Eumis.Domain.Projects;
using Eumis.Log.ActionLogger;
using Eumis.Log.ActionLogger.Enums;
using Eumis.PortalIntegration.Api.Documents.ProjectVersions.DataObjects;
using System;
using System.Linq;
using System.Web.Http;

namespace Eumis.PortalIntegration.Api.Documents.ProjectVersions.Controllers
{
    [RoutePrefix("api/projects")]
    public class ProjectVersionsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IActionLogger actionLogger;
        private IAuthorizer authorizer;
        private IProjectsRepository projectsRepository;
        private IProjectVersionXmlsRepository projectVersionXmlsRepository;
        private IProjectVersionXmlService projectVersionXmlService;

        public ProjectVersionsController(
            IUnitOfWork unitOfWork,
            IActionLogger actionLogger,
            IAuthorizer authorizer,
            IProjectsRepository projectsRepository,
            IProjectVersionXmlsRepository projectVersionXmlsRepository,
            IProjectVersionXmlService projectVersionXmlService)
        {
            this.unitOfWork = unitOfWork;
            this.actionLogger = actionLogger;
            this.authorizer = authorizer;
            this.projectsRepository = projectsRepository;
            this.projectVersionXmlsRepository = projectVersionXmlsRepository;
            this.projectVersionXmlService = projectVersionXmlService;
        }

        [Route("{projectXmlGid:guid}")]
        public ProjectXmlDO GetProjectVersion(Guid projectXmlGid)
        {
            var versionId = this.projectVersionXmlsRepository.GetProjectVersionId(projectXmlGid);

            var projectVersion = this.projectVersionXmlsRepository.Find(projectXmlGid);

            this.authorizer.AssertCanDoAny(
                Tuple.Create<Enum, int?>(ProjectVersionActions.View, versionId),
                Tuple.Create<Enum, int?>(ProjectDossierActions.View, projectVersion.ProjectId));

            var project = this.projectsRepository.Find(projectVersion.ProjectId);

            return new ProjectXmlDO
            {
                Xml = projectVersion.Xml,
                CreateDate = projectVersion.CreateDate,
                Version = projectVersion.Version,
                RegData = new ProjectRegDataDO
                {
                    RegNumber = project.RegNumber,
                    RegDate = project.RegDate,
                },
            };
        }

        [HttpPut]
        [Transaction]
        [PessimisticLock]
        [Route("{projectXmlGid:guid}")]
        public ProjectXmlDO UpdateProjectXml(Guid projectXmlGid, ProjectXmlDO projectXmlDO)
        {
            var versionId = this.projectVersionXmlsRepository.GetProjectVersionId(projectXmlGid);
            this.authorizer.AssertCanDo(ProjectVersionActions.Edit, versionId);

            var projectVersion = this.projectVersionXmlsRepository.FindForUpdate(projectXmlGid, projectXmlDO.Version);

            projectVersion.SetXml(projectXmlDO.Xml);

            this.unitOfWork.Save();

            var response = new ProjectXmlDO
            {
                ModifyDate = projectVersion.ModifyDate,
                Version = projectVersion.Version,
            };

            this.actionLogger.LogAction(
                typeof(ActionLogGroups.EvalSessions.Edit.Projects.Versions.UpdateXml),
                projectVersion.ProjectId,
                projectVersion.ProjectVersionXmlId,
                null,
                null);

            return response;
        }

        [HttpPost]
        [Transaction]
        [PessimisticLock]
        [Route("{projectXmlGid:guid}/activate")]
        public void ActivateProjectVersion(Guid projectXmlGid, ProjectXmlDO projectXmlDO)
        {
            var versionId = this.projectVersionXmlsRepository.GetProjectVersionId(projectXmlGid);
            this.authorizer.AssertCanDo(ProjectVersionActions.Edit, versionId);

            ProjectVersionXml projectVersion = this.projectVersionXmlsRepository.FindForUpdate(projectXmlGid, projectXmlDO.Version);

            projectVersion.SetXml(projectXmlDO.Xml);
            projectVersion.ActivateProjectVersion();
            this.unitOfWork.Save();

            var versionsToArchive = this.projectVersionXmlsRepository.GetNonArchivedProjectVersions(projectVersion.ProjectId)
                .Where(p => p.ProjectVersionXmlId != projectVersion.ProjectVersionXmlId);

            foreach (var projVersion in versionsToArchive)
            {
                projVersion.ArchiveProjectVersion();
            }

            this.unitOfWork.Save();

            this.actionLogger.LogAction(
                typeof(ActionLogGroups.EvalSessions.Edit.Projects.Versions.Activate),
                projectVersion.ProjectId,
                projectVersion.ProjectVersionXmlId,
                null,
                null);
        }

        [Route("{projectXmlGid:guid}/getFilesZip")]
        public object GetFilesZip(Guid projectXmlGid)
        {
            var projectVersionId = this.projectVersionXmlsRepository.GetProjectVersionId(projectXmlGid);
            this.authorizer.AssertCanDo(ProjectVersionActions.View, projectVersionId);

            var zipFile = this.projectVersionXmlService.GetProjectAttachedDocumentsZip(projectVersionId);

            return new
            {
                zipFile = zipFile,
            };
        }
    }
}
