using Eumis.Common.Json;

namespace Eumis.Domain.Irregularities
{
    public enum IrregularityCheckTime
    {
        [Description(Description = nameof(DomainEnumTexts.IrregularityCheckTime_BeforePayment), ResourceType = typeof(DomainEnumTexts))]
        BeforePayment = 1,

        [Description(Description = nameof(DomainEnumTexts.IrregularityCheckTime_AfterPayment), ResourceType = typeof(DomainEnumTexts))]
        AfterPayment = 2,

        [Description(Description = nameof(DomainEnumTexts.IrregularityCheckTime_BeforeAndAfterPayment), ResourceType = typeof(DomainEnumTexts))]
        BeforeAndAfterPayment = 3,
    }
}
