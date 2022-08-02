using Eumis.Common.Json;

namespace Eumis.Domain.Projects
{
    public enum ProjectManagingAuthorityCommunicationSource
    {
        [Description(Description = nameof(DomainEnumTexts.ProjectManagingAuthorityCommunicationSource_Beneficiary), ResourceType = typeof(DomainEnumTexts))]
        Beneficiary = 1,

        [Description(Description = nameof(DomainEnumTexts.ProjectManagingAuthorityCommunicationSource_ManagingAuthority), ResourceType = typeof(DomainEnumTexts))]
        ManagingAuthority = 2,
    }
}
