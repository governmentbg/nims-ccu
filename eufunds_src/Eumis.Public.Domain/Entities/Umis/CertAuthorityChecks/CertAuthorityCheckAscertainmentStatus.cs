using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.CertAuthorityChecks
{
    public enum CertAuthorityCheckAscertainmentStatus
    {
        [Description("Отворена")]
        Open = 1,

        [Description("Затворена")]
        Closed = 2,

        [Description("За информация")]
        Informative = 3
    }
}
