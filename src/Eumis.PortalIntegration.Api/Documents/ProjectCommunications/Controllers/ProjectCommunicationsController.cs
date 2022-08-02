using Eumis.ApplicationServices.Communicators;
using Eumis.ApplicationServices.Services.ProjectCommunication;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Db;
using Eumis.Data.Counters;
using Eumis.Data.Projects.Repositories;
using Eumis.Domain.Projects;
using Eumis.Log.ActionLogger;
using Eumis.Log.ActionLogger.Enums;
using Eumis.PortalIntegration.Api.Core;
using Eumis.PortalIntegration.Api.Documents.ProjectCommunications.DataObjects;
using System;
using System.Web.Http;

namespace Eumis.PortalIntegration.Api.Documents.ProjectCommunications.Controllers
{
    [RoutePrefix("api/projectMessages")]
    public class ProjectCommunicationsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IActionLogger actionLogger;
        private IAuthorizer authorizer;
        private IProjectCommunicationsRepository projectCommunicationsRepository;
        private ICountersRepository countersRepository;
        private IProjectsRepository projectsRepository;
        private IProjectCommunicationService projectCommunicationService;
        private IDocumentRestApiCommunicator documentRestApiCommunicator;

        public ProjectCommunicationsController(
            IUnitOfWork unitOfWork,
            IActionLogger actionLogger,
            IAuthorizer authorizer,
            IProjectCommunicationsRepository projectCommunicationsRepository,
            ICountersRepository countersRepository,
            IProjectsRepository projectsRepository,
            IProjectCommunicationService projectCommunicationService,
            IDocumentRestApiCommunicator documentRestApiCommunicator)
        {
            this.unitOfWork = unitOfWork;
            this.actionLogger = actionLogger;
            this.authorizer = authorizer;
            this.projectCommunicationsRepository = projectCommunicationsRepository;
            this.countersRepository = countersRepository;
            this.projectsRepository = projectsRepository;
            this.projectCommunicationService = projectCommunicationService;
            this.documentRestApiCommunicator = documentRestApiCommunicator;
        }

        [Route("{communicationGid:guid}")]
        public ProjectCommunicationXmlDO GetProjectCommunication(Guid communicationGid)
        {
            var communicationId = this.projectCommunicationsRepository.GetCommunicationId(communicationGid);
            var communication = this.projectCommunicationsRepository.Find(communicationGid);

            var project = this.projectsRepository.FindWithoutIncludes(communication.ProjectId);

            this.authorizer.AssertCanDoAny(
                Tuple.Create<Enum, int?>(ProjectCommunicationActions.View, communicationId),
                Tuple.Create<Enum, int?>(ProjectDossierActions.View, communication.ProjectId));

            return new ProjectCommunicationXmlDO
            {
                Xml = communication.Question.Xml,
                Version = communication.Version,
                MessageEndingDate = communication.QuestionEndingDate,
                RegData = new ProjectRegDataDO
                {
                    RegNumber = project.RegNumber,
                    RegDate = project.RegDate,
                },
            };
        }

