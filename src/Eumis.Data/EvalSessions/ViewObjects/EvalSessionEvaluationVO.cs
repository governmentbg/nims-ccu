using Eumis.Common.Json;
using Eumis.Domain.EvalSessions;
using Eumis.Domain.Procedures;
using Newtonsoft.Json;
using System;

namespace Eumis.Data.EvalSessions.ViewObjects
{
    public class EvalSessionEvaluationVO
    {
        public int EvalSessionId { get; set; }

        public int EvalSessionEvaluationId { get; set; }

        public int ProjectId { get; set; }

        public ProcedureEvalTableType EvalTableType { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ProcedureEvalTableTypeShort? EvalTableTypeName { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public EvalSessionEvaluationCalculationType? CalculationType { get; set; }

        public ProcedureEvalType EvalType { get; set; }

        public bool? EvalIsPassed { get; set; }

        public decimal? EvalPoints { get; set; }

        public string EvalNote { get; set; }

        public bool IsDeleted { get; set; }

        public string IsDeletedNote { get; set; }

        public DateTime CreateDate { get; set; }

        public string EvalSessionNum { get; set; }
    }
}
