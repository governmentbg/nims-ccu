using Eumis.Common.Json;

namespace Eumis.Domain.MonitoringFinancialControl.ReimbursedAmounts
{
    public enum ReimbursementType
    {
        [Description(Description = nameof(DomainEnumTexts.ReimbursementType_ExGratia), ResourceType = typeof(DomainEnumTexts))]
        ExGratia = 1,

        [Description(Description = nameof(DomainEnumTexts.ReimbursementType_NAP), ResourceType = typeof(DomainEnumTexts))]
        NAP = 2,

        [Description(Description = nameof(DomainEnumTexts.ReimbursementType_Collateral), ResourceType = typeof(DomainEnumTexts))]
        Collateral = 3,

        [Description(Description = nameof(DomainEnumTexts.ReimbursementType_Other), ResourceType = typeof(DomainEnumTexts))]
        Other = 4,
    }
}
