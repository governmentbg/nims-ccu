using Eumis.ApplicationServices.Services.ProjectManagingAuthorityCommunication;
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
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Eumis.PortalIntegration.Api.Documents.ProjectManagingAuthorityCommunications.Controllers
{
    [RoutePrefix("api/projectManagingAuthorityCommunications")]
    public class ProjectManagingAuthorityCommunicationsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IActionLogger actionLogger;
        private IAuthorizer authorizer;
        private IProjectManagingAuthorityCommunicationsRepository projectManagingAuthorityCommunicationsRepository;
        private ICountersRepository countersRepository;
        private IProjectManagingAuthorityCommunicationService projectManagingAuthorityCommunicationService;

        public ProjectManagingAuthorityCommunicationsController(
            IUnitOfWork unitOfWork,
            IActionLogger actionLogger,
            IAuthorizer authorizer,
            IProjectManagingAuthorityCommunicationsRepository projectManagingAuthorityCommunicationsRepository,
            ICountersRepository countersRepository,
            IProjectManagingAuthorityCommunicationService projectManagingAuthorityCommunicationService)
        {
            this.unitOfWork = unitOfWork;
            this.actionLogger = actionLogger;
            this.authorizer = authorizer;
            this.projectManagingAuthorityCommunicationsRepository = projectManagingAuthorityCommunicationsRepository;
            this.countersRepository = countersRepository;
            this.projectManagingAuthorityCommunicationService = projectManagingAuthorityCommunicationService;
        }

        [Route("{communicationGid:guid}")]
        public ProjectCommunicationXmlDO GetProjectManagingAuthorityCommunication(Guid communicationGid)
        {
            var communication = this.projectManagingAuthorityCommunicationsRepository.Find(communicationGid);

            this.authorizer.AssertCanDo(ProjectManagingAuthorityCommunicationActions.View, communication.ProjectCommunicationId);

            if (!communication.QuestionReadDate.HasValue && communication.Source == ProjectManagingAuthorityCommunicationSource.Beneficiary)
            {
                communication.SetReadDate();
                this.unitOfWork.Save();
            }

            return new ProjectCommunicationXmlDO
            {
                Xml = communication.Question.Xml,
                Version = communication.Version,
            };
        }

        [HttpPut]
        [Transaction]
        [PessimisticLock]
        [Route("{communicationGid:guid}")]
        public ProjectCommunicationXmlDO UpdateProjectManagingAuthorityCommunication(Guid communicationGid, ProjectCommunicationXmlDO projectMessage)
        {
            var communication = this.projectManagingAuthorityCommunicationsRepository.Find(communicationGid);

            this.authorizer.AssertCanDo(ProjectManagingAuthorityCommunicationActions.Edit, communication.ProjectCommunicationId);

            this.projectManagingAuthorityCommunicationService.AssertIsFromManagingAuthority(communication);
            this.AssertCommunicationPreconditions(communication);

            communication.UpdateAttributes(projectMessage.Subject);

            communication.SetQuestionXml(projectMessage.Xml);

            this.unitOfWork.Save();

            var response = new ProjectCommunicationXmlDO
            {
                ModifyDate = communication.ModifyDate,
                Version = communication.Version,
            };

            this.actionLogger.LogAction(
                typeof(ActionLogGroups.Projects.Edit.ManagingAuthorityCommunications.QuestionUpdated),
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
        public void ActivateProjectManagingAuthorityCommunication(Guid communicationGid, XmlDO projectMessage)
        {
            var communication = this.projectManagingAuthorityCommunicationsRepository.Find(communicationGid);

            this.authorizer.AssertCanDo(ProjectManagingAuthorityCommunicationActions.Edit, communication.ProjectCommunicationId);

            this.projectManagingAuthorityCommunicationService.AssertIsFromManagingAuthority(communication);
            this.AssertCommunicationPreconditions(communication);

            if (!this.projectManagingAuthorityCommunicationService.CanActivateQuestion(communicationGid))
            {
                throw new InvalidOperationException("Cannot activate ProjectManagingAuthorityCommunication.");
            }

            this.countersRepository.CreateProjectManagingAuthorityCommunicationCounter(communication.ProjectId);

            communication.SetQuestionXml(projectMessage.Xml);

            var regNumber = this.countersRepository.GetNextProjectManagingAuthorityCommunicationNumber(communication.ProjectId);
            communication.MakeQuestion(regNumber);

            this.unitOfWork.Save();

            this.actionLogger.LogAction(
                typeof(ActionLogGroups.Projects.Edit.ManagingAuthorityCommunications.ChangeStatusToQuestion),
                communication.ProjectId,
                communication.ProjectCommunicationId,
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
