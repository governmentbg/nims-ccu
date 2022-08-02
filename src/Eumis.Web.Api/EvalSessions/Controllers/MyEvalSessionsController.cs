using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Db;
using Eumis.Common.Localization;
using Eumis.Data.EvalSessions.Repositories;
using Eumis.Data.Procedures.Repositories;
using Eumis.Web.Api.EvalSessions.DataObjects;
using System.Web.Http;

namespace Eumis.Web.Api.EvalSessions.Controllers
{
    [RoutePrefix("api/myEvalSessions")]
    public class MyEvalSessionsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IEvalSessionsRepository evalSessionsRepository;
        private IProceduresRepository proceduresRepository;
        private IProcedureMonitorstatRequestsRepository procedureMonitorstatRequestsRepository;

        public MyEvalSessionsController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IEvalSessionsRepository evalSessionsRepository,
            IProceduresRepository proceduresRepository,
            IProcedureMonitorstatRequestsRepository procedureMonitorstatRequestsRepository)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.evalSessionsRepository = evalSessionsRepository;
            this.proceduresRepository = proceduresRepository;
            this.procedureMonitorstatRequestsRepository = procedureMonitorstatRequestsRepository;
        }

        [Route("{evalSessionId:int}")]
        public EvalSessionDO GetEvalSession(int evalSessionId)
        {
            this.authorizer.AssertCanDo(MyEvalSession.ViewSession, evalSessionId);

            var evalSession = this.evalSessionsRepository.Find(evalSessionId);

            return new EvalSessionDO(evalSession);
        }

        [Route("{evalSessionId:int}/info")]
        public EvalSessionInfoDO GetEvalSessionInfo(int evalSessionId)
        {
            this.authorizer.AssertCanDo(MyEvalSession.ViewSession, evalSessionId);

            var evalSession = this.evalSessionsRepository.Find(evalSessionId);

            var procedure = this.proceduresRepository.FindWithoutIncludes(evalSession.ProcedureId);

            var evalSessionActions = this.evalSessionsRepository.GetEvalSessionAvailableActions(evalSession.ProcedureId, evalSessionId);

            var procedureHasMonitorstatInquiries = this.procedureMonitorstatRequestsRepository.ProcedureHasMonitorstatRequests(evalSession.ProcedureId);

            return new EvalSessionInfoDO(evalSession, SystemLocalization.GetDisplayName(procedure.Name, procedure.NameAlt), procedureHasMonitorstatInquiries, evalSessionActions);
        }
    }
}
