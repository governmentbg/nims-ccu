using Eumis.Common.Json;

namespace Eumis.Domain.Contracts
{
    public enum ContractVersionStatus
    {
        [Description(Description = nameof(DomainEnumTexts.ContractVersionStatus_Draft), ResourceType = typeof(DomainEnumTexts))]
        Draft = 1,

        [Description(Description = nameof(DomainEnumTexts.ContractVersionStatus_Entered), ResourceType = typeof(DomainEnumTexts))]
        Entered = 2,

        [Description(Description = nameof(DomainEnumTexts.ContractVersionStatus_Active), ResourceType = typeof(DomainEnumTexts))]
        Active = 3,

        [Description(Description = nameof(DomainEnumTexts.ContractVersionStatus_Archived), ResourceType = typeof(DomainEnumTexts))]
        Archived = 4,
    }
}
