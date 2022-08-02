using Eumis.Common.Json;

namespace Eumis.Domain.CertAuthorityChecks
{
    public enum CertAuthorityCheckAscertainmentExecutionStatus
    {
        [Description(Description = nameof(DomainEnumTexts.CertAuthorityCheckAscertainmentExecutionStatus_Executed), ResourceType = typeof(DomainEnumTexts))]
        Executed = 1,

        [Description(Description = nameof(DomainEnumTexts.CertAuthorityCheckAscertainmentExecutionStatus_InProcess), ResourceType = typeof(DomainEnumTexts))]
        InProcess = 2,

        [Description(Description = nameof(DomainEnumTexts.CertAuthorityCheckAscertainmentExecutionStatus_Unexecuted), ResourceType = typeof(DomainEnumTexts))]
        Unexecuted = 3,

        [Description(Description = nameof(DomainEnumTexts.CertAuthorityCheckAscertainmentExecutionStatus_Removed), ResourceType = typeof(DomainEnumTexts))]
        Removed = 4,
    }
}
