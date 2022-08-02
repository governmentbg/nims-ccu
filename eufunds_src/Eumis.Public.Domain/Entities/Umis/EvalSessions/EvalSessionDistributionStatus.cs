using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.EvalSessions
{
    public enum EvalSessionDistributionStatus
    {
        [Description("Чернова")]
        Draft = 1,

        [Description("Приложено")]
        Applied = 2,

        [Description("Отказано")]
        Refused = 3
    }
}