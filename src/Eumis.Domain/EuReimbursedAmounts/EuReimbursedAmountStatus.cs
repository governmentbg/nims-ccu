using Eumis.Common.Json;

namespace Eumis.Domain.EuReimbursedAmounts
{
    public enum EuReimbursedAmountStatus
    {
        [Description(Description = nameof(DomainEnumTexts.EuReimbursedAmountStatus_Draft), ResourceType = typeof(DomainEnumTexts))]
        Draft = 1,

        [Description(Description = nameof(DomainEnumTexts.EuReimbursedAmountStatus_Entered), ResourceType = typeof(DomainEnumTexts))]
        Entered = 2,

        [Description(Description = nameof(DomainEnumTexts.EuReimbursedAmountStatus_Removed), ResourceType = typeof(DomainEnumTexts))]
        Removed = 3,
    }
}
