using Eumis.Common.Json;
using Eumis.Common.Localization;
using Eumis.Domain.EvalSessions;
using Newtonsoft.Json;
using System;

namespace Eumis.Data.EvalSessions.ViewObjects
{
    public class EvalSessionsVO
    {
        public int EvalSessionId { get; set; }

        public int ProcedureId { get; set; }

        public string ProcedureName
        {
            get
            {
                return SystemLocalization.GetDisplayName(this.ProcedureNameBg, this.ProcedureNameEn);
            }
        }

        [JsonIgnore]
        public string ProcedureNameBg { get; set; }

        [JsonIgnore]
        public string ProcedureNameEn { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public EvalSessionStatus? EvalSessionStatusName { get; set; }

        public EvalSessionStatus EvalSessionStatus { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public EvalSessionType? EvalSessionType { get; set; }

        public string SessionNum { get; set; }

        public DateTime? SessionDate { get; set; }

        public string OrderNum { get; set; }

        public DateTime? OrderDate { get; set; }

        public bool IsSessionsAdmin { get; set; }

        public bool IsSessionAdminOrObserver { get; set; }

        public bool IsAssessor { get; set; }

        public bool IsReader { get; set; }
    }
}
