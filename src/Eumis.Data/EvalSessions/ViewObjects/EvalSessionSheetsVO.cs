using Eumis.Common.Json;
using Eumis.Common.Localization;
using Eumis.Domain.EvalSessions;
using Eumis.Domain.Procedures;
using Newtonsoft.Json;
using System;

namespace Eumis.Data.EvalSessions.ViewObjects
{
    public class EvalSessionSheetsVO
    {
        public int EvalSessionId { get; set; }

        public int EvalSessionSheetId { get; set; }

        public string ProjectRegNumber { get; set; }

        public string ProjectName
        {
            get
            {
                return SystemLocalization.GetDisplayName(this.ProjectNameBg, this.ProjectNameEn);
            }
        }

        [JsonIgnore]
        public string ProjectNameBg { get; set; }

        [JsonIgnore]
        public string ProjectNameEn { get; set; }

        public string Assessor { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ProcedureEvalTableTypeShort? EvalTableTypeName { get; set; }

        public ProcedureEvalTableType EvalTableType { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public EvalSessionDistributionType? DistributionType { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public EvalSessionSheetStatus? StatusName { get; set; }

        public EvalSessionSheetStatus Status { get; set; }

        public Guid XmlGid { get; set; }

        public ProcedureEvalType EvalType { get; set; }

        public bool? EvalIsPassed { get; set; }

        public decimal? EvalPoints { get; set; }

        public string EvalNote { get; set; }

        public string NoteTFO { get; set; }
    }
}
