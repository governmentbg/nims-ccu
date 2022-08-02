using Eumis.Common.Json;

namespace Eumis.Domain.Contracts
{
    public enum ContractSubcontractType
    {
        [Description(Description = nameof(DomainEnumTexts.ContractSubcontractType_Subcontractor), ResourceType = typeof(DomainEnumTexts))]
        Subcontractor = 1,

        [Description(Description = nameof(DomainEnumTexts.ContractSubcontractType_Member), ResourceType = typeof(DomainEnumTexts))]
        Member = 2,
    }
}
