using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.CertAuthorityChecks
{
    public enum CertAuthorityCheckType
    {
        [Description("Планирана")]
        Planned = 1,

        [Description("Извънредна")]
        NotPlanned = 2
    }
}
