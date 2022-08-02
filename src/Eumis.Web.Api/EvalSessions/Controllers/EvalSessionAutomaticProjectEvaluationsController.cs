using Eumis.ApplicationServices.Services.EvalSessionAutomaticProjectEvaluation;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Domain.Core;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.Core;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.EvalSessions.Controllers
{
    [RoutePrefix("api/evalSessions/{evalSessionId:int}/automaticProjectEvaluations")]
    public class EvalSessionAutomaticProjectEvaluationsController : ApiController
    {
        private IAuthorizer authorizer;
        private IEvalSessionAutomaticProjectEvaluationService evalSessionAutomaticProjectEvaluationService;

        public EvalSessionAutomaticProjectEvaluationsController(
            IAuthorizer authorizer,
            IEvalSessionAutomaticProjectEvaluationService evalSessionAutomaticProjectEvaluationService)
        {
            this.authorizer = authorizer;
            this.evalSessionAutomaticProjectEvaluationService = evalSessionAutomaticProjectEvaluationService;
        }

        [HttpPost]
        [Route("canExecute")]
        public ErrorsDO CanExecuteEvalSessionAutomaticProjectEvaluation(int evalSessionId)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.EditSessionData, evalSessionId);

            IList<string> errorList = this.evalSessionAutomaticProjectEvaluationService.CanExecuteEvalSessionAutomaticProjectEvaluation(evalSessionId);

            return new ErrorsDO(errorList);
        }

        [HttpPost]
        [Route("")]
        [ActionLog(Action = typeof(ActionLogGroups.EvalSessions.Edit.ExecuteAutomaticProjectEvaluation))]
        public ErrorsDO ExecuteEvalSessionAutomaticProjectEvaluation(int evalSessionId, string version, FileDO file)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.EditSessionData, evalSessionId);

            byte[] vers = System.Convert.FromBase64String(version);

            return new ErrorsDO(this.evalSessionAutomaticProjectEvaluationService.ExecuteEvalSessionAutomaticProjectEvaluation(evalSessionId, vers, file.Key));
        }
    }
}
