using Eumis.ApplicationServices.Communicators;
using Eumis.ApplicationServices.Services.ProjectManagingAuthorityCommunication;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.Projects.PortalViewObjects;
using Eumis.Data.Projects.Repositories;
using Eumis.Domain.Projects;
using Eumis.Log.ActionLogger;
using Eumis.Log.ActionLogger.Enums;
using Eumis.PortalIntegration.Api.Core;
using Eumis.PortalIntegration.Api.Portal.Registrations.DataObjects;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Eumis.PortalIntegration.Api.Portal.Registrations.Controllers
{
    [RoutePrefix("api/registration/projectCommunicationAnswers")]
    public class RegProjectCommunicationAnswersController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IActionLogger actionLogger;
        private IAccessContext accessContext;
        private IDocumentRestApiCommunicator documentRestApiCommunicator;
        private IProjectsRepository projectsRepository;
        private IProjectVersionXmlsRepository projectVersionXmlsRepository;
        private IProjectManagingAuthorityCommunicationsRepository projectManagingAuthorityCommunicationsRepository;
        private IProjectManagingAuthorityCommunicationService projectManagingAuthorityCommunicationService;

        public RegProjectCommunicationAnswersController(
            IUnitOfWork unitOfWork,
            IActionLogger actionLogger,
            IAccessContext accessContext,
            IDocumentRestApiCommunicator documentRestApiCommunicator,
            IProjectsRepository projectsRepository,
            IProjectVersionXmlsRepository projectVersionXmlsRepository,
            IProjectManagingAuthorityCommunicationsRepository projectManagingAuthorityCommunicationsRepository,
            IProjectManagingAuthorityCommunicationService projectManagingAuthorityCommunicationService)
        {
            this.unitOfWork = unitOfWork;
            this.actionLogger = actionLogger;
            this.accessContext = accessContext;
            this.documentRestApiCommunicator = documentRestApiCommunicator;
            this.projectsRepository = projectsRepository;
            this.projectVersionXmlsRepository = projectVersionXmlsRepository;
            this.projectManagingAuthorityCommunicationsRepository = projectManagingAuthorityCommunicationsRepository;
            this.projectManagingAuthorityCommunicationService = projectManagingAuthorityCommunicationService;
        }

        [Route("{answerGid:guid}")]
        public RegProjectMessageDO GetProjectCommunicationAnswer(Guid communicationGid, Guid answerGid)
        {
            var projectCommunication = this.projectManagingAuthorityCommunicationsRepository.Find(communicationGid);
            var answer = projectCommunication.FindAnswer(answerGid);

            if (!answer.ReadDate.HasValue && answer.Source == ProjectCommunicationAnswerSource.ManagingAuthority)
            {
                answer.SetReadDate();
                this.unitOfWork.Save();
            }

            var projectRegNumber = this.projectsRepository.FindWithoutIncludes(projectCommunication.ProjectId).RegNumber;

            return new RegProjectMessageDO
            {
                Gid = answer.Gid,
                Xml = answer.Answer.Xml,
                Version = projectCommunication.Version,
                ProjectRegNumber = projectRegNumber,
            };
        }

        [HttpPost]
        [Transaction]
        [PessimisticLock]
        [Route("new")]
        public RegProjectMessageDO GetNewProjectCommunicationAnswer(Guid communicationGid)
        {
            var projectCommunication = this.projectManagingAuthorityCommunicationsRepository.Find(communicationGid);

            this.projectManagingAuthorityCommunicationService.AssertProjectCommunicationAnswerPreconditions(projectCommunication.ProjectCommunicationId);
            this.AssertCommunicationPreconditions(projectCommunication);

            if (!projectCommunication.CanCreateAnswer())
            {
                throw new InvalidOperationException("Cannot create new answer.");
            }

            var initialXml = projectCommunication.Answers.Any() ?
                projectCommunication.GetLastAnswerXml() :
                projectCommunication.Question.Xml;

            var answerXml = this.documentRestApiCommunicator.CreateProjectManagingAuthorityCommunicationAnswerXml(initialXml);

            var actualProjectVersion = this.projectVersionXmlsRepository.GetActualProjectVersion(projectCommunication.ProjectId);

            var projectCommunicatonAnswer = new ProjectCommunicationAnswer(
                answerXml,
                actualProjectVersion.ProjectVersionXmlId,
                projectCommunication.GetNextAnswerOrderNum(),
                ProjectCommunicationAnswerSource.Beneficiary);

            projectCommunication.Answers.Add(projectCommunicatonAnswer);
            this.unitOfWork.Save();

            return new RegProjectMessageDO
            {
                Gid = projectCommunication.Gid,
                AnswerGid = projectCommunicatonAnswer.Gid,
                Xml = answerXml,
                Version = projectCommunication.Version,
            };
        }

        [HttpDelete]
        [Transaction]
        [PessimisticLock]
        [Route("{answerGid:guid}")]
        public void DeleteProjectCommunicationAnswer(Guid communicationGid, Guid answerGid, RegProjectMessageDO messageDO)
        {
            var projectCommunication = this.projectManagingAuthorityCommunicationsRepository.FindForUpdate(
                this.accessContext.RegistrationId,
                communicationGid,
                messageDO.Version);

            this.projectManagingAuthorityCommunicationService.AssertProjectCommunicationAnswerPreconditions(projectCommunication.ProjectCommunicationId);
            this.AssertCommunicationPreconditions(projectCommunication);

            var answer = projectCommunication.FindAnswer(answerGid);

            this.projectManagingAuthorityCommunicationService.AssertIsBeneficiaryAnswer(answer);

            projectCommunication.DeleteAnswer(answer.ProjectCommunicationAnswerId);

            this.unitOfWork.Save();

            this.actionLogger.LogAction(
                typeof(ActionLogPortalGroups.ProjectManagingAuthorityCommunication.Answers.Delete),
                projectCommunication.ProjectCommunicationId,
                answer.ProjectCommunicationAnswerId,
                null,
                null);
        }

        [HttpPut]
        [Transaction]
        [PessimisticLock]
        [Route("{answerGid:guid}")]
        public RegProjectMessageDO UpdateProjectCommunicationAnswer(Guid communicationGid, Guid answerGid, RegProjectMessageDO messageDO)
        {
            var projectCommunication = this.projectManagingAuthorityCommunicationsRepository.FindForUpdate(
                this.accessContext.RegistrationId,
                communicationGid,
                messageDO.Version);

            this.projectManagingAuthorityCommunicationService.AssertProjectCommunicationAnswerPreconditions(projectCommunication.ProjectCommunicationId);

            this.AssertCommunicationPreconditions(projectCommunication);

            var answer = projectCommunication.FindAnswer(answerGid);

            this.projectManagingAuthorityCommunicationService.AssertIsBeneficiaryAnswer(answer);

            answer.SetAnswerXml(messageDO.Xml);

            projectCommunication.UpdateFiles(ProjectCommunicationMessageType.Answer, answer);
            projectCommunication.ModifyDate = DateTime.Now;

            this.unitOfWork.Save();

            var response = new RegProjectMessageDO
            {
                Gid = answer.Gid,
                Xml = answer.Answer.Xml,
                ModifyDate = projectCommunication.ModifyDate,
                Version = projectCommunication.Version,
            };

            this.actionLogger.LogAction(
                typeof(ActionLogPortalGroups.ProjectManagingAuthorityCommunication.Answers.Update),
                projectCommunication.ProjectCommunicationId,
                answer.ProjectCommunicationAnswerId,
                null,
                null);

            return response;
        }

        [HttpPost]
        [Transaction]
        [PessimisticLock]
        [Route("{answerGid:guid}/submit")]
        public void SubmitProjectCommunicationAnswer(Guid communicationGid, Guid answerGid)
        {
            var projectCommunication = this.projectManagingAuthorityCommunicationsRepository.Find(communicationGid);

            this.AssertCommunicationPreconditions(projectCommunication);
            this.projectManagingAuthorityCommunicationService.AssertProjectCommunicationAnswerPreconditions(projectCommunication.ProjectCommunicationId);

            var answer = projectCommunication.FindAnswer(answerGid);

            this.projectManagingAuthorityCommunicationService.AssertIsBeneficiaryAnswer(answer);

            if (projectCommunication.HasActiveAnswer())
            {
                var currentAnswer = projectCommunication
                    .Answers
                    .Where(a => a.Status == ProjectCommunicationAnswerStatus.Answer)
                    .Single();

                currentAnswer.Status = ProjectCommunicationAnswerStatus.Canceled;
            }

            answer.MakeAnswer();
            this.unitOfWork.Save();

            this.actionLogger.LogAction(
                typeof(ActionLogPortalGroups.ProjectManagingAuthorityCommunication.Answers.Update),
                projectCommunication.ProjectCommunicationId,
                answer.ProjectCommunicationAnswerId,
                null,
                null);
        }

        [Route("{answerGid:guid}/sentInfo")]
        public ProjectCommunicationSentPVO GetProjectCommunicationSentAnswerInfo(Guid answerGid)
        {
            return this.projectManagingAuthorityCommunicationsRepository.GetSentAnswerInfo(answerGid);
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

            if (communication.ManagingAuthorityCommunicationStatus == ProjectManagingAuthorityCommunicationStatus.Expired || DateTime.Now > communication.QuestionEndingDate)
            {
                throw new HttpResponseException(
                    this.Request.CreateResponse(
                        HttpStatusCode.BadRequest,
                        new { error = PortalIntegrationErrors.MessageTimedOut }));
            }
        }
    }
}
