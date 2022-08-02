using Eumis.Common.Json;

namespace Eumis.Domain.Contracts
{
    public enum ContractReportRevalidationCertAuthorityCorrectionType
    {
        [Description(Description = nameof(DomainEnumTexts.ContractReportRevalidationCertAuthorityCorrectionType_PaymentRevalidated), ResourceType = typeof(DomainEnumTexts))]
        PaymentRevalidated = 1,

        [Description(Description = nameof(DomainEnumTexts.ContractReportRevalidationCertAuthorityCorrectionType_ContractRevalidated), ResourceType = typeof(DomainEnumTexts))]
        ContractRevalidated = 2,

        [Description(Description = nameof(DomainEnumTexts.ContractReportRevalidationCertAuthorityCorrectionType_ProgrammeRevalidated), ResourceType = typeof(DomainEnumTexts))]
        ProgrammeRevalidated = 3,

        [Description(Description = nameof(DomainEnumTexts.ContractReportRevalidationCertAuthorityCorrectionType_ProgrammePriorityRevalidated), ResourceType = typeof(DomainEnumTexts))]
        ProgrammePriorityRevalidated = 4,

        [Description(Description = nameof(DomainEnumTexts.ContractReportRevalidationCertAuthorityCorrectionType_ProcedureRevalidated), ResourceType = typeof(DomainEnumTexts))]
        ProcedureRevalidated = 5,
    }
}
