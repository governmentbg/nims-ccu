using Eumis.Common.Json;

namespace Eumis.Domain.Contracts
{
    public enum CostSupportingDocumentType
    {
        [Description(Description = nameof(DomainEnumTexts.CostSupportingDocumentType_AdvanceReport), ResourceType = typeof(DomainEnumTexts))]
        AdvanceReport = 1,

        [Description(Description = nameof(DomainEnumTexts.CostSupportingDocumentType_CashReceipt), ResourceType = typeof(DomainEnumTexts))]
        CashReceipt = 2,

        [Description(Description = nameof(DomainEnumTexts.CostSupportingDocumentType_CostCashReceipts), ResourceType = typeof(DomainEnumTexts))]
        CostCashReceipts = 3,

        [Description(Description = nameof(DomainEnumTexts.CostSupportingDocumentType_CountryTravelOrder), ResourceType = typeof(DomainEnumTexts))]
        CountryTravelOrder = 4,

        [Description(Description = nameof(DomainEnumTexts.CostSupportingDocumentType_CreditNotification), ResourceType = typeof(DomainEnumTexts))]
        CreditNotification = 5,

        [Description(Description = nameof(DomainEnumTexts.CostSupportingDocumentType_DebitNotification), ResourceType = typeof(DomainEnumTexts))]
        DebitNotification = 6,

        [Description(Description = nameof(DomainEnumTexts.CostSupportingDocumentType_Invoice), ResourceType = typeof(DomainEnumTexts))]
        Invoice = 7,

        [Description(Description = nameof(DomainEnumTexts.CostSupportingDocumentType_Other), ResourceType = typeof(DomainEnumTexts))]
        Other = 8,

        [Description(Description = nameof(DomainEnumTexts.CostSupportingDocumentType_PaidAmountsAccount), ResourceType = typeof(DomainEnumTexts))]
        PaidAmountsAccount = 9,

        [Description(Description = nameof(DomainEnumTexts.CostSupportingDocumentType_Payroll), ResourceType = typeof(DomainEnumTexts))]
        Payroll = 10,

        [Description(Description = nameof(DomainEnumTexts.CostSupportingDocumentType_VATProtocol), ResourceType = typeof(DomainEnumTexts))]
        VATProtocol = 11,
    }
}
