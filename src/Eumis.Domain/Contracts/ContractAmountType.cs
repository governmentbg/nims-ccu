using Eumis.Common.Json;

namespace Eumis.Domain.Contracts
{
    public enum ContractAmountType
    {
        [Description(Description = nameof(DomainEnumTexts.ContractAmountType_Big), ResourceType = typeof(DomainEnumTexts))]
        Big = 0,

        [Description(Description = nameof(DomainEnumTexts.ContractAmountType_Infrastructure), ResourceType = typeof(DomainEnumTexts))]
        Infrastructure = 1,

        [Description(Description = nameof(DomainEnumTexts.ContractAmountType_Other), ResourceType = typeof(DomainEnumTexts))]
        Other = 2,

        [Description(Description = nameof(DomainEnumTexts.ContractAmountType_FinancialInstruments), ResourceType = typeof(DomainEnumTexts))]
        FinancialInstruments = 3,
    }
}
