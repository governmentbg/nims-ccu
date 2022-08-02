using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.EvalSessions
{
    public enum EvalSessionEvaluationCalculationType
    {
        [Description("Автоматично")]
        Automatic = 1,

        [Description("Ръчно")]
        Manual = 2
    }
}