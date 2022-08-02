using Eumis.Common.Json;

namespace Eumis.Domain.Contracts
{
    public enum ContractReportTechnicalCheckApproval
    {
        [Description(Description = nameof(DomainEnumTexts.ContractReportTechnicalCheckApproval_Approved), ResourceType = typeof(DomainEnumTexts))]
        Approved = 1,

        [Description(Description = nameof(DomainEnumTexts.ContractReportTechnicalCheckApproval_Unapproved), ResourceType = typeof(DomainEnumTexts))]
        Unapproved = 2,
    }
}