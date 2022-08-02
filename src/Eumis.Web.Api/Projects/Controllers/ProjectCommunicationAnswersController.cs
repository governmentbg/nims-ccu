using Eumis.ApplicationServices.Services.ProjectCommunication;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Db;
using Eumis.Data.Core.Relations;
using Eumis.Data.EvalSessions.Repositories;
using Eumis.Data.Projects.Repositories;
using Eumis.Data.Projects.ViewObjects;
using Eumis.Domain;
using Eumis.Domain.Projects;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Print;
using Eumis.Web.Api.Core;
using Eumis.Web.Api.Projects.DataObjects;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace Eumis.Web.Api.ProjectManagingAuthorityCommunications.Controllers
{
    [RoutePrefix("api/projects/{projectId:int}/communications/{communicationId:int}/answers")]

    public class ProjectCommunicationAnswersController : ApiController
    {
        private IAuthorizer authorizer;
        private IUnitOfWork unitOfWork;
        private IPrintManager printManager;
        private IProjectCommunicationsRepository projectCommunicationsRepository;
        private IProjectCommunicationFilesRepository projectCommunicationFilesRepository;
        private IRelationsRepository relationsRepository;
        private IEvalSessionsRepository evalSessionsRepository;
        private IProjectCommunicationService projectCommunicationService;

        public ProjectCommunicationAnswersController(
            IAuthorizer authorizer,
            IUnitOfWork unitOfWork,
            IPrintManager printManager,
            IProjectCommunicationsRepository projectCommunicationsRepository,
            IProjectCommunicationFilesRepository projectCommunicationFilesRepository,
            IRelationsRepository relationsRepository,
            IEvalSessionsRepository evalSessionsRepository,
            IProjectCommunicationService projectCommunicationService)
        {
            this.authorizer = authorizer;
            this.unitOfWork = unitOfWork;
            this.printManager = printManager;
            this.projectCommunicationsRepository = projectCommunicationsRepository;
            this.projectCommunicationFilesRepository = projectCommunicationFilesRepository;
            this.relationsRepository = relationsRepository;
            this.evalSessionsRepository = evalSessionsRepository;
            this.projectCommunicationService = projectCommunicationService;
        }

        [Route("")]
        public IList<ProjectCommunicationAnswerVO> GetProjectCommunicationAnswers(int projectId, int communicationId)
        {
            this.authorizer.AssertCanDo(ProjectCommunicationActions.View, communicationId);
            this.relationsRepository.AssertProjectHasProjectCommunication(projectId, communicationId);

            return this.projectCommunicationsRepository.GetProjectCommunicationAnswers(communicationId);
        }

        [Route("~/api/evalSessionSheets/{sheetId:int}/communications/{communicationId:int}/answers")]
        public IList<ProjectCommunicationAnswerVO> GetProjectCommunicationAnswersForSheet(int sheetId, int communicationId)
        {
            this.authorizer.AssertCanDo(ProjectCommunicationActions.View, communicationId);

            var sheetData = this.evalSessionsRepository.GetSheetData(sheetId);

            if (sheetData.Status == Domain.EvalSessions.EvalSessionSheetStatus.Canceled)
            {
                throw new DomainException("Cannot view sheet project when sheet is 'Canceled'");
            }

            this.relationsRepository.AssertProjectHasProjectCommunication(sheetData.ProjectId, communicationId);

            return this.projectCommunicationsRepository.GetProjectCommunicationAnswers(communicationId);
        }

        [Route("~/api/evalSessionStandpoints/{standpointId:int}/communications/{communicationId:int}/answers")]
        public IList<ProjectCommunicationAnswerVO> GetProjectCommunicationAnswersForStandpoint(int standpointId, int communicationId)
        {
            this.authorizer.AssertCanDo(ProjectCommunicationActions.View, communicationId);

            var standpointData = this.evalSessionsRepository.GetStandpointData(standpointId);

            if (standpointData.Status == Domain.EvalSessions.EvalSessionStandpointStatus.Canceled)
            {
                throw new DomainException("Cannot view standpoint project when standpoint is 'Canceled'");
            }

            this.relationsRepository.AssertProjectHasProjectCommunication(standpointData.ProjectId, communicationId);

            return this.projectCommunicationsRepository.GetProjectCommunicationAnswers(communicationId);
        }

        [Route("{answerId:int}")]
        public ProjectCommunicationAnswerDO GetProjectCommunicationAnswer(int projectId, int communicationId, int answerId)
        {
            this.authorizer.AssertCanDo(ProjectCommunicationActions.View, communicationId);
            this.relationsRepository.AssertProjectHasProjectCommunication(projectId, communicationId);

            var communication = this.projectCommunicationsRepository.Find(communicationId);
            var answer = communication.FindAnswer(answerId);

            var communicationFile = this.projectCommunicationFilesRepository.FindByProjectCommunicationAnswerId(answerId);

            return new ProjectCommunicationAnswerDO(answer, communication, communicationFile);
        }

        [HttpPost]
        [Route("{answerId:int}/canRegister")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public ErrorsDO CanRegisterEvalSessionProjectAnswer(
            int projectId,
            int communicationId,
            int answerId,
            string version,
            ProjectAnswerRegistrationDO reg)
        {
            this.authorizer.AssertCanDo(ProjectCommunicationActions.Register, communicationId);

            var errors = this.projectCommunicationService.CanRegisterEvalSessionProjectAnswer(communicationId, answerId, reg.AnswerHash, reg.RegDate);

            return new ErrorsDO(errors);
        }

        [HttpPost]
        [Route("{answerId:int}/register")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.EvalSessions.Edit.Projects.Communications.AnswerRegistered), IdParam = "projectId", ChildIdParam = "communicationId")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public void RegisterProjectCommunicationAnswer(
            int projectId,
            int communicationId,
            int answerId,
            string version,
            ProjectAnswerRegistrationDO reg)
        {
            this.authorizer.AssertCanDo(ProjectCommunicationActions.Register, communicationId);

            byte[] vers = System.Convert.FromBase64String(version);

            if (this.projectCommunicationService.CanRegisterEvalSessionProjectAnswer(communicationId, answerId, reg.AnswerHash, reg.RegDate).Any())
            {
                throw new InvalidOperationException("Cannot register answer.");
            }

            ProjectCommunication communication = this.projectCommunicationsRepository.FindForUpdate(communicationId, vers);

            communication.RegisterAnswer(answerId, reg.AnswerHash, reg.RegDate);

            this.unitOfWork.Save();
        }

        [HttpGet]
        [Route("{answerId:int}/print")]
        public HttpResponseMessage Print(int communicationId, int answerId)
        {
            this.authorizer.AssertCanDo(ProjectCommunicationActions.PrintRegistration, communicationId);

            var communication = this.projectCommunicationsRepository.Find(communicationId);
            var answer = communication.FindAnswer(answerId);

            if (answer.Status != ProjectCommunicationAnswerStatus.Answer)
            {
                throw new InvalidOperationException("Cannot print answer data.");
            }

            var regData = this.projectCommunicationsRepository.GetCommunicationRegData(communicationId);
            if (!ProjectCommunication.PrintableStatuses.Contains(regData.Status))
            {
                throw new InvalidOperationException("Cannot print answer data.");
            }

            JObject context = JObject.FromObject(regData);

            var pdfBytes = this.printManager.Print(TemplateType.AnswerRegistration, PrintType.PDF, context);

            HttpResponseMessage responseMessage = this.Request.CreateResponse(HttpStatusCode.OK);
            responseMessage.Content = new ByteArrayContent(pdfBytes);
            responseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
            responseMessage.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("inline")
            {
                FileName = regData.RegNumber + ".pdf",
            };

            return responseMessage;
        }
    }
}
