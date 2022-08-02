using Eumis.Common.Json;

namespace Eumis.Domain.Contracts
{
    public enum ContractReportFinancialCheckStatus
    {
        [Description(Description = nameof(DomainEnumTexts.ContractReportFinancialCheckStatus_Draft), ResourceType = typeof(DomainEnumTexts))]
        Draft = 1,

        [Description(Description = nameof(DomainEnumTexts.ContractReportFinancialCheckStatus_Active), ResourceType = typeof(DomainEnumTexts))]
        Active = 2,

        [Description(Description = nameof(DomainEnumTexts.ContractReportFinancialCheckStatus_Archived), ResourceType = typeof(DomainEnumTexts))]
        Archived = 3,
    }
}