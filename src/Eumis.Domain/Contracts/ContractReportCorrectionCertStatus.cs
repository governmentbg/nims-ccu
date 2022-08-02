using Eumis.Common.Json;

namespace Eumis.Domain.Contracts
{
    public enum ContractReportCorrectionCertStatus
    {
        [Description(Description = nameof(DomainEnumTexts.ContractReportCorrectionCertStatus_Draft), ResourceType = typeof(DomainEnumTexts))]
        Draft = 1,

        [Description(Description = nameof(DomainEnumTexts.ContractReportCorrectionCertStatus_Ended), ResourceType = typeof(DomainEnumTexts))]
        Ended = 2,
    }
}