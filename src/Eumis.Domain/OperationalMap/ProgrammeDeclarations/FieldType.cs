using Eumis.Common.Json;
using Eumis.Domain;

namespace Eumis.Domain.OperationalMap.ProgrammeDeclarations
{
    public enum FieldType
    {
        [Description(Description = nameof(DomainEnumTexts.FieldType_CheckBox), ResourceType = typeof(DomainEnumTexts))]
        CheckBox = 1,

        [Description(Description = nameof(DomainEnumTexts.FieldType_Numeric), ResourceType = typeof(DomainEnumTexts))]
        Numeric = 2,

        [Description(Description = nameof(DomainEnumTexts.FieldType_Text), ResourceType = typeof(DomainEnumTexts))]
        Text = 3,

        [Description(Description = nameof(DomainEnumTexts.FieldType_Currency), ResourceType = typeof(DomainEnumTexts))]
        Currency = 4,

        [Description(Description = nameof(DomainEnumTexts.FieldType_Nomenclature), ResourceType = typeof(DomainEnumTexts))]
        Nomenclature = 5,

        [Description(Description = nameof(DomainEnumTexts.FieldType_Date), ResourceType = typeof(DomainEnumTexts))]
        Date = 6,

        [Description(Description = nameof(DomainEnumTexts.FieldType_Period), ResourceType = typeof(DomainEnumTexts))]
        Period = 7,
    }
}
