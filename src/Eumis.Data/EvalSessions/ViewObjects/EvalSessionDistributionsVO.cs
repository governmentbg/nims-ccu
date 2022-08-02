using Eumis.Common.Json;
using Eumis.Domain.EvalSessions;
using Eumis.Domain.Procedures;
using Newtonsoft.Json;
using System;

namespace Eumis.Data.EvalSessions.ViewObjects
{
    public class EvalSessionDistributionsVO
    {
        public int EvalSessionId { get; set; }

        public int EvalSessionDistributionId { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ProcedureEvalTableType? EvalTableType { get; set; }

        public string Code { get; set; }

        public DateTime CreateDate { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public EvalSessionDistributionStatus? StatusName { get; set; }

        public EvalSessionDistributionStatus Status { get; set; }

        public string StatusNote { get; set; }

        public int AssessorsPerProject { get; set; }
    }
}
