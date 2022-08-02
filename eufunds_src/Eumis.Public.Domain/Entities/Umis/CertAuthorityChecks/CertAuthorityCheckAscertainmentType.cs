using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.CertAuthorityChecks
{
    public enum CertAuthorityCheckAscertainmentType
    {
        [Description("Съществена")]
        Primary = 1,

        [Description("Второстепенна")]
        Secondary = 2,

        [Description("Несъществена")]
        Minor = 3,

        [Description("Информативна")]
        Informative = 4
    }
}
