using Eumis.Common.Json;

namespace Eumis.Domain.MonitoringFinancialControl.FlatFinancialCorrections
{
    public enum FlatFinancialCorrectionStatus
    {
        [Description(Description = nameof(DomainEnumTexts.FlatFinancialCorrectionStatus_Draft), ResourceType = typeof(DomainEnumTexts))]
        Draft = 1,

        [Description(Description = nameof(DomainEnumTexts.FlatFinancialCorrectionStatus_Actual), ResourceType = typeof(DomainEnumTexts))]
        Actual = 2,
    }
}