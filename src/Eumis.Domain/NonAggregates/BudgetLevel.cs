using Eumis.Common.Json;

namespace Eumis.Domain.NonAggregates
{
    public enum BudgetLevel
    {
        [Description(Description = nameof(DomainEnumTexts.BudgetLevel_First), ResourceType = typeof(DomainEnumTexts))]
        First = 1,

        [Description(Description = nameof(DomainEnumTexts.BudgetLevel_Second), ResourceType = typeof(DomainEnumTexts))]
        Second = 2,
    }
}
