using Eumis.Common.Json;

namespace Eumis.Domain.Contracts
{
    public enum ContractReportType
    {
        [Description(Description = nameof(DomainEnumTexts.ContractReportType_AdvancePayment), ResourceType = typeof(DomainEnumTexts))]
        AdvancePayment = 1,

        [Description(Description = nameof(DomainEnumTexts.ContractReportType_Technical), ResourceType = typeof(DomainEnumTexts))]
        Technical = 2,

        [Description(Description = nameof(DomainEnumTexts.ContractReportType_PaymentTechnicalFinancial), ResourceType = typeof(DomainEnumTexts))]
        PaymentTechnicalFinancial = 3,

        [Description(Description = nameof(DomainEnumTexts.ContractReportType_PaymentFinancial), ResourceType = typeof(DomainEnumTexts))]
        PaymentFinancial = 4,

        [Description(Description = nameof(DomainEnumTexts.ContractReportType_Financial), ResourceType = typeof(DomainEnumTexts))]
        Financial = 5,
    }
}
