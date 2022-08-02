using Eumis.Common.Json;

namespace Eumis.Domain.Contracts
{
    public enum ContractReportCertAuthorityCorrectionType
    {
        [Description(Description = nameof(DomainEnumTexts.ContractReportCertAuthorityCorrectionType_PaymentCertified), ResourceType = typeof(DomainEnumTexts))]
        PaymentCertified = 1,

        [Description(Description = nameof(DomainEnumTexts.ContractReportCertAuthorityCorrectionType_ContractCertified), ResourceType = typeof(DomainEnumTexts))]
        ContractCertified = 2,

        [Description(Description = nameof(DomainEnumTexts.ContractReportCertAuthorityCorrectionType_ProgrameCertified), ResourceType = typeof(DomainEnumTexts))]
        ProgrameCertified = 3,

        [Description(Description = nameof(DomainEnumTexts.ContractReportCertAuthorityCorrectionType_ProgramePriorityCertified), ResourceType = typeof(DomainEnumTexts))]
        ProgramePriorityCertified = 4,

        [Description(Description = nameof(DomainEnumTexts.ContractReportCertAuthorityCorrectionType_ProcedureCertified), ResourceType = typeof(DomainEnumTexts))]
        ProcedureCertified = 5,
    }
}
