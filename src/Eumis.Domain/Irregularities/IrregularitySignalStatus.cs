using Eumis.Common.Json;

namespace Eumis.Domain.Irregularities
{
    public enum IrregularitySignalStatus
    {
        [Description(Description = nameof(DomainEnumTexts.IrregularitySignalStatus_Draft), ResourceType = typeof(DomainEnumTexts))]
        Draft = 1,

        [Description(Description = nameof(DomainEnumTexts.IrregularitySignalStatus_Active), ResourceType = typeof(DomainEnumTexts))]
        Active = 2,

        [Description(Description = nameof(DomainEnumTexts.IrregularitySignalStatus_Ended), ResourceType = typeof(DomainEnumTexts))]
        Ended = 3,

        [Description(Description = nameof(DomainEnumTexts.IrregularitySignalStatus_Removed), ResourceType = typeof(DomainEnumTexts))]
        Removed = 4,
    }
}
