using Eumis.Common.Json;

namespace Eumis.Data.ActuallyPaidAmounts.ViewObjects
{
    public enum ActuallyPaidAmountCreationType
    {
        [Description(Description = nameof(DataEnumTexts.ActuallyPaidAmountCreationType_Manual), ResourceType = typeof(DataEnumTexts))]
        Manual = 1,

        [Description(Description = nameof(DataEnumTexts.ActuallyPaidAmountCreationType_SAPImport), ResourceType = typeof(DataEnumTexts))]
        SAPImport = 2,
    }
}