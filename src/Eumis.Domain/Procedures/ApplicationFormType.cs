using Eumis.Common.Json;

namespace Eumis.Domain.Procedures
{
    public enum ApplicationFormType
    {
        [Description(Description = nameof(DomainEnumTexts.ApplicationFormType_Standard), ResourceType = typeof(DomainEnumTexts))]
        Standard = 1,

        [Description(Description = nameof(DomainEnumTexts.ApplicationFormType_PreliminarySelection), ResourceType = typeof(DomainEnumTexts))]
        PreliminarySelection = 2,

        [Description(Description = nameof(DomainEnumTexts.ApplicationFormType_StandardWithPreliminarySelection), ResourceType = typeof(DomainEnumTexts))]
        StandardWithPreliminarySelection = 3,

        [Description(Description = nameof(DomainEnumTexts.ApplicationFormType_StandardForBudgetLine), ResourceType = typeof(DomainEnumTexts))]
        StandardForBudgetLine = 4,

        [Description(Description = nameof(DomainEnumTexts.ApplicationFormType_FOFFinancialAgentsInfo), ResourceType = typeof(DomainEnumTexts))]
        FOFFinancialAgentsInfo = 5,

        [Description(Description = nameof(DomainEnumTexts.ApplicationFormType_EndUsersInfo), ResourceType = typeof(DomainEnumTexts))]
        EndUsersInfo = 6,

        [Description(Description = nameof(DomainEnumTexts.ApplicationFormType_StandardSimplified), ResourceType = typeof(DomainEnumTexts))]
        StandardSimplified = 7,
    }
}
