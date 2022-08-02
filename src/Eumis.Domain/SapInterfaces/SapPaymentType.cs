using Eumis.Common.Json;

namespace Eumis.Domain.SapInterfaces
{
    public enum SapPaymentType
    {
        [Description(Description = nameof(DomainEnumTexts.SapPaymentType_Advance), ResourceType = typeof(DomainEnumTexts))]
        Advance = 1,

        [Description(Description = nameof(DomainEnumTexts.SapPaymentType_Intermediate), ResourceType = typeof(DomainEnumTexts))]
        Intermediate = 2,

        [Description(Description = nameof(DomainEnumTexts.SapPaymentType_Final), ResourceType = typeof(DomainEnumTexts))]
        Final = 3,

        [Description(Description = nameof(DomainEnumTexts.SapPaymentType_Fine), ResourceType = typeof(DomainEnumTexts))]
        Fine = 4,

        [Description(Description = nameof(DomainEnumTexts.SapPaymentType_Interest), ResourceType = typeof(DomainEnumTexts))]
        Interest = 5,

        [Description(Description = nameof(DomainEnumTexts.SapPaymentType_VoluntaryReimbursement), ResourceType = typeof(DomainEnumTexts))]
        VoluntaryReimbursement = 6,

        [Description(Description = nameof(DomainEnumTexts.SapPaymentType_MistakeReimbursement), ResourceType = typeof(DomainEnumTexts))]
        MistakeReimbursement = 7,

        [Description(Description = nameof(DomainEnumTexts.SapPaymentType_IrregularityReimbursement), ResourceType = typeof(DomainEnumTexts))]
        IrregularityReimbursement = 8,

        [Description(Description = nameof(DomainEnumTexts.SapPaymentType_Bank), ResourceType = typeof(DomainEnumTexts))]
        Bank = 9,

        [Description(Description = nameof(DomainEnumTexts.SapPaymentType_Transfer), ResourceType = typeof(DomainEnumTexts))]
        Transfer = 10,
    }
}
