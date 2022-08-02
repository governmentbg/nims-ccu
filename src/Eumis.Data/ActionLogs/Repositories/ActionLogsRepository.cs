using System;
using System.Collections.Generic;
using System.Linq;
using Eumis.Common.Db;
using Eumis.Data.ActionLogs.ViewObjects;
using Eumis.Data.Linq;
using Eumis.Domain.ActionLogs;
using Eumis.Log.ActionLogger;
using Eumis.Log.ActionLogger.Enums;

namespace Eumis.Data.ActionLogs.Repositories
{
    internal class ActionLogsRepository : IActionLogsRepository
    {
        private UnitOfWork unitOfWork;

        public ActionLogsRepository(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = (UnitOfWork)unitOfWork;
        }

        public IList<ActionLogVO> GetInternalActionLogs(
            bool procedureActionsOnly = false,
            int? actionId = null,
            int? aggregateRootId = null,
            string username = null,
            string remoteIpAddress = null,
            DateTime? logDate = null)
        {
            return this.GetInternalPortalActionLogs(
                ActionLogType.Internal,
                procedureActionsOnly,
                actionId,
                aggregateRootId,
                username,
                null,
                remoteIpAddress,
                logDate);
        }

        public IList<ActionLogVO> GetPortalActionLogs(
            int? actionId = null,
            int? aggregateRootId = null,
            string registrationEmail = null,
            string remoteIpAddress = null,
            DateTime? logDate = null)
        {
            return this.GetInternalPortalActionLogs(
                ActionLogType.Portal,
                false,
                actionId,
                aggregateRootId,
                null,
                registrationEmail,
                remoteIpAddress,
                logDate);
        }

        public List<ActionLogVO> GetUnsuccessfulLoginActionLogs(
            string username = null,
            string remoteIpAddress = null,
            DateTime? logDate = null)
        {
            var predicateActionLog =
                PredicateBuilder.True<ActionLog>()
                .And(e => e.ActionLogType == ActionLogType.UnsuccessfulLogin)
                .AndStringMatches(p => p.RemoteIpAddress, remoteIpAddress, false)
                .AndStringMatches(p => p.PostData, username, true);

            if (logDate.HasValue)
            {
                var toDate = logDate.Value.AddDays(1);
                predicateActionLog = predicateActionLog.And(e => e.LogDate >= logDate && e.LogDate < toDate);
            }

            return this.unitOfWork.DbContext.Set<ActionLog>().Where(predicateActionLog)
                .OrderByDescending(e => e.LogDate)
                .Take(1000)
                .ToList()
                .Select(e =>
                    new ActionLogVO
                    {
                        ActionLogId = e.ActionLogId,
                        Username = e.PostData,
                        RemoteIpAddress = e.RemoteIpAddress,
                        LogDate = e.LogDate,
                    }).ToList();
        }

        public ActionLogDataVO GetActionLogData(int actionLogId)
        {
            return this.unitOfWork.DbContext.Set<ActionLog>()
                .Where(e => e.ActionLogId == actionLogId)
                .Select(a =>
                    new ActionLogDataVO
                    {
                        ActionLogId = a.ActionLogId,
                        Action = a.Action,
                        AggregateRootId = a.AggregateRootId,
                        Username = a.Username,
                        PostData = a.PostData,
                        RemoteIpAddress = a.RemoteIpAddress,
                        LogDate = a.LogDate,
                    })
                .SingleOrDefault();
        }

        private IList<ActionLogVO> GetInternalPortalActionLogs(
            ActionLogType actionLogType,
            bool procedureActionsOnly = false,
            int? actionId = null,
            int? aggregateRootId = null,
            string username = null,
            string email = null,
            string remoteIpAddress = null,
            DateTime? logDate = null)
        {
            var predicateActionLog =
                PredicateBuilder.True<ActionLog>()
                .And(e => e.ActionLogType == actionLogType)
                .AndStringMatches(p => p.RemoteIpAddress, remoteIpAddress, false)
                .AndStringMatches(p => p.Username, username, true);

            if (!string.IsNullOrWhiteSpace(email))
            {
                predicateActionLog = predicateActionLog.And(
                    a => a.RegistrationEmail == email || a.ContractRegistrationEmail == email || a.ContractAccessCodeEmail == email);
            }

            if (logDate.HasValue)
            {
                var toDate = logDate.Value.AddDays(1);
                predicateActionLog = predicateActionLog.And(e => e.LogDate >= logDate && e.LogDate < toDate);
            }

            if (actionLogType == ActionLogType.Internal && procedureActionsOnly)
            {
                var proceduresRoot = ActionLogGroupUtils.GetClassDescriptionKey(typeof(ActionLogGroups.Procedures));

                var procedureGroupKey = $"{proceduresRoot}.";

                predicateActionLog = predicateActionLog.And(e => e.Action.StartsWith(procedureGroupKey));
            }

            if (actionId.HasValue)
            {
                var actionLogGroupInfo = actionLogType == ActionLogType.Internal ?
                    ActionLogGroupUtils.GetActionLogGroupInfoById(actionId.Value) :
                    ActionLogGroupUtils.GetPortalActionLogGroupInfoById(actionId.Value);

                var actionLevel2Substring = $"{actionLogGroupInfo.Key}.";

                predicateActionLog = predicateActionLog.And(e => e.Action == actionLogGroupInfo.Key || e.Action.StartsWith(actionLevel2Substring));
            }

            if (aggregateRootId.HasValue)
            {
                predicateActionLog = predicateActionLog.And(e => e.AggregateRootId == aggregateRootId);
            }

            return
                this.unitOfWork.DbContext.Set<ActionLog>()
                .Where(predicateActionLog)
                .OrderByDescending(a => a.LogDate)
                .Take(1000)
                .ToList()
                .Select(a =>
                    new ActionLogVO
                    {
                        ActionLogId = a.ActionLogId,
                        Action = actionLogType == ActionLogType.Internal ?
                            ActionLogGroupUtils.GetActionLogInfoByKey(a.Action).DisplayName :
                            ActionLogGroupUtils.GetPortalActionLogInfoByKey(a.Action).DisplayName,
                        AggregateRootId = a.AggregateRootId,
                        Username = a.Username,
                        Email = a.RegistrationEmail ?? a.ContractRegistrationEmail ?? a.ContractAccessCodeEmail,
                        RequestId = a.RequestId,
                        RemoteIpAddress = a.RemoteIpAddress,
                        LogDate = a.LogDate,
                    }).ToList();
        }
    }
}
