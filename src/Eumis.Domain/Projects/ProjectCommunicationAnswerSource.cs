using Eumis.Common.Json;

namespace Eumis.Domain.Projects
{
    public enum ProjectCommunicationAnswerSource
    {
        [Description(Description = nameof(DomainEnumTexts.ProjectCommunicationAnswerSource_Beneficiary), ResourceType = typeof(DomainEnumTexts))]
        Beneficiary = 1,

        [Description(Description = nameof(DomainEnumTexts.ProjectCommunicationAnswerSource_ManagingAuthority), ResourceType = typeof(DomainEnumTexts))]
        ManagingAuthority = 2,
    }
}
