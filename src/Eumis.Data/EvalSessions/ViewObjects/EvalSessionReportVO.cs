using System;
using Eumis.Common.Json;
using Eumis.Domain.EvalSessions;
using Newtonsoft.Json;

namespace Eumis.Data.EvalSessions.ViewObjects
{
    public class EvalSessionReportVO
    {
        public int EvalSessionId { get; set; }

        public int EvalSessionReportId { get; set; }

        public string RegNumber { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public EvalSessionReportType Type { get; set; }

        public string Description { get; set; }

        public bool IsDeleted { get; set; }

        public string IsDeletedNote { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
