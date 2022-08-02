using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.EvalSessions
{
    public enum EvalSessionDistributionType
    {
        [Description("Автоматично")]
        Automatic = 1,

        [Description("Ръчно")]
        Manual = 2,

        [Description("Продължена оценка")]
        Continued = 3
    }
}