        [Route("{communicationGid:guid}/answers/{answerGid:guid}")]
        public ProjectCommunicationXmlDO GetProjectCommunicationAnswer(Guid communicationGid, Guid answerGid)
        {
            var communicationId = this.projectCommunicationsRepository.GetCommunicationId(communicationGid);
            var communication = this.projectCommunicationsRepository.Find(communicationGid);

            this.authorizer.AssertCanDoAny(
                Tuple.Create<Enum, int?>(ProjectCommunicationActions.View, communicationId),
                Tuple.Create<Enum, int?>(ProjectDossierActions.View, communication.ProjectId));

            var answer = communication.FindAnswer(answerGid);

            if (!answer.ReadDate.HasValue && answer.Source == ProjectCommunicationAnswerSource.Beneficiary)
            {
                answer.SetReadDate();
                this.unitOfWork.Save();
            }

            var project = this.projectsRepository.FindWithoutIncludes(communication.ProjectId);

            return new ProjectCommunicationXmlDO
            {
                Xml = answer.Answer.Xml,
                Version = communication.Version,
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
        [Route("{communicationGid:guid}")]
        public XmlDO UpdateQuestion(Guid communicationGid, ProjectCommunicationXmlDO projectMessage)
        {
            var communicationId = this.projectCommunicationsRepository.GetCommunicationId(communicationGid);
            this.authorizer.AssertCanDo(ProjectCommunicationActions.Edit, communicationId);

            if (!this.projectCommunicationService.CanUpdateQuestion(communicationGid))
            {
                throw new InvalidOperationException("Cannot update question.");
            }

            var communication = this.projectCommunicationsRepository.FindForUpdate(communicationGid, projectMessage.Version);

            communication.SetQuestionXml(projectMessage.Xml);

            communication.SetEndingDate(projectMessage.MessageEndingDate);

            this.unitOfWork.Save();

            var response = new XmlDO
            {
                ModifyDate = communication.ModifyDate,
                Version = communication.Version,
            };

            this.actionLogger.LogAction(
                typeof(ActionLogGroups.EvalSessions.Edit.Projects.Communications.QuestionUpdated),
                communication.ProjectId,
                communication.ProjectCommunicationId,
                null,
                null);

            return response;
        }

        [HttpPost]
        [Transaction]
        [PessimisticLock]
        [Route("{communicationGid:guid}/activate")]
        public void ActivateQuestion(Guid communicationGid, XmlDO projectMessage)
        {
            var communicationId = this.projectCommunicationsRepository.GetCommunicationId(communicationGid);
            this.authorizer.AssertCanDo(ProjectCommunicationActions.Edit, communicationId);

            if (!this.projectCommunicationService.CanActivateQuestion(communicationGid))
            {
                throw new InvalidOperationException("Cannot activate question.");
            }

            var communication = this.projectCommunicationsRepository.FindForUpdate(communicationGid, projectMessage.Version);

            this.countersRepository.CreateProjectCommunicationCounter(communication.ProjectId);

            communication.SetQuestionXml(projectMessage.Xml);

            var regNumber = this.countersRepository.GetNextProjectCommunicationNumber(communication.ProjectId);
            communication.MakeQuestion(regNumber);

            this.unitOfWork.Save();

            this.actionLogger.LogAction(
                typeof(ActionLogGroups.EvalSessions.Edit.Projects.Communications.ChangeStatusToQuestion),
                communication.ProjectId,
                communication.ProjectCommunicationId,
                null,
                null);
        }

        [Route("{xmlGid:guid}/getCommunicationProjectFilesZip")]
        public object GetCommunicationProjectFilesZip(Guid xmlGid, Guid answerGid, ProjectCommunicationType messageType)
        {
            var communicationId = this.projectCommunicationsRepository.GetCommunicationId(xmlGid);
            this.authorizer.AssertCanDo(ProjectCommunicationActions.View, communicationId);

            var isQuestion = messageType == ProjectCommunicationType.Message;

            var zipFile = this.projectCommunicationService.GetProjectCommunicationAttachedDocumentsZip(communicationId, answerGid, isQuestion);

            return new
            {
                zipFile = zipFile,
            };
        }

        [Route("{xmlGid:guid}/getCommunicationFilesZip")]
        public object GetCommunicationFilesZip(Guid xmlGid)
        {
            var communicationId = this.projectCommunicationsRepository.GetCommunicationId(xmlGid);
            this.authorizer.AssertCanDo(ProjectCommunicationActions.View, communicationId);

            var zipFile = this.projectCommunicationService.GetCommunicationAttachedDocumentsZip(communicationId);

            return new
            {
                zipFile = zipFile,
            };
        }

        [Route("{communicationGid:guid}/getCommunicationAnswerFilesZip")]
        public object GetCommunicationAnswerFilesZip(Guid communicationGid, Guid answerGid)
        {
            var communicationId = this.projectCommunicationsRepository.GetCommunicationId(communicationGid);
            this.authorizer.AssertCanDo(ProjectCommunicationActions.View, communicationId);

            var zipFile = this.projectCommunicationService.GetCommunicationAnswerAttachedDocumentsZip(communicationId, answerGid);

            return new
            {
                zipFile = zipFile,
            };
        }
    }
}
