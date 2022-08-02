using System;
using System.Collections.Generic;
using Eumis.Data.ActionLogs.ViewObjects;
using Eumis.Domain.ActionLogs;

namespace Eumis.Data.ActionLogs.Repositories
{
    public interface IActionLogsRepository
    {
        IList<ActionLogVO> GetInternalActionLogs(
            bool procedureActionsOnly = false,
            int? actionId = null,
            int? aggregateRootId = null,
            string username = null,
            string remoteIpAddress = null,
            DateTime? logDate = null);

        IList<ActionLogVO> GetPortalActionLogs(
            int? actionId = null,
            int? aggregateRootId = null,
            string email = null,
            string remoteIpAddress = null,
            DateTime? logDate = null);

        List<ActionLogVO> GetUnsuccessfulLoginActionLogs(
            string username = null,
            string remoteIpAddress = null,
            DateTime? logDate = null);

        ActionLogDataVO GetActionLogData(int actionLogId);
    }
}
