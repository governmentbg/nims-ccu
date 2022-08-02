using Eumis.Common.Json;
using Eumis.Domain.EvalSessions;
using Newtonsoft.Json;
using System;

namespace Eumis.Data.EvalSessions.ViewObjects
{
    public class EvalSessionProjectStandingVO
    {
        public int EvalSessionId { get; set; }

        public int EvalSessionProjectStandingId { get; set; }

        public int ProjectId { get; set; }

        public bool IsPreliminary { get; set; }

        public int? OrderNum { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public EvalSessionProjectStandingType Type { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public EvalSessionProjectStandingStatus Status { get; set; }

        public decimal? GrandAmount { get; set; }

        public bool IsDeleted { get; set; }

        public string IsDeletedNote { get; set; }

        public string Notes { get; set; }

        public DateTime CreateDate { get; set; }

        public string EvalSessionNum { get; set; }
    }
}
