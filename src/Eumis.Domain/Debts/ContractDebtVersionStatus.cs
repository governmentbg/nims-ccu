using Eumis.Common.Json;

namespace Eumis.Domain.Debts
{
    public enum ContractDebtVersionStatus
    {
        [Description(Description = nameof(DomainEnumTexts.ContractDebtVersionStatus_Draft), ResourceType = typeof(DomainEnumTexts))]
        Draft = 1,

        [Description(Description = nameof(DomainEnumTexts.ContractDebtVersionStatus_Actual), ResourceType = typeof(DomainEnumTexts))]
        Actual = 2,

        [Description(Description = nameof(DomainEnumTexts.ContractDebtVersionStatus_Archived), ResourceType = typeof(DomainEnumTexts))]
        Archived = 3,
    }
}
