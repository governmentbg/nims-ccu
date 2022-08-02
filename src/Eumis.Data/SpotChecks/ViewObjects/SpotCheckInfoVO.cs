using Eumis.Common.Json;
using Eumis.Domain.SpotChecks;
using Newtonsoft.Json;

namespace Eumis.Data.SpotChecks.ViewObjects
{
    public class SpotCheckInfoVO
    {
        public string ProgrammeCode { get; set; }

        public SpotCheckStatus Status { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public SpotCheckStatus StatusDescr { get; set; }

        public SpotCheckLevel Level { get; set; }

        public byte[] Version { get; set; }
    }
}
