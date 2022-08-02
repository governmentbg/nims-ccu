using Eumis.Common.Json;

namespace Eumis.Domain.Contracts
{
    public enum ContractReportCertCorrectionType
    {
        [Description(Description = nameof(DomainEnumTexts.ContractReportCertCorrectionType_PaymentCertified), ResourceType = typeof(DomainEnumTexts))]
        PaymentCertified = 1,

        [Description(Description = nameof(DomainEnumTexts.ContractReportCertCorrectionType_ContractCertified), ResourceType = typeof(DomainEnumTexts))]
        ContractCertified = 2,

        [Description(Description = nameof(DomainEnumTexts.ContractReportCertCorrectionType_ProgrameCertified), ResourceType = typeof(DomainEnumTexts))]
        ProgrameCertified = 3,

        [Description(Description = nameof(DomainEnumTexts.ContractReportCertCorrectionType_ProgramePriorityCertified), ResourceType = typeof(DomainEnumTexts))]
        ProgramePriorityCertified = 4,

        [Description(Description = nameof(DomainEnumTexts.ContractReportCertCorrectionType_ProcedureCertified), ResourceType = typeof(DomainEnumTexts))]
        ProcedureCertified = 5,
    }
}