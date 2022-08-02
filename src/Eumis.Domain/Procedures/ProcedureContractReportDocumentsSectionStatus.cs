using Eumis.Common.Json;

namespace Eumis.Domain.Procedures
{
    public enum ProcedureContractReportDocumentsSectionStatus
    {
        [Description(Description = nameof(DomainEnumTexts.ProcedureContractReportDocumentsSectionStatus_Draft), ResourceType = typeof(DomainEnumTexts))]
        Draft = 1,

        [Description(Description = nameof(DomainEnumTexts.ProcedureContractReportDocumentsSectionStatus_Active), ResourceType = typeof(DomainEnumTexts))]
        Active = 2,
    }
}
