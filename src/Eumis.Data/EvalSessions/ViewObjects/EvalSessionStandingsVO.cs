using Eumis.Common.Json;
using Eumis.Domain.EvalSessions;
using Newtonsoft.Json;
using System;

namespace Eumis.Data.EvalSessions.ViewObjects
{
    public class EvalSessionStandingsVO
    {
        public int EvalSessionId { get; set; }

        public int EvalSessionStandingId { get; set; }

        public string Code { get; set; }

        public bool IsPreliminary { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public EvalSessionStandingStatus Status { get; set; }

        public EvalSessionStandingStatus StatusName { get; set; }

        public string StatusNote { get; set; }

        public DateTime StatusDate { get; set; }
    }
}
