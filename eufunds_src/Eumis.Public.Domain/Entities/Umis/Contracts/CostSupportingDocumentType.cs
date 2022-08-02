using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.Contracts
{
    public enum CostSupportingDocumentType
    {
        [Description("Авансов отчет")]
        AdvanceReport = 1,

        [Description("Касова бележка (фискален бон)")]
        CashReceipt = 2,

        [Description("Разходен касов ордер")]
        CostCashReceipts = 3,

        [Description("Заповед за командировка в страната")]
        CountryTravelOrder = 4,

        [Description("Кредитно известие")]
        CreditNotification = 5,

        [Description("Дебитно известие")]
        DebitNotification = 6,

        [Description("Фактура")]
        Invoice = 7,

        [Description("Друг")]
        Other = 8,

        [Description("Сметка за изплатени суми")]
        PaidAmountsAccount = 9,

        [Description("Ведомост")]
        Payroll = 10,

        [Description("Протокол по ЗДДС")]
        VATProtocol = 11
    }
}
