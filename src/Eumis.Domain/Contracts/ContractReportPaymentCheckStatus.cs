using Eumis.Common.Json;

namespace Eumis.Domain.Contracts
{
    public enum ContractReportPaymentCheckStatus
    {
        [Description(Description = nameof(DomainEnumTexts.ContractReportPaymentCheckStatus_Draft), ResourceType = typeof(DomainEnumTexts))]
        Draft = 1,

        [Description(Description = nameof(DomainEnumTexts.ContractReportPaymentCheckStatus_Active), ResourceType = typeof(DomainEnumTexts))]
        Active = 2,

        [Description(Description = nameof(DomainEnumTexts.ContractReportPaymentCheckStatus_Archived), ResourceType = typeof(DomainEnumTexts))]
        Archived = 3,
    }
}