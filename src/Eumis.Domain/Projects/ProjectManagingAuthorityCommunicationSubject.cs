using Eumis.Common.Json;

namespace Eumis.Domain.Projects
{
    public enum ProjectManagingAuthorityCommunicationSubject
    {
        [Description(Description = nameof(DomainEnumTexts.ProjectManagingAuthorityCommunicationSubject_ProjectProposalWithdrawal), ResourceType = typeof(DomainEnumTexts))]
        ProjectProposalWithdrawal = 1,

        [Description(Description = nameof(DomainEnumTexts.ProjectManagingAuthorityCommunicationSubject_Complaint), ResourceType = typeof(DomainEnumTexts))]
        Complaint = 2,

        [Description(Description = nameof(DomainEnumTexts.ProjectManagingAuthorityCommunicationSubject_ContractConclusionDocuments), ResourceType = typeof(DomainEnumTexts))]
        ContractConclusionDocuments = 3,

        [Description(Description = nameof(DomainEnumTexts.ProjectManagingAuthorityCommunicationSubject_ChangesAndCircumstances), ResourceType = typeof(DomainEnumTexts))]
        ChangesAndCircumstances = 4,

        [Description(Description = nameof(DomainEnumTexts.ProjectManagingAuthorityCommunicationSubject_Message), ResourceType = typeof(DomainEnumTexts))]
        Message = 5,

        [Description(Description = nameof(DomainEnumTexts.ProjectManagingAuthorityCommunicationSubject_TourismMinistryReport), ResourceType = typeof(DomainEnumTexts))]
        TourismMinistryReport = 6,

        [Description(Description = nameof(DomainEnumTexts.ProjectManagingAuthorityCommunicationSubject_ServiceOfAct), ResourceType = typeof(DomainEnumTexts))]
        ServiceOfAct = 7,

        [Description(Description = nameof(DomainEnumTexts.ProjectManagingAuthorityCommunicationSubject_ManagingAuthorityReport), ResourceType = typeof(DomainEnumTexts))]
        ManagingAuthorityReport = 8,
    }
}
