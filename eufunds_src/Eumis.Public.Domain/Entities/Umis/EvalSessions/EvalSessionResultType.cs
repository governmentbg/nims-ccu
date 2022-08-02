using Eumis.Public.Domain.Helpers;

namespace Eumis.Public.Domain.Entities.Umis.EvalSessions
{
    public enum EvalSessionResultType
    {
        [LocalizableDescription("EvalSessionResultType_Preliminary")]
        Preliminary = 1,

        [LocalizableDescription("EvalSessionResultType_AdminAdmiss")]
        AdminAdmiss = 2,

        [LocalizableDescription("EvalSessionResultType_Standing")]
        Standing = 3,
    }
}
