using Eumis.Common.Json;

namespace Eumis.Domain.Irregularities
{
    public enum IrregularityVersionStatus
    {
        [Description(Description = nameof(DomainEnumTexts.IrregularityVersionStatus_Draft), ResourceType = typeof(DomainEnumTexts))]
        Draft = 1,

        [Description(Description = nameof(DomainEnumTexts.IrregularityVersionStatus_Active), ResourceType = typeof(DomainEnumTexts))]
        Active = 2,

        [Description(Description = nameof(DomainEnumTexts.IrregularityVersionStatus_Archived), ResourceType = typeof(DomainEnumTexts))]
        Archived = 3,
    }
}
