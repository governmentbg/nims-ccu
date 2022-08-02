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
using System.Linq;
using System.Web.Http;

namespace Eumis.Web.Api.Projects.Controllers
{
    [RoutePrefix("api/projects/{projectId:int}/managingAuthorityCommunications/{communicationId:int}/answers")]
    public class ProjectProjectManagingAuthorityCommunicationAnswersController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IProjectManagingAuthorityCommunicationsRepository projectManagingAuthorityCommunicationsRepository;
        private IProjectManagingAuthorityCommunicationService projectManagingAuthorityCommunicationService;
        private IRelationsRepository relationsRepository;

        public ProjectProjectManagingAuthorityCommunicationAnswersController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IProjectManagingAuthorityCommunicationsRepository projectManagingAuthorityCommunicationsRepository,
            IProjectManagingAuthorityCommunicationService projectManagingAuthorityCommunicationService,
            IRelationsRepository relationsRepository)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.projectManagingAuthorityCommunicationsRepository = projectManagingAuthorityCommunicationsRepository;
            this.projectManagingAuthorityCommunicationService = projectManagingAuthorityCommunicationService;
            this.relationsRepository = relationsRepository;
        }

        [Route("")]
        public IList<ProjectCommunicationAnswerVO> GetProjectManagingAuthorityCommunicationAnswers(int projectId, int communicationId)
        {
            this.authorizer.AssertCanDo(ProjectManagingAuthorityCommunicationActions.View, communicationId);
            this.relationsRepository.AssertProjectHasManagingAuthorityCommunication(projectId, communicationId);

            return this.projectManagingAuthorityCommunicationsRepository.GetProjectManagingAuthorityCommunicationAnswers(communicationId);
        }

        [Route("{answerId:int}")]
        public ProjectManagingAuthorityCommunicationAnswerDO GetProjectManagingAuthorityCommunicationAnswer(int projectId, int communicationId, int answerId)
        {
            this.authorizer.AssertCanDo(ProjectManagingAuthorityCommunicationActions.View, communicationId);
            this.relationsRepository.AssertProjectHasManagingAuthorityCommunication(projectId, communicationId);

            var communication = this.projectManagingAuthorityCommunicationsRepository.Find(communicationId);

            var answer = communication.FindAnswer(answerId);

            return new ProjectManagingAuthorityCommunicationAnswerDO(answer, communication);
        }

        [HttpPost]
        [Route("canCreate")]
        public object CanCreateProjectManagingAuthorityCommunicationAnswer(int projectId, int communicationId)
        {
            this.authorizer.AssertCanDo(ProjectManagingAuthorityCommunicationActions.Edit, communicationId);
            this.relationsRepository.AssertProjectHasManagingAuthorityCommunication(projectId, communicationId);

            var errorList = this.projectManagingAuthorityCommunicationService.CanCreateProjectCommunicationAnswer(communicationId);

            return new ErrorsDO(errorList);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Projects.Edit.ManagingAuthorityCommunications.Answers.Create), IdParam = "projectId", ChildIdParam = "communicationId")]
        public object CreateProjectManagingAuthorityCommunicationAnswer(int projectId, int communicationId, string version)
        {
            this.authorizer.AssertCanDo(ProjectManagingAuthorityCommunicationActions.Edit, communicationId);
            this.relationsRepository.AssertProjectHasManagingAuthorityCommunication(projectId, communicationId);

            byte[] vers = System.Convert.FromBase64String(version);

            var projectCommunication = this.projectManagingAuthorityCommunicationsRepository.FindForUpdate(communicationId, vers);

            var errorList = this.projectManagingAuthorityCommunicationService.CanCreateProjectCommunicationAnswer(communicationId);

            if (errorList.Count > 0)
            {
                throw new InvalidOperationException("Cannot create answer");
            }

            this.projectManagingAuthorityCommunicationService.AssertIsFromBeneficiary(projectCommunication);

            var answer = this.projectManagingAuthorityCommunicationService.CreateProjectCommunicationAnswer(
                projectCommunication,
                projectCommunication.ProjectId,
                projectCommunication.GetNextAnswerOrderNum());

            projectCommunication.Answers.Add(answer);
            this.unitOfWork.Save();

            return new { ProjectCommunicationAnswerId = answer.ProjectCommunicationAnswerId };
        }

        [HttpDelete]
        [Route("{answerId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Projects.Edit.ManagingAuthorityCommunications.Answers.Delete), IdParam = "projectId", ChildIdParam = "answerId")]
        public void DeleteProjectManagingAuthorityCommunicationAnswer(int projectId, int communicationId, int answerId, string version)
        {
            this.authorizer.AssertCanDo(ProjectManagingAuthorityCommunicationActions.Delete, communicationId);
            this.relationsRepository.AssertProjectHasManagingAuthorityCommunication(projectId, communicationId);

            byte[] vers = System.Convert.FromBase64String(version);

            ProjectManagingAuthorityCommunication communication = this.projectManagingAuthorityCommunicationsRepository.FindForUpdate(communicationId, vers);

            var answer = communication.FindAnswer(answerId);
            this.projectManagingAuthorityCommunicationService.AssertIsManagingAuthorityAnswer(answer);

            communication.DeleteAnswer(answer.ProjectCommunicationAnswerId);
            this.unitOfWork.Save();
        }
    }
}
