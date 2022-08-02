using Eumis.Common.Json;

namespace Eumis.Domain.Contracts
{
    public enum ContractReportPaymentType
    {
        [Description(Description = nameof(DomainEnumTexts.ContractReportPaymentType_Advance), ResourceType = typeof(DomainEnumTexts))]
        Advance = 1,

        [Description(Description = nameof(DomainEnumTexts.ContractReportPaymentType_AdvanceVerification), ResourceType = typeof(DomainEnumTexts))]
        AdvanceVerification = 2,

        [Description(Description = nameof(DomainEnumTexts.ContractReportPaymentType_Intermediate), ResourceType = typeof(DomainEnumTexts))]
        Intermediate = 3,

        [Description(Description = nameof(DomainEnumTexts.ContractReportPaymentType_Final), ResourceType = typeof(DomainEnumTexts))]
        Final = 4,
    }
}