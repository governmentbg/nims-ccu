using Eumis.Common.Json;

namespace Eumis.Domain.Contracts
{
    public enum ContractReportPaymentStatus
    {
        [Description(Description = nameof(DomainEnumTexts.ContractReportPaymentStatus_Draft), ResourceType = typeof(DomainEnumTexts))]
        Draft = 1,

        [Description(Description = nameof(DomainEnumTexts.ContractReportPaymentStatus_Entered), ResourceType = typeof(DomainEnumTexts))]
        Entered = 2,

        [Description(Description = nameof(DomainEnumTexts.ContractReportPaymentStatus_Actual), ResourceType = typeof(DomainEnumTexts))]
        Actual = 3,

        [Description(Description = nameof(DomainEnumTexts.ContractReportPaymentStatus_Returned), ResourceType = typeof(DomainEnumTexts))]
        Returned = 4,
    }
}