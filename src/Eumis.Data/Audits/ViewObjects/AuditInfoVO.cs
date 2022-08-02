using Eumis.Common.Json;
using Eumis.Domain.Audits;
using Newtonsoft.Json;

namespace Eumis.Data.Audits.ViewObjects
{
    public class AuditInfoVO
    {
        [JsonConverter(typeof(EnumDescriptionConverter))]
        public AuditLevel LevelDescr { get; set; }

        public AuditLevel Level { get; set; }

        public string ProgrammeCode { get; set; }

        public byte[] Version { get; set; }
    }
}
