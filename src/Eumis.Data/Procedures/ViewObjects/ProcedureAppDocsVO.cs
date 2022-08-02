using Eumis.Common.Json;
using Eumis.Domain.Procedures;
using Newtonsoft.Json;

namespace Eumis.Data.Procedures.ViewObjects
{
    public class ProcedureAppDocsVO
    {
        public int ProcedureApplicationDocId { get; set; }

        public int ProcedureId { get; set; }

        public string Name { get; set; }

        public string Extension { get; set; }

        public bool IsRequired { get; set; }

        public bool IsSignatureRequired { get; set; }

        public bool IsActive { get; set; }

        public bool IsActivated { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ActiveStatus ActiveStatus { get; set; }
    }
}
