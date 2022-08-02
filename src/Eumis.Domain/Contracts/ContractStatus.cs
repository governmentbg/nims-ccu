using Eumis.Common.Json;

namespace Eumis.Domain.Contracts
{
    public enum ContractStatus
    {
        [Description(Description = nameof(DomainEnumTexts.ContractStatus_Draft), ResourceType = typeof(DomainEnumTexts))]
        Draft = 1,

        [Description(Description = nameof(DomainEnumTexts.ContractStatus_Entered), ResourceType = typeof(DomainEnumTexts))]
        Entered = 2,
    }
}
