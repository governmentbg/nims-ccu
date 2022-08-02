using Eumis.Common.Json;

namespace Eumis.Data.Projects.PortalViewObjects
{
    public enum RegMessageType
    {
        [Description(Description = nameof(DataEnumTexts.RegMessageType_New), ResourceType = typeof(DataEnumTexts))]
        New = 1,

        [Description(Description = nameof(DataEnumTexts.RegMessageType_Draft), ResourceType = typeof(DataEnumTexts))]
        Draft = 2,

        [Description(Description = nameof(DataEnumTexts.RegMessageType_Finalized), ResourceType = typeof(DataEnumTexts))]
        Finalized = 3,

        [Description(Description = nameof(DataEnumTexts.RegMessageType_PaperSubmitted), ResourceType = typeof(DataEnumTexts))]
        PaperSubmitted = 4,

        [Description(Description = nameof(DataEnumTexts.RegMessageType_Submitted), ResourceType = typeof(DataEnumTexts))]
        Submitted = 5,

        [Description(Description = nameof(DataEnumTexts.RegMessageType_Cancelled), ResourceType = typeof(DataEnumTexts))]
        Cancelled = 6,

        [Description(Description = nameof(DataEnumTexts.RegMessageType_Expired), ResourceType = typeof(DataEnumTexts))]
        Expired = 7,

        [Description(Description = nameof(DataEnumTexts.RegMessageType_Processed), ResourceType = typeof(DataEnumTexts))]
        Processed = 8,
    }
}
