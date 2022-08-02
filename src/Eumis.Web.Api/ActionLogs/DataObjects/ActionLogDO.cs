using System;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.Procedures;
using Eumis.Log.ActionLogger;
using Eumis.Web.Api.Core;
using Newtonsoft.Json;

namespace Eumis.Web.Api.ActionLogs.DataObjects
{
    public class ActionLogDO
    {
        public ActionLogDO(int actionLogId, string action, int? aggregateRootId, string username, string remoteIpAddress, string postData, DateTime logDate)
        {
            this.ActionLogId = actionLogId;
            this.AggregateRootId = aggregateRootId;
            this.Username = username;
            this.RemoteIpAddress = remoteIpAddress;
            this.PostData = postData;
            this.LogDate = logDate;
            this.Action = ActionLogGroupUtils.GetActionLogInfoByKey(action).DisplayName;
        }

        public int ActionLogId { get; set; }

        public string Action { get; set; }

        public int? AggregateRootId { get; set; }

        public string Username { get; set; }

        public string RemoteIpAddress { get; set; }

        public string PostData { get; set; }

        public DateTime LogDate { get; set; }
    }
}
