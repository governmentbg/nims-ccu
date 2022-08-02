using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.Contracts
{
    public enum Source
    {
        [Description("Бенефициент")]
        Beneficiary = 1,

        [Description("УО")]
        AdministrativeAuthority = 2
    }
}
