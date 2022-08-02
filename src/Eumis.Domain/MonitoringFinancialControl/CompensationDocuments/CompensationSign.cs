using Eumis.Common.Json;

namespace Eumis.Domain.MonitoringFinancialControl.CompensationDocuments
{
    public enum CompensationSign
    {
        [Description(Description = nameof(DomainEnumTexts.CompensationSign_Plus), ResourceType = typeof(DomainEnumTexts))]
        Plus = 1,

        [Description(Description = nameof(DomainEnumTexts.CompensationSign_Minus), ResourceType = typeof(DomainEnumTexts))]
        Minus = -1,
    }
}
