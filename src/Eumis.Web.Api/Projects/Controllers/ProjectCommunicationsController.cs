using Eumis.ApplicationServices.Services.ProjectCommunication;
using Eumis.ApplicationServices.Services.ProjectVersionXml;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Db;
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

namespace Eumis.Web.Api.Projects.Controllers
{
    [RoutePrefix("api/projects/{projectId:int}/communications")]
    public class ProjectCommunicationsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IProjectCommunicationsRepository projectCommunicationsRepository;
        private IProjectCommunicationService projectCommunicationService;
        private IProjectVersionXmlService projectVersionXmlService;
        private IEvalSessionsRepository evalSessionsRepository;

        public ProjectCommunicationsController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IProjectCommunicationsRepository projectCommunicationsRepository,
            IProjectCommunicationService projectCommunicationService,
            IProjectVersionXmlService projectVersionXmlService,
            IEvalSessionsRepository evalSessionsRepository)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.projectCommunicationsRepository = projectCommunicationsRepository;
            this.projectCommunicationService = projectCommunicationService;
            this.projectVersionXmlService = projectVersionXmlService;
            this.evalSessionsRepository = evalSessionsRepository;
        }

        [Route("")]
        public IList<CommunicationVO> GetProjectCommunications(int projectId)
        {
            this.authorizer.AssertCanDoAny(
                Tuple.Create<Enum, int?>(ProjectActions.SearchCommunication, projectId),
                Tuple.Create<Enum, int?>(ProjectDossierActions.View, projectId));

            return this.projectCommunicationsRepository.GetProjectCommunications(projectId);
        }

        [Route("~/api/evalSessions/{evalSessionId:int}/communications")]
        public IList<EvalSessionCommunicationVO> GetEvalSessionCommunications(
            int evalSessionId,
            int? projectId = null,
            ProjectCommunicationStatus? statusId = null,
            DateTime? questionDateFrom = null,
            DateTime? questionDateTo = null)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.ViewSessionData, evalSessionId);

            return this.projectCommunicationsRepository.GetProjectCommunicationsForEvalSession(evalSessionId, projectId, statusId, questionDateFrom, questionDateTo);
        }

        [Route("~/api/evalSessionSheets/{sheetId:int}/communications")]
        public IList<CommunicationVO> GetProjectCommunicationsForSheet(int sheetId)
        {
            var sheetData = this.evalSessionsRepository.GetSheetData(sheetId);

            if (sheetData.Status == Domain.EvalSessions.EvalSessionSheetStatus.Canceled)
            {
                throw new DomainException("Cannot view sheet project when sheet is 'Canceled'");
            }

            this.authorizer.AssertCanDo(ProjectActions.SearchCommunication, sheetData.ProjectId);

            return this.projectCommunicationsRepository.GetProjectCommunications(sheetData.ProjectId, true);
        }

        [Route("~/api/evalSessionStandpoints/{standpointId:int}/communications")]
        public IList<CommunicationVO> GetProjectCommunicationsForStandpoint(int standpointId)
        {
            var standpointData = this.evalSessionsRepository.GetStandpointData(standpointId);

            if (standpointData.Status == Domain.EvalSessions.EvalSessionStandpointStatus.Canceled)
            {
                throw new DomainException("Cannot view standpoint project when sheet is 'Canceled'");
            }

            this.authorizer.AssertCanDo(ProjectActions.SearchCommunication, standpointData.ProjectId);

            return this.projectCommunicationsRepository.GetProjectCommunications(standpointData.ProjectId, true);
        }

        [Route("{communicationId:int}")]
        public ProjectCommunicationDO GetProjectCommunication(int projectId, int communicationId)
        {
            this.authorizer.AssertCanDoAny(
                Tuple.Create<Enum, int?>(ProjectCommunicationActions.View, communicationId),
                Tuple.Create<Enum, int?>(ProjectDossierActions.View, projectId));

            var projectData = this.projectCommunicationsRepository.GetCommunicationRegData(communicationId);

            var communication = this.projectCommunicationsRepository.Find(communicationId);

            return new ProjectCommunicationDO(communication, projectData.ProjectRegNumber, projectData.CompanyName, projectData.RegDate);
        }

        [Route("~/api/evalSessionSheets/{sheetId:int}/communications/{communicationId:int}")]
        public ProjectCommunicationDO GetProjectCommunicationForSheet(int sheetId, int communicationId)
        {
            var sheetData = this.evalSessionsRepository.GetSheetData(sheetId);

            if (sheetData.Status == Domain.EvalSessions.EvalSessionSheetStatus.Canceled)
            {
                throw new DomainException("Cannot view sheet project communication when sheet is 'Canceled'");
            }

            this.authorizer.AssertCanDoAny(
                Tuple.Create<Enum, int?>(ProjectCommunicationActions.View, communicationId),
                Tuple.Create<Enum, int?>(ProjectDossierActions.View, sheetData.ProjectId));

            var projectData = this.projectCommunicationsRepository.GetCommunicationRegData(communicationId);

            var communication = this.projectCommunicationsRepository.Find(communicationId);

            return new ProjectCommunicationDO(communication, projectData.ProjectRegNumber, projectData.CompanyName, projectData.RegDate);
        }

        [Route("~/api/evalSessionStandpoints/{standpointId:int}/communications/{communicationId:int}")]
        public ProjectCommunicationDO GetProjectCommunicationForStandpoint(int standpointId, int communicationId)
        {
            var standpointData = this.evalSessionsRepository.GetStandpointData(standpointId);

            if (standpointData.Status == Domain.EvalSessions.EvalSessionStandpointStatus.Canceled)
            {
                throw new DomainException("Cannot view standpoint project communication when sheet is 'Canceled'");
            }

            this.authorizer.AssertCanDoAny(
                Tuple.Create<Enum, int?>(ProjectCommunicationActions.View, communicationId),
                Tuple.Create<Enum, int?>(ProjectDossierActions.View, standpointData.ProjectId));

            var projectData = this.projectCommunicationsRepository.GetCommunicationRegData(communicationId);

            var communication = this.projectCommunicationsRepository.Find(communicationId);

            return new ProjectCommunicationDO(communication, projectData.ProjectRegNumber, projectData.CompanyName, projectData.RegDate);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.EvalSessions.Edit.Projects.Communications.Create), IdParam = "projectId")]
        public object CreateProjectCommunication(int projectId, int evalSessionId)
        {
            this.authorizer.AssertCanDo(ProjectActions.CreateCommunication, projectId);

            if (this.projectCommunicationService.CanCreate(projectId, evalSessionId).Any())
            {
                throw new InvalidOperationException("Cannot create communication.");
            }

            ProjectCommunication newCommunication = this.projectCommunicationService.CreateCommunication(projectId, evalSessionId);
            this.projectCommunicationsRepository.Add(newCommunication);

            this.projectCommunicationService.ApplyAnsweredProjectCommunication(projectId);

            this.unitOfWork.Save();

            return new { CommunicationId = newCommunication.ProjectCommunicationId };
        }

        [HttpPost]
        [Route("canCreate")]
        public ErrorsDO CanCreateProjectCommunication(int projectId, int evalSessionId)
        {
            this.authorizer.AssertCanDo(ProjectActions.CreateCommunication, projectId);

            var errorList = this.projectCommunicationService.CanCreate(projectId, evalSessionId);

            return new ErrorsDO(errorList);
        }

        [HttpPut]
        [Route("{communicationId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.EvalSessions.Edit.Projects.Communications.Edit), IdParam = "projectId", ChildIdParam = "communicationId")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public void UpdateProjectCommunication(int projectId, int communicationId, ProjectCommunicationDO projectCommunication)
        {
            this.authorizer.AssertCanDo(ProjectCommunicationActions.Edit, communicationId);

            if (!this.projectCommunicationService.IsCommunicationEvalSessionStatusActive(communicationId))
            {
                throw new InvalidOperationException("Cannot update communication data.");
            }

            ProjectCommunication communication = this.projectCommunicationsRepository.FindForUpdate(communicationId, projectCommunication.Version);

            communication.UpdateAttributes(projectCommunication.QuestionEndingDate);

            this.unitOfWork.Save();
        }

        [HttpDelete]
        [Route("{communicationId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.EvalSessions.Edit.Projects.Communications.Delete), IdParam = "projectId", ChildIdParam = "communicationId")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public void DeleteProjectCommunication(int projectId, int communicationId, string version)
        {
            this.authorizer.AssertCanDo(ProjectCommunicationActions.Delete, communicationId);

            if (!this.projectCommunicationService.CanDelete(communicationId))
            {
                throw new InvalidOperationException("Cannot delete communication.");
            }

            byte[] vers = System.Convert.FromBase64String(version);

            ProjectCommunication communication = this.projectCommunicationsRepository.FindForUpdate(communicationId, vers);

            this.projectCommunicationsRepository.Remove(communication);

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{communicationId:int}/cancel")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.EvalSessions.Edit.Projects.Communications.ChangeStatusToCanceled), IdParam = "projectId", ChildIdParam = "communicationId")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public void CancelProjectCommunication(int projectId, int communicationId, string version, ConfirmDO confirm)
        {
            this.authorizer.AssertCanDo(ProjectCommunicationActions.Cancel, communicationId);

            if (!this.projectCommunicationService.CanCancel(communicationId))
            {
                throw new InvalidOperationException("Cannot cancel communication.");
            }

            byte[] vers = System.Convert.FromBase64String(version);
            ProjectCommunication communication = this.projectCommunicationsRepository.FindForUpdate(communicationId, vers);
            communication.MakeCancelled(confirm.Note);

            this.unitOfWork.Save();
        }
    }
}
