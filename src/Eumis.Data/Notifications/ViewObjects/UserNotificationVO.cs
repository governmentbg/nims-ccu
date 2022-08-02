using Eumis.Common;
using System;

namespace Eumis.Data.Notifications.ViewObjects
{
    public class UserNotificationVO
    {
        public int UserNotificationId { get; set; }

        public string EventName { get; set; }

        public int EventId { get; set; }

        public string DispatcherPath { get; set; }

        public int DispatcherId { get; set; }

        public bool IsRead { get; set; }

        public DateTime CreateDate { get; set; }

        public int CreateTime
        {
            get
            {
                return this.CreateDate.ConvertHoursToMilliseconds();
            }
        }

        public string ProgrammeCode { get; set; }

        public string ProgrammePriorityCode { get; set; }

        public string ProcedureCode { get; set; }

        public string ContractCode { get; set; }
    }
}
