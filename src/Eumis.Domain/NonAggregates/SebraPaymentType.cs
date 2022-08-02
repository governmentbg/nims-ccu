using Eumis.Common.Json;

namespace Eumis.Domain.NonAggregates
{
    public enum SebraPaymentType
    {
        [Description(Description = nameof(DomainEnumTexts.SebraPaymentType_CurrentSubsidiesForEnterprises), ResourceType = typeof(DomainEnumTexts))]
        CurrentSubsidiesForEnterprises = 30,

        [Description(Description = nameof(DomainEnumTexts.SebraPaymentType_PaymentsForFixedAssetsMajorRepairsAndCapitalTransfers), ResourceType = typeof(DomainEnumTexts))]
        PaymentsForFixedAssetsMajorRepairsAndCapitalTransfers = 50,
    }
}
