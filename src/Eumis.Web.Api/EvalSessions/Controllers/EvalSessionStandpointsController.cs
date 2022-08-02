using System.Collections.Generic;
using System.Web.Http;
using Eumis.ApplicationServices.Services.EvalSessionSheetXml;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Db;
using Eumis.Data.EvalSessions.Repositories;
using Eumis.Data.EvalSessions.ViewObjects;
using Eumis.Data.Projects.Repositories;
using Eumis.Domain;
using Eumis.Domain.EvalSessions;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.Core;
using Eumis.Web.Api.EvalSessions.DataObjects;

namespace Eumis.Web.Api.EvalSessions.Controllers
{
    [RoutePrefix("api/evalSessions/{evalSessionId}/standpoints")]
    public class EvalSessionStandpointsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IEvalSessionsRepository evalSessionsRepository;
        private IEvalSessionStandpointXmlsRepository evalSessionStandpointXmlsRepository;
        private IEvalSessionStandpointXmlService evalSessionStandpointXmlService;
        private IProjectsRepository projectsRepository;

        public EvalSessionStandpointsController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IEvalSessionsRepository evalSessionsRepository,
            IEvalSessionStandpointXmlsRepository evalSessionStandpointXmlsRepository,
            IEvalSessionStandpointXmlService evalSessionStandpointXmlService,
            IProjectsRepository projectsRepository)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.evalSessionsRepository = evalSessionsRepository;
            this.evalSessionStandpointXmlsRepository = evalSessionStandpointXmlsRepository;
            this.evalSessionStandpointXmlService = evalSessionStandpointXmlService;
            this.projectsRepository = projectsRepository;
        }

        [Route("")]
        public IList<EvalSessionStandpointVO> GetEvalSessionStandpoints(
            int evalSessionId,
            int? project = null,
            int? user = null,
            [FromUri] EvalSessionStandpointStatus[] statuses = null)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.ViewSessionData, evalSessionId);

            return this.evalSessionsRepository.GetEvalSessionStandpoints(evalSessionId, project, user, null, statuses);
        }

        [Route("~/api/evalSessionStandpoints/{standpointId:int}/projectStandpoints")]
        public IList<EvalSessionStandpointVO> GetProjectStandpointsForStandpoint(int standpointId)
        {
            this.authorizer.AssertCanDo(MyEvalSessionStandpointActions.Edit, standpointId);

            var standpointData = this.evalSessionsRepository.GetStandpointData(standpointId);

            if (standpointData.Status == Domain.EvalSessions.EvalSessionStandpointStatus.Canceled)
            {
                throw new DomainException("Cannot view standpoint project when sheet is 'Canceled'");
            }

            return this.evalSessionsRepository.GetEvalSessionStandpoints(
                null,
                standpointData.ProjectId,
                null,
                null,
                new EvalSessionStandpointStatus[] { EvalSessionStandpointStatus.Ended, EvalSessionStandpointStatus.Canceled });
        }

        [Route("~/api/evalSessionSheets/{sheetId:int}/standpoints")]
        public IList<EvalSessionStandpointVO> GetStandpointsForSheet(int sheetId)
        {
            this.authorizer.AssertCanDo(MyEvalSessionSheetActions.Edit, sheetId);
            var sheetData = this.evalSessionsRepository.GetSheetData(sheetId);

            if (sheetData.Status == Domain.EvalSessions.EvalSessionSheetStatus.Canceled)
            {
                throw new DomainException("Cannot view sheet project when sheet is 'Canceled'");
            }

            return this.evalSessionsRepository.GetEvalSessionStandpoints(
                null,
                sheetData.ProjectId,
                null,
                null,
                new EvalSessionStandpointStatus[] { EvalSessionStandpointStatus.Ended, EvalSessionStandpointStatus.Canceled });
        }

        [Route("{standpointId:int}")]
        public EvalSessionStandpointDO GetEvalSessionStandpoint(int evalSessionId, int standpointId)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.ViewSessionData, evalSessionId);

            var evalSession = this.evalSessionsRepository.Find(evalSessionId);

            var evalSessionStandpoint = evalSession.FindEvalSessionStandpoint(standpointId);

            var evalSessionStandpointXml = this.evalSessionStandpointXmlsRepository.FindByEvalSessionStandpointId(standpointId);

            return new EvalSessionStandpointDO(evalSessionStandpoint, evalSession.Version, evalSessionStandpointXml);
        }

        [HttpGet]
        [Route("new")]
        public EvalSessionStandpointDO NewEvalSessionStandpoint(int evalSessionId)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.EditSessionData, evalSessionId);

            var evalSession = this.evalSessionsRepository.Find(evalSessionId);

            return new EvalSessionStandpointDO(evalSessionId, evalSession.Version);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.EvalSessions.Edit.Standpoints.Create), IdParam = "evalSessionId")]
        public object AddEvalSessionStandpoint(int evalSessionId, EvalSessionStandpointDO evalSessionStandpoint)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.EditSessionData, evalSessionId);

            EvalSession evalSession = this.evalSessionsRepository.FindForUpdate(evalSessionId, evalSessionStandpoint.Version);

            var projectStatus = this.projectsRepository.GetProjectRegistrationStatus(evalSessionStandpoint.ProjectId.Value);

            var sessionStandpoint = evalSession.AddEvalSessionStandpoint(
                evalSessionStandpoint.EvalSessionUserId.Value,
                evalSessionStandpoint.ProjectId.Value,
                projectStatus,
                evalSessionStandpoint.Note);

            this.unitOfWork.Save();

            var evalSessionStandpointXml = this.evalSessionStandpointXmlService.CreateStandpoint(sessionStandpoint);
            this.evalSessionStandpointXmlsRepository.Add(evalSessionStandpointXml);

            this.unitOfWork.Save();

            return new { EvalSessionStandpointId = sessionStandpoint.EvalSessionStandpointId };
        }

        [HttpPost]
        [Route("canCreate")]
        public string CanCreateEvalSessionStandpoint(int evalSessionId, EvalSessionStandpointDO evalSessionStandpoint)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.EditSessionData, evalSessionId);

            var evalSession = this.evalSessionsRepository.Find(evalSessionId);

            var projectStatus = this.projectsRepository.GetProjectRegistrationStatus(evalSessionStandpoint.ProjectId.Value);

            var error = evalSession.CanCreateEvalSessionStandpoint(
                evalSessionStandpoint.ProjectId.Value,
                projectStatus);

            return error;
        }

        [HttpPost]
        [Route("{standpointId:int}/cancelStandpoint")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.EvalSessions.Edit.Standpoints.ChangeStatusToCanceled), IdParam = "evalSessionId", ChildIdParam = "standpointId")]
        public void CancelEvalSessionStandpoint(int evalSessionId, int standpointId, string version, ConfirmDO confirm)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.EditSessionData, evalSessionId);

            byte[] vers = System.Convert.FromBase64String(version);
            EvalSession evalSession = this.evalSessionsRepository.FindForUpdate(evalSessionId, vers);

            evalSession.CancelEvalSessionStandpoint(standpointId, confirm.Note);

            this.unitOfWork.Save();
        }
    }
}
