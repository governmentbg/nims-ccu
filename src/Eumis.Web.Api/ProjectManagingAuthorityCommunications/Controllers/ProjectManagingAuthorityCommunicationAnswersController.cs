using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Data.Projects.Repositories;
using Eumis.Data.Projects.ViewObjects;
using Eumis.Web.Api.Projects.DataObjects;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.ProjectManagingAuthorityCommunications.Controllers
{
    [RoutePrefix("api/projectManagingAuthorityCommunications/{communicationId:int}/answers")]

    public class ProjectManagingAuthorityCommunicationAnswersController : ApiController
    {
        private IAuthorizer authorizer;
        private IProjectManagingAuthorityCommunicationsRepository projectManagingAuthorityCommunicationsRepository;

        public ProjectManagingAuthorityCommunicationAnswersController(
            IAuthorizer authorizer,
            IProjectManagingAuthorityCommunicationsRepository projectManagingAuthorityCommunicationsRepository)
        {
            this.authorizer = authorizer;
            this.projectManagingAuthorityCommunicationsRepository = projectManagingAuthorityCommunicationsRepository;
        }

        [Route("")]
        public IList<ProjectCommunicationAnswerVO> GetProjectManagingAuthorityCommunicationAnswers(int communicationId)
        {
            this.authorizer.AssertCanDo(ProjectManagingAuthorityCommunicationActions.View, communicationId);

            return this.projectManagingAuthorityCommunicationsRepository.GetProjectManagingAuthorityCommunicationAnswers(communicationId);
        }

        [Route("{answerId:int}")]
        public ProjectManagingAuthorityCommunicationAnswerDO GetProjectManagingAuthorityCommunicationAnswer(int communicationId, int answerId)
        {
            this.authorizer.AssertCanDo(ProjectManagingAuthorityCommunicationActions.View, communicationId);

            var communication = this.projectManagingAuthorityCommunicationsRepository.Find(communicationId);
            var answer = communication.FindAnswer(answerId);

            return new ProjectManagingAuthorityCommunicationAnswerDO(answer, communication);
        }
    }
}
