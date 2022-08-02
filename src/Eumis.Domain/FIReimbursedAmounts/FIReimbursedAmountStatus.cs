using Eumis.Common.Json;

namespace Eumis.Domain.FIReimbursedAmounts
{
    public enum FIReimbursedAmountStatus
    {
        [Description(Description = nameof(DomainEnumTexts.FIReimbursedAmountStatus_Draft), ResourceType = typeof(DomainEnumTexts))]
        Draft = 1,

        [Description(Description = nameof(DomainEnumTexts.FIReimbursedAmountStatus_Entered), ResourceType = typeof(DomainEnumTexts))]
        Entered = 2,

        [Description(Description = nameof(DomainEnumTexts.FIReimbursedAmountStatus_Deleted), ResourceType = typeof(DomainEnumTexts))]
        Deleted = 3,
    }
}
