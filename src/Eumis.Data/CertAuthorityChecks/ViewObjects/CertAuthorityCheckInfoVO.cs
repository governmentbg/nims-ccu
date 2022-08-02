using Eumis.Common.Json;
using Eumis.Domain.CertAuthorityChecks;
using Newtonsoft.Json;

namespace Eumis.Data.CertAuthorityChecks.ViewObjects
{
    public class CertAuthorityCheckInfoVO
    {
        public CertAuthorityCheckLevel Level { get; set; }

        public CertAuthorityCheckStatus Status { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public CertAuthorityCheckStatus StatusDescr { get; set; }

        public byte[] Version { get; set; }
    }
}
