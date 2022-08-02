using Eumis.Common.Json;

namespace Eumis.Domain.Contracts
{
    public enum ContractReportStatus
    {
        [Description(Description = nameof(DomainEnumTexts.ContractReportStatus_Draft), ResourceType = typeof(DomainEnumTexts))]
        Draft = 1,

        [Description(Description = nameof(DomainEnumTexts.ContractReportStatus_Entered), ResourceType = typeof(DomainEnumTexts))]
        Entered = 2,

        [Description(Description = nameof(DomainEnumTexts.ContractReportStatus_SentChecked), ResourceType = typeof(DomainEnumTexts))]
        SentChecked = 3,

        [Description(Description = nameof(DomainEnumTexts.ContractReportStatus_Unchecked), ResourceType = typeof(DomainEnumTexts))]
        Unchecked = 4,

        [Description(Description = nameof(DomainEnumTexts.ContractReportStatus_Accepted), ResourceType = typeof(DomainEnumTexts))]
        Accepted = 5,

        [Description(Description = nameof(DomainEnumTexts.ContractReportStatus_Refused), ResourceType = typeof(DomainEnumTexts))]
        Refused = 6,
    }
}