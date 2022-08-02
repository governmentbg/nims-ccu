using Eumis.Common.Json;

namespace Eumis.Domain.Irregularities
{
    public enum IrregularityReasonNotReportingToOlaf
    {
        [Description(Description = nameof(DomainEnumTexts.IrregularityReasonNotReportingToOlaf_NotNecessary), ResourceType = typeof(DomainEnumTexts))]
        NotNecessary = 1,

        [Description(Description = nameof(DomainEnumTexts.IrregularityReasonNotReportingToOlaf_Exception), ResourceType = typeof(DomainEnumTexts))]
        Exception = 2,
    }
}
