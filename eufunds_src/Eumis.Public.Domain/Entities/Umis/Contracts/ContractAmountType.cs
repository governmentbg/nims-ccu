using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.Contracts
{
    public enum ContractAmountType
    {
        [Description("Проектът е голям проект съгласно чл. 100 от Регламент (ЕС) № 1303/ 2013 г.")]
        Big = 0,

        [Description("Инфраструктурен проект на стойност над 5 000 000 лв.")]
        Infrastructure = 1,

        [Description("Друго")]
        Other = 2,

        [Description("Финансови инструменти")]
        FinancialInstruments = 3
    }
}
