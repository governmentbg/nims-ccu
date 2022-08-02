using Eumis.ApplicationServices.Communicators;
using Eumis.ApplicationServices.Services.ProjectCommunication;
using Eumis.ApplicationServices.Services.ProjectVersionXml;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.Core;
using Eumis.Data.Projects.PortalViewObjects;
using Eumis.Data.Projects.Repositories;
using Eumis.Domain.Projects;
using Eumis.Log.ActionLogger;
using Eumis.Log.ActionLogger.Enums;
using Eumis.PortalIntegration.Api.Core;
using Eumis.PortalIntegration.Api.Portal.Registrations.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Eumis.PortalIntegration.Api.Portal.Registrations.Controllers
{
    [RoutePrefix("api/registration/messages")]
    public class RegProjectMessagesController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IActionLogger actionLogger;
        private IAccessContext accessContext;
        private IProjectVersionXmlsRepository projectVersionXmlsRepository;
        private IProjectCommunicationsRepository projectCommunicationsRepository;
        private IProjectCommunicationFilesRepository projectCommunicationFilesRepository;
        private IDocumentRestApiCommunicator documentRestApiCommunicator;
        private IProjectCommunicationService projectCommunicationService;
        private IProjectVersionXmlService projectVersionXmlService;

        public RegProjectMessagesController(
            IUnitOfWork unitOfWork,
            IActionLogger actionLogger,
            IAccessContext accessContext,
            IProjectVersionXmlsRepository projectVersionXmlsRepository,
            IProjectCommunicationsRepository projectCommunicationsRepository,
            IProjectCommunicationFilesRepository projectCommunicationFilesRepository,
            IDocumentRestApiCommunicator documentRestApiCommunicator,
            IProjectCommunicationService projectCommunicationService,
            IProjectVersionXmlService projectVersionXmlService)
        {
            this.unitOfWork = unitOfWork;
            this.actionLogger = actionLogger;
            this.accessContext = accessContext;
            this.projectVersionXmlsRepository = projectVersionXmlsRepository;
            this.projectCommunicationsRepository = projectCommunicationsRepository;
            this.projectCommunicationFilesRepository = projectCommunicationFilesRepository;
            this.documentRestApiCommunicator = documentRestApiCommunicator;
            this.projectCommunicationService = projectCommunicationService;
            this.projectVersionXmlService = projectVersionXmlService;
        }

        [Route("")]
        public PagePVO<RegMessagePVO> GetAll(int offset = 0, int? limit = null)
        {
            return this.projectCommunicationsRepository.GetAllForRegistration(this.accessContext.RegistrationId, offset, limit);
        }

        [Route("count")]
        public RegMessageCountPVO GetCount()
        {
            return this.projectCommunicationsRepository.GetCountForRegistration(this.accessContext.RegistrationId);
        }

        [Route("{communicationGid:guid}")]
        public RegProjectMessageDO GetProjectCommunication(Guid communicationGid)
        {
            var communication = this.projectCommunicationsRepository.Find(this.accessContext.RegistrationId, communicationGid);

            if (!communication.QuestionReadDate.HasValue)
            {
                using (var transaction = this.unitOfWork.BeginTransaction())
                {
                    communication.SetReadDate();

                    this.unitOfWork.Save();
                    transaction.Commit();
                }
            }

            return new RegProjectMessageDO
            {
                Gid = communication.Gid,
                RegistrationNumber = communication.RegNumber,
                MessageDate = communication.Question.MessageDate,
                MessageEndingDate = communication.QuestionEndingDate,
                Xml = communication.Question.Xml,
                Version = communication.Version,
            };
        }

        [HttpPost]
        [Transaction]
        [PessimisticLock]
        [Route("{communicationGid:guid}/answers/new")]
        public RegProjectMessageDO GetNewAnswer(Guid communicationGid, RegProjectMessageDO messageDO)
        {
            var communication = this.projectCommunicationsRepository.FindForUpdate(
                this.accessContext.RegistrationId,
                communicationGid,
                messageDO.Version);

            this.AssertMessagePreconditions(communication);
            communication.CanCreateAnswer();

            var actualProjectVersion = this.projectVersionXmlsRepository.GetActualProjectVersion(communication.ProjectId);

            var initialXml = communication.Answers.Any() ?
                communication.GetLastAnswerXml() :
                communication.Question.Xml;

            var answerXml = this.documentRestApiCommunicator.CreateProjectCommunicationAnswerXml(initialXml);

            var answer = new ProjectCommunicationAnswer(
                answerXml,
                actualProjectVersion.ProjectVersionXmlId,
                communication.GetNextAnswerOrderNum(),
                ProjectCommunicationAnswerSource.Beneficiary);

            communication.Answers.Add(answer);
            this.unitOfWork.Save();

            return new RegProjectMessageDO
            {
                Gid = communication.Gid,
                AnswerGid = answer.Gid,
                Xml = answerXml,
                Version = communication.Version,
            };
        }

        [Route("{communicationGid:guid}/answers/{answerGid:guid}")]
        public RegProjectMessageDO GetAnswer(Guid communicationGid, Guid answerGid)
        {
            var communication = this.projectCommunicationsRepository.Find(this.accessContext.RegistrationId, communicationGid);
            var answer = communication.FindAnswer(answerGid);

            if (!answer.ReadDate.HasValue)
            {
                answer.SetReadDate();
                this.unitOfWork.Save();
            }

            var lastSendingDate = communication.Question.MessageDate.Value;
            if (communication.HasActiveAnswer())
            {
                lastSendingDate = communication.GetActiveAnswerDate();
            }

            return new RegProjectMessageDO
            {
                Gid = answer.Gid,
                RegistrationNumber = communication.RegNumber,
                MessageDate = communication.Question.MessageDate,
                MessageEndingDate = communication.QuestionEndingDate,
                LastSendingDate = lastSendingDate,
                Xml = answer.Answer.Xml,
                Version = communication.Version,
            };
        }

        [HttpPut]
        [Transaction]
        [PessimisticLock]
        [Route("{communicationGid:guid}/answers/{answerGid:guid}")]
        public RegProjectMessageDO UpdateAnswer(Guid communicationGid, Guid answerGid, RegProjectMessageDO messageDO)
        {
            var communication = this.projectCommunicationsRepository.FindForUpdate(
                this.accessContext.RegistrationId,
                communicationGid,
                messageDO.Version);

            this.AssertMessagePreconditions(communication);

            var answer = communication.FindAnswer(answerGid);
            answer.SetAnswerXml(messageDO.Xml);

            communication.MakeDraftAnswer();

            communication.UpdateFiles(ProjectCommunicationMessageType.Answer, answer);
            communication.ModifyDate = DateTime.Now;

            this.unitOfWork.Save();

            var response = new RegProjectMessageDO
            {
                Gid = answer.Gid,
                Xml = answer.Answer.Xml,
                ModifyDate = communication.ModifyDate,
                Version = communication.Version,
            };

            this.actionLogger.LogAction(
                typeof(ActionLogPortalGroups.ProjectCommunicationAnswer.Update),
                communication.ProjectCommunicationId,
                answer.ProjectCommunicationAnswerId,
                null,
                null);

            return response;
        }

        [HttpDelete]
        [Transaction]
        [PessimisticLock]
        [Route("{communicationGid:guid}/answers/{answerGid:guid}/delete")]
        public void DeleteAnswer(Guid communicationGid, Guid answerGid, RegProjectMessageDO messageDO)
        {
            var communication = this.projectCommunicationsRepository.FindForUpdate(
                this.accessContext.RegistrationId,
                communicationGid,
                messageDO.Version);

            this.AssertMessagePreconditions(communication);

            var answer = communication.FindAnswer(answerGid);

            communication.RemoveAnswer(answer.ProjectCommunicationAnswerId);

            this.projectCommunicationService.DeleteProjectCommunicationAnswerMessageFiles(answer.ProjectCommunicationAnswerId);

            this.unitOfWork.Save();

            this.actionLogger.LogAction(
                typeof(ActionLogPortalGroups.ProjectCommunicationAnswer.Delete),
                communication.ProjectCommunicationId,
                null,
                null,
                null);
        }

        [HttpPost]
        [Transaction]
        [PessimisticLock]
        [Route("{communicationGid:guid}/answers/{answerGid:guid}/definalize")]
        public void DefinalizeAnswer(Guid communicationGid, Guid answerGid, RegProjectMessageDO messageDO)
        {
            var communication = this.projectCommunicationsRepository.FindForUpdate(
                this.accessContext.RegistrationId,
                communicationGid,
                messageDO.Version);

            this.AssertMessagePreconditions(communication);

            communication.DefinalizeAnswer(answerGid);

            this.unitOfWork.Save();

            this.actionLogger.LogAction(
                typeof(ActionLogPortalGroups.ProjectCommunicationAnswer.ChangeStatusToDraftAnswer),
                communication.ProjectCommunicationId,
                null,
                null,
                null);
        }

        [HttpPost]
        [Transaction]
        [PessimisticLock]
        [Route("{communicationGid:guid}/answers/{answerGid:guid}/finalize")]
        public void FinalizeAnswer(Guid communicationGid, Guid answerGid, RegProjectMessageDO messageDO)
        {
            var communication = this.projectCommunicationsRepository.FindForUpdate(
                this.accessContext.RegistrationId,
                communicationGid,
                messageDO.Version);

            this.AssertMessagePreconditions(communication);

            communication.MakeAnswerFinalized(answerGid);

            var answer = communication.FindAnswer(answerGid);

            this.unitOfWork.Save();

            this.actionLogger.LogAction(
                typeof(ActionLogPortalGroups.ProjectCommunicationAnswer.ChangeStatusToAnswerFinalized),
                communication.ProjectCommunicationId,
                answer.ProjectCommunicationAnswerId,
                null,
                null);
        }

        [HttpPost]
        [Transaction]
        [PessimisticLock]
        [Route("{communicationGid:guid}/answers/{answerGid:guid}/submit")]
        public object SubmitAnswer(Guid communicationGid, Guid answerGid, RegProjectMessageDO messageDO)
        {
            var communication = this.projectCommunicationsRepository.FindForUpdate(
                this.accessContext.RegistrationId,
                communicationGid,
                messageDO.Version);

            this.AssertMessagePreconditions(communication);

            var answer = communication.FindAnswer(answerGid);

            communication.MakePaperAnswer(answerGid);

            this.unitOfWork.Save();

            var response = new
            {
                MessageDate = communication.Question.MessageDate,
                RegistrationNumber = communication.RegNumber,
                Hash = answer.Answer.Hash,
            };

            this.actionLogger.LogAction(
                typeof(ActionLogPortalGroups.ProjectCommunicationAnswer.ChangeStatusToPaperAnswer),
                communication.ProjectCommunicationId,
                answer.ProjectCommunicationAnswerId,
                null,
                response);

            return response;
        }

        [HttpPost]
        [Transaction]
        [Route("send")]
        public object SendAnswer(SubmitDO submitDO)
        {
            if (submitDO.Isun == null)
            {
                throw new ArgumentException("IsunFile cannot be null");
            }

            if (!submitDO.IsValid())
            {
                throw new Exception("Invalid isun file signatures.");
            }

            var xml = submitDO.UnzipData();
            var hash = ProjectCommunicationMessage.GetHash(xml);

            var answer = this.projectCommunicationsRepository.FindAnswer(this.accessContext.RegistrationId, hash);

            var communication = this.projectCommunicationsRepository.FindForUpdate(answer.ProjectCommunicationId, submitDO.Version);

            this.AssertMessagePreconditions(communication);

            communication.MakeAnswer(answer.ProjectCommunicationAnswerId);

            this.projectVersionXmlService.CreateProjectVersionFromCommunication(communication, answer.Answer.Xml);

            this.unitOfWork.Save();

            var isunFileTuple = new Tuple<byte[], string>(submitDO.Isun, communication.RegNumber + "-" + hash + ".isun");

            var signaturesTuples = new List<Tuple<byte[], string>>();

            int counter = 1;
            foreach (var signature in submitDO.Signatures)
            {
                signaturesTuples.Add(new Tuple<byte[], string>(signature, communication.RegNumber + "-" + hash + "-sig" + counter + ".p7s"));
                counter++;
            }

            this.projectCommunicationFilesRepository.Add(new ProjectCommunicationFile(
                answer.ProjectCommunicationId,
                answer.ProjectCommunicationAnswerId,
                isunFileTuple,
                signaturesTuples));

            var response = new
            {
                RegistrationNumber = communication.RegNumber,
                ReplyDate = answer.Answer.MessageDate,
            };

            this.actionLogger.LogAction(
                typeof(ActionLogPortalGroups.ProjectCommunicationAnswer.ChangeStatusToAnswer),
                communication.ProjectCommunicationId,
                answer.ProjectCommunicationAnswerId,
                null,
                null);

            this.unitOfWork.Save();

            return response;
        }

        private void AssertMessagePreconditions(ProjectCommunication communication)
        {
            if (communication.Status == ProjectCommunicationStatus.Canceled)
            {
                throw new HttpResponseException(
                    this.Request.CreateResponse(
                        HttpStatusCode.BadRequest,
                        new { error = PortalIntegrationErrors.MessageCanceled }));
            }

            if (communication.Status == ProjectCommunicationStatus.Expired || DateTime.Now > communication.QuestionEndingDate)
            {
                throw new HttpResponseException(
                    this.Request.CreateResponse(
                        HttpStatusCode.BadRequest,
                        new { error = PortalIntegrationErrors.MessageTimedOut }));
            }
        }
    }
}
