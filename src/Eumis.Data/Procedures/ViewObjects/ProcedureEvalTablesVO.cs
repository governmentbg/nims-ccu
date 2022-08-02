using System;
using Eumis.Common.Json;
using Eumis.Domain.Procedures;
using Newtonsoft.Json;

namespace Eumis.Data.Procedures.ViewObjects
{
    public class ProcedureEvalTablesVO
    {
        public int ProcedureEvalTableId { get; set; }

        public int ProcedureId { get; set; }

        public string Name { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ProcedureEvalTableType Type { get; set; }

        public Guid XmlGid { get; set; }

        public bool IsActive { get; set; }

        public bool IsActivated { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ActiveStatus ActiveStatus { get; set; }
    }
}
