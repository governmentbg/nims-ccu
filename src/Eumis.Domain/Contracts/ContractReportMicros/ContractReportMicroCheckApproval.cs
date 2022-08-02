using Eumis.Common.Json;

namespace Eumis.Domain.Contracts.ContractReportMicros
{
    public enum ContractReportMicroCheckApproval
    {
        [Description(Description = nameof(DomainEnumTexts.ContractReportMicroCheckApproval_Approved), ResourceType = typeof(DomainEnumTexts))]
        Approved = 1,

        [Description(Description = nameof(DomainEnumTexts.ContractReportMicroCheckApproval_Unapproved), ResourceType = typeof(DomainEnumTexts))]
        Unapproved = 2,
    }
}