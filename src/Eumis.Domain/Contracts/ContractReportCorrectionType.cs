using Eumis.Common.Json;

namespace Eumis.Domain.Contracts
{
    public enum ContractReportCorrectionType
    {
        [Description(Description = nameof(DomainEnumTexts.ContractReportCorrectionType_PaymentVerified), ResourceType = typeof(DomainEnumTexts))]
        PaymentVerified = 1,

        [Description(Description = nameof(DomainEnumTexts.ContractReportCorrectionType_ContractVerified), ResourceType = typeof(DomainEnumTexts))]
        ContractVerified = 2,

        [Description(Description = nameof(DomainEnumTexts.ContractReportCorrectionType_ProgrameVerified), ResourceType = typeof(DomainEnumTexts))]
        ProgrameVerified = 3,

        [Description(Description = nameof(DomainEnumTexts.ContractReportCorrectionType_ProgramePriorityVerified), ResourceType = typeof(DomainEnumTexts))]
        ProgramePriorityVerified = 4,

        [Description(Description = nameof(DomainEnumTexts.ContractReportCorrectionType_ProcedureVerified), ResourceType = typeof(DomainEnumTexts))]
        ProcedureVerified = 5,

        [Description(Description = nameof(DomainEnumTexts.ContractReportCorrectionType_AdvanceCovered), ResourceType = typeof(DomainEnumTexts))]
        AdvanceCovered = 6,
    }
}
