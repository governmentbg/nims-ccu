using Eumis.Common.Json;

namespace Eumis.Domain.Procedures
{
    public enum ProcedureSpecFieldMaxLength
    {
        [Description(Description = nameof(DomainEnumTexts.ProcedureSpecFieldMaxLength_Small), ResourceType = typeof(DomainEnumTexts))]
        Small = 1000,

        [Description(Description = nameof(DomainEnumTexts.ProcedureSpecFieldMaxLength_Middle), ResourceType = typeof(DomainEnumTexts))]
        Middle = 3000,

        [Description(Description = nameof(DomainEnumTexts.ProcedureSpecFieldMaxLength_Large), ResourceType = typeof(DomainEnumTexts))]
        Large = 5000,

        [Description(Description = nameof(DomainEnumTexts.ProcedureSpecFieldMaxLength_VeryLarge), ResourceType = typeof(DomainEnumTexts))]
        VeryLarge = 10000,

        [Description(Description = nameof(DomainEnumTexts.ProcedureSpecFieldMaxLength_IBAN), ResourceType = typeof(DomainEnumTexts))]
        IBAN = 0,
    }
}
