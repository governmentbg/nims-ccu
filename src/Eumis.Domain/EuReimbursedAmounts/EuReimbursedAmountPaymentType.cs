using Eumis.Common.Json;

namespace Eumis.Domain.EuReimbursedAmounts
{
    public enum EuReimbursedAmountPaymentType
    {
        [Description(Description = nameof(DomainEnumTexts.EuReimbursedAmountPaymentType_Advance), ResourceType = typeof(DomainEnumTexts))]
        Advance = 1,

        [Description(Description = nameof(DomainEnumTexts.EuReimbursedAmountPaymentType_Intermediate), ResourceType = typeof(DomainEnumTexts))]
        Intermediate = 2,

        [Description(Description = nameof(DomainEnumTexts.EuReimbursedAmountPaymentType_Final), ResourceType = typeof(DomainEnumTexts))]
        Final = 3,
    }
}
