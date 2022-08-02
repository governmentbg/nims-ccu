using Eumis.Common.Json;

namespace Eumis.Domain.Projects
{
    public enum ProjectRegistrationStatus
    {
        [Description(Description = nameof(DomainEnumTexts.ProjectRegistrationStatus_Registered), ResourceType = typeof(DomainEnumTexts))]
        Registered = 1,

        [Description(Description = nameof(DomainEnumTexts.ProjectRegistrationStatus_RegisteredLate), ResourceType = typeof(DomainEnumTexts))]
        RegisteredLate = 2,

        [Description(Description = nameof(DomainEnumTexts.ProjectRegistrationStatus_Withdrawn), ResourceType = typeof(DomainEnumTexts))]
        Withdrawn = 3,

        [Description(Description = nameof(DomainEnumTexts.ProjectRegistrationStatus_RegisteredService), ResourceType = typeof(DomainEnumTexts))]
        RegisteredService = 4,
    }
}
