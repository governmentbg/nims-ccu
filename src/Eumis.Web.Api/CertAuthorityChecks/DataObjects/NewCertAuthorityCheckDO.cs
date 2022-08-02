using Eumis.Domain.CertAuthorityChecks;

namespace Eumis.Web.Api.CertAuthorityChecks.DataObjects
{
    public class NewCertAuthorityCheckDO
    {
        public CertAuthorityCheckLevel? Level { get; set; }

        public CertAuthorityCheckKind? Kind { get; set; }

        public CertAuthorityCheckType? Type { get; set; }
    }
}
