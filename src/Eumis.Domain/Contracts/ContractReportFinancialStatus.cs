using Eumis.Common.Json;

namespace Eumis.Domain.Contracts
{
    public enum ContractReportFinancialStatus
    {
        [Description(Description = nameof(DomainEnumTexts.ContractReportFinancialStatus_Draft), ResourceType = typeof(DomainEnumTexts))]
        Draft = 1,

        [Description(Description = nameof(DomainEnumTexts.ContractReportFinancialStatus_Entered), ResourceType = typeof(DomainEnumTexts))]
        Entered = 2,

        [Description(Description = nameof(DomainEnumTexts.ContractReportFinancialStatus_Actual), ResourceType = typeof(DomainEnumTexts))]
        Actual = 3,

        [Description(Description = nameof(DomainEnumTexts.ContractReportFinancialStatus_Returned), ResourceType = typeof(DomainEnumTexts))]
        Returned = 4,
    }
}