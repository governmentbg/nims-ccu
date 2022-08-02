using Eumis.Common.Json;

namespace Eumis.Domain.MonitoringFinancialControl.ReimbursedAmounts
{
    public enum ReimbursedAmountStatus
    {
        [Description(Description = nameof(DomainEnumTexts.ReimbursedAmountStatus_Draft), ResourceType = typeof(DomainEnumTexts))]
        Draft = 1,

        [Description(Description = nameof(DomainEnumTexts.ReimbursedAmountStatus_Entered), ResourceType = typeof(DomainEnumTexts))]
        Entered = 2,

        [Description(Description = nameof(DomainEnumTexts.ReimbursedAmountStatus_Deleted), ResourceType = typeof(DomainEnumTexts))]
        Deleted = 3,
    }
}
