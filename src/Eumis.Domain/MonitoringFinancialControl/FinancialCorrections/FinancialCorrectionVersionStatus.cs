using Eumis.Common.Json;

namespace Eumis.Domain.MonitoringFinancialControl.FinancialCorrections
{
    public enum FinancialCorrectionVersionStatus
    {
        [Description(Description = nameof(DomainEnumTexts.FinancialCorrectionVersionStatus_Draft), ResourceType = typeof(DomainEnumTexts))]
        Draft = 1,

        [Description(Description = nameof(DomainEnumTexts.FinancialCorrectionVersionStatus_Actual), ResourceType = typeof(DomainEnumTexts))]
        Actual = 2,

        [Description(Description = nameof(DomainEnumTexts.FinancialCorrectionVersionStatus_Archived), ResourceType = typeof(DomainEnumTexts))]
        Archived = 3,
    }
}