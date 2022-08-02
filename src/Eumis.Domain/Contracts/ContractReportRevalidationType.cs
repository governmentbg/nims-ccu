using Eumis.Common.Json;

namespace Eumis.Domain.Contracts
{
    public enum ContractReportRevalidationType
    {
        [Description(Description = nameof(DomainEnumTexts.ContractReportRevalidationType_PaymentRevalidated), ResourceType = typeof(DomainEnumTexts))]
        PaymentRevalidated = 1,

        [Description(Description = nameof(DomainEnumTexts.ContractReportRevalidationType_ContractRevalidated), ResourceType = typeof(DomainEnumTexts))]
        ContractRevalidated = 2,

        [Description(Description = nameof(DomainEnumTexts.ContractReportRevalidationType_ProgrameRevalidated), ResourceType = typeof(DomainEnumTexts))]
        ProgrameRevalidated = 3,

        [Description(Description = nameof(DomainEnumTexts.ContractReportRevalidationType_ProgramePriorityRevalidated), ResourceType = typeof(DomainEnumTexts))]
        ProgramePriorityRevalidated = 4,

        [Description(Description = nameof(DomainEnumTexts.ContractReportRevalidationType_ProcedureRevalidated), ResourceType = typeof(DomainEnumTexts))]
        ProcedureRevalidated = 5,
    }
}