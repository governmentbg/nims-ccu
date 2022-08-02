using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.EvalSessions
{
    public enum EvalSessionStandingStatus
    {
        [Description("Приложено")]
        Applied = 1,

        [Description("Отказано")]
        Refused = 2
    }
}