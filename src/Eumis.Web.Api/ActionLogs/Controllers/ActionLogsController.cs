using System;
using System.Collections.Generic;
using System.Web.Http;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Data;
using Eumis.Data.ActionLogs.Repositories;
using Eumis.Data.ActionLogs.ViewObjects;
using Eumis.Data.Users.Repositories;
using Eumis.Log.ActionLogger;
using Eumis.Log.ActionLogger.Enums;

namespace Eumis.Web.Api.ActionLogs.Controllers
{
    [RoutePrefix("api/actionLogs")]
    public class ActionLogsController : ApiController
    {
        private IActionLogsRepository actionLogsRepository;
        private IUsersRepository usersRepository;
        private IAuthorizer authorizer;

        public ActionLogsController(
            IActionLogsRepository actionLogsRepository,
            IUsersRepository usersRepository,
            IAuthorizer authorizer)
        {
            this.actionLogsRepository = actionLogsRepository;
            this.usersRepository = usersRepository;
            this.authorizer = authorizer;
        }

        [Route("internal")]
        public IList<ActionLogVO> GetInternalActionLogs(
            bool procedureActionsOnly = false,
            int? actionId = null,
            int? aggregateRootId = null,
            string username = null,
            string remoteIpAddress = null,
            DateTime? logDate = null)
        {
            this.authorizer.AssertCanDo(ActionLogActions.View);

            return this.actionLogsRepository.GetInternalActionLogs(procedureActionsOnly, actionId, aggregateRootId, username, remoteIpAddress, logDate);
        }

        [Route("portal")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public IList<ActionLogVO> GetPortalActionLogs(
            int? actionId = null,
            int? aggregateRootId = null,
            string username = null,
            string email = null,
            string remoteIpAddress = null,
            DateTime? logDate = null)
        {
            this.authorizer.AssertCanDo(ActionLogActions.View);

            return this.actionLogsRepository.GetPortalActionLogs(actionId, aggregateRootId, email, remoteIpAddress, logDate);
        }

        [Route("unsuccessfulLogin")]
        public IList<ActionLogVO> GetUnsuccessfulLoginActionLogs(
            string username = null,
            string remoteIpAddress = null,
            DateTime? logDate = null)
        {
            this.authorizer.AssertCanDo(ActionLogActions.View);

            return this.actionLogsRepository.GetUnsuccessfulLoginActionLogs(username, remoteIpAddress, logDate);
        }

        [Route("{actionLogId:int}")]
        public ActionLogDataVO GetActionLog(int actionLogId)
        {
            this.authorizer.AssertCanDo(ActionLogActions.View);

            var actionLog = this.actionLogsRepository.GetActionLogData(actionLogId);

            if (actionLog == null || !actionLog.Action.StartsWith(ActionLogGroupUtils.GetClassDescriptionKey(typeof(ActionLogGroups.Procedures))))
            {
                throw new DataObjectNotFoundException("ActionLog", actionLogId);
            }

            return actionLog;
        }
    }
}
