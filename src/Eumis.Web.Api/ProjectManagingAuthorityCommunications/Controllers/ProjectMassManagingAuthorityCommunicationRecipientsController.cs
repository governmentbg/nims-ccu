using Eumis.ApplicationServices.Services.ProjectManagingAuthorityCommunication;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Db;
using Eumis.Data.Projects.Repositories;
using Eumis.Data.Projects.ViewObjects;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.Core;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.ProjectManagingAuthorityCommunications.Controllers
{
    [RoutePrefix("api/projectMassManagingAuthorityCommunications/{communicationId:int}/recipients")]

    public class ProjectMassManagingAuthorityCommunicationRecipientsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IProjectMassManagingAuthorityCommunicationsRepository projectMassManagingAuthorityCommunicationsRepository;
        private IProjectManagingAuthorityCommunicationService projectManagingAuthorityCommunicationService;

        public ProjectMassManagingAuthorityCommunicationRecipientsController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IProjectMassManagingAuthorityCommunicationsRepository projectMassManagingAuthorityCommunicationsRepository,
            IProjectManagingAuthorityCommunicationService projectManagingAuthorityCommunicationService)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.projectMassManagingAuthorityCommunicationsRepository = projectMassManagingAuthorityCommunicationsRepository;
            this.projectManagingAuthorityCommunicationService = projectManagingAuthorityCommunicationService;
        }

        [Route("unattachedProjects")]
        public IList<ProjectMassManagingAuthorityCommunicationRecipientVO> GetUnattachedProjectMassManagingAuthorityCommunicationRecipients(int communicationId)
        {
            this.authorizer.AssertCanDo(ProjectMassManagingAuthorityCommunicationActions.Edit, communicationId);

            var communication = this.projectMassManagingAuthorityCommunicationsRepository.FindWithoutIncludes(communicationId);

            return this.projectMassManagingAuthorityCommunicationsRepository.GetUnattachedProjects(communicationId, communication.ProcedureId);
        }

        [HttpGet]
        [Route("loadRecipientsFromFile")]
        public ProjectsDO LoadRecipientsFromFile(int communicationId, Guid fileKey)
        {
            this.authorizer.AssertCanDo(ProjectMassManagingAuthorityCommunicationActions.Edit, communicationId);

            IList<string> errors;
            var projectIds = this.projectManagingAuthorityCommunicationService.ParseRecipientsExcelFile(fileKey, out errors);

            return new ProjectsDO(projectIds, errors);
        }

        [Route("")]
        public IList<ProjectMassManagingAuthorityCommunicationRecipientVO> GetAttachedProjectMassManagingAuthorityCommunicationRecipients(int communicationId)
        {
            this.authorizer.AssertCanDo(ProjectMassManagingAuthorityCommunicationActions.View, communicationId);

            return this.projectMassManagingAuthorityCommunicationsRepository.GetAttachedProjects(communicationId);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ProjectMassManagingAuthorityCommunication.Edit.Recipients.Create), IdParam = "communicationId")]
        public void AddProjectMassManagingAuthorityCommunicationRecipients(int communicationId, string version, int[] projectIds)
        {
            this.authorizer.AssertCanDo(ProjectMassManagingAuthorityCommunicationActions.Edit, communicationId);

            byte[] vers = Convert.FromBase64String(version);

            var communication = this.projectMassManagingAuthorityCommunicationsRepository.FindForUpdate(communicationId, vers);

            communication.AddRecipients(projectIds);

            this.unitOfWork.Save();
        }

        [HttpDelete]
        [Route("{projectId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ProjectMassManagingAuthorityCommunication.Edit.Recipients.Delete), IdParam = "communicationId", ChildIdParam = "projectId")]
        public void DeleteProjectMassManagingAuthorityCommunicationRecipient(int communicationId, int projectId, string version)
        {
            this.authorizer.AssertCanDo(ProjectMassManagingAuthorityCommunicationActions.Edit, communicationId);

            byte[] vers = Convert.FromBase64String(version);

            var communication = this.projectMassManagingAuthorityCommunicationsRepository.FindForUpdate(communicationId, vers);

            communication.RemoveRecipient(projectId);

            this.unitOfWork.Save();
        }
    }
}
