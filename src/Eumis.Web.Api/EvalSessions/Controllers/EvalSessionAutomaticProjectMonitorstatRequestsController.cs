using Eumis.ApplicationServices.Services.EvalSession;
using Eumis.ApplicationServices.Services.Monitorstat;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Data.EvalSessions.Repositories;
using Eumis.Data.Projects.ViewObjects;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.Core;
using Eumis.Web.Api.EvalSessions.DataObjects;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.EvalSessions.Controllers
{
    [RoutePrefix("api/evalSessions/{evalSessionId:int}/automaticProjectMonitorstatRequests")]
    public class EvalSessionAutomaticProjectMonitorstatRequestsController : ApiController
    {
        private IAuthorizer authorizer;
        private IEvalSessionsRepository evalSessionsRepository;
        private IEvalSessionService evalSessionService;
        private IMonitorstatService monitorstatService;

        public EvalSessionAutomaticProjectMonitorstatRequestsController(
            IAuthorizer authorizer,
            IEvalSessionsRepository evalSessionsRepository,
            IEvalSessionService evalSessionService,
            IMonitorstatService monitorstatService)
        {
            this.authorizer = authorizer;
            this.evalSessionsRepository = evalSessionsRepository;
            this.evalSessionService = evalSessionService;
            this.monitorstatService = monitorstatService;
        }

        [Route("projects")]
        public IList<ProjectRegistrationsVO> GetProjectsForAutomaticProjectMonitorstatRequests(int evalSessionId)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.EditSessionData, evalSessionId);

            return this.evalSessionsRepository.GetProjectsForAutomaticProjectMonitorstatRequests(evalSessionId);
        }

        [HttpGet]
        [Route("loadProjectsFromFile")]
        public object LoadProjectsFromFile(int evalSessionId, Guid fileKey)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.EditSessionData, evalSessionId);

            return this.evalSessionService.ParseProjectsExcelFile(evalSessionId, fileKey);
        }

        [HttpPost]
        [Route("sendAutomaticRequests")]
        public ErrorsDO SendAutomaticProjectMonitorstatRequests(int evalSessionId, EvalSessionAutomaticProjectMonitorstatRequestsDO data)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.EditSessionData, evalSessionId);

            IList<string> errors = this.monitorstatService.SendAutomaticProjectMonitorstatRequests(
                data.ProjectIds,
                evalSessionId,
                data.ProcedureMonitorstatRequestId,
                data.ProcedureApplicationDocId,
                data.ProgrammeDeclarationId);

            return new ErrorsDO(errors);
        }
    }
}
