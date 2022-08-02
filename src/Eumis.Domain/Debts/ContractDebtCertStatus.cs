using Eumis.Common.Json;

namespace Eumis.Domain.Debts
{
    public enum ContractDebtCertStatus
    {
        [Description(Description = nameof(DomainEnumTexts.ContractDebtCertStatus_Yes), ResourceType = typeof(DomainEnumTexts))]
        Yes = 1,

        [Description(Description = nameof(DomainEnumTexts.ContractDebtCertStatus_No), ResourceType = typeof(DomainEnumTexts))]
        No = 2,

        [Description(Description = nameof(DomainEnumTexts.ContractDebtCertStatus_Partial), ResourceType = typeof(DomainEnumTexts))]
        Partial = 3,
    }
}
