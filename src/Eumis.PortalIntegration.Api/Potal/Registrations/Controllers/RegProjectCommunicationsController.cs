using Eumis.ApplicationServices.Services.ProjectManagingAuthorityCommunication;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.Counters;
using Eumis.Data.Projects.PortalViewObjects;
using Eumis.Data.Projects.Repositories;
using Eumis.Data.Registrations.Repositories;
using Eumis.Domain.Projects;
using Eumis.Log.ActionLogger;
using Eumis.Log.ActionLogger.Enums;
using Eumis.PortalIntegration.Api.Core;
using Eumis.PortalIntegration.Api.Portal.Registrations.DataObjects;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Eumis.PortalIntegration.Api.Portal.Registrations.Controllers
{
    [RoutePrefix("api/registration/projectCommunications")]
    public class RegProjectCommunicationsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IActionLogger actionLogger;
        private IAccessContext accessContext;
        private IProjectsRepository projectsRepository;
        private IRegProjectXmlsRepository regProjectXmlsRepository;
        private ICountersRepository countersRepository;
        private IProjectManagingAuthorityCommunicationsRepository projectManagingAuthorityCommunicationsRepository;
        private IProjectManagingAuthorityCommunicationService projectManagingAuthorityCommunicationService;

        public RegProjectCommunicationsController(
            IUnitOfWork unitOfWork,
            IActionLogger actionLogger,
            IAccessContext accessContext,
            IProjectsRepository projectsRepository,
            IRegProjectXmlsRepository regProjectXmlsRepository,
            ICountersRepository countersRepository,
            IProjectManagingAuthorityCommunicationsRepository projectManagingAuthorityCommunicationsRepository,
            IProjectManagingAuthorityCommunicationService projectManagingAuthorityCommunicationService)
        {
            this.unitOfWork = unitOfWork;
            this.actionLogger = actionLogger;
            this.accessContext = accessContext;
            this.projectsRepository = projectsRepository;
            this.regProjectXmlsRepository = regProjectXmlsRepository;
            this.countersRepository = countersRepository;
            this.projectManagingAuthorityCommunicationsRepository = projectManagingAuthorityCommunicationsRepository;
            this.projectManagingAuthorityCommunicationService = projectManagingAuthorityCommunicationService;
        }

        [Route("getAll")]
        public ProjectCommunicationPVO GetAll(Guid registeredGid, int offset = 0, int? limit = null)
        {
            return this.projectManagingAuthorityCommunicationsRepository.GetProjectCommunications(registeredGid, offset, limit);
        }

        [HttpPost]
        [Transaction]
        [PessimisticLock]
        [Route("new")]
        public RegProjectMessageDO CreateNewProjectCommunication(Guid registeredGid)
        {
            var projectId = this.regProjectXmlsRepository.GetProjectId(registeredGid);

            if (!projectId.HasValue)
            {
                throw new InvalidOperationException("Cannot create communication for not registered project.");
            }

            if (this.projectManagingAuthorityCommunicationService.CanCreate(projectId.Value).Count > 0)
            {
                throw new InvalidOperationException("Cannot create communication.");
            }

            var projectCommunication = this.projectManagingAuthorityCommunicationService.CreateProjectCommunication(projectId.Value, ProjectManagingAuthorityCommunicationSource.Beneficiary);

            this.projectManagingAuthorityCommunicationsRepository.Add(projectCommunication);

            this.unitOfWork.Save();

            return new RegProjectMessageDO
            {
                Gid = projectCommunication.Gid,
                Xml = projectCommunication.Question.Xml,
                Version = projectCommunication.Version,
            };
        }

        [Route("{communicationGid:guid}")]
        public RegProjectMessageDO GetProjectCommunication(Guid communicationGid)
        {
            var projectCommunication = this.projectManagingAuthorityCommunicationsRepository.Find(communicationGid);

            if (!projectCommunication.QuestionReadDate.HasValue && projectCommunication.Source == ProjectManagingAuthorityCommunicationSource.ManagingAuthority)
            {
                projectCommunication.SetReadDate();
                this.unitOfWork.Save();
            }

            var projectRegNumber = this.projectsRepository.FindWithoutIncludes(projectCommunication.ProjectId).RegNumber;

            return new RegProjectMessageDO
            {
                Gid = projectCommunication.Gid,
                Xml = projectCommunication.Question.Xml,
                Version = projectCommunication.Version,
                ProjectRegNumber = projectRegNumber,
            };
        }

        [HttpDelete]
        [Transaction]
        [PessimisticLock]
        [Route("{communicationGid:guid}")]
        public void DeleteProjectCommunication(Guid communicationGid)
        {
            var projectCommunication = this.projectManagingAuthorityCommunicationsRepository.Find(communicationGid);

            if (!this.projectManagingAuthorityCommunicationService.CanDelete(projectCommunication.ProjectCommunicationId))
            {
                throw new InvalidOperationException("Cannot delete ProjectCommunication");
            }

            this.AssertCommunicationPreconditions(projectCommunication);

            this.projectManagingAuthorityCommunicationService.AssertIsFromBeneficiary(projectCommunication);

            this.projectManagingAuthorityCommunicationsRepository.Remove(projectCommunication);

            this.unitOfWork.Save();

            this.actionLogger.LogAction(
                typeof(ActionLogPortalGroups.ProjectManagingAuthorityCommunication.Questions.Delete),
                projectCommunication.ProjectCommunicationId,
                null,
                null,
                null);
        }

        [HttpPost]
        [Transaction]
        [PessimisticLock]
        [Route("{communicationGid:guid}/cancel")]
        public void CancelProjectCommunication(Guid communicationGid, RegProjectMessageDO messageDO)
        {
            var projectCommunication = this.projectManagingAuthorityCommunicationsRepository.FindForUpdate(
                this.accessContext.RegistrationId,
                communicationGid,
                messageDO.Version);

            this.projectManagingAuthorityCommunicationService.AssertIsFromBeneficiary(projectCommunication);

            projectCommunication.MakeCancelled();
            this.unitOfWork.Save();
        }

        [HttpPut]
        [Transaction]
        [PessimisticLock]
        [Route("{communicationGid:guid}")]
        public RegProjectMessageDO UpdateProjectCommunication(Guid communicationGid, RegProjectMessageDO messageDO)
        {
            var projectCommunication = this.projectManagingAuthorityCommunicationsRepository.FindForUpdate(
                this.accessContext.RegistrationId,
                communicationGid,
                messageDO.Version);

            projectCommunication.AssertIsDraft();

            this.projectManagingAuthorityCommunicationService.AssertIsFromBeneficiary(projectCommunication);
            this.AssertCommunicationPreconditions(projectCommunication);

            projectCommunication.SetQuestionXml(messageDO.Xml);

            projectCommunication.UpdateAttributes(messageDO.Subject);

            this.unitOfWork.Save();

            var response = new RegProjectMessageDO
            {
                Gid = projectCommunication.Gid,
                Xml = projectCommunication.Question.Xml,
                ModifyDate = projectCommunication.ModifyDate,
                Version = projectCommunication.Version,
            };

            this.actionLogger.LogAction(
                typeof(ActionLogPortalGroups.ProjectManagingAuthorityCommunication.Questions.Update),
                projectCommunication.ProjectCommunicationId,
                null,
                null,
                null);

            return response;
        }

        [HttpPost]
        [Transaction]
        [PessimisticLock]
        [Route("{communicationGid:guid}/submit")]
        public void SubmitProjectCommunication(Guid communicationGid, RegProjectMessageDO messageDO)
        {
            var projectCommunication = this.projectManagingAuthorityCommunicationsRepository.FindForUpdate(
                this.accessContext.RegistrationId,
                communicationGid,
                messageDO.Version);

            this.projectManagingAuthorityCommunicationService.AssertIsFromBeneficiary(projectCommunication);
            this.AssertCommunicationPreconditions(projectCommunication);

            this.countersRepository.CreateProjectManagingAuthorityCommunicationCounter(projectCommunication.ProjectId);

            var regNumber = this.countersRepository.GetNextProjectManagingAuthorityCommunicationNumber(projectCommunication.ProjectId);
            projectCommunication.MakeQuestion(regNumber);

            this.unitOfWork.Save();

            this.actionLogger.LogAction(
                typeof(ActionLogPortalGroups.ProjectManagingAuthorityCommunication.Questions.Submit),
                projectCommunication.ProjectCommunicationId,
                null,
                null,
                null);
        }

        [Route("{communicationGid:guid}/sentInfo")]
        public ProjectCommunicationSentPVO GetSentProjectCommunicationInfo(Guid communicationGid)
        {
            return this.projectManagingAuthorityCommunicationsRepository.GetSentCommunicationInfo(communicationGid);
        }

        [HttpGet]
        [Route("hasCommunications")]
        public bool ProjectHasExistingCommunications(Guid registeredGid)
        {
            return this.projectManagingAuthorityCommunicationsRepository.ProjectHasExistingCommunications(registeredGid);
        }

        [HttpGet]
        [Route("hasNewCommunications")]
        public bool RegistrationHasNewCommunications()
        {
            return this.projectManagingAuthorityCommunicationsRepository.RegistrationHasNewCommunications(this.accessContext.RegistrationId);
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
