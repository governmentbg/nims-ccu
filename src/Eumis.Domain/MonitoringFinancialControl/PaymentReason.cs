using Eumis.Common.Json;

namespace Eumis.Domain.MonitoringFinancialControl
{
    public enum PaymentReason
    {
        [Description(Description = nameof(DomainEnumTexts.PaymentReason_AdvancePayment), ResourceType = typeof(DomainEnumTexts))]
        AdvancePayment = 1,

        [Description(Description = nameof(DomainEnumTexts.PaymentReason_ApprovedExpenses), ResourceType = typeof(DomainEnumTexts))]
        ApprovedExpenses = 2,

        [Description(Description = nameof(DomainEnumTexts.PaymentReason_ReimbursedExpenses), ResourceType = typeof(DomainEnumTexts))]
        ReimbursedExpenses = 3,

        [Description(Description = nameof(DomainEnumTexts.PaymentReason_AdditionalPayment), ResourceType = typeof(DomainEnumTexts))]
        AdditionalPayment = 4,
    }
}