using Eumis.Common.Json;

namespace Eumis.Domain.Projects
{
    public enum ProjectRecieveType
    {
        [Description(Description = nameof(DomainEnumTexts.ProjectRecieveType_InPerson), ResourceType = typeof(DomainEnumTexts))]
        InPerson = 1,

        [Description(Description = nameof(DomainEnumTexts.ProjectRecieveType_Mail), ResourceType = typeof(DomainEnumTexts))]
        Mail = 2,

        [Description(Description = nameof(DomainEnumTexts.ProjectRecieveType_Courier), ResourceType = typeof(DomainEnumTexts))]
        Courier = 3,

        [Description(Description = nameof(DomainEnumTexts.ProjectRecieveType_Fax), ResourceType = typeof(DomainEnumTexts))]
        Fax = 4,

        [Description(Description = nameof(DomainEnumTexts.ProjectRecieveType_Electronic), ResourceType = typeof(DomainEnumTexts))]
        Electronic = 5,
    }
}
