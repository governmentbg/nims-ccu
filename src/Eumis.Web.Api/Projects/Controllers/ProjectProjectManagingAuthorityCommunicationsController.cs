using Eumis.ApplicationServices.Services.ProjectManagingAuthorityCommunication;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Db;
using Eumis.Data.Core.Relations;
using Eumis.Data.Projects.Repositories;
using Eumis.Data.Projects.ViewObjects;
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
    [RoutePrefix("api/projects/{projectId:int}/managingAuthorityCommunications")]
    public class ProjectProjectManagingAuthorityCommunicationsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IProjectsRepository projectsRepository;
        private IProjectManagingAuthorityCommunicationsRepository projectManagingAuthorityCommunicationsRepository;
        private IRelationsRepository relationsRepository;
        private IProjectManagingAuthorityCommunicationService projectManagingAuthorityCommunicationService;

        public ProjectProjectManagingAuthorityCommunicationsController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IProjectsRepository projectsRepository,
            IProjectManagingAuthorityCommunicationsRepository projectManagingAuthorityCommunicationsRepository,
            IRelationsRepository relationsRepository,
            IProjectManagingAuthorityCommunicationService projectManagingAuthorityCommunicationService)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.projectsRepository = projectsRepository;
            this.projectManagingAuthorityCommunicationsRepository = projectManagingAuthorityCommunicationsRepository;
            this.relationsRepository = relationsRepository;
            this.projectManagingAuthorityCommunicationService = projectManagingAuthorityCommunicationService;
        }

        [Route("")]
        public IList<ProjectManagingAuthorityCommunicationVO> GetProjectManagingAuthorityCommunications(int projectId)
        {
            this.authorizer.AssertCanDo(ProjectManagingAuthorityCommunicationListActions.Search);

            return this.projectManagingAuthorityCommunicationsRepository.GetProjectManagingAuthorityCommunications(projectId);
        }

        [Route("{communicationId:int}")]
        public ProjectManagingAuthorityCommunicationDO GetProjectCommunication(int projectId, int communicationId)
        {
            this.authorizer.AssertCanDo(ProjectManagingAuthorityCommunicationActions.View, communicationId);
            this.relationsRepository.AssertProjectHasManagingAuthorityCommunication(projectId, communicationId);

            var project = this.projectsRepository.FindWithoutIncludes(projectId);

            var communication = this.projectManagingAuthorityCommunicationsRepository.Find(communicationId);

            return new ProjectManagingAuthorityCommunicationDO(communication, project.RegNumber, project.CompanyName);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Projects.Edit.ManagingAuthorityCommunications.Create), IdParam = "projectId")]
        public object CreateProjectManagingAuthorityCommunication(int projectId)
        {
            this.authorizer.AssertCanDo(ProjectManagingAuthorityCommunicationListActions.Create);

            if (this.projectManagingAuthorityCommunicationService.CanCreate(projectId).Count > 0)
            {
                throw new InvalidOperationException("Cannot create communication.");
            }

            var newCommunication = this.projectManagingAuthorityCommunicationService.CreateProjectCommunication(projectId, ProjectManagingAuthorityCommunicationSource.ManagingAuthority);

            this.projectManagingAuthorityCommunicationsRepository.Add(newCommunication);

            this.unitOfWork.Save();

            return new { ProjectCommunicationId = newCommunication.ProjectCommunicationId };
        }

        [HttpPost]
        [Route("canCreate")]
        public ErrorsDO CanCreateProjectManagingAuthorityCommunication(int projectId)
        {
            this.authorizer.AssertCanDo(ProjectManagingAuthorityCommunicationListActions.Create);

            var errorList = this.projectManagingAuthorityCommunicationService.CanCreate(projectId);

            return new ErrorsDO(errorList);
        }

        [HttpPut]
        [Route("{communicationId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Projects.Edit.ManagingAuthorityCommunications.Edit), IdParam = "projectId", ChildIdParam = "communicationId")]
        public void UpdateProjectManagingAuthorityCommunication(int projectId, int communicationId, ProjectManagingAuthorityCommunicationDO projectCommunication)
        {
            this.authorizer.AssertCanDo(ProjectManagingAuthorityCommunicationActions.Edit, communicationId);
            this.relationsRepository.AssertProjectHasManagingAuthorityCommunication(projectId, communicationId);

            ProjectManagingAuthorityCommunication communication = this.projectManagingAuthorityCommunicationsRepository.FindForUpdate(communicationId, projectCommunication.Version);

            communication.UpdateAttributes(projectCommunication.QuestionEndingDate);

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{communicationId:int}/cancel")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Projects.Edit.ManagingAuthorityCommunications.Cancel), IdParam = "projectId", ChildIdParam = "communicationId")]
        public void CancelProjectManagingAuthorityCommunication(int projectId, int communicationId, string version, ConfirmDO confirm)
        {
            this.authorizer.AssertCanDo(ProjectManagingAuthorityCommunicationActions.Cancel, communicationId);
            this.relationsRepository.AssertProjectHasManagingAuthorityCommunication(projectId, communicationId);

            byte[] vers = System.Convert.FromBase64String(version);
            var communication = this.projectManagingAuthorityCommunicationsRepository.FindForUpdate(communicationId, vers);

            this.projectManagingAuthorityCommunicationService.AssertIsFromManagingAuthority(communication);
            communication.MakeCancelled(confirm.Note);

            this.unitOfWork.Save();
        }

        [HttpDelete]
        [Route("{communicationId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Projects.Edit.ManagingAuthorityCommunications.Delete), IdParam = "projectId", ChildIdParam = "communicationId")]
        public void DeleteProjectManagingAuthorityCommunication(int projectId, int communicationId, string version)
        {
            this.authorizer.AssertCanDo(ProjectManagingAuthorityCommunicationActions.Delete, communicationId);
            this.relationsRepository.AssertProjectHasManagingAuthorityCommunication(projectId, communicationId);

            if (!this.projectManagingAuthorityCommunicationService.CanDelete(communicationId))
            {
                throw new InvalidOperationException("Cannot delete communication.");
            }

            byte[] vers = System.Convert.FromBase64String(version);

            ProjectManagingAuthorityCommunication communication = this.projectManagingAuthorityCommunicationsRepository.FindForUpdate(communicationId, vers);

            this.projectManagingAuthorityCommunicationsRepository.Remove(communication);

            this.unitOfWork.Save();
        }
    }
}
