using Eumis.Common.Json;

namespace Eumis.Domain.Contracts
{
    public enum ContractCommunicationSource
    {
        [Description(Description = nameof(DomainEnumTexts.ContractCommunicationSource_Beneficiary), ResourceType = typeof(DomainEnumTexts))]
        Beneficiary = 1,

        [Description(Description = nameof(DomainEnumTexts.ContractCommunicationSource_AdministrativeAuthority), ResourceType = typeof(DomainEnumTexts))]
        AdministrativeAuthority = 2,

        [Description(Description = nameof(DomainEnumTexts.ContractCommunicationSource_AuditAuthority), ResourceType = typeof(DomainEnumTexts))]
        AuditAuthority = 3,

        [Description(Description = nameof(DomainEnumTexts.ContractCommunicationSource_CertAuthority), ResourceType = typeof(DomainEnumTexts))]
        CertAuthority = 4,
    }
}
