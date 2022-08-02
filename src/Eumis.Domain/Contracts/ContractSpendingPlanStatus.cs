using Eumis.Common.Json;

namespace Eumis.Domain.Contracts
{
    public enum ContractSpendingPlanStatus
    {
        [Description(Description = nameof(DomainEnumTexts.ContractSpendingPlanStatus_Draft), ResourceType = typeof(DomainEnumTexts))]
        Draft = 1,

        [Description(Description = nameof(DomainEnumTexts.ContractSpendingPlanStatus_Entered), ResourceType = typeof(DomainEnumTexts))]
        Entered = 2,

        [Description(Description = nameof(DomainEnumTexts.ContractSpendingPlanStatus_Active), ResourceType = typeof(DomainEnumTexts))]
        Active = 3,

        [Description(Description = nameof(DomainEnumTexts.ContractSpendingPlanStatus_Archived), ResourceType = typeof(DomainEnumTexts))]
        Archived = 4,
    }
}
