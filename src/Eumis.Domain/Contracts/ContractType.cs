using Eumis.Common.Json;

namespace Eumis.Domain.Contracts
{
    public enum ContractType
    {
        [Description(Description = nameof(DomainEnumTexts.ContractType_Contract), ResourceType = typeof(DomainEnumTexts))]
        Contract = 3,

        [Description(Description = nameof(DomainEnumTexts.ContractType_ServiceAgreement), ResourceType = typeof(DomainEnumTexts))]
        ServiceAgreement = 8,
    }
}
