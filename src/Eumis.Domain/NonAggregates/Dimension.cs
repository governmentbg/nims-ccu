using Eumis.Common.Json;

namespace Eumis.Domain.NonAggregates
{
    public enum Dimension
    {
        [Description(Description = nameof(DomainEnumTexts.Dimension_InterventionField), ResourceType = typeof(DomainEnumTexts))]
        InterventionField = 1,

        [Description(Description = nameof(DomainEnumTexts.Dimension_FormOfFinance), ResourceType = typeof(DomainEnumTexts))]
        FormOfFinance = 2,

        [Description(Description = nameof(DomainEnumTexts.Dimension_TerritorialDimension), ResourceType = typeof(DomainEnumTexts))]
        TerritorialDimension = 3,

        [Description(Description = nameof(DomainEnumTexts.Dimension_TerritorialDeliveryMechanism), ResourceType = typeof(DomainEnumTexts))]
        TerritorialDeliveryMechanism = 4,

        [Description(Description = nameof(DomainEnumTexts.Dimension_ThematicObjective), ResourceType = typeof(DomainEnumTexts))]
        ThematicObjective = 5,

        [Description(Description = nameof(DomainEnumTexts.Dimension_ESFSecondaryTheme), ResourceType = typeof(DomainEnumTexts))]
        ESFSecondaryTheme = 6,

        [Description(Description = nameof(DomainEnumTexts.Dimension_EconomicDimension), ResourceType = typeof(DomainEnumTexts))]
        EconomicDimension = 7,
    }
}
