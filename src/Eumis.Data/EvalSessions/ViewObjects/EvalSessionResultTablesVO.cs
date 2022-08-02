using Eumis.Common.Json;
using Eumis.Domain.EvalSessions;
using Newtonsoft.Json;
using System;

namespace Eumis.Data.EvalSessions.ViewObjects
{
    public class EvalSessionResultTablesVO
    {
        public int EvalSessionResultId { get; set; }

        public int EvalSessionId { get; set; }

        public int OrderNum { get; set; }

        public EvalSessionResultStatus Status { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public EvalSessionResultStatus StatusDesc
        {
            get
            {
                return this.Status;
            }
        }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public EvalSessionResultType TypeDesc
        {
            get
            {
                return this.Type;
            }
        }

        public EvalSessionResultType Type { get; set; }

        public string StatusNote { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
