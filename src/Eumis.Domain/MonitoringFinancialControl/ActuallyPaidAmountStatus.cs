using Eumis.Common.Json;

namespace Eumis.Domain.MonitoringFinancialControl
{
    public enum ActuallyPaidAmountStatus
    {
        [Description(Description = nameof(DomainEnumTexts.ActuallyPaidAmountStatus_Draft), ResourceType = typeof(DomainEnumTexts))]
        Draft = 1,

        [Description(Description = nameof(DomainEnumTexts.ActuallyPaidAmountStatus_Entered), ResourceType = typeof(DomainEnumTexts))]
        Entered = 2,

        [Description(Description = nameof(DomainEnumTexts.ActuallyPaidAmountStatus_Deleted), ResourceType = typeof(DomainEnumTexts))]
        Deleted = 3,
    }
}
