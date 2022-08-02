using Eumis.Common.Json;

namespace Eumis.Domain.SapInterfaces
{
    public enum SapPaidAmountFund
    {
        [Description(Description = nameof(DomainEnumTexts.SapPaidAmountFund_ESF), ResourceType = typeof(DomainEnumTexts))]
        ESF = 1,

        [Description(Description = nameof(DomainEnumTexts.SapPaidAmountFund_ERDF), ResourceType = typeof(DomainEnumTexts))]
        ERDF = 2,

        [Description(Description = nameof(DomainEnumTexts.SapPaidAmountFund_CF), ResourceType = typeof(DomainEnumTexts))]
        CF = 3,

        [Description(Description = nameof(DomainEnumTexts.SapPaidAmountFund_FEAD), ResourceType = typeof(DomainEnumTexts))]
        FEAD = 5,
    }
}
