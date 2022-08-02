using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.CertAuthorityChecks
{
    public enum CertAuthorityCheckStatus
    {
        [Description("Чернова")]
        Draft = 1,

        [Description("Въведена")]
        Entered = 2,

        [Description("Анулирана")]
        Removed = 3
    }
}
