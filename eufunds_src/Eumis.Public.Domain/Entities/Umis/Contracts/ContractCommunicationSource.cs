using System.ComponentModel;
namespace Eumis.Public.Domain.Entities.Umis.Contracts
{
    public enum ContractCommunicationSource
    {
        [Description("Бенефициент")]
        Beneficiary = 1,

        [Description("УО")]
        AdministrativeAuthority = 2,

        [Description("Одитен орган")]
        AuditAuthority = 3,

        [Description("Серт. орган")]
        CertAuthority = 4
    }
}
