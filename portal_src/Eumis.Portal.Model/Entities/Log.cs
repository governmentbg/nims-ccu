using System;

namespace Eumis.Portal.Model.Entities
{
    public partial class Log
    {
        public int LogId { get; set; }
        public string Level { get; set; }
        public Nullable<System.DateTime> LogDate { get; set; }
        public string IP { get; set; }
        public string RawUrl { get; set; }
        public string Form { get; set; }
        public string UserAgent { get; set; }
        public string SessionId { get; set; }
        public Nullable<System.Guid> RequestId { get; set; }
        public string Message { get; set; }
    }
}
