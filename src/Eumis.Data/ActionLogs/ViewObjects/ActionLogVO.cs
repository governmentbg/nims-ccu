using System;

namespace Eumis.Data.ActionLogs.ViewObjects
{
    public class ActionLogVO
    {
        public int ActionLogId { get; set; }

        public string Action { get; set; }

        public int? AggregateRootId { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public Guid? RequestId { get; set; }

        public string RemoteIpAddress { get; set; }

        public DateTime LogDate { get; set; }
    }
}
