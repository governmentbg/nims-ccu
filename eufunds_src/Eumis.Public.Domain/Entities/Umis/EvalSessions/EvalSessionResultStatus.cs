using Eumis.Public.Domain.Helpers;

namespace Eumis.Public.Domain.Entities.Umis.EvalSessions
{
    public enum EvalSessionResultStatus
    {
        [LocalizableDescription("EvalSessionResultStatus_Draft")]
        Draft = 1,

        [LocalizableDescription("EvalSessionResultStatus_Publish")]
        Published = 2,

        [LocalizableDescription("EvalSessionResultStatus_Archived")]
        Archived = 3,

        [LocalizableDescription("EvalSessionResultStatus_Canceled")]
        Canceled = 4,
    }
}
