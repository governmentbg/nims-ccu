using Eumis.Common.Json;

namespace Eumis.Domain.Irregularities
{
    public enum IrregularityCaseState
    {
        [Description(Description = nameof(DomainEnumTexts.IrregularityCaseState_Active), ResourceType = typeof(DomainEnumTexts))]
        Active = 1,

        [Description(Description = nameof(DomainEnumTexts.IrregularityCaseState_Ended), ResourceType = typeof(DomainEnumTexts))]
        Ended = 2,

        [Description(Description = nameof(DomainEnumTexts.IrregularityCaseState_Terminated), ResourceType = typeof(DomainEnumTexts))]
        Terminated = 3,

        [Description(Description = nameof(DomainEnumTexts.IrregularityCaseState_Canceled), ResourceType = typeof(DomainEnumTexts))]
        Canceled = 4,
    }
}
