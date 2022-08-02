using System.Collections.Generic;
using System.Web.Http;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.EvalSessions.Repositories;
using Eumis.Data.EvalSessions.ViewObjects;
using Eumis.Domain.EvalSessions;
using Eumis.Web.Api.EvalSessions.DataObjects;

namespace Eumis.Web.Api.EvalSessions.Controllers
{
    [RoutePrefix("api/myEvalSessions/{evalSessionId}/standpoints")]
    public class MyEvalSessionStandpointsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAccessContext accessContext;
        private IAuthorizer authorizer;
        private IEvalSessionsRepository evalSessionsRepository;
        private IEvalSessionStandpointXmlsRepository evalSessionStandpointXmlsRepository;

        public MyEvalSessionStandpointsController(
            IUnitOfWork unitOfWork,
            IAccessContext accessContext,
            IAuthorizer authorizer,
            IEvalSessionsRepository evalSessionsRepository,
            IEvalSessionStandpointXmlsRepository evalSessionStandpointXmlsRepository)
        {
            this.unitOfWork = unitOfWork;
            this.accessContext = accessContext;
            this.authorizer = authorizer;
            this.evalSessionsRepository = evalSessionsRepository;
            this.evalSessionStandpointXmlsRepository = evalSessionStandpointXmlsRepository;
        }

        [Route("")]
        public IList<EvalSessionStandpointVO> GetEvalSessionStandpoints(
            int evalSessionId,
            int? project = null,
            [FromUri] EvalSessionStandpointStatus[] statuses = null)
        {
            this.authorizer.AssertCanDo(MyEvalSession.ViewSessionStandpoints, evalSessionId);

            return this.evalSessionsRepository.GetEvalSessionStandpoints(evalSessionId, project, null, this.accessContext.UserId, statuses);
        }

        [Route("{standpointId:int}")]
        public EvalSessionStandpointDO GetEvalSessionSheet(int evalSessionId, int standpointId)
        {
            this.authorizer.AssertCanDo(MyEvalSessionStandpointActions.Edit, standpointId);

            var evalSession = this.evalSessionsRepository.Find(evalSessionId);

            var evalSessionStandpoint = evalSession.FindEvalSessionStandpoint(standpointId);

            var evalSessionStandpointXml = this.evalSessionStandpointXmlsRepository.FindByEvalSessionStandpointId(standpointId);

            return new EvalSessionStandpointDO(evalSessionStandpoint, evalSession.Version, evalSessionStandpointXml);
        }
    }
}
