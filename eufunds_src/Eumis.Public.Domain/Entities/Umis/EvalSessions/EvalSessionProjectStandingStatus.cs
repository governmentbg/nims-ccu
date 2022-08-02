using Eumis.Public.Domain.Helpers;

namespace Eumis.Public.Domain.Entities.Umis.EvalSessions
{
    public enum EvalSessionProjectStandingStatus
    {
        [LocalizableDescription("EvalSessionProjectStandingStatus_Approved")] 
        Approved = 1,

        [LocalizableDescription("EvalSessionProjectStandingStatus_Reserve")]
        Reserve = 2,

        [LocalizableDescription("EvalSessionProjectStandingStatus_Rejected")]
        Rejected = 3,

        [LocalizableDescription("EvalSessionProjectStandingStatus_RejectedAtASD")]
        RejectedAtASD = 4,

        [LocalizableDescription("EvalSessionProjectStandingStatus_RejectedAtTFO")]
        RejectedAtTFO = 5,

        [LocalizableDescription("EvalSessionProjectStandingStatus_RejectedAtPO")]
        RejectedAtPO = 6
    }
}