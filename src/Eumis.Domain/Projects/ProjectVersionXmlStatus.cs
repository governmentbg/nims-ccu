using Eumis.Common.Json;

namespace Eumis.Domain.Projects
{
    public enum ProjectVersionXmlStatus
    {
        [Description(Description = nameof(DomainEnumTexts.ProjectVersionXmlStatus_Draft), ResourceType = typeof(DomainEnumTexts))]
        Draft = 1,

        [Description(Description = nameof(DomainEnumTexts.ProjectVersionXmlStatus_Actual), ResourceType = typeof(DomainEnumTexts))]
        Actual = 2,

        [Description(Description = nameof(DomainEnumTexts.ProjectVersionXmlStatus_Archive), ResourceType = typeof(DomainEnumTexts))]
        Archive = 3,
    }
}
