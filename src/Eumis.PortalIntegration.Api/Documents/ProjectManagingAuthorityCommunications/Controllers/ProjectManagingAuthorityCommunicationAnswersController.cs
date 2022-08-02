using Eumis.ApplicationServices.Communicators;
using Eumis.ApplicationServices.Services.ProjectCommunication;
using Eumis.ApplicationServices.Services.ProjectManagingAuthorityCommunication;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Db;
using Eumis.Data.Counters;
using Eumis.Data.Projects.Repositories;
using Eumis.Domain;
using Eumis.Domain.Projects;
using Eumis.Log.ActionLogger;
using Eumis.Log.ActionLogger.Enums;
using Eumis.PortalIntegration.Api.Core;
using Eumis.PortalIntegration.Api.Documents.ProjectCommunications.DataObjects;
using Eumis.PortalIntegration.Api.Portal.Registrations.DataObjects;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Eumis.PortalIntegration.Api.Documents.ProjectManagingAuthorityCommunications.Controllers
{
    [RoutePrefix("api/projectManagingAuthorityCommunicationAnswers")]
    public class ProjectManagingAuthorityCommunicationAnswersController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IActionLogger actionLogger;
        private IAuthorizer authorizer;
        private IProjectManagingAuthorityCommunicationsRepository projectManagingAuthorityCommunicationsRepository;
        private ICountersRepository countersRepository;
        private IProjectsRepository projectsRepository;
        private IProjectManagingAuthorityCommunicationService projectManagingAuthorityCommunicationService;
        private IDocumentRestApiCommunicator documentRestApiCommunicator;

        public ProjectManagingAuthorityCommunicationAnswersController(
            IUnitOfWork unitOfWork,
            IActionLogger actionLogger,
            IAuthorizer authorizer,
            IProjectManagingAuthorityCommunicationsRepository projectManagingAuthorityCommunicationsRepository,
            ICountersRepository countersRepository,
            IProjectsRepository projectsRepository,
            IProjectManagingAuthorityCommunicationService projectManagingAuthorityCommunicationService,
            IDocumentRestApiCommunicator documentRestApiCommunicator)
        {
            this.unitOfWork = unitOfWork;
            this.actionLogger = actionLogger;
            this.authorizer = authorizer;
            this.projectManagingAuthorityCommunicationsRepository = projectManagingAuthorityCommunicationsRepository;
            this.countersRepository = countersRepository;
            this.projectsRepository = projectsRepository;
            this.projectManagingAuthorityCommunicationService = projectManagingAuthorityCommunicationService;
            this.documentRestApiCommunicator = documentRestApiCommunicator;
        }

        [Route("{answerGid:guid}")]
        public ProjectCommunicationXmlDO GetProjectCommunicationAnswer(Guid communicationGid, Guid answerGid)
        {
            var communication = this.projectManagingAuthorityCommunicationsRepository.Find(communicationGid);

            this.authorizer.AssertCanDo(ProjectManagingAuthorityCommunicationActions.View, communication.ProjectCommunicationId);

            var answer = communication.Answers.Where(a => a.Gid == answerGid).SingleOrDefault();

            if (answer == null)
            {
                throw new DomainObjectNotFoundException($"Cannot find ProjectCommunicaionAnswer with Gid {answerGid} attached to ProjectCommunication with id {communication.ProjectCommunicationId}");
            }

            if (!answer.ReadDate.HasValue && answer.Source == ProjectCommunicationAnswerSource.Beneficiary)
            {
                answer.SetReadDate();
                this.unitOfWork.Save();
            }

            return new ProjectCommunicationXmlDO
            {
                Xml = answer.Answer.Xml,
                Version = communication.Version,
            };
        }

        [HttpPut]
        [Transaction]
        [PessimisticLock]
        [Route("{answerGid:guid}")]
        public XmlDO UpdateProjectCommunicationAnswer(Guid communicationGid, Guid answerGid, XmlDO messageDO)
        {
            var projectCommunication = this.projectManagingAuthorityCommunicationsRepository.FindForUpdate(communicationGid, messageDO.Version);

            this.authorizer.AssertCanDo(ProjectManagingAuthorityCommunicationActions.Edit, projectCommunication.ProjectCommunicationId);

            this.projectManagingAuthorityCommunicationService.AssertProjectCommunicationAnswerPreconditions(projectCommunication.ProjectCommunicationId);

            this.AssertCommunicationPreconditions(projectCommunication);

            var answer = projectCommunication.FindAnswer(answerGid);

            this.projectManagingAuthorityCommunicationService.AssertIsManagingAuthorityAnswer(answer);

            answer.SetAnswerXml(messageDO.Xml);

            projectCommunication.UpdateFiles(ProjectCommunicationMessageType.Answer, answer);
            projectCommunication.ModifyDate = DateTime.Now;

            this.unitOfWork.Save();

            var response = new XmlDO
            {
                Gid = answer.Gid,
                Xml = answer.Answer.Xml,
                ModifyDate = projectCommunication.ModifyDate,
                Version = projectCommunication.Version,
            };

            this.actionLogger.LogAction(
                typeof(ActionLogGroups.Projects.Edit.ManagingAuthorityCommunications.Answers.Update),
                projectCommunication.ProjectCommunicationId,
                answer.ProjectCommunicationAnswerId,
                null,
                null);

            return response;
        }

        [HttpPost]
        [Transaction]
        [PessimisticLock]
        [Route("{answerGid:guid}/activate")]
        public void ActivateProjectCommunicationAnswer(Guid communicationGid, Guid answerGid)
        {
            var projectCommunication = this.projectManagingAuthorityCommunicationsRepository.Find(communicationGid);

            this.authorizer.AssertCanDo(ProjectManagingAuthorityCommunicationActions.Edit, projectCommunication.ProjectCommunicationId);

            this.AssertCommunicationPreconditions(projectCommunication);

            var answer = projectCommunication.FindAnswer(answerGid);

            this.projectManagingAuthorityCommunicationService.AssertIsManagingAuthorityAnswer(answer);

            if (projectCommunication.HasActiveAnswer())
            {
                var currentAnswer = projectCommunication.Answers.Where(a => a.Status == ProjectCommunicationAnswerStatus.Answer).Single();
                currentAnswer.Status = ProjectCommunicationAnswerStatus.Canceled;
            }

            answer.MakeAnswer();
            this.unitOfWork.Save();

            this.actionLogger.LogAction(
                typeof(ActionLogGroups.Projects.Edit.ManagingAuthorityCommunications.Answers.ChangeStatusToAnswer),
                projectCommunication.ProjectCommunicationId,
                answer.ProjectCommunicationAnswerId,
                null,
                null);
        }

        private void AssertCommunicationPreconditions(ProjectManagingAuthorityCommunication communication)
        {
            if (communication.ManagingAuthorityCommunicationStatus == ProjectManagingAuthorityCommunicationStatus.Canceled)
            {
                throw new HttpResponseException(
                    this.Request.CreateResponse(
                        HttpStatusCode.BadRequest,
                        new { error = PortalIntegrationErrors.MessageCanceled }));
            }

            if (communication.ManagingAuthorityCommunicationStatus == ProjectManagingAuthorityCommunicationStatus.Expired ||
                DateTime.Now > communication.QuestionEndingDate)
            {
                throw new HttpResponseException(
                    this.Request.CreateResponse(
                        HttpStatusCode.BadRequest,
                        new { error = PortalIntegrationErrors.MessageTimedOut }));
            }
        }
    }
}
