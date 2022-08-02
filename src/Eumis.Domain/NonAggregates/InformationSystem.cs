using Eumis.Common.Json;

namespace Eumis.Domain.NonAggregates
{
    public enum InformationSystem
    {
        [Description(Description = nameof(DomainEnumTexts.InformationSystem_Isak), ResourceType = typeof(DomainEnumTexts))]
        Isak = 1,
        [Description(Description = nameof(DomainEnumTexts.InformationSystem_Efz), ResourceType = typeof(DomainEnumTexts))]
        Efz = 2,
        [Description(Description = nameof(DomainEnumTexts.InformationSystem_Popai), ResourceType = typeof(DomainEnumTexts))]
        Popai = 3,
        [Description(Description = nameof(DomainEnumTexts.InformationSystem_Cbc), ResourceType = typeof(DomainEnumTexts))]
        Cbc = 4,
        [Description(Description = nameof(DomainEnumTexts.InformationSystem_Olaf), ResourceType = typeof(DomainEnumTexts))]
        Olaf = 5,
        [Description(Description = nameof(DomainEnumTexts.InformationSystem_Rdp), ResourceType = typeof(DomainEnumTexts))]
        Rdp = 6,
        [Description(Description = nameof(DomainEnumTexts.InformationSystem_Control), ResourceType = typeof(DomainEnumTexts))]
        Control = 7,
        [Description(Description = nameof(DomainEnumTexts.InformationSystem_Aop), ResourceType = typeof(DomainEnumTexts))]
        Aop = 8,
        [Description(Description = nameof(DomainEnumTexts.InformationSystem_Litigation), ResourceType = typeof(DomainEnumTexts))]
        Litigation = 9,
        [Description(Description = nameof(DomainEnumTexts.InformationSystem_Gis), ResourceType = typeof(DomainEnumTexts))]
        Gis = 10,
        [Description(Description = nameof(DomainEnumTexts.InformationSystem_InfraProjects), ResourceType = typeof(DomainEnumTexts))]
        InfraProjects = 11,
        [Description(Description = nameof(DomainEnumTexts.InformationSystem_Other), ResourceType = typeof(DomainEnumTexts))]
        Other = 12,
        [Description(Description = nameof(DomainEnumTexts.InformationSystem_Document), ResourceType = typeof(DomainEnumTexts))]
        Document = 13,
    }
}
