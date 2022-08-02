using Eumis.Common.Json;

namespace Eumis.Domain.Projects
{
    public enum ProjectDossierDocumentType
    {
        [Description(Description = nameof(DomainEnumTexts.ProjectDossierDocumentType_ProjectVersion), ResourceType = typeof(DomainEnumTexts))]
        ProjectVersion = 1,

        [Description(Description = nameof(DomainEnumTexts.ProjectDossierDocumentType_EvalSessionSheet), ResourceType = typeof(DomainEnumTexts))]
        EvalSessionSheet = 2,

        [Description(Description = nameof(DomainEnumTexts.ProjectDossierDocumentType_ProjectCommunication), ResourceType = typeof(DomainEnumTexts))]
        ProjectCommunication = 3,

        [Description(Description = nameof(DomainEnumTexts.ProjectDossierDocumentType_ContractVersion), ResourceType = typeof(DomainEnumTexts))]
        ContractVersion = 4,

        [Description(Description = nameof(DomainEnumTexts.ProjectDossierDocumentType_ContractsContractRegistration), ResourceType = typeof(DomainEnumTexts))]
        ContractsContractRegistration = 5,

        [Description(Description = nameof(DomainEnumTexts.ProjectDossierDocumentType_ContractProcurement), ResourceType = typeof(DomainEnumTexts))]
        ContractProcurement = 6,

        [Description(Description = nameof(DomainEnumTexts.ProjectDossierDocumentType_ContractReportPayment), ResourceType = typeof(DomainEnumTexts))]
        ContractReportPayment = 7,

        [Description(Description = nameof(DomainEnumTexts.ProjectDossierDocumentType_ContractReportFinancial), ResourceType = typeof(DomainEnumTexts))]
        ContractReportFinancial = 8,

        [Description(Description = nameof(DomainEnumTexts.ProjectDossierDocumentType_ContractReportTechnical), ResourceType = typeof(DomainEnumTexts))]
        ContractReportTechnical = 9,

        [Description(Description = nameof(DomainEnumTexts.ProjectDossierDocumentType_ContractReportMicroType1), ResourceType = typeof(DomainEnumTexts))]
        ContractReportMicroType1 = 10,

        [Description(Description = nameof(DomainEnumTexts.ProjectDossierDocumentType_ContractReportMicroType2), ResourceType = typeof(DomainEnumTexts))]
        ContractReportMicroType2 = 11,

        [Description(Description = nameof(DomainEnumTexts.ProjectDossierDocumentType_ContractReportMicroType3), ResourceType = typeof(DomainEnumTexts))]
        ContractReportMicroType3 = 12,

        [Description(Description = nameof(DomainEnumTexts.ProjectDossierDocumentType_ContractReportMicroType4), ResourceType = typeof(DomainEnumTexts))]
        ContractReportMicroType4 = 13,

        [Description(Description = nameof(DomainEnumTexts.ProjectDossierDocumentType_ContractAdminAuthorityCommunication), ResourceType = typeof(DomainEnumTexts))]
        ContractAdminAuthorityCommunication = 113,
    }
}
